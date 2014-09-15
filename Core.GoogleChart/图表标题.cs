using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    public interface 图表标题支持接口 : 参数支持接口 { }

    [Serializable]
    public class 图表标题 : 参数
    {
        public override string 唯一参数标识
        {
            get { return "图表标题"; }
        }

        public override string 参数类型代码
        {
            get { return "chtt"; }
        }

        /// <summary>
        /// 使用加号 (+) 指定一个空格。使用管道符 (|) 进行强制换行。
        /// </summary>
        public string 标题
        {
            get
            {
                return _标题;
            }
            set
            {
                _标题 = value;
            }
        }
        private string _标题;

        public 标题样式 样式
        {
            get
            {
                return _样式;
            }
            set
            {
                _样式 = value;
            }
        }
        private 标题样式 _样式;

        protected override string 生成参数值代码()
        {
            return 标题 + (样式 == null ? null : "&"+样式.生成代码());
        }
    }

    [Serializable]
    public class 标题样式
    {
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

        public string 生成代码()
        {
            return "chts=" + 颜色.生成颜色代码() + "," + 字体尺寸;
        }
    }
}
