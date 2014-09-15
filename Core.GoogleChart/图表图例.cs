using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public enum 图表图例位置
    { 
        上,下,左,右
    }

    public interface 图表图例支持接口 : 参数支持接口 { }

    [Serializable]
    public class 图表图例 : 参数
    {
        public 图表图例()
            : base()
        {
            this.依赖参数标识列表.Add("图表颜色");
            _图例名称列表 = new List<string>();
        }

        public override string 唯一参数标识
        {
            get { return "图表图例"; }
        }

        public override string 参数类型代码
        {
            get { return "chdl"; }
        }

        public List<string> 图例名称列表
        {
            get
            {
                return _图例名称列表;
            }
        }
        private List<string> _图例名称列表;

        public 图表图例位置? 位置
        {
            get
            {
                return _位置;
            }
            set
            {
                _位置 = value;
            }
        }
        private 图表图例位置? _位置;

        protected override string 生成参数值代码()
        {
            var s = new StringBuilder();
            foreach (var f in 图例名称列表)
            {
                if (s.Length > 0) s.Append('|');
                s.Append(f);
            }
            if (位置 != null)
            {
                s.Append("&chdlp=");
                switch (位置.Value)
                {
                    case 图表图例位置.上: s.Append('t');
                        break;
                    case 图表图例位置.下: s.Append('b');
                        break;
                    case 图表图例位置.左: s.Append('l');
                        break;
                    case 图表图例位置.右: s.Append('r');
                        break;
                    default:
                        break;
                }
            }
            return s.ToString();
        }
    }
}
