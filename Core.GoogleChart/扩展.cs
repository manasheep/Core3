using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    public static class 辅助扩展
    {
        public static StringBuilder 生成颜色代码(this Color 颜色)
        {
            var s = new StringBuilder();
            s.Append(颜色.R.ToString("X2"));
            s.Append(颜色.G.ToString("X2"));
            s.Append(颜色.B.ToString("X2"));
            return s;
        }

        public static StringBuilder 生成带透明度值的颜色代码(this Color 颜色)
        {
            var s = 生成颜色代码(颜色);
            s.Append(颜色.A.ToString("X2"));
            return s;
        }
    }

    public static class 接口扩展
    {
        private static void _添加参数(参数支持接口 o, 参数 参数)
        {
            if (参数.唯一参数标识 != null)
            {
                if (o.参数列表.Count(f => f.唯一参数标识 == 参数.唯一参数标识) > 0) throw new Exception(string.Format("只允许使用一个{0}类型参数", 参数.唯一参数标识));
            }
            o.参数列表.Add(参数);
        }

        public static void 添加参数(this 图表颜色支持接口 o, 图表颜色 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 图表尺寸支持接口 o, 图表尺寸 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 图表数据支持接口 o, 图表数据 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 饼图和指数标签支持接口 o, 饼图和指数标签 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 实体填充支持接口 o, 实体填充 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 线性渐变填充支持接口 o, 线性渐变填充 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 线性条纹填充支持接口 o, 线性条纹填充 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 图表标题支持接口 o, 图表标题 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 图表图例支持接口 o, 图表图例 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 多轴标签支持接口 o, 多轴标签 参数)
        {
            _添加参数(o, 参数);
        }

        public static void 添加参数(this 区域填充支持接口 o, 区域填充 参数)
        {
            _添加参数(o, 参数);
        }
    }
}
