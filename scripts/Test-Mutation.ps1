[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [switch]$OpenReport
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$Solution = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'F0.Talks.MutationTesting.sln'
$TestProject = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'F0.Talks.MutationTesting.Tests', 'F0.Talks.MutationTesting.Tests.csproj'
$ProjectReference = (Split-Path -Path $TestProject -Leaf).Replace('.Tests.csproj', '.csproj');
$TestProject = "['$($TestProject.Replace('\', '/'))']"
$ConfigurationFile = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'stryker-config.json'
$SolutionDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'demo'

$Location = Get-Location

Set-Location -Path $SolutionDirectory

dotnet tool run dotnet-stryker --solution-path $Solution --test-projects $TestProject --project-file $ProjectReference --config-file-path $ConfigurationFile

Set-Location -Path $Location

if ($OpenReport) {
    $ReportDirectory = Join-Path -Path $SolutionDirectory -ChildPath 'StrykerOutput'
    $ReportFile = Get-ChildItem -Path $ReportDirectory -Directory | Sort-Object -Property Name -Descending | Select-Object -First 1
    $ReportFile = Join-Path -Path $ReportFile -ChildPath 'reports' -AdditionalChildPath 'mutation-report.html'
    Invoke-Item -Path $ReportFile
}
