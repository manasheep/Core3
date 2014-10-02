using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
    /// <summary>
    /// 直接输出的HTML代码，通常作为自定义的内嵌内容
    /// </summary>
    public class HTML代码:文本
    {
        public HTML代码(string 内容) : base(内容)
        {
        }

        public override string 生成代码()
        {
            return 内容;
        }
    }
}
