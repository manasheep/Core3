using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    public interface 多轴标签支持接口 : 参数支持接口 { }

    [Serializable]
    public class 多轴标签 : 参数
    {
        public 多轴标签()
            : base()
        {
            _轴标签列表 = new List<轴标签>();
        }

        public override string 唯一参数标识
        {
            get { return "多轴标签"; }
        }

        public override string 参数类型代码
        {
            get { return null; }
        }

        public List<轴标签> 轴标签列表
        {
            get
            {
                return _轴标签列表;
            }
        }
        private List<轴标签> _轴标签列表;

        protected override string 生成参数值代码()
        {
            return null;
        }

        public override string 生成代码()
        {
            var 类型 = new StringBuilder();
            var 范围 = new StringBuilder();
            var 标签 = new StringBuilder();
            var 样式 = new StringBuilder();
            var 位置 = new StringBuilder();
            var x = 0;
            foreach (var f in 轴标签列表)
            {
                if (类型.Length > 0) 类型.Append(',');
                类型.Append(f.生成轴类型代码());
                var t = f.生成轴标签代码();
                if (t.Length > 0)
                {
                    if (标签.Length > 0) 标签.Append('|');
                    标签.Append(x);
                    标签.Append(':');
                    标签.Append(t);
                }
                var p = f.生成轴标签位置代码();
                if (p.Length > 0)
                {
                    if (位置.Length > 0) 位置.Append('|');
                    位置.Append(x);
                    位置.Append(p);
                }
                if (f.轴范围 != null)
                {
                    if (范围.Length > 0) 范围.Append('|');
                    范围.Append(x);
                    范围.Append(',');
                    范围.Append(f.轴范围.生成代码());
                }
                if (f.轴样式 != null)
                {
                    if (样式.Length > 0) 样式.Append('|');
                    样式.Append(x);
                    样式.Append(f.轴样式.生成代码());
                }
                x++;
            }
            if (类型.Length > 0) 类型.Insert(0, "chxt=");
            if (标签.Length > 0) 标签.Insert(0, "&chxl=");
            if (位置.Length > 0) 位置.Insert(0, "&chxp=");
            if (范围.Length > 0) 范围.Insert(0, "&chxr=");
            if (样式.Length > 0) 样式.Insert(0, "&chxs=");
            return 类型.Append(范围).Append(位置).Append(标签).Append(样式).ToString();
        }
    }

    public enum 轴类型
    {
        顶部, 底部, 左侧, 右侧
    }

    [Serializable]
    public class 轴标签
    {
        public 轴标签()
        {
            _轴标签列表 = new List<string>();
            _轴标签位置列表 = new List<double>();
        }

        public 轴类型 类型
        {
            get
            {
                return _类型;
            }
            set
            {
                _类型 = value;
            }
        }
        private 轴类型 _类型;

        public char 生成轴类型代码()
        {
            switch (类型)
            {
                case 轴类型.底部: return 'x';
                case 轴类型.左侧: return 'y';
                case 轴类型.右侧: return 'r';
                default: return 't';
            }
        }

        public List<string> 轴标签列表
        {
            get
            {
                return _轴标签列表;
            }
        }
        private List<string> _轴标签列表;

        public StringBuilder 生成轴标签代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 轴标签列表)
            {
                s.Append('|');
                s.Append(f);
            }
            return s;
        }

        public List<double> 轴标签位置列表
        {
            get
            {
                return _轴标签位置列表;
            }
        }
        private List<double> _轴标签位置列表;

        public StringBuilder 生成轴标签位置代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 轴标签位置列表)
            {
                s.Append(',');
                s.Append(f);
            }
            return s;
        }

        /// <summary>
        /// 当起始值大于终止值时，轴标签将逆向显示
        /// </summary>
        public 轴范围 轴范围
        {
            get
            {
                return _轴范围;
            }
            set
            {
                _轴范围 = value;
            }
        }
        private 轴范围 _轴范围;

        public 轴样式 轴样式
        {
            get
            {
                return _轴样式;
            }
            set
            {
                _轴样式 = value;
            }
        }
        private 轴样式 _轴样式;
    }

    public enum 轴对齐方式
    {
        左 = -1,
        中 = 0,
        右 = 1
    }

    [Serializable]
    public class 轴范围
    {
        public int 起始值
        {
            get
            {
                return _起始值;
            }
            set
            {
                _起始值 = value;
            }
        }
        private int _起始值;

        public int 终止值
        {
            get
            {
                return _终止值;
            }
            set
            {
                _终止值 = value;
            }
        }
        private int _终止值;

        public string 生成代码()
        {
            return 起始值 + "," + 终止值;
        }
    }

    [Serializable]
    public class 轴样式
    {
        public 轴样式()
        {
            字体尺寸 = 12;
        }

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

        public int 字体尺寸
        {
            get
            {
                return _字体尺寸;
            }
            set
            {
                _字体尺寸 = value;
            }
        }
        private int _字体尺寸;

        public 轴对齐方式? 对齐方式
        {
            get
            {
                return _对齐方式;
            }
            set
            {
                _对齐方式 = value;
            }
        }
        private 轴对齐方式? _对齐方式;

        public StringBuilder 生成代码()
        {
            StringBuilder s = new StringBuilder();
            s.Append(',');
            s.Append(颜色.生成颜色代码());
            s.Append(',');
            s.Append(字体尺寸);
            if (对齐方式 != null)
            {
                s.Append(',');
                s.Append((int)对齐方式.Value);
            }
            return s;
        }
    }
}
