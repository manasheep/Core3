using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public interface 图表尺寸支持接口 : 参数支持接口
    { 
    
    }

    [Serializable]
    public class 图表尺寸 : 参数
    {
        public override string 唯一参数标识
        {
            get { return "图表尺寸"; }
        }

        public override string 参数类型代码
        {
            get { return "chs"; }
        }

        public short 宽度
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
        private short _宽度;

        public short 高度
        {
            get
            {
                return _高度;
            }
            set
            {
                _高度 = value;
            }
        }
        private short _高度;

        protected override string 生成参数值代码()
        {
            return 宽度 + "x" + 高度;
        }
    }
}
