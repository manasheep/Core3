using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging.Filters;

namespace Core.AForge.WFActivity
{

    public sealed class 构建转换为灰度图像滤镜 : 构建滤镜基类<Grayscale>
    {
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var f = Grayscale.CommonAlgorithms.BT709;
            输出滤镜(context, f);
        }
    }
}
