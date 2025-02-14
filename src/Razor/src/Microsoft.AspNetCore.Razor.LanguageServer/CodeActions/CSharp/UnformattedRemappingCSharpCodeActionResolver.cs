﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.LanguageServer.CodeActions.Models;
using Microsoft.AspNetCore.Razor.LanguageServer.Hosting;
using Microsoft.CodeAnalysis.Razor.DocumentMapping;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;
using Microsoft.CodeAnalysis.Razor.Protocol;
using Microsoft.CodeAnalysis.Razor.Workspaces;
using Microsoft.VisualStudio.LanguageServer.Protocol;

namespace Microsoft.AspNetCore.Razor.LanguageServer.CodeActions;

/// <summary>
/// Resolves and remaps the code action, without running formatting passes.
/// </summary>
internal sealed class UnformattedRemappingCSharpCodeActionResolver(
    IDocumentContextFactory documentContextFactory,
    IClientConnection clientConnection,
    IRazorDocumentMappingService documentMappingService) : CSharpCodeActionResolver(clientConnection)
{
    private readonly IDocumentContextFactory _documentContextFactory = documentContextFactory;
    private readonly IRazorDocumentMappingService _documentMappingService = documentMappingService;

    public override string Action => LanguageServerConstants.CodeActions.UnformattedRemap;

    public async override Task<CodeAction> ResolveAsync(
        CodeActionResolveParams csharpParams,
        CodeAction codeAction,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_documentContextFactory.TryCreateForOpenDocument(csharpParams.RazorFileIdentifier, out var documentContext))
        {
            return codeAction;
        }

        var resolvedCodeAction = await ResolveCodeActionWithServerAsync(csharpParams.RazorFileIdentifier, documentContext.Version, RazorLanguageKind.CSharp, codeAction, cancellationToken).ConfigureAwait(false);
        if (resolvedCodeAction?.Edit?.DocumentChanges is null)
        {
            // Unable to resolve code action with server, return original code action
            return codeAction;
        }

        if (resolvedCodeAction.Edit.DocumentChanges.Value.Count() != 1)
        {
            // We don't yet support multi-document code actions, return original code action
            Debug.Fail($"Encountered an unsupported multi-document code action edit with ${codeAction.Title}.");
            return codeAction;
        }

        var documentChanged = resolvedCodeAction.Edit.DocumentChanges.Value.First();
        if (!documentChanged.TryGetFirst(out var textDocumentEdit))
        {
            // Only Text Document Edit changes are supported currently, return original code action
            return codeAction;
        }

        var textEdit = textDocumentEdit.Edits.FirstOrDefault();
        if (textEdit is null)
        {
            // No text edit available
            return codeAction;
        }

        var codeDocument = await documentContext.Snapshot.GetGeneratedOutputAsync().ConfigureAwait(false);
        if (codeDocument.IsUnsupported())
        {
            return codeAction;
        }

        if (!_documentMappingService.TryMapToHostDocumentRange(codeDocument.GetCSharpDocument(), textEdit.Range, MappingBehavior.Inclusive, out var originalRange))
        {
            // Text edit failed to map
            return codeAction;
        }

        textEdit.Range = originalRange;

        var codeDocumentIdentifier = new OptionalVersionedTextDocumentIdentifier()
        {
            Uri = csharpParams.RazorFileIdentifier.Uri,
            Version = documentContext.Version,
        };
        resolvedCodeAction.Edit = new WorkspaceEdit()
        {
            DocumentChanges = new[] {
                new TextDocumentEdit()
                {
                    TextDocument = codeDocumentIdentifier,
                    Edits = [textEdit],
                }
            },
        };

        return resolvedCodeAction;
    }
}
