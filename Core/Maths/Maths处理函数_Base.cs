using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Maths
{
    public static partial class Maths处理函数
    {
        /// <summary>
        /// 将小数转换为百分数.
        /// </summary>
        /// <param name="数值">要转换的数值</param>
        /// <returns>字符串形式显示的百分数</returns>
        public static string 转换为百分数(this double 数值)
        {
            return String.Format("{0:P0}", 数值);
        }

        /// <summary>
        /// 将数字转换为16进制显示，如:3F
        /// </summary>
        public static string 转换为16进制显示(this int o)
        {
            return o.ToString("x");
        }

        /// <summary>
        /// 获得斐波那契数列中指定位置的值
        /// </summary>
        /// <param name="位">索引位置</param>
        /// <returns>数值</returns>
        public static long 获得斐波那契数(int 位)
        {
            long x = 0;
            if (位 <= 0) x = 0;
            else if (位 <= 2) x = 1;
            else x = 获得斐波那契数(位 - 1) + 获得斐波那契数(位 - 2);
            return x;
        }

        /// <summary>
        /// 将输入的数字四舍五入
        /// </summary>
        /// <param name="数字">输入数字</param>
        /// <param name="保留小数位">要保留的小数位数(可以为负数)</param>
        /// <returns>四舍五入后的值</returns>
        public static double 四舍五入(this double 数字, int 保留小数位)
        {
            数字 = 数字 * System.Math.Pow(10, 保留小数位) + 0.5;
            数字 = ((int)数字) / System.Math.Pow(10, 保留小数位);
            return 数字;
        }
    }
}
