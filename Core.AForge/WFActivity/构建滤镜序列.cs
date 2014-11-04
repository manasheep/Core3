using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{

    public sealed class 构建滤镜序列 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IFilter[]> 滤镜数组 { get; set; }

        [RequiredArgument]
        public OutArgument<FiltersSequence> 输出目标 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(输出目标, new FiltersSequence(context.GetValue(滤镜数组)));
        }
    }
}
