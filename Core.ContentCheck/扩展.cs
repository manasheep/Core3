using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Core.ContentCheck
{
    public static class 内容审查扩展
    {
        /// <summary>
        /// 获得列表中最大的匹配文本长度
        /// </summary>
        /// <returns>最大长度</returns>
        public static int 获取最大长度(this List<内容审查规则> l)
        {
            return 获取最大长度(l, true);
        }

        /// <summary>
        /// 获得列表中最大的匹配文本长度
        /// </summary>
        /// <param name="是否使用缓存">指示是否从缓存中检索数据</param>
        /// <returns>最大长度</returns>
        public static int 获取最大长度(this List<内容审查规则> l, bool 是否使用缓存)
        {
            if (!是否使用缓存 || 缓存[l] == null)
            {
                //"未使用缓存".调试输出();
                var t = l.Max(f => f.最大长度);
                缓存[l] = t;
                return t;
            }
            else
            {
                //"使用缓存".调试输出();
                return (int)缓存[l];
            }
        }

        private static Hashtable 缓存 = new Hashtable();
    }
}
