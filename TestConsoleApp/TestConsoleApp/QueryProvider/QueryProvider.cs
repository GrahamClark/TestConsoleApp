using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;

namespace TestConsoleApp.QueryProvider
{
    public abstract class QueryProvider : IQueryProvider
    {
        protected QueryProvider()
        {
        }

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(this, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(
                    typeof(Query<>).MakeGenericType(elementType), 
                    new object[] { this, expression });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            return (TResult)this.Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return this.Execute(expression);
        }

        public abstract string GetQueryText(Expression expression);

        public abstract object Execute(Expression expression);
    }
}
