// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;

namespace AutoRest.CSharp.V3.AutoRest.Plugins
{
    internal class Configuration
    {
        public Configuration(string outputFolder, string ns, string? name, bool saveInputs, bool publicClients, bool generateMetadata)
        {
            OutputFolder = outputFolder;
            Namespace = ns;
            var namespaceParts = ns.Split('.');
            LibraryName = name ?? namespaceParts.Last();
            SaveInputs = saveInputs;
            PublicClients = publicClients;
            GenerateMetadata = generateMetadata;
        }

        public string OutputFolder { get; }
        public string Namespace { get; }
        public string LibraryName { get; }
        public bool SaveInputs { get; }
        public bool PublicClients { get; }
        public bool GenerateMetadata { get; }
    }
}
