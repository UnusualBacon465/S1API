using System;

namespace S1API.Internal.Utils
{
    public static class ArrayUtils
    {
        /// <summary>
        /// Adds an element to the array instance
        /// </summary>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void Add<T>(ref T[] array, T item)
        {
            if (array == null)
            {
                array = new[] { item };
                return;
            }

            Array.Resize(ref array, array.Length + 1);
            array[^1] = item;
        }
    }
}
