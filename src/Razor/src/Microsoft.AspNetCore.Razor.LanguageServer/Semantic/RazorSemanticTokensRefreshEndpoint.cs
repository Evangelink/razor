﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.LanguageServer.Common;
using Microsoft.AspNetCore.Razor.LanguageServer.EndpointContracts;
using Microsoft.CommonLanguageServerProtocol.Framework;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Newtonsoft.Json.Linq;

namespace Microsoft.AspNetCore.Razor.LanguageServer.Semantic;

[LanguageServerEndpoint(RazorLanguageServerCustomMessageTargets.RazorSemanticTokensRefreshEndpoint)]
internal sealed class RazorSemanticTokensRefreshEndpoint : IRazorNotificationHandler<SemanticTokensRefreshParams>, ICapabilitiesProvider
{
    private readonly WorkspaceSemanticTokensRefreshPublisher _semanticTokensRefreshPublisher;

    public bool MutatesSolutionState { get; } = false;

    public RazorSemanticTokensRefreshEndpoint(WorkspaceSemanticTokensRefreshPublisher semanticTokensRefreshPublisher)
    {
        _semanticTokensRefreshPublisher = semanticTokensRefreshPublisher ?? throw new ArgumentNullException(nameof(semanticTokensRefreshPublisher));
    }

    public void ApplyCapabilities(VSInternalServerCapabilities serverCapabilities, VSInternalClientCapabilities clientCapabilities)
    {
        serverCapabilities.Experimental ??= new Dictionary<string, object>();

        var dict = (Dictionary<string, object>)serverCapabilities.Experimental;
        dict["workspace.semanticTokens"] = new SemanticTokensWorkspaceSetting
        {
            RefreshSupport = true,
        };
    }

    public Task HandleNotificationAsync(SemanticTokensRefreshParams request, RazorRequestContext requestContext, CancellationToken cancellationToken)
    {
        // We have to invalidate the tokens cache since it may no longer be up to date.
        _semanticTokensRefreshPublisher.EnqueueWorkspaceSemanticTokensRefresh();

        return Task.CompletedTask;
    }
}
