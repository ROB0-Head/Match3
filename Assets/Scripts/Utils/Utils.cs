using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Utils
{
    public static class Utils
    {
        public static bool IsNullOrEmpty(this IEnumerable @this)
            => !(@this?.GetEnumerator().MoveNext() ?? false);

        public static void Shuffle<T>(this IList<T> list)
        {
            var shuffledList = list.OrderBy(x => new Random().Next()).ToList();
            list.Clear();
            list.AddRange(shuffledList);
        }
    }
}