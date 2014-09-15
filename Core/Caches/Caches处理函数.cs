using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Caches
{
    public static class Caches处理函数
    {
        private static Dictionary<object, Dictionary<string, object>> 缓存 = new Dictionary<object, Dictionary<string, object>>();

        /// <summary>
        /// 将键值存入当前对象的独立缓存区
        /// </summary>
        /// <param name="键">标识字符串</param>
        /// <param name="值">存入的值</param>
        public static void 存入缓存(this object o, string 键, object 值)
        {
            if (缓存.Keys.Contains(o)) 缓存[o][键] = 值;
            else
            {
                缓存.Add(o, new Dictionary<string, object>());
                缓存[o].Add(键, 值);
            }
        }

        /// <summary>
        /// 从当前对象的独立缓存区中取出指定值
        /// </summary>
        /// <param name="键">标识字符串</param>
        /// <returns>此前存入的值</returns>
        public static object 取出缓存(this object o, string 键)
        {
            if (缓存.Keys.Contains(o) && 缓存[o].Keys.Contains(键))
                return 缓存[o][键];
            else return null;
        }

        /// <summary>
        /// 从当前对象的独立缓存区中移除指定键值
        /// </summary>
        /// <param name="键">标识字符串</param>
        public static void 移除缓存(this object o, string 键)
        {
            if (缓存.Keys.Contains(o) && 缓存[o].Keys.Contains(键)) 缓存[o].Remove(键);
        }

        /// <summary>
        /// 清除当前对象的独立缓存区
        /// </summary>
        public static void 移除缓存(this object o)
        {
            if (缓存.Keys.Contains(o)) 缓存.Remove(o);
        }
    }
}
