[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [switch]$OpenReport
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$SolutionDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'demo'
$ConfigurationFile = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'stryker-config.json'

$Location = Get-Location

Set-Location -Path $SolutionDirectory

if ($OpenReport) {
    dotnet tool run dotnet-stryker --config-file $ConfigurationFile --log-to-file --open-report
} else {
    dotnet tool run dotnet-stryker --config-file $ConfigurationFile --log-to-file
}

Set-Location -Path $Location
