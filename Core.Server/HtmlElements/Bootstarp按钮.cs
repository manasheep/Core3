using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
    public class Bootstarp按钮 : 基本元素
    {
        protected Bootstarp按钮(string 标签名称, string 标签显示内容)
            : base(标签名称)
        {
            初始化(标签显示内容);
        }

        public Bootstarp按钮(string 标签显示内容)
            : base("button")
        {
            初始化(标签显示内容);
        }

        protected virtual void 初始化(string 标签显示内容)
        {
            添加属性("type", "button");
            添加Css类("btn");
            this.标签显示内容 = 标签显示内容;
        }

        public string 标签显示内容 { get; set; }
        public bool 是否为大尺寸样式 { get; set; }
        public Bootstarp按钮样式类型? 样式类型 { get; set; }
        public Bootstarp图标样式类型? 图标样式类型 { get; set; }
        public bool 是否禁用 { get; set; }

        protected virtual void 按需添加禁用属性(基本元素 元素)
        {
            if (是否禁用)
            {
                元素.添加属性("disabled", "disabled");
            }
        }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            按需添加禁用属性(this);
            添加Css类("btn-" + (样式类型 == null ? "default" : 样式类型.Value.ToString()));
            if (是否为大尺寸样式)
            {
                添加Css类("btn-lg");
            }

            if (图标样式类型 != null)
            {
                添加子元素(new Bootstarp图标(图标样式类型.Value));
                添加子元素(new 文本(" "));
            }

            添加子元素(new 文本(标签显示内容));
            return base.生成标签构造器();
        }
    }
}
