using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{

    public sealed class 应用滤镜到图像 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Bitmap> 处理目标 { get; set; }
        [RequiredArgument]
        public InArgument<IFilter> 应用滤镜 { get; set; }
        [RequiredArgument]
        public OutArgument<Bitmap> 输出目标 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(输出目标, context.GetValue(应用滤镜).Apply(context.GetValue(处理目标)));
        }
    }
}
