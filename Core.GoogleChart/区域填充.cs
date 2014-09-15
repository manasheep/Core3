using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    public interface 区域填充支持接口 : 参数支持接口 { }

    [Serializable]
    public class 区域填充 : 参数
    {
        public 区域填充()
            : base()
        {
            _填充列表 = new List<区域填充接口>();
        }

        public override string 唯一参数标识
        {
            get { return "区域填充"; }
        }

        public override string 参数类型代码
        {
            get { return "chm"; }
        }

        public List<区域填充接口> 填充列表
        {
            get
            {
                return _填充列表;
            }
        }
        private List<区域填充接口> _填充列表;

        protected override string 生成参数值代码()
        {
            var s = new StringBuilder();
            foreach (var f in 填充列表)
            {
                if (s.Length > 0) s.Append('|');
                s.Append(f.生成代码());
            }
            return s.ToString();
        }
    }

    public interface 区域填充接口
    {
        string 生成代码();
        Color 颜色 { get; set; }
    }

    [Serializable]
    public class 数据实体填充 : 区域填充接口
    {
        public byte 目标数据组索引
        {
            get
            {
                return _目标数据组索引;
            }
            set
            {
                _目标数据组索引 = value;
            }
        }
        private byte _目标数据组索引;

        #region 区域填充接口 成员

        public Color 颜色
        {
            get
            {
                return _颜色;
            }
            set
            {
                _颜色 = value;
            }
        }
        private Color _颜色;

        public string 生成代码()
        {
            return string.Format("B,{0},{1},0,0", 颜色.生成带透明度值的颜色代码(), 目标数据组索引);
        }

        #endregion
    }

    [Serializable]
    public class 实体间隙填充 : 区域填充接口
    {
        public byte 起始数据组索引
        {
            get
            {
                return _起始数据组索引;
            }
            set
            {
                _起始数据组索引 = value;
            }
        }
        private byte _起始数据组索引;

        public byte 终止数据组索引
        {
            get
            {
                return _终止数据组索引;
            }
            set
            {
                _终止数据组索引 = value;
            }
        }
        private byte _终止数据组索引;

        #region 区域填充接口 成员

        public Color 颜色
        {
            get
            {
                return _颜色;
            }
            set
            {
                _颜色 = value;
            }
        }
        private Color _颜色;

        public string 生成代码()
        {
            return string.Format("B,{0},{1},{2},0", 颜色.生成带透明度值的颜色代码(), 起始数据组索引,终止数据组索引);
        }

        #endregion
    }
}
