using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    public interface 图表颜色支持接口 : 参数支持接口
    {

    }

    [Serializable]
    public class 图表颜色 : 参数
    {
        public 图表颜色()
            : base()
        {
            _颜色列表 = new List<Color>();
        }

        public bool 分别应用到单个数据组中的各个数值
        {
            get
            {
                return _分别应用到单个数据组中的各个数值;
            }
            set
            {
                _分别应用到单个数据组中的各个数值 = value;
            }
        }
        private bool _分别应用到单个数据组中的各个数值;

        public List<Color> 颜色列表
        {
            get
            {
                return _颜色列表;
            }
        }
        private List<Color> _颜色列表;

        public override string 唯一参数标识
        {
            get { return "图表颜色"; }
        }

        public override string 参数类型代码
        {
            get { return "chco"; }
        }

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 颜色列表)
            {
                if (s.Length > 0) s.Append(分别应用到单个数据组中的各个数值 ? '|' : ',');
                s.Append(f.生成带透明度值的颜色代码());
            }
            return s.ToString();
        }
    }
}
