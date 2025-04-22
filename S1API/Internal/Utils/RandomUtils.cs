using System;
using System.Collections.Generic;
using UnityEngine;

namespace S1API.Utils
{
    public static class RandomUtils
    {
        /// <summary>
        /// Returns a random element from a list, or default if list is empty/null.
        /// </summary>
        public static T PickOne<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                return default;

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Returns a random element that satisfies a condition, with retry limit.
        /// </summary>
        public static T PickUnique<T>(this IList<T> list, Func<T, bool> isDuplicate, int maxTries = 10)
        {
            if (list == null || list.Count == 0)
                return default;

            for (int i = 0; i < maxTries; i++)
            {
                T item = list.PickOne();
                if (!isDuplicate(item))
                    return item;
            }

            return default;
        }

        /// <summary>
        /// Returns a specified number of unique random elements from a list.
        /// If the count exceeds the number of available elements, returns all elements in random order.
        /// </summary>
        /// <param name="list">The list of items to pick from.</param>
        /// <param name="count">The number of random items to pick.</param>
        /// <returns>A list containing the selected random items, or an empty list if the input list is null or empty.</returns>
        public static List<T> PickMany<T>(this IList<T> list, int count)
        {
            if (list == null || list.Count == 0) return new List<T>();
            var copy = new List<T>(list);
            var result = new List<T>();

            for (int i = 0; i < count && copy.Count > 0; i++)
            {
                int index = UnityEngine.Random.Range(0, copy.Count);
                result.Add(copy[index]);
                copy.RemoveAt(index);
            }

            return result;
        }

        private static readonly System.Random SystemRng = new System.Random();

        public static int RangeInt(int minInclusive, int maxExclusive)
        {
            return SystemRng.Next(minInclusive, maxExclusive);
        }
    }
    
}