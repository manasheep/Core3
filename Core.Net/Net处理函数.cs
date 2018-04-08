using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Core.Text;
using System.Text.RegularExpressions;

namespace Core.Net
{
    public static partial class Net处理函数
    {
        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long 转换为整数(this IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }

        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress 转换为IP地址(this long l)
        {
            var b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }

        /// <summary>
        /// 通过域名获得IP地址
        /// </summary>
        /// <param name="主机域名">要查看的域名</param>
        /// <returns>IP地址列表</returns>
        public static IPAddress[] 获取IP地址(string 主机域名)
        {
            return Dns.GetHostAddresses(主机域名);
        }

        /// <summary>
        /// 验证字符串是否符合IP地址规则，如：192.168.0.1
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>是否符合IP地址规则</returns>
        public static bool 验证是否为IP地址(this string s)
        {
            return s.RegexIsMatch(@"^(((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))$");
        }

        /// <summary>
        /// 获取字符串的IP地址及端口号匹配项，如：192.168.0.1:8080，如果匹配成功的话，组$1代表IP地址部分，组$11代表端口部分
        /// </summary>
        /// <param name="s">字符串</param>
        /// <returns>IP地址及端口号匹配项</returns>
        public static Match 获取IP地址及端口匹配项(this string s)
        {
            return s.RegexMatch(@"^(((25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))):([0-9]|[1-9]\d{1}|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$");
        }
   }
}
