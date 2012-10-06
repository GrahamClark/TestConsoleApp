using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestConsoleApp.GenericsHashing
{
    internal class Hasher
    {
        public static Dictionary<string, T> Hash<T>(params Expression<Func<string, T>>[] args)
            where T : class
        {
            Dictionary<string, T> items = new Dictionary<string, T>();

            foreach (Expression<Func<string, T>> expression in args)
            {
                ConstantExpression constantExpression = expression.Body as ConstantExpression;
                T item = null;
                if (constantExpression != null)
                {
                    item = constantExpression.Value as T;
                }
                else
                {
                    item = Expression.Lambda<Func<T>>(expression.Body).Compile()();
                }
                items.Add(expression.Parameters[0].Name, item);
            }
            return items;
        }
    }
}
