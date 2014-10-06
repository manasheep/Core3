using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public class Bootstarp复选框元素组 : Bootstarp表单控件元素组
    {
        public Bootstarp复选框元素组():base()
        {
            
        }

        public Bootstarp复选框元素组(string 控件名, bool 是否已勾选)
            : base(控件名)
        {
            this.是否已勾选 = 是否已勾选;
        }

        public override Bootstarp表单控件元素组 应用模型属性元数据(ModelMetadata 模型属性元数据)
        {
            base.应用模型属性元数据(模型属性元数据);
            是否已勾选 = 模型属性元数据.Model.ToStringSafety().ToLower() == "true";
            return this;
        }

        public bool 是否已勾选 { get; set; }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            var input = new 基本元素("input").添加属性("type", "checkbox").添加属性("value", "true");
            添加控件名属性(input);
            按需添加禁用属性(input);
            if (是否已勾选)
            {
                input.添加属性("checked", "checked");
            }

            var label = new 基本元素("label").添加子元素(input).添加子元素(new 文本(标签显示内容));

            var checkboxdiv = new 基本元素("div");
            checkboxdiv.添加Css类("checkbox");
            if (是否为大尺寸样式)
            {
                checkboxdiv.添加Css类("input-lg");
            }
            checkboxdiv.添加子元素(label);

            var leftdiv = new 基本元素("div");
            按需添加左偏移栅格Css类(leftdiv);
            添加左栏占据栅格Css类(leftdiv);
            添加子元素(leftdiv);

            var rounddiv = new 基本元素("div");
            添加右栏占据栅格Css类(rounddiv);
            rounddiv.添加子元素(checkboxdiv);
            按需添加附注元素到子元素列表(rounddiv);

            添加子元素(rounddiv);

            return base.生成标签构造器();
        }
    }
}
