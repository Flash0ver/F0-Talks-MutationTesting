Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$DocumentationRootDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'presentation'
$CodeProjectFile = Join-Path -Path $DocumentationRootDirectory -ChildPath 'Snippets' -AdditionalChildPath 'Snippets.csproj'

dotnet tool restore

dotnet restore $CodeProjectFile
