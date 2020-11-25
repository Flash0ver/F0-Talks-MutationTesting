Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$RepositoryRootPath = (Get-Item -Path $PSScriptRoot).Parent
$DocumentationRootDirectory = Join-Path -Path $RepositoryRootPath -ChildPath 'presentation'

dotnet try $DocumentationRootDirectory
