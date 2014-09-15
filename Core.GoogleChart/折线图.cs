using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public enum 折线图类型
    {
        /// <summary>
        /// 数据点沿 x 轴均匀分布。
        /// </summary>
        均布折线图,
        /// <summary>
        /// 为您要绘制的每条线提供一对数据集，则每对数据集的第一个数据集用于指定 x- 轴坐标，第二个数据集用于指定 y- 轴坐标。如果您传递奇数个数据集，则最后一个集会被忽略。
        /// </summary>
        自定义折线图
    }

    [Serializable]
    public class 折线图 : Sparkline图
    {
        public 折线图类型 类型
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
        private 折线图类型 _类型;

        public override string 类型代码
        {
            get
            {
                switch (类型)
                {
                    case 折线图类型.自定义折线图: return "lxy";
                    default: return "lc";
                }
            }
        }
    }
}
