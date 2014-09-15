using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Drawing;

namespace Core.GoogleChart
{
    [Serializable]
    public abstract class 图表 : 图表颜色支持接口, 图表尺寸支持接口, 图表数据支持接口, 实体填充支持接口
    {
        public 图表()
        {
            _参数列表 = new List<参数>();
        }

        public abstract string 类型代码 { get; }

        public virtual string 生成代码()
        {
            return HttpUtility.UrlPathEncode(string.Format("http://chart.apis.google.com/chart?cht={0}&{1}", 类型代码, 生成参数列表代码()));
        }

        protected virtual StringBuilder 生成参数列表代码()
        {
            if (this["图表尺寸"] == null) throw new Exception("图表必须包含图表尺寸参数");
            if (this["图表数据"] == null) throw new Exception("图表必须包含图表数据参数");
            StringBuilder s = new StringBuilder();
            foreach (var f in 参数列表)
            {
                if (f is 背景填充) continue;
                if (f.依赖参数标识列表.Count > 0)
                {
                    foreach (var ts in f.依赖参数标识列表)
                    {
                        if (参数列表.Count(d => d.唯一参数标识 == ts) == 0) throw new Exception(string.Format("{0}参数依赖于{1}参数，但参数表中没有该依赖参数", f.参数类型代码, ts));
                    }
                }
                if (s.Length > 0) s.Append('&');
                s.Append(f.生成代码());
            }
            var t = 背景填充.生成背景填充代码(参数列表);
            if (t != null)
            {
                if (s.Length > 0) s.Append('&');
                s.Append(t);
            }
            return s;
        }

        public 参数 this[string 参数标识]
        {
            get
            {
                return 参数列表.FirstOrDefault(f => f.唯一参数标识 == 参数标识);
            }
            set
            {
                var s = 参数列表.FirstOrDefault(f => f.唯一参数标识 == 参数标识);
                if (s == null)
                {
                    参数列表.Add(value);
                }
                else
                {
                    参数列表[参数列表.IndexOf(s)] = value;
                }
            }
        }

        #region 参数接口 成员

        public List<参数> 参数列表
        {
            get
            {
                return _参数列表;
            }
        }
        private List<参数> _参数列表;

        #endregion
    }
}
