// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading.Tasks;
using AutoRest.CSharp.V3.AutoRest.Communication;
using AutoRest.CSharp.V3.Input;

namespace AutoRest.CSharp.V3.AutoRest.Plugins
{
    // ReSharper disable once StringLiteralTypo
    [PluginName("azure-functions-csharpproj")]
    // ReSharper disable once IdentifierTypo
    internal class CSharpProj : IPlugin
    {
        private string _csProjContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
    <PropertyGroup><TargetFramework>netcoreapp3.1</TargetFramework>
        <AzureFunctionsVersion>v3</AzureFunctionsVersion>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include=""Microsoft.NET.Sdk.Functions"" Version=""3.0.3""/>
    </ItemGroup>
    <ItemGroup>
        <None Update=""host.json""><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></None>
        <None Update=""local.settings.json"">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
            <CopyToPublishDirectory>Never</CopyToPublishDirectory>
        </None>
</ItemGroup></Project>
";
        public async Task<bool> Execute(IPluginCommunication autoRest)
        {
            var ns = await autoRest.GetValue<string>("namespace");
            await autoRest.WriteFile($"{ns}.csproj", _csProjContent, "source-file-csharp");

            return true;
        }
    }
}
