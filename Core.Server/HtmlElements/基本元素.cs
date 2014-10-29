using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Core.HtmlElements
{
    public class 基本元素
    {
        protected string 标签名称 { get; set; }

        public 基本元素(string 标签名称)
        {
            this.标签名称 = 标签名称;
        }

        private List<string> Css类列表 { get; set; }
        public string Id { get; set; }
        private Dictionary<string, string> 属性字典 { get; set; }
        private List<基本元素> 子元素列表 { get; set; }

        public 基本元素 添加Css类(string 类名)
        {
            if (Css类列表 == null) Css类列表 = new List<string>();
            if (!Css类列表.Contains(类名)) Css类列表.Add(类名);
            return this;
        }

        public 基本元素 添加属性(string 属性名, string 属性值)
        {
            if (添加属性目标.属性字典 == null) 添加属性目标.属性字典 = new Dictionary<string, string>();
            if (添加属性目标.属性字典.ContainsKey(属性名))
            {
                添加属性目标.属性字典[属性名] = 属性值;
            }
            else {
                添加属性目标.属性字典.Add(属性名, 属性值);
            }
            return this;
        }

        public 基本元素 添加子元素(基本元素 子元素)
        {
            if (子元素列表 == null) 子元素列表 = new List<基本元素>();
            if (!子元素列表.Contains(子元素)) 子元素列表.Add(子元素);
            return this;
        }

        public 基本元素 清空子元素()
        {
            if (子元素列表 != null) 子元素列表.Clear();
            return this;
        }

        protected bool 是否已进行过生成 { get; set; }

        public virtual 基本元素 添加属性目标 { get { return this; } }

        public virtual TagBuilder 生成标签构造器()
        {
            if (是否已进行过生成) throw new Exception("此对象无法执行多次生成代码操作。");
            var tb = new TagBuilder(标签名称);
            if (!Id.IsNullOrEmpty()) tb.GenerateId(Id);
            if (Css类列表 != null)
            {
                foreach (var f in Css类列表)
                {
                    tb.AddCssClass(f);
                }
            }
            if (属性字典 != null)
            {
                foreach (var f in 属性字典)
                {
                    tb.Attributes.Add(f.Key, f.Value);
                }
            }
            if (子元素列表 != null)
            {
                foreach (var f in 子元素列表)
                {
                    tb.InnerHtml += f.生成代码();
                }
            }
            是否已进行过生成 = true;
            return tb;
        }
        public virtual string 生成代码()
        {
            return 生成标签构造器().ToString(TagRenderMode.Normal);
        }
    }
}
