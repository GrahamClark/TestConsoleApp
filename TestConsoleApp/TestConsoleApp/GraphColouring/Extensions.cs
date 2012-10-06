using System;
using System.Collections.Generic;

namespace TestConsoleApp.GraphColouring
{
    internal static class Extensions
    {
        public static int FirstIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (T item in items)
            {
                if (predicate(item))
                {
                    return i;
                }
                i++;
            }

            throw new Exception();
        }
    }
}
