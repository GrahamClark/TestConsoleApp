using System;
using System.Linq;
using System.Threading;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;

namespace ReplaceCountWithAny
{
    class CodeAction : ICodeAction
    {
        private readonly ICodeActionEditFactory editFactory;
        private readonly IDocument document;
        private readonly BinaryExpressionSyntax binaryExpression;

        public CodeAction(
            ICodeActionEditFactory editFactory,
            IDocument document,
            BinaryExpressionSyntax binaryExpression)
        {
            this.editFactory = editFactory;
            this.document = document;
            this.binaryExpression = binaryExpression;
        }

        public string Description
        {
            get { return "Change use of Enumerable.Count() to Enumerable.Any()"; }
        }

        public System.Windows.Media.ImageSource Icon
        {
            get { return null; }
        }

        public ICodeActionEdit GetEdit(CancellationToken cancellationToken)
        {
            var syntaxTree = (SyntaxTree)document.GetSyntaxTree(cancellationToken);
            var newExpression = GetNewNode(binaryExpression)
                                    .WithLeadingTrivia(binaryExpression.GetLeadingTrivia())
                                    .WithTrailingTrivia(binaryExpression.GetTrailingTrivia());

            var newRoot = syntaxTree.Root.ReplaceNode(binaryExpression, newExpression);

            return editFactory.CreateTreeTransformEdit(
                document.Project.Solution,
                syntaxTree,
                newRoot,
                cancellationToken: cancellationToken);
        }

        private ExpressionSyntax GetNewNode(BinaryExpressionSyntax node)
        {
            var invocation = node.DescendentNodes().OfType<InvocationExpressionSyntax>().Single();
            var caller = invocation.DescendentNodes().OfType<MemberAccessExpressionSyntax>().Single();

            return invocation.Update(caller.Update(
                caller.Expression,
                caller.OperatorToken,
                Syntax.IdentifierName("Any")),
                invocation.ArgumentList);
        }
    }
}
