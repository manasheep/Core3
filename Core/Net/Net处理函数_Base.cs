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
        /// 返回指定IP是否在指定的IP数组所限定的范围内, IP数组内的IP地址可以使用*表示该IP段任意, 例如192.168.1.*
        /// </summary>
        /// <param name="IP地址">要进行验证的IP地址</param>
        /// <param name="IP网段数组">作为验证依据的IP网段数组</param>
        /// <returns>是否匹配</returns>
        public static bool 验证IP网段(string IP地址, string[] IP网段数组)
        {

            string[] userip = IP地址.切分字符串(@".");
            for (int ipIndex = 0; ipIndex < IP网段数组.Length; ipIndex++)
            {
                string[] tmpip = IP网段数组[ipIndex].切分字符串(@".");
                int r = 0;
                for (int i = 0; i < tmpip.Length; i++)
                {
                    if (tmpip[i] == "*")
                    {
                        return true;
                    }

                    if (userip.Length > i)
                    {
                        if (tmpip[i] == userip[i])
                        {
                            r++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                if (r == 4)
                {
                    return true;
                }


            }
            return false;

        }
    }
}
