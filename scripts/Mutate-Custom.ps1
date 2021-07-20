[CmdletBinding()]
param (
    [Parameter(Mandatory=$false)]
    [Alias('c','configuration')]
    [ValidateSet('Debug', 'Release')]
    [string]$BuildConfiguration = 'Debug'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$ProjectFile = Join-Path -Path $RepositoryRootPath -ChildPath 'demo' -AdditionalChildPath 'F0.Talks.MutationTesting.FaultInjector', 'F0.Talks.MutationTesting.FaultInjector.csproj'

dotnet run --configuration $BuildConfiguration --project $ProjectFile
