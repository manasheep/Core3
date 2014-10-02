using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
    public class Bootstarp超链接按钮 : Bootstarp按钮
    {
        public Bootstarp超链接按钮(string 标签显示内容,string 超链接网址) : base("a",标签显示内容)
        {
            this.超链接网址 = 超链接网址;
        }

        public string 超链接网址 { get; set; }

        protected override void 初始化(string 标签显示内容)
        {
            添加属性("role", "button");
            添加Css类("btn");
            this.标签显示内容 = 标签显示内容;
        }

        protected override void 按需添加禁用属性(基本元素 元素)
        {
            if (是否禁用)
            {
                添加Css类("disabled");
            }
        }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            添加属性("href", 超链接网址);
            return base.生成标签构造器();
        }
    }
}
