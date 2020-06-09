﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Syntax;
using Microsoft.AspNetCore.Razor.LanguageServer.Common;
using Microsoft.AspNetCore.Razor.LanguageServer.ProjectSystem;
using Microsoft.CodeAnalysis.Razor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OmniSharp.Extensions.LanguageServer.Protocol.Client.Capabilities;
using OmniSharp.Extensions.LanguageServer.Protocol.Models;

namespace Microsoft.AspNetCore.Razor.LanguageServer.Refactoring
{
    class ExtractToCodeBehindCodeActionProvider : IRazorRefactoringCodeActionProvider
    {
        public Task<CommandOrCodeActionContainer> Provide(RefactoringContext context, CancellationToken cancellationToken)
        {
            var directiveNode = (RazorDirectiveSyntax)context.Document.GetNodeAtLocation(context.Location, n => n.Kind == Language.SyntaxKind.RazorDirective);
            if (directiveNode is null)
            {
                return null;
            }

            var cSharpCodeBlockNode = directiveNode.Body.DescendantNodes().FirstOrDefault(n => n is CSharpCodeBlockSyntax);
            if (cSharpCodeBlockNode is null)
            {
                return null;
            }

            var extractToCodeBehindParams = new RazorCodeActionResolutionParams()
            {
                Action = "ExtractToCodeBehind",
                Data = new Dictionary<string, object>()
                {
                    { "uri", context.Document.Source.FilePath },
                    { "extractStart", cSharpCodeBlockNode.Span.Start },
                    { "extractEnd", cSharpCodeBlockNode.Span.End },
                    { "removeStart", directiveNode.Span.Start },
                    { "removeEnd", directiveNode.Span.End }
                },
            };

            var container = new List<CommandOrCodeAction>
            {
                new Command()
                {
                    Title = "Extract code block into backing document",
                    Name = "razor/runCodeAction",
                    Arguments = new JArray(JToken.FromObject(extractToCodeBehindParams))
                }
            };

            return Task.FromResult((CommandOrCodeActionContainer)container);
        }
    }

    class ExtractToCodeBehindEndpoint : IRazorCodeActionResolutionHandler
    {
        private readonly ILogger _logger;
        private readonly ForegroundDispatcher _foregroundDispatcher;
        private readonly DocumentResolver _documentResolver;
        private ExecuteCommandCapability _capability;

        public ExtractToCodeBehindEndpoint(
            ForegroundDispatcher foregroundDispatcher,
            DocumentResolver documentResolver,
            ILoggerFactory loggerFactory)
        {
            if (foregroundDispatcher == null)
            {
                throw new ArgumentNullException(nameof(foregroundDispatcher));
            }

            if (documentResolver == null)
            {
                throw new ArgumentNullException(nameof(documentResolver));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _foregroundDispatcher = foregroundDispatcher;
            _documentResolver = documentResolver;
            _logger = loggerFactory.CreateLogger<ExtractToCodeBehindEndpoint>();
        }

        public async Task<RazorCodeActionResolutionResponse> Handle(RazorCodeActionResolutionParams request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handle code action resolution response!");
            _logger.LogInformation(request.Action, request.Data);
            if (!string.Equals(request.Action, "ExtractToCodeBehind", StringComparison.Ordinal))
            {
                return null;
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var filePath = (string)request.Data["uri"];
            var cutStart = Convert.ToInt32(request.Data["extractStart"]);
            var cutEnd = Convert.ToInt32(request.Data["extractEnd"]);
            var removeStart = Convert.ToInt32(request.Data["removeStart"]);
            var removeEnd = Convert.ToInt32(request.Data["removeEnd"]);

            var document = await Task.Factory.StartNew(() =>
            {
                _documentResolver.TryResolveDocument(filePath, out var documentSnapshot);
                return documentSnapshot;
            }, cancellationToken, TaskCreationOptions.None, _foregroundDispatcher.ForegroundScheduler);

            if (document is null)
            {
                return null;
            }

            var codeDocument = await document.GetGeneratedOutputAsync();
            if (codeDocument.IsUnsupported())
            {
                return null;
            }

            _logger.LogInformation("Finishing resolve!");

            var changes = new Dictionary<Uri, IEnumerable<TextEdit>>
            {
                [new Uri(filePath)] = new[]
                {
                    new TextEdit()
                    {
                        NewText = "",
                        Range = codeDocument.RangeFromIndices(removeStart, removeEnd)
                    }
                }
            };

            return new RazorCodeActionResolutionResponse()
            {
                Edit = new WorkspaceEdit() {
                    Changes = changes,
                }
            };
        }
    }
}
