﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoRest.CSharp.V3.AutoRest.Communication;
using AutoRest.CSharp.V3.Generation.Types;
using AutoRest.CSharp.V3.Generation.Writers;
using AutoRest.CSharp.V3.Input;
using AutoRest.CSharp.V3.Input.Source;
using AutoRest.CSharp.V3.Output.Builders;
using AutoRest.CSharp.V3.Output.Models.Responses;
using AutoRest.CSharp.V3.Output.Models.Types;
using AutoRest.CSharp.V3.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Simplification;
using Microsoft.CodeAnalysis.Text;
using Diagnostic = Microsoft.CodeAnalysis.Diagnostic;

namespace AutoRest.CSharp.V3.AutoRest.Plugins
{
    [PluginName("azure-functions-csharp")]
    internal class CSharpGen : IPlugin
    {

        public async Task<GeneratedCodeWorkspace> ExecuteAsync(CodeModel codeModel, Configuration configuration)
        {
            var directory = Directory.CreateDirectory(configuration.OutputFolder);
            var project = GeneratedCodeWorkspace.Create(configuration.OutputFolder);
            var sourceInputModel = new SourceInputModel(await project.GetCompilationAsync());

            var context = new BuildContext(codeModel, configuration, sourceInputModel);

            var modelWriter = new ModelWriter();
            var clientWriter = new ClientWriter();
            var restClientWriter = new RestClientWriter();
            var restServerWriter = new RestServerWriter();
            var serializeWriter = new SerializationWriter();
            var headerModelModelWriter = new ResponseHeaderGroupWriter();

            // Generate the landing zone for the files.
            if (configuration.GenerateMetadata)
            {
                var x = new ProcessStartInfo("func");
                x.WorkingDirectory = configuration.OutputFolder;
                x.ArgumentList.Add("init");
                x.ArgumentList.Add("--worker-runtime");
                x.ArgumentList.Add("dotnet");
                x.ArgumentList.Add("--force");
                Process.Start(x).WaitForExit();
            }

            foreach (var model in context.Library.Models)
            {
                var codeWriter = new CodeWriter();
                modelWriter.WriteModel(codeWriter, model);

                //var serializerCodeWriter = new CodeWriter();
                //serializeWriter.WriteSerialization(serializerCodeWriter, model);

                var name = model.Type.Name;
                project.AddGeneratedFile($"Models/{name}.cs", codeWriter.ToString());
                //project.AddGeneratedFile($"Models/{name}.Serialization.cs", serializerCodeWriter.ToString());
            }

            foreach (var client in context.Library.RestClients)
            {
                // HACK: since I'm mooching off of rest clients, need to map based on path segments
                // to reasonable file chunks
                var apiGroups = client.Methods.GroupBy(m =>
                {
                    var pathSegment = m.Request.PathSegments.First(s => {
                        var segementValue = s.Value.IsConstant ? s.Value.Constant.Value : null;
                        if (segementValue != null)
                        {
                            return (segementValue.ToString() ?? string.Empty).StartsWith("/");
                        }
                        return false;
                    });

                    if (pathSegment != null)
                    {
                        var pathString = pathSegment.Value.Constant.Value?.ToString();

                        if (!string.IsNullOrWhiteSpace(pathString) && pathString.Contains('/'))
                        {
                            return pathString.Split('/', StringSplitOptions.RemoveEmptyEntries).First().ToLower();
                        }
                    }

                    return string.Empty;
                });

                foreach (var apiGroup in apiGroups)
                {
                    var codeWriter = new CodeWriter();
                    var cs = new CSharpType(new SelfTypeProvider(context), client.Type.Namespace, $"{apiGroup.Key.ToCleanName()}Api");
                    restServerWriter.WriteServer(codeWriter, apiGroup, cs);

                    project.AddGeneratedFile($"{cs.Name}.cs", codeWriter.ToString());
                }


            }

            /*foreach (var client in context.Library.RestClients)
            {
                var restCodeWriter = new CodeWriter();
                restClientWriter.WriteClient(restCodeWriter, client);

                project.AddGeneratedFile($"{client.Type.Name}.cs", restCodeWriter.ToString());
            }

            foreach (ResponseHeaderGroupType responseHeaderModel in context.Library.HeaderModels)
            {
                var headerModelCodeWriter = new CodeWriter();
                headerModelModelWriter.WriteHeaderModel(headerModelCodeWriter, responseHeaderModel);

                project.AddGeneratedFile($"{responseHeaderModel.Type.Name}.cs", headerModelCodeWriter.ToString());
            }

            foreach (var client in context.Library.Clients)
            {
                var codeWriter = new CodeWriter();
                clientWriter.WriteClient(codeWriter, client, context.Configuration);

                project.AddGeneratedFile($"{client.Type.Name}.cs", codeWriter.ToString());
            }

            foreach (var operation in context.Library.LongRunningOperations)
            {
                var codeWriter = new CodeWriter();
                LongRunningOperationWriter.Write(codeWriter, operation);

                project.AddGeneratedFile($"{operation.Type.Name}.cs", codeWriter.ToString());
            }

            if (context.Configuration.AzureArm)
            {
                var codeWriter = new CodeWriter();
                ManagementClientWriter.WriteClientOptions(codeWriter, context);
                project.AddGeneratedFile($"{context.Configuration.LibraryName}ManagementClientOptions.cs", codeWriter.ToString());

                var clientCodeWriter = new CodeWriter();
                ManagementClientWriter.WriteAggregateClient(clientCodeWriter, context);
                project.AddGeneratedFile($"{context.Configuration.LibraryName}ManagementClient.cs", clientCodeWriter.ToString());

            }*/

            return project;
        }

        public async Task<bool> Execute(IPluginCommunication autoRest)
        {
            string codeModelFileName = (await autoRest.ListInputs()).FirstOrDefault();
            if (string.IsNullOrEmpty(codeModelFileName)) throw new Exception("Generator did not receive the code model file.");

            var codeModelYaml = await autoRest.ReadFile(codeModelFileName);

            CodeModel codeModel = CodeModelSerialization.DeserializeCodeModel(codeModelYaml);

            var configuration = new Configuration(
                new Uri(GetRequiredOption(autoRest, "output-folder")).LocalPath,
                GetRequiredOption(autoRest, "namespace"),
                autoRest.GetValue<string?>("library-name").GetAwaiter().GetResult(),
                autoRest.GetValue<bool?>("save-inputs").GetAwaiter().GetResult() ?? false,
                autoRest.GetValue<bool?>("public-clients").GetAwaiter().GetResult() ?? false,
                autoRest.GetValue<bool?>("generate-metadata").GetAwaiter().GetResult() ?? true
            );

            if (configuration.SaveInputs)
            {
                await autoRest.WriteFile("Configuration.json", StandaloneGeneratorRunner.SaveConfiguration(configuration), "source-file-csharp");
                await autoRest.WriteFile("CodeModel.yaml", codeModelYaml, "source-file-csharp");
            }

            var project = await ExecuteAsync(codeModel, configuration);
            await foreach (var file in project.GetGeneratedFilesAsync())
            {
                await autoRest.WriteFile(file.Name, file.Text, "source-file-csharp");
            }

            return true;
        }

        private string GetRequiredOption(IPluginCommunication autoRest, string name)
        {
            return autoRest.GetValue<string?>(name).GetAwaiter().GetResult() ?? throw new InvalidOperationException($"{name} configuration parameter is required");
        }
    }
}
