using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public class Bootstarp表单提交按钮 : Bootstarp表单控件元素组
    {
        public Bootstarp表单提交按钮()
            : base(String.Empty)
        {
            按钮 = new Bootstarp按钮("提交");
            按钮.样式类型 = Bootstarp按钮样式类型.warning;
            按钮.图标样式类型 = Bootstarp图标样式类型.glyphicon_send;
        }

        private Bootstarp按钮 按钮 { get; set; }

        public new bool 是否为大尺寸样式
        {
            get { return 按钮.是否为大尺寸样式; }
            set { 按钮.是否为大尺寸样式 = value; }
        }
        public Bootstarp按钮样式类型? 样式类型
        {
            get { return 按钮.样式类型; }
            set { 按钮.样式类型 = value; }
        }
        public Bootstarp图标样式类型? 图标样式类型
        {
            get { return 按钮.图标样式类型; }
            set { 按钮.图标样式类型 = value; }
        }
        public new string 标签显示内容
        {
            get
            {
                return 按钮.标签显示内容;
            }
            set
            {
                按钮.标签显示内容 = value;
            }
        }

        public override TagBuilder 生成标签构造器()
        {
            按钮.添加属性("type", "submit");
            按需添加禁用属性(按钮);

            var leftdiv = new 基本元素("div");
            按需添加左偏移栅格Css类(leftdiv);
            添加左栏占据栅格Css类(leftdiv);
            添加子元素(leftdiv);

            var rounddiv = new 基本元素("div");
            添加右栏占据栅格Css类(rounddiv);
            rounddiv.添加子元素(按钮);
            按需添加附注元素到子元素列表(rounddiv);

            添加子元素(rounddiv);

            return base.生成标签构造器();
        }
    }
}
