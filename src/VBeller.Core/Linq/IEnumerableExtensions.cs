using System;
using System.Collections.Generic;

namespace VBeller.Linq
{
    /// <summary>
    /// Provides LINQ extension methods for the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Excutes an action on all items of a sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/> of items.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute.</param>
        /// <returns>The given <see cref="IEnumerable{T}"/> to allow chaining.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
                yield return element;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Array"/> from an <see cref="IEnumerable{T}"/>. Increase efficiency by passing the array size.
        /// </summary>
        /// <typeparam name="TSource">Generic member of the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="count">The array size. Equal or larger than the enumerable's size.</param>
        /// <returns>An <see cref="Array"/> created created from the source enumerable.</returns>
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            var array = new TSource[count];
            var i = 0;
            foreach (var item in source)
            {
                array[i++] = item;
            }
            return array;
        }

        /// <summary>
        /// Creates a new <see cref="List{T}"/> from an <see cref="IEnumerable{T}"/>. Increase efficiency by passing the list size.
        /// </summary>
        /// <typeparam name="TSource">Generic member of the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="count">The list size. Equal or larger than the enumerable's size.</param>
        /// <returns>An <see cref="Array"/> created from the source enumerable.</returns>
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            var list = new List<TSource>(count);
            list.AddRange(source);
            return list;
        }
    }
}