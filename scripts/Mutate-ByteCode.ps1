[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [switch]$OpenReport
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$SolutionDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'demo'
$DirectoryName = 'FaultifyOutput'
$ReportDirectory = Join-Path -Path $SolutionDirectory -ChildPath $DirectoryName
$FileName = '.gitignore'
$FileContent = "*$([Environment]::NewLine)"
$DirectoryExists = Test-Path -Path $ReportDirectory
$TestProject = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'F0.Talks.MutationTesting.Tests', 'F0.Talks.MutationTesting.Tests.csproj'

if ($DirectoryExists) {
    Remove-Item -Path $ReportDirectory -Recurse
}

New-Item -Path $SolutionDirectory -Name $DirectoryName -ItemType 'directory' | Out-Null
New-Item -Path $ReportDirectory -Name $FileName -ItemType 'file' -Value $FileContent | Out-Null

dotnet tool run faultify --testProjectName $TestProject --reportPath $ReportDirectory --reportFormat html

if ($OpenReport) {
    $ReportFile = Get-ChildItem -Path $ReportDirectory -Directory | Sort-Object -Property Name -Descending | Select-Object -First 1
    $ReportFile = Get-ChildItem -Path $ReportFile -File -Filter '*.html' | Sort-Object -Property Name -Descending | Select-Object -First 1
    Invoke-Item -Path $ReportFile
}
