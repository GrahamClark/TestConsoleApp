using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleApp.QueryProvider
{
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type sequenceType)
        {
            Type enumerable = FindIEnumerable(sequenceType);
            if (enumerable == null)
            {
                return sequenceType;
            }

            return enumerable.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type sequenceType)
        {
            if (sequenceType == null || sequenceType == typeof(string))
            {
                return null;
            }

            if (sequenceType.IsArray)
            {
                return typeof(IEnumerable<>).MakeGenericType(sequenceType.GetElementType());
            }

            if (sequenceType.IsGenericType)
            {
                foreach (Type argument in sequenceType.GetGenericArguments())
                {
                    Type enumerable = typeof(IEnumerable<>).MakeGenericType(argument);
                    if (enumerable.IsAssignableFrom(sequenceType))
                    {
                        return enumerable;
                    }


                }
            }

            Type[] interfaces = sequenceType.GetInterfaces();
            if (interfaces != null && interfaces.Length > 0)
            {
                foreach (Type iface in interfaces)
                {
                    Type enumerable = FindIEnumerable(iface);
                    if (enumerable != null)
                    {
                        return enumerable;
                    }
                }
            }

            if (sequenceType.BaseType != null && sequenceType.BaseType != typeof(object))
            {
                return FindIEnumerable(sequenceType.BaseType);
            }

            return null;
        }
    }
}
