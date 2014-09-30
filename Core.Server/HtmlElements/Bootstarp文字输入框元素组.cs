using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
    public class Bootstarp文字输入框元素组 : 基本元素
    {
        public Bootstarp文字输入框元素组(string 控件名, string 值)
            : base("div")
        {
            this.控件名 = 控件名;
            this.值 = 值;
            添加Css类("form-group");
            控件类型 = 文字输入框控件类型.text;
            左栏占据栅格数 = 2;
            总占据栅格数 = 12;
        }

        public string 控件名 { get; set; }
        public string 值 { get; set; }
        public string 标签显示内容 { get; set; }
        public string 替代文字 { get; set; }
        public string 附注 { get; set; }
        public 文字输入框控件类型 控件类型 { get; set; }
        public string 首注 { get; set; }
        public string 尾注 { get; set; }
        public bool 是否禁用 { get; set; }
        public bool 是否只读 { get; set; }
        public bool 是否为大尺寸样式 { get; set; }
        public int 左栏占据栅格数 { get; set; }
        public int 总占据栅格数 { get; set; }
        public int 左偏移栅格数 { get; set; }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            if (是否已进行过生成) throw new Exception("此对象无法执行多次生成代码操作。");

            if (是否为大尺寸样式)
            {
                添加Css类("form-group-lg");
            }

            var id = "id" + Guid.NewGuid();

            var input = new 基本元素("input")
            {
                Id = id
            }
            .添加Css类("form-control")
            .添加属性("name", 控件名)
            .添加属性("type", 控件类型.ToString());
            if (!值.IsNullOrEmpty())
            {
                input.添加属性("value", 值);
            }
            if (!替代文字.IsNullOrEmpty())
            {
                input.添加属性("placeholder", 替代文字);
            }
            if (是否禁用)
            {
                input.添加属性("disabled", "true");
            }
            if (是否只读)
            {
                input.添加属性("readonly", "true");
            }

            var label = new 基本元素("label")
                .添加子元素(new 文本(标签显示内容))
                .添加Css类("control-label")
                .添加Css类("col-sm-" + 左栏占据栅格数)
                .添加属性("for", id);
            if (左偏移栅格数 != 0) label.添加Css类("col-xs-offset-" + 左偏移栅格数);
            if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 控件名;
            添加子元素(label);

            var div = new 基本元素("div")
                .添加Css类("col-sm-" + (总占据栅格数 - 左栏占据栅格数));
            if (首注.IsNullOrEmpty() && 尾注.IsNullOrEmpty())
            {
                div.添加子元素(input);
            }
            else
            {
                var roundgroup = new 基本元素("div").添加Css类("input-group");
                if (是否为大尺寸样式)
                {
                    roundgroup.添加Css类("input-group-lg");
                }
                if (!首注.IsNullOrEmpty())
                {
                    roundgroup.添加子元素(
                        new 基本元素("span")
                        .添加Css类("input-group-addon")
                        .添加子元素(new 文本(首注))
                        );
                }
                roundgroup.添加子元素(input);
                if (!尾注.IsNullOrEmpty())
                {
                    roundgroup.添加子元素(
                        new 基本元素("span")
                        .添加Css类("input-group-addon")
                        .添加子元素(new 文本(尾注))
                        );
                }
                div.添加子元素(roundgroup);
            }
            if (!附注.IsNullOrEmpty())
            {
                div.添加子元素(
                    new 基本元素("span")
                    .添加Css类("help-block")
                    .添加子元素(new 文本(附注))
                    );
            }
            添加子元素(div);

            return base.生成标签构造器();
        }
    }
}
