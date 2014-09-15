using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Linq
{
    public static class Linq处理函数
    {
        /// <summary>
        /// 分页获取集合中的实体对象集合
        /// </summary>
        /// <param name="页码">当前页数，从0开始记数</param>
        /// <param name="每页实体数量">每页承载的实体数目</param>
        /// <returns>分页后的实体对象集合</returns>
        public static IQueryable<T> 分页获取<T>(this IQueryable<T> o, int 页码, int 每页实体数量)
        {
            return o.Skip(页码 * 每页实体数量).Take(每页实体数量);
        }

        /// <summary>
        /// 计算分页后的总页数
        /// </summary>
        /// <param name="每页实体数量">每页承载的实体数目</param>
        /// <returns>分页后的总页数</returns>
        public static int 分页总数<T>(this IQueryable<T> o, int 每页实体数量)
        {
            var x = o.Count();
            if (x == 0) return 0;
            return x / 每页实体数量 + (x % 每页实体数量 > 0 ? 1 : 0);
        }

        /// <summary>
        /// 分页获取集合中的实体对象集合
        /// </summary>
        /// <param name="页码">当前页数，从0开始记数</param>
        /// <param name="每页实体数量">每页承载的实体数目</param>
        /// <returns>分页后的实体对象集合</returns>
        public static IEnumerable<T> 分页获取<T>(this IEnumerable<T> o, int 页码, int 每页实体数量)
        {
            return o.Skip(页码 * 每页实体数量).Take(每页实体数量);
        }

        /// <summary>
        /// 计算分页后的总页数
        /// </summary>
        /// <param name="每页实体数量">每页承载的实体数目</param>
        /// <returns>分页后的总页数</returns>
        public static int 分页总数<T>(this IEnumerable<T> o, int 每页实体数量)
        {
            var x = o.Count();
            if (x == 0) return 0;
            return x / 每页实体数量 + (x % 每页实体数量 > 0 ? 1 : 0);
        }
    }
}
