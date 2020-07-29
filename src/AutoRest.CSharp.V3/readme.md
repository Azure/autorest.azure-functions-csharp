# AutoRest.CSharp.V3
> see https://aka.ms/autorest

## Configuration
```yaml
use-extension:
  "@autorest/modelerfour": "4.15.396"
modelerfour:
  always-create-content-type-parameter: true
  flatten-models: true
  flatten-payloads: true
  group-parameters: true
pipeline:
  azure-functions-csharp:
    input: modelerfour/identity
  azure-functions-csharp/emitter:
    input: azure-functions-csharp
    scope: output-scope
output-scope:
  output-artifact: source-file-csharp
```
