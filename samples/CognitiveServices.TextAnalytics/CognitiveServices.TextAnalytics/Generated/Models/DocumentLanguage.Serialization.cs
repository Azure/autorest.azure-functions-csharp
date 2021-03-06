// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace CognitiveServices.TextAnalytics.Models
{
    public partial class DocumentLanguage
    {
        internal static DocumentLanguage DeserializeDocumentLanguage(JsonElement element)
        {
            string id = default;
            IReadOnlyList<DetectedLanguage> detectedLanguages = default;
            Optional<DocumentStatistics> statistics = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("detectedLanguages"))
                {
                    List<DetectedLanguage> array = new List<DetectedLanguage>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(DetectedLanguage.DeserializeDetectedLanguage(item));
                    }
                    detectedLanguages = array;
                    continue;
                }
                if (property.NameEquals("statistics"))
                {
                    statistics = DocumentStatistics.DeserializeDocumentStatistics(property.Value);
                    continue;
                }
            }
            return new DocumentLanguage(id, detectedLanguages, statistics.Value);
        }
    }
}
