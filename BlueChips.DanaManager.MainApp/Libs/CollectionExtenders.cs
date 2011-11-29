using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueChips.DanaManager.MainApp.Libs
{
    public static class CollectionExtenders
    {
        #region foreach
        /// <summary>
        /// for-each do lambda with side effect on generic IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enu, Action<T> action)
        {
            if (enu == null) return null;
            foreach (T val in enu)
                action(val);
            return enu;
        }

        /// <summary>
        /// for-each do lambda + row index with side effect on generic IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enu, Action<T, Int32> action)
        {
            if (enu == null) return null;
            int i = 0;
            foreach (T val in enu)
                action(val, i++);
            return enu;
        }

        /// <summary>
        /// for-each do lambda with side-effect on List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="operation"></param>
        public static List<T> Each<T>(this List<T> collection, Action<T> operation)
        {
            if (collection == null) return null;
            foreach (T el in collection)
                operation.Invoke(el);
            return collection;
        }
        /// <summary>
        /// for-each do lambda + row index with side-effect on List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="operation"></param>
        public static List<T> Each<T>(this List<T> collection, Action<T, Int32> operation)
        {
            if (collection == null) return null;
            Int32 i = 0;
            foreach (T el in collection)
                operation.Invoke(el, i++);
            return collection;
        }
        /// <summary>
        /// for-each do lambda with side-effect on IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="operation"></param>
        public static IQueryable<T> Each<T>(this IQueryable<T> collection, Action<T> operation)
        {
            if (collection == null) return null;
            foreach (T el in collection)
                operation.Invoke(el);
            return collection;
        }
        /// <summary>
        /// for-each do lambda + row index with side-effect on IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="operation"></param>
        public static IQueryable<T> Each<T>(this IQueryable<T> collection, Action<T, Int32> operation)
        {
            if (collection == null) return null;
            Int32 i = 0;
            foreach (T el in collection)
                operation.Invoke(el, i++);
            return collection;
        }
        #endregion
    }
}
