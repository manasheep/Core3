using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Text;
using System.Web;
using System.Drawing;

namespace Core.Web
{
    public static class Web处理函数
    {
        /// <summary>
        /// 将颜色转换为网页代码形式，如FFDA00
        /// </summary>
        public static string 转换为网页用16进制字串(this Color c)
        {
            return string.Format("{0:x2}{1:x2}{2:x2}", c.R, c.G, c.B);
        }

        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        public static string 转换UBB内容(this string s)
        {
            return UBB.转换为Html代码(s);
        }

        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        /// <param name="新窗口打开链接">设置超链接是否由新窗口打开</param>
        public static string 转换UBB内容(this string s, bool 新窗口打开链接)
        {
            return UBB.转换为Html代码(s, 新窗口打开链接);
        }

        /// <summary>
        /// 清除字符串内的UBB标签
        /// </summary>
        public static string 清除UBB格式(this string 字符串)
        {
            while (Regex.IsMatch(字符串, @"\[(\w+)=?[^\]]*\]([\s\S]*?)\[/\1\]"))
                字符串 = Regex.Replace(字符串, @"\[(\w+)\s?[^\]]*\]([\s\S]*?)\[/\1\]", "$2");
            return 字符串;
        }

        /// <summary>
        /// 清除字符串内的HTML标签的方式之一，适合对较为规则的文档进行简单替换。
        /// </summary>
        public static string 清除HTML代码(this string 字符串)
        {
            var ex = @"<\s*(?<tag>\w+)\s*.*?>(?<text>.*?)</\k<tag>\s*>";
            var op = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            字符串 = 字符串.RegexReplace(@"<\s*(?<tag>\w+)\s*[^>]+?/>", "");
            while (字符串.RegexIsMatch(ex, op))
                字符串 = 字符串.RegexReplace(ex, "${text}", op);
            return 字符串.RegexReplace(@"<\s*[\!-\[]*(?<tag>\w+)\s*[^>]+?>", "");
        }

        /// <summary>
        /// 清除字符串内的HTML标签的方式之二，可以提供更好的文档代码容错性，并拥有更多选项。
        /// </summary>
        /// <param name="是否清除脚本代码">是否清除脚本代码</param>
        /// <param name="是否转换特定标记为换行符">是否转换特定标记为换行符，包括br、hr及p、div、li、h1、h2……的结尾</param>
        public static string 清除HTML代码(this string 字符串, bool 是否清除脚本代码, bool 是否转换特定标记为换行符)
        {
            string v = 字符串;
            if (是否清除脚本代码)
            {
                v = v.RegexReplace(@"<\s*script.+?>.*?<\s*/script\s*>", String.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            if (是否转换特定标记为换行符)
            {
                v = v.RegexReplace(@"<\s*/\s*(?:p|br|div|li|h1|h2|h3|h4|h5|h6|hr|tr|dd|table|ul|ol|dl)\s*>", "【【【LineBreak】】】", RegexOptions.IgnoreCase);
                v = v.RegexReplace(@"<\s*(?:br|hr|p|tr|dd|div)[^>]*?/>", "【【【LineBreak】】】", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            v = v.RegexReplace(@"<[^>]+>", String.Empty).Replace("&nbsp;", " ").RegexReplace(@"\s+", " ");
            if (是否转换特定标记为换行符)
            {
                v = v.Replace("【【【LineBreak】】】", "\r\n");
            }
            return v;
        }

        /// <summary>
        /// 将字符串内的HTML标记符(不包括换行、空格等字符)转换为在HTML页面中可呈现的形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string 进行HTML编码(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 逆向执行“进行HTML编码”方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string 进行HTML解码(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 首先执行“进行HTML编码”方法，然后转换空格、Tab及换行符为HTML页面中可呈现的形式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义结果</returns>
        public static string 进行HTML转义(this string str)
        {
            return Regex.Replace(进行HTML编码(str).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;".重复(4)), @"(\r)?\n", "<br />");
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string 进行URL编码(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string 进行URL编码(this string str, Encoding 字符编码)
        {
            return HttpUtility.UrlEncode(str, 字符编码);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string 进行URL解码(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string 进行URL解码(this string str, Encoding 字符编码)
        {
            return HttpUtility.UrlDecode(str, 字符编码);
        }

        /// <summary>
        /// 返回 URL 地址字符串的编码结果
        /// </summary>
        /// <param name="URL地址">URL路径字符串</param>
        /// <returns>编码结果</returns>
        public static string 进行URL路径编码(this string URL地址)
        {
            return HttpUtility.UrlPathEncode(URL地址);
        }

    }
}
