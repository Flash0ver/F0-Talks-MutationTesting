[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    [Parameter(Mandatory=$false)]
    [switch]$OpenReport
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$SolutionFile = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'F0.Talks.MutationTesting.sln'
$SettingsFile = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'test.runsettings'
$TestResultsDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'TestResults'
$CoverageReports = Join-Path -Path $TestResultsDirectory -ChildPath '*' -AdditionalChildPath 'coverage.cobertura.xml'
$ReportTargetDirectory = Join-Path -Path $TestResultsDirectory -ChildPath 'coveragereport'
$ReportFile = Join-Path -Path $ReportTargetDirectory -ChildPath 'index.htm'

if (Test-Path -Path $TestResultsDirectory) {
    Remove-Item -Path $TestResultsDirectory -Recurse
}

dotnet test $SolutionFile --collect:"XPlat Code Coverage" --settings $SettingsFile --configuration $Configuration --results-directory $TestResultsDirectory

dotnet tool run reportgenerator -reports:$CoverageReports -targetdir:$ReportTargetDirectory -verbosity:Info -reporttypes:HtmlInline_AzurePipelines

if ($OpenReport) {
    Invoke-Item -Path $ReportFile
}
