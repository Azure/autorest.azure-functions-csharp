// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;

namespace custom_baseUrl_more_options
{
    internal partial class PathsOperations
    {
        private string subscriptionId;
        private string dnsSuffix;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of PathsOperations. </summary>
        public PathsOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string subscriptionId, string dnsSuffix = "host")
        {
            if (subscriptionId == null)
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }
            if (dnsSuffix == null)
            {
                throw new ArgumentNullException(nameof(dnsSuffix));
            }

            this.subscriptionId = subscriptionId;
            this.dnsSuffix = dnsSuffix;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        internal HttpMessage CreateGetEmptyRequest(string vault, string secret, string keyName, string keyVersion)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethodAdditional.Get;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(vault, false);
            uri.AppendRaw(secret, false);
            uri.AppendRaw(dnsSuffix, false);
            uri.AppendPath("/customuri/", false);
            uri.AppendPath(subscriptionId, true);
            uri.AppendPath("/", false);
            uri.AppendPath(keyName, true);
            if (keyVersion != null)
            {
                uri.AppendQuery("keyVersion", keyVersion, true);
            }
            request.Uri = uri;
            return message;
        }
        /// <summary> Get a 200 to test a valid base uri. </summary>
        /// <param name="vault"> The vault name, e.g. https://myvault. </param>
        /// <param name="secret"> Secret value. </param>
        /// <param name="keyName"> The key name with value &apos;key1&apos;. </param>
        /// <param name="keyVersion"> The key version. Default value &apos;v1&apos;. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> GetEmptyAsync(string vault, string secret, string keyName, string keyVersion, CancellationToken cancellationToken = default)
        {
            if (vault == null)
            {
                throw new ArgumentNullException(nameof(vault));
            }
            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret));
            }
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            using var scope = clientDiagnostics.CreateScope("PathsOperations.GetEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyRequest(vault, secret, keyName, keyVersion);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw await clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> Get a 200 to test a valid base uri. </summary>
        /// <param name="vault"> The vault name, e.g. https://myvault. </param>
        /// <param name="secret"> Secret value. </param>
        /// <param name="keyName"> The key name with value &apos;key1&apos;. </param>
        /// <param name="keyVersion"> The key version. Default value &apos;v1&apos;. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response GetEmpty(string vault, string secret, string keyName, string keyVersion, CancellationToken cancellationToken = default)
        {
            if (vault == null)
            {
                throw new ArgumentNullException(nameof(vault));
            }
            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret));
            }
            if (keyName == null)
            {
                throw new ArgumentNullException(nameof(keyName));
            }

            using var scope = clientDiagnostics.CreateScope("PathsOperations.GetEmpty");
            scope.Start();
            try
            {
                using var message = CreateGetEmptyRequest(vault, secret, keyName, keyVersion);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
                    default:
                        throw clientDiagnostics.CreateRequestFailedExceptionAsync(message.Response);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
