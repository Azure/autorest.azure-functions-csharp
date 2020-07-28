// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using AutoRest.CSharp.V3.Output.Models.Types;

namespace AutoRest.CSharp.V3.Generation.Writers
{
    internal class SelfTypeProvider : TypeProvider
    {
        public SelfTypeProvider(BuildContext buildContext)
            : base(buildContext)
        {

        }

        protected override string DefaultName => throw new NotImplementedException();

        protected override string DefaultAccessibility => throw new NotImplementedException();
    }
}
