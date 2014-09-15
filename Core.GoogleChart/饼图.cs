using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    [Serializable]
    public class 饼图 : 图表, 饼图和指数标签支持接口, 线性渐变填充支持接口, 线性条纹填充支持接口, 图表标题支持接口
    {
        public override string 类型代码
        {
            get { return "p"; }
        }
    }

    [Serializable]
    public class 三维饼图 : 饼图
    {
        public override string 类型代码
        {
            get
            {
                return "p3";
            }
        }
    }
}
