using System;
using System.Collections.Generic;

namespace CommonNet.Sequence
{
    public static class IListExtensions
    {
        /// <summary>
        /// Swaps the element in the <paramref name="first"/> index with the element in the <paramref name="second"/> index.
        /// </summary>
        /// <typeparam name="T">The generic list type.</typeparam>
        /// <param name="list">The list being acted upon.</param>
        /// <param name="first">The first element to swap.</param>
        /// <param name="second">The second element to swap.</param>
        /// <exception cref="ArgumentOutOfRangeException">If first or second are less than 0 or more than the list size.</exception>
        public static void Swap<T>(this IList<T> list, int first, int second)
        {
            CheckBounds(list, first, nameof(first));
            CheckBounds(list, second, nameof(second));
            SwapUnsafe<T>(list, first, second);
        }

        /// <summary>
        /// Swaps the element in the <paramref name="first"/> index with the element in the <paramref name="second"/> index.
        /// </summary>
        /// <typeparam name="T">The generic list type.</typeparam>
        /// <param name="list">The list being acted upon.</param>
        /// <param name="first">The first element to swap.</param>
        /// <param name="second">The second element to swap.</param>
        public static void SwapUnsafe<T>(this IList<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        private static void CheckBounds<T>(IList<T> list, int index, string parameterName)
        {
            if (index < 0 || index >= list.Count)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"{nameof(Swap)}'s {parameterName} index is out of bounds: {index}");
            }
        }
    }
}
