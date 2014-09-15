using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Db4objects.Db4o.Linq;

    public static class Db4o扩展
    {
         public static IDb4oLinqQueryable<TSource> AsQueryable<TSource>(this IDb4oLinqQuery<TSource> self)
        {
            return Db4oLinqQueryExtensions.AsQueryable(self);
        }
        public static int Count<TSource>(this IDb4oLinqQuery<TSource> self)
        {
            return Db4oLinqQueryExtensions.Count(self);
        }
        public static IDb4oLinqQuery<TSource> OrderBy<TSource, TKey>(this IDb4oLinqQuery<TSource> self, Expression<Func<TSource, TKey>> expression)
        {
            return Db4oLinqQueryExtensions.OrderBy(self, expression);
        }
        public static IDb4oLinqQuery<TSource> OrderByDescending<TSource, TKey>(this IDb4oLinqQuery<TSource> self, Expression<Func<TSource, TKey>> expression)
        {
            return Db4oLinqQueryExtensions.OrderByDescending(self, expression);
        }
        public static IDb4oLinqQuery<TRet> Select<TSource, TRet>(this IDb4oLinqQuery<TSource> self, Func<TSource, TRet> selector)
        {
            return Db4oLinqQueryExtensions.Select(self, selector);
        }
        public static IDb4oLinqQuery<TSource> ThenBy<TSource, TKey>(this IDb4oLinqQuery<TSource> self, Expression<Func<TSource, TKey>> expression)
        {
            return Db4oLinqQueryExtensions.ThenBy(self, expression);
        }
        public static IDb4oLinqQuery<TSource> ThenByDescending<TSource, TKey>(this IDb4oLinqQuery<TSource> self, Expression<Func<TSource, TKey>> expression)
        {
            return Db4oLinqQueryExtensions.ThenByDescending(self, expression);
        }
        public static IDb4oLinqQuery<TSource> Where<TSource>(this IDb4oLinqQuery<TSource> self, Expression<Func<TSource, bool>> expression)
        {
            return Db4oLinqQueryExtensions.Where(self, expression);
        }

        /// <summary>
        /// 分页获取集合中的实体对象集合
        /// </summary>
        /// <param name="pageNumber">当前页数，从0开始记数</param>
        /// <param name="itemQuantity">每页承载的实体数目</param>
        /// <returns>分页后的实体对象集合</returns>
        public static IEnumerable<T> PaginationGet<T>(this IDb4oLinqQuery<T> o, int pageNumber, int itemQuantity)
        {
            return o.Skip(pageNumber * itemQuantity).Take(itemQuantity);
        }

        /// <summary>
        /// 计算分页后的总页数
        /// </summary>
        /// <param name="itemQuantity">每页承载的实体数目</param>
        /// <returns>分页后的总页数</returns>
        public static int PaginationCount<T>(this IDb4oLinqQuery<T> o, int itemQuantity)
        {
            var x = o.Count();
            if (x == 0) return 0;
            return x / itemQuantity + (x % itemQuantity > 0 ? 1 : 0);
        }
    }
