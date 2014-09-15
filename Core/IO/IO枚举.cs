using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.IO
{
    public enum 存储单位
    {
        /// <summary>
        /// 字节 byte (B)
        /// </summary>
        [Remark("字节")]
        B = 1,
        /// <summary>
        /// 千字节 Kilobyte(K/KB)
        /// </summary>
        [Remark("千字节")]
        KB = 1024,
        /// <summary>
        /// 兆字节 Megabyte(M/MB)
        /// </summary>
        [Remark("兆字节")]
        MB = 1048576,
        /// <summary>
        /// 千兆字节 Gigabyte(G/GB)
        /// </summary>
        [Remark("千兆字节")]
        GB = 1073741824
    }
}
