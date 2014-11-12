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
    public sealed class 构建二值化滤镜 : 构建滤镜基类<Threshold>
    {
        [RequiredArgument]
        public InArgument<Int32> 阈值 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var f = new Threshold(context.GetValue(阈值));
            输出滤镜(context,f);
        }
    }
}
