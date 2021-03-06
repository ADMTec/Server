using System;
using System.Collections.Generic;

namespace Dirac.Math
{
    /// <summary>
    /// This class is used to enforce that preconditions are met for method calls
    /// using clear and consice semantics.
    /// </summary>
    public static class Contract
    {
        /// <summary>
        /// Requires that a condition evaluates to <c>true</c>.
        /// </summary>
        /// <param name="condition"></param>
        /// <exception cref="ArgumentException">Condition is <c>false</c>.</exception>
        public static void Requires(bool condition)
        {
            if (!condition)
            {
                throw new ArgumentException("Method condition violated.");
            }
        }

        /// <overloads>
        /// <param name="name">Name of the requirement, this should be something unique to make it easy to find.</param>
        /// </overloads>
        public static void Requires(bool condition, string name)
        {
            Proclaim.NotEmpty(name);

            if (!condition)
            {
                throw new ArgumentException("Invalid parameter value.", name);
            }
        }

        /// <overloads>
        /// <param name="message">Message if the condition isn't met</param>
        /// </overloads>
        public static void Requires(bool condition, string name, string message)
        {
            Proclaim.NotEmpty(name);

            if (!condition)
            {
                throw new ArgumentException(message, name);
            }
        }

        /// <summary>
        /// Requires that a value not be <c>null</c>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException">Value is <c>null</c>.</exception>
        public static void RequiresNotNull<T>(T value, string name) where T : class
        {
            Proclaim.NotEmpty(name);

            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Requires that the string not be <c>null</c> and not zero length.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException">String is <c>null</c> or zero length.</exception>
        public static void RequiresNotEmpty(string str, string name)
        {
            RequiresNotNull(str, name);
            if (str.Length == 0)
            {
                throw new ArgumentException("Non-empty string required.", name);
            }
        }

        /// <summary>
        /// Requires that the collection not be <c>null</c> and has at least one element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException">Collection is <c>null</c> or has no elements.</exception>
        public static void RequiresNotEmpty<T>(ICollection<T> collection, string name)
        {
            RequiresNotNull(collection, name);
            if (collection.Count == 0)
            {
                throw new ArgumentException("Non-empty collection required.", name);
            }
        }

        /// <summary>
        /// Requires the specified index to point inside the array.
        /// </summary>
        /// <exception cref="ArgumentNullException">Array is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Index is outside the array.</exception>
        public static void RequiresArrayIndex<T>(IList<T> array, int index, string indexName)
        {
            Proclaim.NotEmpty(indexName);
            Proclaim.NotNull(array);

            if (index < 0 || index >= array.Count)
            {
                throw new ArgumentOutOfRangeException(indexName);
            }
        }

        /// <summary>
        /// Requires the specified index to point inside the array or at the end.
        /// </summary>
        /// <exception cref="ArgumentNullException">Array is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Index is outside the array.</exception>
        public static void RequiresArrayInsertIndex<T>(IList<T> array, int index, string indexName)
        {
            Proclaim.NotEmpty(indexName);
            Proclaim.NotNull(array);

            if (index < 0 || index > array.Count)
            {
                throw new ArgumentOutOfRangeException(indexName);
            }
        }

        /// <summary>
        /// Requires the range [offset, offset + count] to be a subset of [0, array.Count].
        /// </summary>
        /// <exception cref="ArgumentNullException">Array is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Offset or count are out of range.</exception>
        public static void RequiresArrayRange<T>(IList<T> array, int offset, int count, string offsetName, string countName)
        {
            Proclaim.NotEmpty(offsetName);
            Proclaim.NotEmpty(countName);
            Proclaim.NotNull(array);

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(countName);
            }
            if (offset < 0 || array.Count - offset < count)
            {
                throw new ArgumentOutOfRangeException(offsetName);
            }
        }

        /// <summary>
        /// Requires the range [offset, offset + count] to be a subset of [0, array.Count].
        /// </summary>
        /// <exception cref="ArgumentNullException">String is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Offset or count are out of range.</exception>
        public static void RequiresArrayRange(string str, int offset, int count, string offsetName, string countName)
        {
            Proclaim.NotEmpty(offsetName);
            Proclaim.NotEmpty(countName);
            Proclaim.NotNull(str);

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(countName);
            }
            if (offset < 0 || str.Length - offset < count)
            {
                throw new ArgumentOutOfRangeException(offsetName);
            }
        }

        /// <summary>
        /// Requires the array and all its items to be non-null.
        /// </summary>
        public static void RequiresNotNullItems<T>(IList<T> items, string name)
        {
            Proclaim.NotNull(name);
            RequiresNotNull(items, name);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    throw new ArgumentException(name);
                }
            }
        }
    }
}