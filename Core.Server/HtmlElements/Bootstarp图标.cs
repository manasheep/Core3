using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core.HtmlElements
{
   public class Bootstarp图标:基本元素
    {
       public Bootstarp图标(Bootstarp图标样式类型 样式类型) : base("span")
       {
           this.样式类型 = 样式类型;
           添加Css类("glyphicon");
       }

       public Bootstarp图标样式类型 样式类型 { get; set; }

       public override System.Web.Mvc.TagBuilder 生成标签构造器()
       {
           添加Css类(GetEnumDescription(样式类型));
           return base.生成标签构造器();
       }

       static string GetEnumDescription(Bootstarp图标样式类型 value)
       {
           Type enumType = typeof(Bootstarp图标样式类型);
           var name = Enum.GetName(enumType, Convert.ToInt32(value));
           if (name == null)
               return string.Empty;
           object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
           if (objs == null || objs.Length == 0)
           {
               return string.Empty;
           }
           else
           {
               DescriptionAttribute attr = objs[0] as DescriptionAttribute;
               return attr.Description;
           }
       }
    }
}
