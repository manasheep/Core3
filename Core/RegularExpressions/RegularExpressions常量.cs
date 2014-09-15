using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.RegularExpressions
{
    public static class RegularExpressions常量
    {
        public const string 电子邮件地址格式正则匹配代码 = @"[a-zA-Z0-9_\.]+@[a-zA-Z0-9\-_\.]{1,16}\.(cn|com|net|org|biz|name|info|tw|hk)";
        public const string 超文本协议地址格式正则匹配代码 = @"(?<协议>http|https)://(?<地址>[%\w\.\/\?=:\-_]+)";
    }
}
