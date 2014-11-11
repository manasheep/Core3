using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{
    [Designer(typeof(二值化滤镜参数输入))]
    public sealed class 构建二值化滤镜 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Int32> 阈值 { get; set; }

        public InOutArgument<FiltersSequence> 添加到目标滤镜序列 { get; set; }

        public OutArgument<Threshold> 输出目标 { get; set; }


        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var f = new Threshold(context.GetValue(阈值));
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
