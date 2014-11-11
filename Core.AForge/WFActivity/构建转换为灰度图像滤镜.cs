using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{

    public sealed class 构建转换为灰度图像滤镜 : CodeActivity
    {
        public OutArgument<Grayscale> 输出目标 { get; set; }

        public InOutArgument<FiltersSequence> 添加到目标滤镜序列 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var f = Grayscale.CommonAlgorithms.BT709;
            context.SetValue(输出目标,f);
            var fs = context.GetValue(添加到目标滤镜序列);
            if (fs != null)
            {
                fs.Add(f);
                context.SetValue(添加到目标滤镜序列, fs);
            }
        }
    }
}
