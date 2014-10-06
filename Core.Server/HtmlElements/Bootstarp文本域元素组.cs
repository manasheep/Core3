using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
   public class Bootstarp文本域元素组 : Bootstarp表单控件元素组
    {
       public Bootstarp文本域元素组(string 控件名, string 值)
           : base(控件名)
       {
           默认显示行数 = 3;
           this.值 = 值;
       }

       public Bootstarp文本域元素组() : base()
       {
           
       }

       public override Bootstarp表单控件元素组 应用模型属性元数据(ModelMetadata 模型属性元数据)
       {
           base.应用模型属性元数据(模型属性元数据);
           默认显示行数 = 3;
           值 = 模型属性元数据.Model.ToStringSafety();
           替代文字 = 模型属性元数据.Watermark;
           是否只读 = 模型属性元数据.IsReadOnly;
           return this;
       }

       public string 值 { get; set; }
       public string 替代文字 { get; set; }
       public bool 是否只读 { get; set; }
       public int 默认显示行数 { get; set; }

       public override TagBuilder 生成标签构造器()
       {
           是否为大尺寸样式 = false;

           var input = new 基本元素("textarea")
           {
               Id = Id
           }
               .添加Css类("form-control")
               .添加属性("rows", 默认显示行数.ToString());
           if (!值.IsNullOrEmpty())
           {
               input.添加子元素(new HTML代码(值));
           }
           if (!替代文字.IsNullOrEmpty())
           {
               input.添加属性("placeholder", 替代文字);
           }
           if (是否只读)
           {
               input.添加属性("readonly", "readonly");
           }
           添加控件名属性(input);
           按需添加禁用属性(input);

           var label = new 基本元素("label")
                .添加子元素(new 文本(标签显示内容))
                .添加Css类("control-label")
                .添加属性("for", Id);
           添加左栏占据栅格Css类(label);
           按需添加左偏移栅格Css类(label);
           添加子元素(label);

           var div = new 基本元素("div");
           添加右栏占据栅格Css类(div);
           div.添加子元素(input);
           按需添加附注元素到子元素列表(div);
           添加子元素(div);

           return base.生成标签构造器();
       }
    }
}
