using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public class Bootstarp下拉菜单元素组 : Bootstarp表单控件元素组
    {
        //public Bootstarp下拉菜单元素组(IEnumerable<object> 列表项名称列表, IEnumerable<object> 列表项值列表)
        //{
        //    this.按钮 = new Bootstarp按钮("--未指定--");
        //    this.列表项名称列表 = 列表项名称列表;
        //    this.列表项值列表 = 列表项值列表;
        //}

        //public Bootstarp按钮 按钮 { get; set; }

        //public override 基本元素 添加属性目标
        //{
        //    get
        //    {
        //        return 按钮;
        //    }
        //}

        //public IEnumerable<object> 列表项名称列表 { get; set; }
        //public IEnumerable<object> 列表项值列表 { get; set; }


        //public override Bootstarp表单控件元素组 应用模型属性元数据(ModelMetadata 模型属性元数据)
        //{
        //    base.应用模型属性元数据(模型属性元数据);
        //    值 = 模型属性元数据.Model.ToStringSafety();
        //    替代文字 = 模型属性元数据.Watermark;
        //    return this;
        //}

        //public string 值 { get; set; }
        //public string 替代文字 { get; set; }

        //public override System.Web.Mvc.TagBuilder 生成标签构造器()
        //{
        //    button.Id = Id;
        //    button.添加Css类("btn").添加属性("type", 控件类型.ToString());
        //    if (!值.IsNullOrEmpty())
        //    {
        //        input.添加属性("value", 值);
        //    }
        //    if (!替代文字.IsNullOrEmpty())
        //    {
        //        input.添加属性("placeholder", 替代文字);
        //    }
        //    if (是否只读)
        //    {
        //        input.添加属性("readonly", "readonly");
        //    }
        //    添加控件名属性(input);
        //    按需添加禁用属性(input);

        //    var label = new 基本元素("label")
        //        .添加子元素(new 文本(标签显示内容))
        //        .添加Css类("control-label")
        //        .添加属性("for", Id);
        //    添加左栏占据栅格Css类(label);
        //    按需添加左偏移栅格Css类(label);
        //    添加子元素(label);

        //    var div = new 基本元素("div");
        //    添加右栏占据栅格Css类(div);
        //    if (首注.IsNullOrEmpty() && 尾注.IsNullOrEmpty())
        //    {
        //        div.添加子元素(input);
        //    }
        //    else
        //    {
        //        var roundgroup = new 基本元素("div").添加Css类("input-group");
        //        if (是否为大尺寸样式)
        //        {
        //            roundgroup.添加Css类("input-group-lg");
        //        }
        //        if (!首注.IsNullOrEmpty())
        //        {
        //            roundgroup.添加子元素(
        //                new 基本元素("span")
        //                .添加Css类("input-group-addon")
        //                .添加子元素(new 文本(首注))
        //                );
        //        }
        //        roundgroup.添加子元素(input);
        //        if (!尾注.IsNullOrEmpty())
        //        {
        //            roundgroup.添加子元素(
        //                new 基本元素("span")
        //                .添加Css类("input-group-addon")
        //                .添加子元素(new 文本(尾注))
        //                );
        //        }
        //        div.添加子元素(roundgroup);
        //    }
        //    按需添加附注元素到子元素列表(div);
        //    添加子元素(div);

        //    return base.生成标签构造器();
        //}
    }
}
