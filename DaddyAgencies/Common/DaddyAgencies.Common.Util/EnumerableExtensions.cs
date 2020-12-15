using System;
using System.Collections.Generic;

namespace DaddyAgencies.Common.Util
{
    public static class EnumerableExtensions
    {
        public static string JoinStrings(this IEnumerable<string> strings, string separator = "\n")
            => string.Join(separator, strings);
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action?.Invoke(item);
        }
    }
}
