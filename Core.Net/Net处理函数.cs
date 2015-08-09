using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Core.Text;

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
   }
}
