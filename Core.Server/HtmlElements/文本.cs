using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
    public class 文本 : 基本元素
    {
        public 文本(string 内容)
            : base(null)
        {
            this.内容 = 内容;
        }

        public string 内容 { get; set; }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            return null;
        }

        public override string 生成代码()
        {
            return 内容.IsNullOrEmpty() ? String.Empty : Core.Web.Web处理函数.进行HTML转义(内容);
        }
    }
}
