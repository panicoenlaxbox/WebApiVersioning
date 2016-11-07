using System;
using System.Collections.Generic;

namespace WebApiVersioning
{
    public static class TypeExtensions
    {
        public static Type GetTypeFromIEnumerable(this Type type)
        {
            return IsIEnumerable(type) ? type.GetGenericArguments()[0] : null;
        }

        private static bool IsIEnumerable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
    }
}