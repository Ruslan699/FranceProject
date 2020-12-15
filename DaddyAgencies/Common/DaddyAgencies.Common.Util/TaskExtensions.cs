using System;
using System.Threading.Tasks;

namespace DaddyAgencies.Common.Util
{
    public static class TaskExtensions
    {
        public static Task<TResult> Map<TSource, TResult>(this Task<TSource> self, Func<TSource, TResult> map) =>
            self.Select(map);
        public static async Task<TResult> Select<TSource, TResult>(this Task<TSource> self, Func<TSource, TResult> map) =>
            map(await self);
    }
}
