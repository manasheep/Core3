using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public interface 图表数据支持接口 : 参数支持接口
    { 
    
    }

    [Serializable]
    public abstract class 图表数据 : 参数
    {
        public 图表数据()
        {
            _数据列表 = new List<List<double>>();
        }

        public List<List<double>> 数据列表
        {
            get
            {
                return _数据列表;
            }
        }
        private List<List<double>> _数据列表;

        protected abstract string 类型代码 { get; }

        public override string 唯一参数标识
        {
            get { return "图表数据"; }
        }

        public override string 参数类型代码
        {
            get { return "chd"; }
        }

        protected override string 生成参数值代码()
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class 文本编码数据 : 图表数据
    {
        public 文本编码数据()
            : base()
        {

        }

        protected override string 类型代码
        {
            get { return "t"; }
        }

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 数据列表)
            {
                if (s.Length > 0) s.Append('|');
                for (int i = 0; i < f.Count; i++)
                {
                    if (i > 0) s.Append(',');
                    s.Append(f[i]);
                }
            }
            return string.Format("{0}:{1}", 类型代码, s);
        }
    }

    [Serializable]
    public class 带有数据换算的文本编码数据 : 文本编码数据
    {
        public 带有数据换算的文本编码数据()
            : base()
        {
            _换算标准 = new List<范围限定>();
        }

        public List<范围限定> 换算标准
        {
            get
            {
                return _换算标准;
            }
        }
        private List<范围限定> _换算标准;

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 换算标准)
            {
                if (s.Length > 0) s.Append('|');
                s.Append(f.生成代码());
            }
            return base.生成参数值代码() + "&chds=" + s;
        }
    }

    [Serializable]
    public class 范围限定
    {
        public double 最小值
        {
            get
            {
                return _最小值;
            }
            set
            {
                _最小值 = value;
            }
        }
        private double _最小值;

        public double 最大值
        {
            get
            {
                return _最大值;
            }
            set
            {
                _最大值 = value;
            }
        }
        private double _最大值;

        public string 生成代码()
        {
            return 最小值 + "," + 最大值;
        }
    }
}
