# Azure Functions C# code generator for AutoRest V3

## Setup
- [NodeJS](https://nodejs.org/en/) (13.x.x)
- `npm install` (at root)
- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0) (3.0.100)
- [PowerShell Core](https://github.com/PowerShell/PowerShell/releases/latest)

## Build
- `dotnet build` (at root)

## Configuration
```yaml
# autorest-core version
version: 3.0.6289
shared-source-folder: $(this-folder)/src/assets
save-inputs: true
use: $(this-folder)/artifacts/bin/AutoRest.CSharp.V3/Debug/netcoreapp3.0/
clear-output-folder: false
public-clients: true
pipeline:
  azure-functions-csharpproj:
    input: modelerfour/identity
  azure-functions-csharpproj/emitter:
    input: azure-functions-csharpproj
    scope: output-scope
```
