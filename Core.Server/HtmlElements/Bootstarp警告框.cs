using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
   public class Bootstarp警告框:基本元素
    {
       public Bootstarp警告框(Bootstarp警告框样式类型 样式类型,string HTML内容) : base("div")
       {
           this.HTML内容 = HTML内容;
           this.样式类型 = 样式类型;
           添加Css类("alert");
           添加属性("role", "alert");
       }

       public Bootstarp警告框样式类型 样式类型 { get; set; }
       public bool 是否可被关闭 { get; set; }
       public string HTML内容 { get; set; }

       public override System.Web.Mvc.TagBuilder 生成标签构造器()
       {
           添加Css类("alert-" + 样式类型);
           if (是否可被关闭)
           {
               添加Css类("alert-dismissible");
               添加子元素(
                   new HTML代码(
                       @"<button type=""button"" class=""close"" data-dismiss=""alert""><span aria-hidden=""true"">&times;</span><span class=""sr-only"">关闭</span></button>"));
           }
           添加子元素(new HTML代码(HTML内容));
           return base.生成标签构造器();
       }
    }
}
