using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    [Serializable]
    public abstract class 背景填充 : 参数
    {
        public 背景填充()
            : base()
        {

        }

        public override string 唯一参数标识
        {
            get { return null; }
        }

        public override string 参数类型代码
        {
            get { return "chf"; }
        }

        protected override string 生成参数值代码()
        {
            throw new NotImplementedException();
        }

        public override string 生成代码()
        {
            return 生成参数值代码();
        }

        public static string 生成背景填充代码(List<参数> 参数表)
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 参数表)
            {
                if (f is 背景填充)
                {
                    if (s.Length > 0) s.Append('|');
                    s.Append(f.生成代码());
                }
            }
            return s.Length > 0 ? "chf=" + s : null;
        }
    }

    public enum 实体填充类型
    {
        整体背景填充,
        图表区域背景填充,
        整体透明度更改
    }

    public interface 实体填充支持接口 : 参数支持接口 { }

    [Serializable]
    public class 实体填充 : 背景填充
    {
        public 实体填充()
            : base()
        {

        }

        public 实体填充类型 填充类型
        {
            get
            {
                return _填充类型;
            }
            set
            {
                _填充类型 = value;
            }
        }
        private 实体填充类型 _填充类型;

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

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            switch (填充类型)
            {
                case 实体填充类型.整体背景填充:
                    s.Append("bg");
                    break;
                case 实体填充类型.图表区域背景填充:
                    s.Append('c');
                    break;
                case 实体填充类型.整体透明度更改:
                    s.Append('a');
                    break;
                default:
                    break;
            }
            s.Append(",s,");
            s.Append(颜色.生成带透明度值的颜色代码());
            return s.ToString();
        }
    }

    public enum 线性渐变填充类型
    {
        整体背景填充,
        图表区域背景填充
    }

    public interface 线性渐变填充支持接口 : 参数支持接口 { }

    [Serializable]
    public class 线性渐变填充 : 背景填充
    {
        public 线性渐变填充()
        {
            _色标列表 = new List<色标>();
        }

        /// <summary>
        /// 角度值介于 0（水平）和 90（垂直）之间。
        /// </summary>
        public byte 角度
        {
            get
            {
                return _角度;
            }
            set
            {
                _角度 = value;
            }
        }
        private byte _角度;

        public 线性渐变填充类型 填充类型
        {
            get
            {
                return _填充类型;
            }
            set
            {
                _填充类型 = value;
            }
        }
        private 线性渐变填充类型 _填充类型;

        public List<色标> 色标列表
        {
            get
            {
                return _色标列表;
            }
        }
        private List<色标> _色标列表;

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            switch (填充类型)
            {
                case 线性渐变填充类型.整体背景填充: s.Append("bg");
                    break;
                case 线性渐变填充类型.图表区域背景填充: s.Append('c');
                    break;
                default:
                    break;
            }
            s.Append(",lg,");
            s.Append(角度);
            foreach (var f in 色标列表)
            {
                s.Append("," + f.颜色.生成带透明度值的颜色代码() + "," + f.位置);
            }
            return s.ToString();
        }
    }

    [Serializable]
    public class 色标
    {
        /// <summary>
        /// 指定颜色为纯色的点，其值应介于0到1之间，0 指定最左侧的图表位置而 1 指定最右侧的位置。
        /// </summary>
        public double 位置
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
        private double _位置;

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
    }

    public enum 线性条纹填充类型
    {
        整体背景填充,
        图表区域背景填充
    }

    public interface 线性条纹填充支持接口 : 参数支持接口 { }

    [Serializable]
    public class 线性条纹填充 : 背景填充
    {
        public 线性条纹填充()
        {
            _色条列表 = new List<色条>();
        }

        /// <summary>
        /// 角度值介于 0（水平）和 90（垂直）之间。
        /// </summary>
        public byte 角度
        {
            get
            {
                return _角度;
            }
            set
            {
                _角度 = value;
            }
        }
        private byte _角度;

        public 线性条纹填充类型 填充类型
        {
            get
            {
                return _填充类型;
            }
            set
            {
                _填充类型 = value;
            }
        }
        private 线性条纹填充类型 _填充类型;

        public List<色条> 色条列表
        {
            get
            {
                return _色条列表;
            }
        }
        private List<色条> _色条列表;

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            switch (填充类型)
            {
                case 线性条纹填充类型.整体背景填充: s.Append("bg");
                    break;
                case 线性条纹填充类型.图表区域背景填充: s.Append('c');
                    break;
                default:
                    break;
            }
            s.Append(",ls,");
            s.Append(角度);
            foreach (var f in 色条列表)
            {
                s.Append("," + f.颜色.生成带透明度值的颜色代码() + "," + f.宽度);
            }
            return s.ToString();
        }
    }

    [Serializable]
    public class 色条
    {
        /// <summary>
        /// 必须介于 0 和 1 之间，其中 1 是图表的全宽度。条纹重复出现，直到填满图表。
        /// </summary>
        public double 宽度
        {
            get
            {
                return _宽度;
            }
            set
            {
                _宽度 = value;
            }
        }
        private double _宽度;

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
    }
}
