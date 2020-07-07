using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz.Extensions
{
    public static class ForEachExtension
    {
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}
