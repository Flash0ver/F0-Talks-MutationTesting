# Stryker.NET

### Install
.NET Core tool via _NuGet.org_
[dotnet-stryker](https://www.nuget.org/packages/dotnet-stryker/)

#### .NET Core global tool
```console
dotnet tool install -g dotnet-stryker
```

#### .NET Core local tool
```console
dotnet new tool-manifest
```
```console
dotnet tool install dotnet-stryker
```
```console
dotnet tool restore
```

### Use
```console
dotnet stryker --config-file-path ./stryker-config.json
```
```console
dotnet stryker --help
```

### [Configuration](https://github.com/stryker-mutator/stryker-net/blob/master/docs/Configuration.md)

### [Reporters](https://github.com/stryker-mutator/stryker-net/blob/master/docs/Reporters.md)

#### Global and local tools
- [Install a global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-global-tool)
- [Install a local tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-local-tool)

---
###### [Conclusion](./MutationTesting_ProsCons.md)
