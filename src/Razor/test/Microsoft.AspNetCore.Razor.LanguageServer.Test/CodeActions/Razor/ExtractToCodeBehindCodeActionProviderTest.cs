﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Components;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.AspNetCore.Razor.LanguageServer.CodeActions.Models;
using Microsoft.AspNetCore.Razor.Test.Common.LanguageServer;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;
using Microsoft.CodeAnalysis.Razor.Protocol.CodeActions;
using Microsoft.CodeAnalysis.Razor.Workspaces;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.AspNetCore.Razor.LanguageServer.CodeActions;

public class ExtractToCodeBehindCodeActionProviderTest(ITestOutputHelper testOutput) : LanguageServerTestBase(testOutput)
{
    [Fact]
    public async Task Handle_InvalidFileKind()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @$$code {}
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);
        context.CodeDocument.SetFileKind(FileKinds.Legacy);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Fact]
    public async Task Handle_OutsideCodeDirective()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/$$test"
            @code {}
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Fact]
    public async Task Handle_InCodeDirectiveBlock_ReturnsNull()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @code {$$}
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Fact]
    public async Task Handle_InCodeDirectiveMalformed_ReturnsNull()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @$$code
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Fact]
    public async Task Handle_InCodeDirectiveWithMarkup_ReturnsNull()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @$$code {
                void Test()
                {
                    <h1>Hello, world!</h1>
                }
            }
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Theory]
    [InlineData("@$$code")]
    [InlineData("@c$$ode")]
    [InlineData("@co$$de")]
    [InlineData("@cod$$e")]
    [InlineData("@code$$")]
    public async Task Handle_InCodeDirective_SupportsFileCreationTrue_ReturnsResult(string codeDirective)
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = $$"""
            @page "/test"
            {|remove:{{codeDirective}}{|extract: { private var x = 1; }|}|}
            """;

        TestFileMarkupParser.GetPositionAndSpans(
            contents, out contents, out int cursorPosition,
            out ImmutableDictionary<string, ImmutableArray<TextSpan>> namedSpans);

        var extractSpan = namedSpans["extract"].Single();
        var removeSpan = namedSpans["remove"].Single();

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents, supportsFileCreation: true);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.NotNull(commandOrCodeActionContainer);
        var codeAction = Assert.Single(commandOrCodeActionContainer);
        var razorCodeActionResolutionParams = ((JsonElement)codeAction.Data!).Deserialize<RazorCodeActionResolutionParams>();
        Assert.NotNull(razorCodeActionResolutionParams);
        var actionParams = ((JsonElement)razorCodeActionResolutionParams.Data).Deserialize<ExtractToCodeBehindCodeActionParams>();
        Assert.NotNull(actionParams);

        Assert.Equal(removeSpan, TextSpan.FromBounds(actionParams.RemoveStart, actionParams.RemoveEnd));
        Assert.Equal(extractSpan, TextSpan.FromBounds(actionParams.ExtractStart, actionParams.ExtractEnd));
    }

    [Fact]
    public async Task Handle_AtEndOfCodeDirectiveWithNoSpace_ReturnsResult()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            {|remove:@code$${|extract:{ private var x = 1; }|}|}
            """;

        TestFileMarkupParser.GetPositionAndSpans(
            contents, out contents, out int cursorPosition,
            out ImmutableDictionary<string, ImmutableArray<TextSpan>> namedSpans);

        var extractSpan = namedSpans["extract"].Single();
        var removeSpan = namedSpans["remove"].Single();

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents, supportsFileCreation: true);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.NotNull(commandOrCodeActionContainer);
        var codeAction = Assert.Single(commandOrCodeActionContainer);
        var razorCodeActionResolutionParams = ((JsonElement)codeAction.Data!).Deserialize<RazorCodeActionResolutionParams>();
        Assert.NotNull(razorCodeActionResolutionParams);
        var actionParams = ((JsonElement)razorCodeActionResolutionParams.Data).Deserialize<ExtractToCodeBehindCodeActionParams>();
        Assert.NotNull(actionParams);

        Assert.Equal(removeSpan, TextSpan.FromBounds(actionParams.RemoveStart, actionParams.RemoveEnd));
        Assert.Equal(extractSpan, TextSpan.FromBounds(actionParams.ExtractStart, actionParams.ExtractEnd));
    }

    [Fact]
    public async Task Handle_InCodeDirective_SupportsFileCreationFalse_ReturnsNull()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @$$code { private var x = 1; }
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents, supportsFileCreation: false);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    [Fact]
    public async Task Handle_InFunctionsDirective_SupportsFileCreationTrue_ReturnsResult()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            {|remove:@$$functions{|extract: { private var x = 1; }|}|}
            """;

        TestFileMarkupParser.GetPositionAndSpans(
            contents, out contents, out int cursorPosition,
            out ImmutableDictionary<string, ImmutableArray<TextSpan>> namedSpans);

        var extractSpan = namedSpans["extract"].Single();
        var removeSpan = namedSpans["remove"].Single();

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = new VSInternalCodeActionContext()
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.NotNull(commandOrCodeActionContainer);
        var codeAction = Assert.Single(commandOrCodeActionContainer);
        var razorCodeActionResolutionParams = ((JsonElement)codeAction.Data!).Deserialize<RazorCodeActionResolutionParams>();
        Assert.NotNull(razorCodeActionResolutionParams);
        var actionParams = ((JsonElement)razorCodeActionResolutionParams.Data).Deserialize<ExtractToCodeBehindCodeActionParams>();
        Assert.NotNull(actionParams);

        Assert.Equal(removeSpan, TextSpan.FromBounds(actionParams.RemoveStart, actionParams.RemoveEnd));
        Assert.Equal(extractSpan, TextSpan.FromBounds(actionParams.ExtractStart, actionParams.ExtractEnd));
    }

    [Fact]
    public async Task Handle_NullRelativePath_ReturnsNull()
    {
        // Arrange
        var documentPath = "c:/Test.razor";
        var contents = """
            @page "/test"
            @$$code { private var x = 1; }
            """;
        TestFileMarkupParser.GetPosition(contents, out contents, out var cursorPosition);

        var request = new VSCodeActionParams()
        {
            TextDocument = new VSTextDocumentIdentifier { Uri = new Uri(documentPath) },
            Range = new Range(),
            Context = null!
        };

        var location = new SourceLocation(cursorPosition, -1, -1);
        var context = CreateRazorCodeActionContext(request, location, documentPath, contents, relativePath: null);

        var provider = new ExtractToCodeBehindCodeActionProvider(LoggerFactory);

        // Act
        var commandOrCodeActionContainer = await provider.ProvideAsync(context, default);

        // Assert
        Assert.Null(commandOrCodeActionContainer);
    }

    private static RazorCodeActionContext CreateRazorCodeActionContext(VSCodeActionParams request, SourceLocation location, string filePath, string text, bool supportsFileCreation = true)
        => CreateRazorCodeActionContext(request, location, filePath, text, relativePath: filePath, supportsFileCreation: supportsFileCreation);

    private static RazorCodeActionContext CreateRazorCodeActionContext(VSCodeActionParams request, SourceLocation location, string filePath, string text, string? relativePath, bool supportsFileCreation = true)
    {
        var sourceDocument = RazorSourceDocument.Create(text, RazorSourceDocumentProperties.Create(filePath, relativePath));
        var options = RazorParserOptions.Create(o =>
        {
            o.Directives.Add(ComponentCodeDirective.Directive);
            o.Directives.Add(FunctionsDirective.Directive);
        });
        var syntaxTree = RazorSyntaxTree.Parse(sourceDocument, options);

        var codeDocument = TestRazorCodeDocument.Create(sourceDocument, imports: default);
        codeDocument.SetFileKind(FileKinds.Component);
        codeDocument.SetCodeGenerationOptions(RazorCodeGenerationOptions.Create(o =>
        {
            o.RootNamespace = "ExtractToCodeBehindTest";
        }));
        codeDocument.SetSyntaxTree(syntaxTree);

        var documentSnapshot = Mock.Of<IDocumentSnapshot>(document =>
            document.GetGeneratedOutputAsync() == Task.FromResult(codeDocument) &&
            document.GetTextAsync() == Task.FromResult(codeDocument.GetSourceText()), MockBehavior.Strict);

        var sourceText = SourceText.From(text);

        var context = new RazorCodeActionContext(request, documentSnapshot, codeDocument, location, sourceText, supportsFileCreation, supportsCodeActionResolve: true);

        return context;
    }
}
