using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    [Serializable]
    public class Sparkline图 : 图表, 线性渐变填充支持接口, 线性条纹填充支持接口, 图表标题支持接口, 图表图例支持接口, 多轴标签支持接口, 区域填充支持接口
    {
        public override string 类型代码
        {
            get { return "ls"; }
        }
    }
}
