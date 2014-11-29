using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(获取截图参数输入))]
    public sealed class 获取截图 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Int32> 起始X坐标 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 起始Y坐标 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 截取宽度 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 截取高度 { get; set; }
        [RequiredArgument]
        public InArgument<PixelFormat> 像素格式 { get; set; }

        [RequiredArgument]
        public OutArgument<Bitmap> 输出图像 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            输出图像.Set(
                context,
                Client处理函数.获取截图(
                起始X坐标.Get(context), 
                起始Y坐标.Get(context), 
                截取宽度.Get(context), 
                截取高度.Get(context),
                像素格式.Get(context)
                    ));
        }
    }
}
