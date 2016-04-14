using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ContentCheck
{
    /// <summary>
    /// 用于记录扫描原文时搜索到的首尾触发字符信息
    /// </summary>
    [Serializable]
    public class 触发记录
    {
        /// <summary>
        /// 触发的字符
        /// </summary>
        public char 字符
        {
            get
            {
                return _字符;
            }
            set
            {
                _字符 = value;
            }
        }
        private char _字符;
        /// <summary>
        /// 在原文中的索引位置
        /// </summary>
        public int 索引位置
        {
            get
            {
                return _索引位置;
            }
            set
            {
                _索引位置 = value;
            }
        }
        private int _索引位置;
    }
}
