using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Text;

namespace Core.Web
{
    public static class Web处理函数
    {

        /// <summary>
        /// 首先执行“进行HTML编码”方法，然后转换空格、Tab及换行符为HTML页面中可呈现的形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义结果</returns>
        public static string 进行HTML转义(this string str)
        {
            return Regex.Replace(str.Replace(" ", "&nbsp;").Replace("\t", "&nbsp;".重复(4)), @"(\r)?\n", "<br />");
        }
    }
}
