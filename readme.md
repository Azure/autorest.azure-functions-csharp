# Azure Functions C# code generator

## Setup

- [NodeJS](https://nodejs.org/en/) (13.x.x)
- `npm install` (at root)
- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0) (3.0.100)
- [PowerShell Core](https://github.com/PowerShell/PowerShell/releases/latest)
- [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools)

## Build

- `dotnet build` (at root)

## Usage

*Prerequisites*: To run Stencil on your box today, you need to have NodeJS and NPM installed. We rely on autorest (Microsoft's OpenAPI specification generator) and you need to install it on your box.

```bash
npm install -g autorest
```

To use C# generator, you use the `--azure-functions-csharp` generator plugin. The `--input-file` parameter can also take in a URL as well.

```bash
autorest --azure-functions-csharp \
         --input-file:/path/to/spec.json \
         --output-folder:./generated-azfunctions \
         --version:3.0.6314 \
         --namespace:Namespace
```

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

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit [https://cla.microsoft.com](https://cla.microsoft.com).

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repositories using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
