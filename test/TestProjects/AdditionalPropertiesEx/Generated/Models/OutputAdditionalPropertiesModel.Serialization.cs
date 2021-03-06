// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace AdditionalPropertiesEx.Models
{
    public partial class OutputAdditionalPropertiesModel
    {
        internal static OutputAdditionalPropertiesModel DeserializeOutputAdditionalPropertiesModel(JsonElement element)
        {
            int id = default;
            IReadOnlyDictionary<string, string> additionalProperties = default;
            Dictionary<string, string> additionalPropertiesDictionary = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetInt32();
                    continue;
                }
                additionalPropertiesDictionary ??= new Dictionary<string, string>();
                additionalPropertiesDictionary.Add(property.Name, property.Value.GetString());
            }
            additionalProperties = additionalPropertiesDictionary;
            return new OutputAdditionalPropertiesModel(id, additionalProperties);
        }
    }
}
