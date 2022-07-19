using System;
using System.Collections.Generic;

namespace Domain
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
            {
                action(element);
            }
        }
    }
}
