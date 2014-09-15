using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;

namespace Core.Text
{
    public static partial class Text处理函数
    {
        
        /// <summary>
        /// 转换为半角字符串
        /// </summary>
        public static string 转换为半角(this string 字符串)
        {
            return Strings.StrConv(字符串, VbStrConv.Narrow, 0);
        }

        /// <summary>
        /// 转换为简体中文
        /// </summary>
        public static string 转换为简体中文(this string 字符串)
        {
            return Strings.StrConv(字符串, VbStrConv.SimplifiedChinese, 0);
        }

        /// <summary>
        /// 转换为繁体中文
        /// </summary>
        public static string 转换为繁体中文(this string 字符串)
        {
            return Strings.StrConv(字符串, VbStrConv.TraditionalChinese, 0);
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string 生成MD5码(this string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
                ret.Append(b[i].ToString("x").PadLeft(2, '0'));
            return ret.ToString();
        }

        /// <summary>
        /// SHA256函数
        /// </summary>
        /// /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string 生成SHA256码(this string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

    }
}
