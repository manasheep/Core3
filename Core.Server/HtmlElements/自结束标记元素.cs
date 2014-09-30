using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public class 自结束标记元素 : 基本元素
    {
        public 自结束标记元素(string 标签名称) : base(标签名称)
        {
        }

        public override string 生成代码()
        {
            return 生成标签构造器().ToString(TagRenderMode.SelfClosing);
        }
    }
}
