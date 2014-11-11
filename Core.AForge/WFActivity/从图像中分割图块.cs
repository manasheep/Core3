using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Activities;
using AForge.Imaging;

namespace Core.AForge.WFActivity
{
    [Designer(typeof(从图像中分割图块参数输入))]
    public sealed class 从图像中分割图块 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Bitmap> 处理目标 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 最小宽度 { get; set; }
        [RequiredArgument]
        public InArgument<Int32> 最小高度 { get; set; }

        [RequiredArgument]
        public OutArgument<Blob[]> 输出目标 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            // create an instance of blob counter algorithm
            BlobCounterBase bc = new BlobCounter();
            // set filtering options
            bc.FilterBlobs = true;
            bc.MinWidth = context.GetValue(最小宽度);
            bc.MinHeight = context.GetValue(最小高度);
            // set ordering options
            bc.ObjectsOrder = ObjectsOrder.Size;
            // process binary image
            bc.ProcessImage(context.GetValue(处理目标));
            var blobs = bc.GetObjectsInformation();
            context.SetValue(输出目标, blobs);
        }
    }
}
