using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;

namespace ReplaceCountWithAny
{
    [ExportSyntaxNodeCodeIssueProvider("ReplaceCountWithAny", LanguageNames.CSharp, typeof(BinaryExpressionSyntax))]
    class CodeIssueProvider : ICodeIssueProvider
    {
        private readonly ICodeActionEditFactory editFactory;

        [ImportingConstructor]
        public CodeIssueProvider(ICodeActionEditFactory editFactory)
        {
            this.editFactory = editFactory;
        }

        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxNode node, CancellationToken cancellationToken)
        {
            // check for users typing one of the following, and warn them to use Any() instead:
            //  <IEnumerable>.Count() > 0
            //  <IEnumerable>.Count() >= 1
            // 0 < <IEnumerable>.Count()
            // 1 <= <IEnumerable>.Count()

            var binaryExpression = (BinaryExpressionSyntax)node;
            var left = binaryExpression.Left;
            var right = binaryExpression.Right;
            var kind = binaryExpression.Kind;

            if (IsCallToEnumerableCount(document, left, cancellationToken) &&
                IsRelevantRightSideComparison(document, right, kind, cancellationToken) ||
                IsCallToEnumerableCount(document, right, cancellationToken) &&
                IsRelevantLeftSideComparison(document, left, kind, cancellationToken))
            {
                string description = String.Format(
                    "Change {0} to use Any() instead of Count() to avoid possible enumeration of the entire sequence.",
                    binaryExpression);

                yield return new CodeIssue(
                    CodeIssue.Severity.Info,
                    binaryExpression.Span,
                    description,
                    new CodeAction(this.editFactory, document, binaryExpression));
            }
        }

        private bool IsCallToEnumerableCount(IDocument document, ExpressionSyntax expression, CancellationToken cancellationToken)
        {
            var invocation = expression as InvocationExpressionSyntax;
            if (invocation == null)
            {
                return false;
            }

            var call = invocation.Expression as MemberAccessExpressionSyntax;
            if (call == null)
            {
                return false;
            }

            var semanticModel = document.GetSemanticModel(cancellationToken);
            var methodSymbol = semanticModel.GetSemanticInfo(call, cancellationToken).Symbol as MethodSymbol;
            if (methodSymbol == null || methodSymbol.Name != "Count" || methodSymbol.ConstructedFrom == null)
            {
                return false;
            }

            var enumerable = semanticModel.Compilation.GetTypeByMetadataName(typeof(Enumerable).FullName);
            if (enumerable == null || !methodSymbol.ConstructedFrom.ContainingType.Equals(enumerable))
            {
                return false;
            }

            return true;
        }

        private bool IsRelevantRightSideComparison(
            IDocument document,
            ExpressionSyntax expression,
            SyntaxKind kind,
            CancellationToken cancellationToken)
        {
            var semanticInfo = document.GetSemanticModel(cancellationToken).GetSemanticInfo(expression);
            int? value;
            if (!semanticInfo.IsCompileTimeConstant || (value = semanticInfo.ConstantValue as int?) == null)
            {
                return false;
            }

            if (kind == SyntaxKind.GreaterThanExpression && value == 0 ||
                kind == SyntaxKind.GreaterThanOrEqualExpression && value == 1)
            {
                return true;
            }

            return false;
        }

        private bool IsRelevantLeftSideComparison(
            IDocument document,
            ExpressionSyntax expression,
            SyntaxKind kind,
            CancellationToken cancellationToken)
        {
            var semanticInfo = document.GetSemanticModel(cancellationToken).GetSemanticInfo(expression);
            int? value;
            if (!semanticInfo.IsCompileTimeConstant || (value = semanticInfo.ConstantValue as int?) == null)
            {
                return false;
            }

            if (kind == SyntaxKind.LessThanExpression && value == 0 ||
                kind == SyntaxKind.LessThanOrEqualExpression && value == 1)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxToken token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodeIssue> GetIssues(IDocument document, CommonSyntaxTrivia trivia, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
