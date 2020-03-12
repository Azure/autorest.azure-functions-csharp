// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace media_types.Models
{
    internal static class ContentTypeExtensions
    {
        public static string ToSerialString(this ContentType value) => value switch
        {
            ContentType.ApplicationPdf => "application/pdf",
            ContentType.ImageJpeg => "image/jpeg",
            ContentType.ImagePng => "image/png",
            ContentType.ImageTiff => "image/tiff",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ContentType value.")
        };

        public static ContentType ToContentType(this string value) => value switch
        {
            "application/pdf" => ContentType.ApplicationPdf,
            "image/jpeg" => ContentType.ImageJpeg,
            "image/png" => ContentType.ImagePng,
            "image/tiff" => ContentType.ImageTiff,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ContentType value.")
        };
    }
}