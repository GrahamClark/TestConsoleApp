using System;
using System.Linq.Expressions;

namespace TestConsoleApp.ExpressionCombining
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            Expression<Func<string, bool>> expr1 = s => s.Length == 5;
            Expression<Func<string, bool>> expr2 = s => s == "someString";
            var paramExpr = Expression.Parameter(typeof(string));
            var exprBody = Expression.Or(expr1.Body, expr2.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<string, bool>>(exprBody, paramExpr);
        }
    }

    class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(this.parameter);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            this.parameter = parameter;
        }
    }
}
