using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{
    [Designer(typeof(HSL色彩保留滤镜参数输入))]
    public sealed class 构建HSL色彩保留滤镜 : 构建滤镜基类<HSLFiltering>
    {
        [RequiredArgument]
        public InArgument<Int32> 最低色相 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 最高色相 { get; set; }
        [RequiredArgument]
        public InArgument<Single> 最低饱和度 { get; set; }
        [RequiredArgument]
        public InArgument<Single> 最高饱和度 { get; set; }
        [RequiredArgument]
        public InArgument<Single> 最低亮度 { get; set; }
        [RequiredArgument]
        public InArgument<Single> 最高亮度 { get; set; }

        public InArgument<HSL> 填充颜色 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var f = new HSLFiltering(new IntRange(context.GetValue(最低色相), context.GetValue(最高色相)), new Range(context.GetValue(最低饱和度), context.GetValue(最高饱和度)), new Range(context.GetValue(最低亮度), context.GetValue(最高亮度)));
            var color = context.GetValue(填充颜色);
            if (color != null) f.FillColor = color;
            输出滤镜(context, f);
        }
    }
}
