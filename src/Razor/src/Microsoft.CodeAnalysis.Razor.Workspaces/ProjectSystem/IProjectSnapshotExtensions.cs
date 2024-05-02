﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.PooledObjects;
using Microsoft.AspNetCore.Razor.ProjectSystem;
using Microsoft.AspNetCore.Razor.Serialization;

namespace Microsoft.CodeAnalysis.Razor.ProjectSystem;

internal static class IProjectSnapshotExtensions
{
    public static RazorProjectInfo ToRazorProjectInfo(this IProjectSnapshot project)
    {
        using var documents = new PooledArrayBuilder<DocumentSnapshotHandle>();

        foreach (var documentFilePath in project.DocumentFilePaths)
        {
            if (project.GetDocument(documentFilePath) is { } document)
            {
                var documentHandle = document.ToHandle();

                documents.Add(documentHandle);
            }
        }

        return new RazorProjectInfo(
            projectKey: project.Key,
            filePath: project.FilePath,
            configuration: project.Configuration,
            rootNamespace: project.RootNamespace,
            displayName: project.DisplayName,
            projectWorkspaceState: project.ProjectWorkspaceState,
            documents: documents.DrainToImmutable());
    }

    public static string ToBase64EncodedProjectInfo(this IProjectSnapshot project)
    {
        var projectInfo = project.ToRazorProjectInfo();

        using var stream = new MemoryStream();
        projectInfo.SerializeTo(stream);
        var base64ProjectInfo = Convert.ToBase64String(stream.ToArray());

        return base64ProjectInfo;
    }

    public static ImmutableArray<TagHelperDescriptor> GetTagHelpersSynchronously(this IProjectSnapshot projectSnapshot)
    {
        var canResolveTagHelpersSynchronously = projectSnapshot is ProjectSnapshot ||
            projectSnapshot.GetType().FullName == "Microsoft.VisualStudio.LegacyEditor.Razor.EphemeralProjectSnapshot";

        Debug.Assert(canResolveTagHelpersSynchronously, "The ProjectSnapshot in the VisualStudioDocumentTracker should not be a cohosted project.");
        var tagHelperTask = projectSnapshot.GetTagHelpersAsync(CancellationToken.None);
        Debug.Assert(tagHelperTask.IsCompleted, "GetTagHelpersAsync should be synchronous for non-cohosted projects.");
        return tagHelperTask.Result;
    }
}
