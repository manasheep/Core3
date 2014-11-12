using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{

    public sealed class 构建滤镜序列 : 构建滤镜基类<FiltersSequence>
    {
        public InArgument<IFilter[]> 滤镜数组 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var args = context.GetValue(滤镜数组);
            var f = args == null ? new FiltersSequence() : new FiltersSequence(args);
            输出滤镜(context, f);
        }
    }
}
