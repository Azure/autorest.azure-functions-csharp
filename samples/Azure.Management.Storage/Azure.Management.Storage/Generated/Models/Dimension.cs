// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.Management.Storage.Models
{
    /// <summary> Dimension of blobs, possibly be blob type or access tier. </summary>
    public partial class Dimension
    {
        /// <summary> Initializes a new instance of Dimension. </summary>
        internal Dimension()
        {
        }

        /// <summary> Initializes a new instance of Dimension. </summary>
        /// <param name="name"> Display name of dimension. </param>
        /// <param name="displayName"> Display name of dimension. </param>
        internal Dimension(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        /// <summary> Display name of dimension. </summary>
        public string Name { get; }
        /// <summary> Display name of dimension. </summary>
        public string DisplayName { get; }
    }
}
