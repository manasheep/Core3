using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{
    public abstract class 构建滤镜基类<T> : CodeActivity where T : IFilter
    {
        public InOutArgument<FiltersSequence> 添加到目标滤镜序列 { get; set; }
        public OutArgument<T> 输出目标 { get; set; }

        public void 输出滤镜(CodeActivityContext context, T 滤镜)
        {
            context.SetValue(输出目标, 滤镜);
            var fs = context.GetValue(添加到目标滤镜序列);
            if (fs != null)
            {
                fs.Add(滤镜);
                //context.SetValue(添加到目标滤镜序列, fs);
            }
        }
    }
}
