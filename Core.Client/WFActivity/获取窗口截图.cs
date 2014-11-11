using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(窗口句柄输入))]
    public sealed class 获取窗口截图 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IntPtr> 窗口句柄 { get; set; }

        [RequiredArgument]
        public OutArgument<Bitmap> 输出图像 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(输出图像,Client处理函数.获取窗口截图(context.GetValue(窗口句柄)));
        }
    }
}
