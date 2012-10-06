using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace TestConsoleApp.QueryProvider
{
    public class Query<T> : IQueryable<T>, IQueryable, IEnumerable<T>, IEnumerable, IOrderedQueryable<T>, IOrderedQueryable
    {
        private QueryProvider provider;
        private Expression expression;

        public Query(QueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            this.provider = provider;
            this.expression = Expression.Constant(this);
        }

        public Query(QueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            this.provider = provider;
            this.expression = expression;
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression
        {
            get { return this.expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.provider; }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.provider.Execute(this.expression)).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)this.provider.Execute(this.expression)).GetEnumerator();
        }

        public override string ToString()
        {
            return this.provider.GetQueryText(this.expression);
        }
    }
}
