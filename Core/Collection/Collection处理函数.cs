using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Core.Collection
{
   public static class Collection处理函数
    {
        /// <summary>
        /// 将另一个字典的内容复制到此字典
        /// </summary>
        /// <typeparam name="Key">键</typeparam>
        /// <typeparam name="Value">值</typeparam>
        /// <param name="并入字典">待并入的字典</param>
        /// <param name="覆盖冲突键对应值">指示发现已有的键时，使用并入字典的值，否则将保留原值</param>
        public static void 并入<Key, Value>(this Dictionary<Key, Value> o, Dictionary<Key, Value> 并入字典, bool 覆盖冲突键对应值)
        {
            foreach (Key f in 并入字典.Keys)
            {
                if (o.ContainsKey(f))
                {
                    if (覆盖冲突键对应值) o[f] = 并入字典[f];
                }
                else
                {
                    o.Add(f, 并入字典[f]);
                }
            }
        }

       /// <summary>
       /// 如果键存在则更新其值，否则就添加一个新键值对
       /// </summary>
        public static void 添加或更新<Key, Value>(this Dictionary<Key, Value> o, Key 键, Value 值)
        {
            if (o.ContainsKey(键)) o[键] = 值;
            else o.Add(键, 值);
        }

        /// <summary>
        /// 获取集合中的第一个对象，如果没有则返回null
        /// </summary>
        public static object 获取第一个对象(this IEnumerable o)
        {
            var e = o.GetEnumerator();
            if (e.MoveNext()) return e.Current;
            else return null;
        }

        /// <summary>
        /// 获取集合中指定索引位置的对象，如果没有则返回null
        /// </summary>
        public static object 获取对象(this IEnumerable o, int 索引位置)
        {
            var e = o.GetEnumerator();
            for (int i = 0; i < 索引位置; i++)
            {
                if (!e.MoveNext()) return null;
            }
            return e.Current;
        }
    }
}
