﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public abstract class Bootstarp表单控件元素组 : 基本元素
    {
        private static readonly Bootstarp表单控件元素组布局参数 默认布局参数 = new Bootstarp表单控件元素组布局参数()
              {
                  总占据栅格数 = 12,
                  左栏占据栅格数 = 2
              };

        public Bootstarp表单控件元素组():base("div")
        {
            添加Css类("form-group");
            栅格布局最低兼容屏幕类型 = Bootstarp布局目标屏幕类型.sm;
            _Id = "B" + Guid.NewGuid().ToString("N");
        }

        public Bootstarp表单控件元素组(string 控件名)
            : this()
        {
            this.控件名 = 控件名;
        }

        public virtual Bootstarp表单控件元素组 应用模型属性元数据(ModelMetadata 模型属性元数据)
        {
            控件名 = 模型属性元数据.PropertyName;
            附注 = 模型属性元数据.Description;
            标签显示内容 = 模型属性元数据.DisplayName;
            return this;
        }

        public virtual Bootstarp表单控件元素组 设置布局参数(Bootstarp表单控件元素组布局参数 布局参数)
        {
            this.布局参数 = 布局参数;
            return this;
        }

        protected Bootstarp表单控件元素组布局参数 布局参数
        {
            get
            {
                if (_布局参数 == null)
                {
                    return 默认布局参数;
                }
                else return _布局参数;
            }
            set { _布局参数 = value; }
        }
        private Bootstarp表单控件元素组布局参数 _布局参数;

        private string _Id;

        public virtual string Id
        {
            get { return _Id; }
        }


        public string 控件名 { get; set; }
        public int 左栏占据栅格数 { get { return 布局参数.左栏占据栅格数; } set { 布局参数.左栏占据栅格数 = value; } }
        public int 总占据栅格数 { get { return 布局参数.总占据栅格数; } set { 布局参数.总占据栅格数 = value; } }
        public int 左偏移栅格数 { get { return 布局参数.左偏移栅格数; } set { 布局参数.左偏移栅格数 = value; } }
        public bool 是否禁用 { get; set; }
        public bool 是否为大尺寸样式 { get { return 布局参数.是否为大尺寸样式; } set { 布局参数.是否为大尺寸样式 = value; } }
        public string 标签显示内容 { get; set; }
        public string 附注 { get; set; }
        public Bootstarp布局目标屏幕类型 栅格布局最低兼容屏幕类型 { get; set; }


        public void 添加控件名属性(基本元素 元素)
        {
            元素.添加属性("name", 控件名);
        }

        public void 按需添加禁用属性(基本元素 元素)
        {
            if (是否禁用)
            {
                元素.添加属性("disabled", "disabled");
            }
        }


        public string 左偏移栅格Css类名
        {
            get { return "col-" + 栅格布局最低兼容屏幕类型 + "-offset-" + 左偏移栅格数; }
        }

        public void 按需添加左偏移栅格Css类(基本元素 元素)
        {
            if (左偏移栅格数 != 0)
            {
                元素.添加Css类(左偏移栅格Css类名);
            }
        }

        public string 左栏占据栅格Css类名
        {
            get { return "col-" + 栅格布局最低兼容屏幕类型 + "-" + 左栏占据栅格数; }
        }

        public void 添加左栏占据栅格Css类(基本元素 元素)
        {
            元素.添加Css类(左栏占据栅格Css类名);
        }

        public string 右栏占据栅格Css类名
        {
            get { return "col-" + 栅格布局最低兼容屏幕类型 + "-" + (总占据栅格数 - 左栏占据栅格数); }
        }

        public void 添加右栏占据栅格Css类(基本元素 元素)
        {
            元素.添加Css类(右栏占据栅格Css类名);
        }

        public void 按需添加附注元素到子元素列表(基本元素 元素)
        {
            if (附注.IsNullOrEmpty()) return;
            元素.添加子元素(new 基本元素("span")
                .添加Css类("help-block")
                .添加子元素(new 文本(附注)));
        }

        public override System.Web.Mvc.TagBuilder 生成标签构造器()
        {
            if (是否为大尺寸样式)
            {
                添加Css类("form-group-lg");
            }

            return base.生成标签构造器();
        }
    }
}
