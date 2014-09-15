using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public enum 条形图类型
    { 
        横向分布,横向累加,纵向分布,纵向累加
    }

    [Serializable]
    public class 条形图 : 图表, 线性渐变填充支持接口, 线性条纹填充支持接口, 图表标题支持接口, 图表图例支持接口, 多轴标签支持接口, 区域填充支持接口
    {
        public override string 类型代码
        {
            get
            {
                switch (类型)
                {
                    case 条形图类型.横向累加: return "bhs";
                    case 条形图类型.纵向分布: return "bvg"; 
                    case 条形图类型.纵向累加: return "bvs";
                    default: return "bhg";
                }
            }
        }

        public 条形图类型 类型
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
        private 条形图类型 _类型;
    }
}
