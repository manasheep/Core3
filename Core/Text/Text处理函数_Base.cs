using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;

namespace Core.Text
{
    [System.Runtime.InteropServices.GuidAttribute("76B41627-9A6F-4211-B9E8-CEFC44BB180F")]
    public static partial class Text处理函数
    {
        /// <summary>
        /// 比对两个字符串的相似度，每少余、多余、异于一个字符，就会增记1点差异值，最后将差异值返回
        /// </summary>
        /// <param name="原始字符串">原始字符串</param>
        /// <param name="对比字符串">用于对比的字符串</param>
        /// <returns>差异值，该值不会超过两个字符串长度之和</returns>
        public static int 计算相似度(this string 原始字符串, string 对比字符串)
        {
            int lenA = (int)原始字符串.Length;
            int lenB = (int)对比字符串.Length;
            int[,] c = new int[lenA + 1, lenB + 1];

            // i: begin point of strA
            // j: begin point of strB
            for (int i = 0; i < lenA; i++) c[i, lenB] = lenA - i;
            for (int j = 0; j < lenB; j++) c[lenA, j] = lenB - j;
            c[lenA, lenB] = 0;

            for (int i = lenA - 1; i >= 0; i--)
                for (int j = lenB - 1; j >= 0; j--)
                {
                    if (对比字符串[j] == 原始字符串[i])
                        c[i, j] = c[i + 1, j + 1];
                    else
                        c[i, j] = minValue(c[i, j + 1], c[i + 1, j], c[i + 1, j + 1]) + 1;
                }

            return c[0, 0];
        }

        static int minValue(int a, int b, int c)
        {
            if (a < b && a < c) return a;
            else if (b < a && b < c) return b;
            else return c;
        }

        /// <summary>
        /// 查找字符串内是否包含指定字符
        /// </summary>
        /// <param name="源字符串">要在其中进行查找的源字符串</param>
        /// <param name="全部匹配">是否要求包含有所有查找字符的情况下才返回true，否则只需包含至少一个即可返回true</param>
        /// <param name="查找字符">用于查找的一个或多个字符</param>
        /// <returns>验证结果</returns>
        public static bool? 验证是否包含指定字符(this string 源字符串, bool 全部匹配, params char[] 查找字符)
        {
            if (查找字符 == null || !源字符串.验证有效性()) return null;
            foreach (char c in 查找字符)
            {
                bool b = false;
                foreach (char f in 源字符串)
                {
                    if (f == c)
                    {
                        if (!全部匹配) return true;
                        b = true;
                        break;
                    }
                }
                if (全部匹配 && b == false) return false;
            }
            return 全部匹配;
        }

        /// <summary>
        /// 将字符串转换为对应的密钥代码
        /// </summary>
        /// <param name="字符串">密钥原文</param>
        /// <returns>密钥</returns>
        public static string 生成密钥(this string 字符串)
        {
            var s = new string[] 
                { 
                    "oockid1dALPqqd",
                    "ci199jAVdjal178",
                    "ccccoo130C1a059jfllao",
                    "00dVdo10149kcaclaccoq",
                    "cnam10DD10d9ajp1",
                    "mmac9aA188ACdjjaiqp[",
                    "cc0q0ikaicaiaji11a",
                    "cc10K!LCL;ack1cD",
                    "cc00c1kOFOFOAJ<",
                    "CLLCocjj1dhhagxmX"
                };
            var S = new StringBuilder();
            foreach (char c in 字符串)
            {
                S.Append(s[(int)c % 10]);
            }
            return S.ToString();
        }

        /// <summary>
        /// 替换字符串方法
        /// </summary>
        public static string 替换字符串(this string 源字符串, string 查找字符串, string 替代字符串, bool 区分大小写)
        {
            if (区分大小写) return 源字符串.Replace(查找字符串, 替代字符串);
            else
            {
                int i = 0;
                int j = 0;
                while (true)
                {
                    i = 源字符串.ToLower().IndexOf(查找字符串.ToLower(), j);
                    if (i < 0) break;
                    源字符串 = 源字符串.Remove(i, 查找字符串.Length).Insert(i, 替代字符串);
                    j = i + 替代字符串.Length;
                }
                return 源字符串;
            }
        }

        /// <summary>
        /// 检索子字符串在母字符串中出现的次数
        /// </summary>
        public static int 检索出现次数(this string 母字符串, string 子字符串)
        {
            StringBuilder s = new StringBuilder(母字符串);
            return (s.Length - s.Replace(子字符串, string.Empty).Length) / 子字符串.Length;
        }

        ///// <summary>
        ///// 转换为半角字符串
        ///// </summary>
        //public static string 转换为半角(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.Narrow, 0);
        //}

        ///// <summary>
        ///// 转换为简体中文
        ///// </summary>
        //public static string 转换为简体中文(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.SimplifiedChinese, 0);
        //}

        ///// <summary>
        ///// 转换为繁体中文
        ///// </summary>
        //public static string 转换为繁体中文(this string 字符串)
        //{
        //    return Strings.StrConv(字符串, VbStrConv.TraditionalChinese, 0);
        //}

        ///// <summary>
        ///// MD5函数
        ///// </summary>
        ///// <param name="str">原始字符串</param>
        ///// <returns>MD5结果</returns>
        //public static string 生成MD5码(this string str)
        //{
        //    byte[] b = Encoding.Default.GetBytes(str);
        //    b = new MD5CryptoServiceProvider().ComputeHash(b);
        //    StringBuilder ret = new StringBuilder();
        //    for (int i = 0; i < b.Length; i++)
        //        ret.Append(b[i].ToString("x").PadLeft(2, '0'));
        //    return ret.ToString();
        //}

        ///// <summary>
        ///// SHA256函数
        ///// </summary>
        ///// /// <param name="str">原始字符串</param>
        ///// <returns>SHA256结果</returns>
        //public static string 生成SHA256码(this string str)
        //{
        //    byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
        //    SHA256Managed Sha256 = new SHA256Managed();
        //    byte[] Result = Sha256.ComputeHash(SHA256Data);
        //    return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        //}

        /// <summary>
        /// 将指定字符串内的所有字符随机打乱顺序重新排列
        /// </summary>
        /// <param name="字符串">要处理的字符串</param>
        /// <returns>乱序排列后的字符串</returns>
        public static string 乱序排列字符(this string 字符串)
        {
            for (int i = 0; i < 字符串.Length; i++)
            {
                int Index = R.Next(字符串.Length);
                char TMP = 字符串[i];
                字符串 = 替换指定位置字符(字符串, i, 字符串[Index]);
                字符串 = 替换指定位置字符(字符串, Index, TMP);
            }
            return 字符串;
        }

        /// <summary>
        /// 替换字符串内指定位置字符为另一个字符
        /// </summary>
        /// <param name="字符串">要处理的字符串</param>
        /// <param name="位置">要替换字符的索引位置</param>
        /// <param name="字符">要替换为的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string 替换指定位置字符(this string 字符串, int 位置, char 字符)
        {
            字符串 = 字符串.Remove(位置, 1);
            字符串 = 字符串.Insert(位置, 字符.ToString());
            return 字符串;
        }

        /// <summary>
        /// 改变文件名字符串的扩展名部分
        /// </summary>
        /// <param name="文件名">文件名字串,如"MyNotes.doc"</param>
        /// <param name="新扩展名">新扩展名,如".txt"</param>
        public static string 改变扩展名(this string 文件名, string 新扩展名)
        {
            return Path.ChangeExtension(文件名, 新扩展名);
        }

        /// <summary>
        /// 为字符串添加尾缀,如果该尾缀已存在,则不进行操作。通常用于路径字符串尾部添加斜杠或反斜杠。
        /// </summary>
        /// <param name="字符串">原字符串</param>
        /// <param name="尾缀">要添加的尾缀</param>
        /// <returns>处理后的字符串</returns>
        public static string 添加尾缀(this string 字符串, string 尾缀)
        {
            if (字符串.EndsWith(尾缀)) return 字符串;
            else return 字符串 + 尾缀;
        }

        /// <summary>
        /// 初始化随机数
        /// </summary>
        private static Random R = new Random(DateTime.Now.GetHashCode());

        /// <summary>
        /// 遍历对象集合获取字符串形式的串接，以指定分隔符隔离每个对象
        /// </summary>
        /// <param name="对象集合">需处理的集合</param>
        /// <param name="间隔符号">间隔字符</param>
        /// <returns>拼接后的字符串</returns>
        public static string 展开对象数组字符(this System.Collections.IEnumerable 对象集合, string 间隔符号)
        {
            StringBuilder S = new StringBuilder();
            foreach (object o in 对象集合)
            {
                if (S.Length > 0) S.Append(间隔符号);
                S.Append(o.ToString());
            }
            return S.ToString();
        }

        /// <summary>
        /// 遍历字符串数组获取字符串形式的串接，以指定分隔符隔离每个对象
        /// </summary>
        /// <param name="字符串数组">需处理的数组</param>
        /// <param name="间隔符号">间隔字符</param>
        /// <returns>拼接后的字符串</returns>
        public static string 展开字符串数组字符(this IEnumerable<string> 字符串数组, string 间隔符号)
        {
            StringBuilder S = new StringBuilder();
            foreach (string o in 字符串数组)
            {
                if (S.Length > 0) S.Append(间隔符号);
                S.Append(o);
            }
            return S.ToString();
        }

        /// <summary>
        /// 输出指定重复次数的字符串
        /// </summary>
        /// <param name="字符串">要重复的字符串</param>
        /// <param name="次数">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string 重复(this string 字符串, int 次数)
        {
            StringBuilder S = new StringBuilder();
            for (int i = 0; i < 次数; i++) S.Append(字符串);
            return S.ToString();
        }

        /// <summary>
        /// 将字符串转为星号显示
        /// </summary>
        /// <param name="原字符串">原字符串</param>
        /// <returns>星号显示的字符串</returns>
        public static string 转换为密码显示(this string 原字符串)
        {
            return 重复("*", 原字符串.Length);
        }

        /// <summary>
        /// 验证一个字符串变量是否有效,当其为null或者空字符串以及空白字符时视为无效
        /// </summary>
        /// <param name="字符串">用于验证的字符串</param>
        /// <returns>是否有效</returns>
        public static bool 验证有效性(this string 字符串)
        {
            return 验证有效性(字符串, false);
        }

        /// <summary>
        /// 验证一个字符串变量是否有效,当其为null或者空字符串时视为无效
        /// </summary>
        /// <param name="字符串">用于验证的字符串</param>
        /// <param name="允许存在空白字符">是否允许存在空格及换行符等无意义字符</param>
        /// <returns>是否有效</returns>
        public static bool 验证有效性(this string 字符串, bool 允许存在空白字符)
        {
            //return 字符串 != null && 字符串.Length > 0 && (允许存在空白字符 || 字符串.清除首尾空白().Length > 0);
            return !String.IsNullOrEmpty(字符串) && (允许存在空白字符 || 字符串.清除首尾空白().Length > 0);
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="随机字符串长度">指定生成的随机字符串的长度</param>
        /// <returns>随机字符串</returns>
        public static string 生成随机字符串(int 随机字符串长度)
        {
            return 生成随机字符串("1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", 随机字符串长度);
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="可出现的字符">指定包含所有可能出现的字符的字符串</param>
        /// <param name="随机字符串长度">指定生成的随机字符串的长度</param>
        /// <returns>随机字符串</returns>
        public static string 生成随机字符串(string 可出现的字符, int 随机字符串长度)
        {
            StringBuilder S = new StringBuilder();
            for (int i = 0; i < 随机字符串长度; i++)
            {
                S.Append(可出现的字符[R.Next(可出现的字符.Length)]);
            }
            return S.ToString();
        }

        ///// <summary>
        ///// 查找字符串在字符串数组中首次出现的索引位置
        ///// </summary>
        ///// <param name="字符串">需要查询的字符串</param>
        ///// <param name="字符串数组">查询的目标数组</param>
        ///// <param name="完全匹配">是否需要完全匹配目标字符串</param>
        //public static int 查找所在位置(string 字符串, string[] 字符串数组, bool 完全匹配)
        //{
        //    for (int i = 0; i < 字符串数组.Length; i++)
        //    {
        //        if (完全匹配 && 字符串数组[i] == 字符串) return i;
        //        if (!完全匹配 && 字符串数组[i].IndexOf(字符串) >= 0) return i;
        //    }
        //    return -1;
        //}

        /// <summary>
        /// 在[源字符串]中查找[查询字符串]的首次出现位置，如果[源字符串]为空或未找到包含有[查询字符串]则返回-1
        /// </summary>
        /// <param name="源字符串">要在其中查找的母字符串</param>
        /// <param name="查询字符串">用于查找的子字符串</param>
        /// <param name="匹配大小写">是否匹配大小写</param>
        /// <returns>索引位置</returns>
        public static int 查找字符串位置(this string 源字符串, string 查询字符串, bool 匹配大小写)
        {
            if (源字符串.验证有效性(true))
            {
                if (!匹配大小写)
                {
                    源字符串 = 源字符串.ToLower();
                    查询字符串 = 查询字符串.ToLower();
                }
                return 源字符串.IndexOf(查询字符串);
            }
            return -1;
        }

        /// <summary>
        /// 根据分隔符将字符串切割,返回切割后的字符串数组
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="分隔符">分隔字符串的符号,可使用正则表达式语法</param>
        public static string[] 切分字符串(this string 字符串, string 分隔符)
        {
            return Regex.Split(字符串, 分隔符);
        }

        /// <summary>
        /// 根据逗号将字符串切割,返回切割后的字符串数组
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        public static string[] 切分字符串(this string 字符串)
        {
            return 切分字符串(字符串, ",");
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <returns>长度</returns>
        public static int 获取字符串长度(this string 字符串)
        {
            return Encoding.Unicode.GetBytes(字符串).Length;
        }

        /// <summary>
        /// 将字符串按指定长度裁切
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="长度">要保留的字符长度字节数,汉字计为2个字节</param>
        /// <param name="超过长度显示的字符">例如:".."</param>
        public static string 截断(this string 字符串, int 长度, string 超过长度显示的字符)
        {
            int x = 0;
            string s = "";
            for (int i = 0; i < 字符串.Length; i++)
            {
                if (Regex.IsMatch(字符串[i].ToString(), @"[^\x00-\xff]"))
                {
                    if (x + 2 > 长度) break;
                    x += 2;
                }
                else x++;
                s += 字符串[i];
                if (x >= 长度) break;
            }
            if (s.Length < 字符串.Length) s += 超过长度显示的字符;
            return s;
        }

        /// <summary>
        /// 将字符串按指定长度裁切
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="长度">要保留的字符长度字节数,汉字计为2个字节</param>
        public static string 截断(this string 字符串, int 长度)
        {
            return 截断(字符串, 长度, "..");
        }

        /*
        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="CSS类">超链接所使用的CSS类，没有的话使用空字符串</param>
        /// <param name="新窗口打开链接">设置超链接是否由新窗口打开</param>
        /// <param name="允许HTML标记">设置是否支持内嵌HTML代码的标记</param>
        public static string UBB转换为HTML(string 字符串, string CSS类, bool 新窗口打开链接, bool 允许HTML标记)
        {
            if (字符串 == null) return null;
            MatchCollection HTML = Regex.Matches(字符串, @"\[html\]([\s\S]*?)\[/html\]", RegexOptions.IgnoreCase);
            if (允许HTML标记)
            {
                字符串 = Regex.Replace(字符串, @"\[html\]([\s\S]*?)\[/html\]", "[||HTMLCODE||]", RegexOptions.IgnoreCase);
            }
            字符串 = 进行HTML编码(字符串);
            字符串 = Regex.Replace(字符串, @" ", "&nbsp;");
            字符串 = Regex.Replace(字符串, @"\t", 重复("&nbsp;", 6));
            字符串 = Regex.Replace(字符串, @"(\r)?\n", "<br />");
            字符串 = Regex.Replace(字符串, @"\[b\]([\s\S]*?)\[/b\]", "<strong>$1</strong>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[u\]([\s\S]*?)\[/u\]", "<u>$1</u>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[i\]([\s\S]*?)\[/i\]", "<i>$1</i>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[center\]([\s\S]*?)\[/center\]", "<center>$1</center>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[quote\]([\s\S]*?)\[/quote\]", @"<br />
<table border=""0"" cellpadding=""5""  width=""100%"" cellspacing=""1"" bgcolor=""#FFF2D9"">
  <tr>
    <td bgcolor=""#FFFDEC"">$1</td>
  </tr>
</table>
<br />", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[size=([1-7])\]([\s\S]*?)\[/size\]", "<font size=\"$1\">$2</font>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[font=(.*?)\]([\s\S]*?)\[/font\]", "<font face=\"$1\">$2</font>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[color=(.*?)\]([\s\S]*?)\[/color\]", "<font color=\"$1\">$2</font>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[email\](.*?)\[/email\]", "<a " + (CSS类.Length > 0 ? "class=\"" + CSS类 + "\"" : "") + " " + (新窗口打开链接 ? "target=\"_blank\"" : "") + " href=\"mailto:$1\">$1</a>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[email=(.*?)\]([\s\S]*?)\[/email\]", "<a " + (CSS类.Length > 0 ? "class=\"" + CSS类 + "\"" : "") + " " + (新窗口打开链接 ? "target=\"_blank\"" : "") + " href=\"mailto:$1\">$2</a>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[url\](.*?)\[/url\]", "<a " + (CSS类.Length > 0 ? "class=\"" + CSS类 + "\"" : "") + " " + (新窗口打开链接 ? "target=\"_blank\"" : "") + " href=\"[UrlEconding]$1[/UrlEconding]\">$1</a>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[url=(.*?)\]([\s\S]*?)\[/url\]", "<a " + (CSS类.Length > 0 ? "class=\"" + CSS类 + "\"" : "") + " " + (新窗口打开链接 ? "target=\"_blank\"" : "") + " href=\"[UrlEconding]$1[/UrlEconding]\">$2</a>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[(swf|flash)=([\d%]+),([\d%]+)\](.*?)\[/\1\]", @"<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""$2"" height=""$3""> <param name=""movie"" 值=""[UrlEconding]$4[/UrlEconding]"" /><param name=""quality"" 值=""high"" /></object>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[(img|image)\](.*?)\[/\1\]", "<img src=\"[UrlEconding]$2[/UrlEconding]\" border=\"0\" hspace=\"5\" vspace=\"5\" />", RegexOptions.IgnoreCase);
            MatchCollection m = Regex.Matches(字符串, @"\[UrlEconding\](.*?)\[/UrlEconding\]");
            for (int i = 0; i < m.Count; i++)
            {
                字符串 = 字符串.Replace(m[i].Value, 进行URL路径编码(m[i].Groups[1].Value.Replace("&nbsp;", " ")));
            }
            字符串 = Regex.Replace(字符串, @"　", 重复("&nbsp;",2));
            if (允许HTML标记)
            {
                for (int i = 0; i < HTML.Count; i++)
                {
                    字符串 = 字符串.Insert(字符串.IndexOf("[||HTMLCODE||]"), "[||NOWCHANGE||]");
                    字符串 = 字符串.Replace("[||NOWCHANGE||][||HTMLCODE||]", HTML[i].Groups[1].Value);
                }
            }
            return 字符串;
        }

        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        public static string UBB转换为HTML(string 字符串)
        {
            return UBB转换为HTML(字符串, "", true, true);
        }

        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="新窗口打开链接">设置超链接是否由新窗口打开</param>
        public static string UBB转换为HTML(string 字符串, bool 新窗口打开链接)
        {
            return UBB转换为HTML(字符串, "", 新窗口打开链接, true);
        }

        /// <summary>
        /// 将UBB代码转换为HTML代码
        /// </summary>
        /// <param name="字符串">需要处理的字符串</param>
        /// <param name="CSS类">超链接所使用的CSS类，没有的话使用空字符串</param>
        public static string UBB转换为HTML(string 字符串, string CSS类)
        {
            return UBB转换为HTML(字符串, CSS类, true, true);
        }
         */

        /// <summary>
        /// 验证字符串是否有效,有效则原样返回,无效则返回替代字符串
        /// </summary>
        /// <param name="验证字符串">要验证的字符串</param>
        /// <param name="替代字符串">替代的字符串</param>
        /// <returns>字符串</returns>
        public static string 验证及替代(this string 验证字符串, string 替代字符串)
        {
            return 验证有效性(验证字符串) ? 验证字符串 : 替代字符串;
        }

        /// <summary>
        /// 验证字符串是否有效,有效则将其格式化并返回,无效则返回替代字符串
        /// </summary>
        /// <param name="验证字符串">要验证的字符串</param>
        /// <param name="格式化字符串">当字符串有效时，以此将其格式化并输出，使用“{0}”表示原字符串</param>
        /// <param name="替代字符串">替代的字符串</param>
        /// <returns>字符串</returns>
        public static string 验证及替代(this string 验证字符串, string 格式化字符串, string 替代字符串)
        {
            return 验证有效性(验证字符串) ? String.Format(格式化字符串, 验证字符串) : 替代字符串;
        }

                /// <summary>
        /// 转换输出字符串，使其不会使正则表达式对之产生歧义
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string 进行正则表达式转义(this string str)
        {
            return Regex.Replace(str, @"([\[\]\(\)\{\}\,\.\$\^\*\+\?\|\-\\\/\<\>\:])", @"\$1");
        }

        /// <summary>
        /// Unicode将字符串中的每个字符转为转义字符串，如\u0c2f
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>转义后的字符串</returns>
        public static string 进行Unicode转义(this string str)
        {
            StringBuilder S = new StringBuilder();
            var s = Encoding.Unicode.GetBytes(str);
            for (int i = 0; i < s.Length; i += 2)
            {
                S.Append(@"\u");
                S.Append(s[i + 1].ToString("x2"));
                S.Append(s[i].ToString("x2"));
            }
            return S.ToString();
        }

        /// <summary>
        /// 按照指定要求，清除字符串中的首尾空白，当传入字符串为null时直接返回。
        /// </summary>
        /// <param name="字符串">需处理的字符串</param>
        /// <param name="段首">是否清除段首空白</param>
        /// <param name="段尾">是否清除段尾空白</param>
        /// <param name="清除换行符">是否同时清除换行符</param>
        /// <returns>处理后的字符串</returns>
        public static string 清除首尾空白(this string 字符串, bool 段首, bool 段尾, bool 清除换行符)
        {
            if (字符串 == null) return null;
            if (段首)
            {
                if (清除换行符) 字符串 = Regex.Replace(字符串, @"^[\s\r\n]*", "");
                else 字符串 = Regex.Replace(字符串, @"^\s*", "");
            }
            if (段尾)
            {
                if (清除换行符) 字符串 = Regex.Replace(字符串, @"[\s\r\n]*$", "");
                else 字符串 = Regex.Replace(字符串, @"\s*$", "");
            }
            return 字符串;
        }

        /// <summary>
        /// 清除字符串中的首尾空白
        /// </summary>
        /// <param name="字符串">需处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string 清除首尾空白(this string 字符串)
        {
            return 清除首尾空白(字符串, true, true, true);
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表格式为：[查询字串][置换字符][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号替代，如“====_R_====”
        /// </summary>
        /// <param name="输入文本">要进行置换的原始文本</param>
        /// <param name="置换表">置换表</param>
        /// <param name="置换字符">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="回车替代符">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <returns>置换后的文本</returns>
        public static string 文本置换(this string 输入文本, string 置换表, string 置换字符, string 回车替代符)
        {
            MatchCollection M = Regex.Matches(置换表, "(.+)" + 置换字符 + "(.+)");
            foreach (Match f in M)
            {
                输入文本 = 输入文本.Replace(f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(回车替代符, "\r\n"));
            }
            return 输入文本;
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表格式为：[查询字串][置换字符][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号替代，如“====_R_====”
        /// </summary>
        /// <param name="输入文本">要进行置换的原始文本</param>
        /// <param name="正则表达式">查询及替换时是否使用正则表达式</param>
        /// <param name="置换表">置换表</param>
        /// <param name="置换字符">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="回车替代符">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <returns>置换后的文本</returns>
        public static string 文本置换(this string 输入文本, bool 正则表达式, string 置换表, string 置换字符, string 回车替代符)
        {
            int x = 0;
            return 文本置换(输入文本, 正则表达式, 置换表, 置换字符, 回车替代符, out x, out x);
        }

        /// <summary>
        /// 依据置换表置换输入文本，置换表格式为：[查询字串][置换字符][替换字串]，如“我的====_C_====我们的”，每行一条，如需替代为包含换行符的文本，换行符使用自定义符号替代，如“====_R_====”
        /// </summary>
        /// <param name="输入文本">要进行置换的原始文本</param>
        /// <param name="正则表达式">查询及替换时是否使用正则表达式</param>
        /// <param name="置换表">置换表</param>
        /// <param name="置换字符">自定义置换符，如“====_C_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="回车替代符">自定义回车替代符，如“====_R_====”，注意不要使用令正则表达式产生歧义的字符</param>
        /// <param name="生效数">返回置换生效的条目数</param>
        /// <param name="总计数">返回置换条目的总数</param>
        /// <returns>置换后的文本</returns>
        public static string 文本置换(this string 输入文本, bool 正则表达式, string 置换表, string 置换字符, string 回车替代符, out int 生效数, out int 总计数)
        {
            生效数 = 总计数 = 0;
            MatchCollection M = Regex.Matches(置换表, "([^\r\n]+)" + 置换字符 + "([^\r\n]*)");
            foreach (Match f in M)
            {
                总计数++;
                if (正则表达式)
                {
                    try
                    {
                        if (Regex.IsMatch(输入文本, f.Groups[1].Value.Replace("/r", "").Replace("/n", ""))) 生效数++;
                        输入文本 = Regex.Replace(输入文本, f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(回车替代符, "\r\n"));
                    }
                    catch { }
                }
                else
                {
                    if (输入文本.IndexOf(f.Groups[1].Value) >= 0) 生效数++;
                    输入文本 = 输入文本.Replace(f.Groups[1].Value.Replace("/r", "").Replace("/n", ""), f.Groups[2].Value.Replace("/r", "").Replace("/n", "").Replace(回车替代符, "\r\n"));
                }
            }
            return 输入文本;
        }


        /// <summary>
        /// 获取字符串的起始字符
        /// </summary>
        /// <returns>首字符</returns>
        public static char 获取首字符(this string s)
        {
            return s[0];
        }

        /// <summary>
        /// 获取字符串的末尾字符
        /// </summary>
        /// <returns>尾字符</returns>
        public static char 获取尾字符(this string s)
        {
            return s[s.Length - 1];
        }

        /// <summary>
        /// 转换为Int16方法
        /// </summary>
        public static Int16 转换为Int16(this string s)
        {
            return Int16.Parse(s);
        }

        /// <summary>
        /// 转换为Int32方法
        /// </summary>
        public static Int32 转换为Int32(this string s)
        {
            return Int32.Parse(s);
        }

        /// <summary>
        /// 转换为Int64方法
        /// </summary>
        public static Int64 转换为Int64(this string s)
        {
            return Int64.Parse(s);
        }

        /// <summary>
        /// 转换为double方法
        /// </summary>
        public static double 转换为double(this string s)
        {
            return double.Parse(s);
        }

        /// <summary>
        /// 转换为float方法
        /// </summary>
        public static float 转换为float(this string s)
        {
            return float.Parse(s);
        }


        /// <summary>
        /// 根据字数限制截取原始内容，并将截取后的字符串中最后一个换行符以后的字符删除
        /// </summary>
        public static string 段落截取(this string 原始内容, int 字数限制)
        {
            var s=原始内容.Substring(0, 200);
            var l=s.LastIndexOf("\n");
            return s.Substring(0, l);
        }

        /// <summary>
        /// 转换字符串为首字母大写形式，如Name
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>首字母大写形式</returns>
        public static string 转换为首字母大写形式(this string s)
        {
            return s.ToUpper()[0] + s.ToLower().Substring(1);
        }

        /// <summary>
        /// 忽略符号和空格，将字符串以驼峰形式重组，例如windows media player 10会重组为WindowsMediaPlayer10
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="首字母是否大写">首字母是否大写</param>
        /// <returns>驼峰形式重组的字符串</returns>
        public static string 转换为驼峰字母形式(this string s,bool 首字母是否大写)
        {
            var m = s.RegexMatches(@"[\w\d]+");
            StringBuilder sb = new StringBuilder();
            foreach (var f in m.Cast<Match>())
            {
                if (sb.Length == 0 && !首字母是否大写) sb.Append(f.Value.ToLower());
                else sb.Append(f.Value.转换为首字母大写形式());
            }
            return sb.ToString();
        }
    }
}
