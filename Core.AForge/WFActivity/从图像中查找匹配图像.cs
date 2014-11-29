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
    [Designer(typeof(从图像中查找匹配图像参数输入))]
    public sealed class 从图像中查找匹配图像 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Bitmap> 原图 { get; set; }
        [RequiredArgument]
        public InArgument<Bitmap> 匹配图 { get; set; }
        /// <summary>
        /// 1表示100%相似，越少容差度越高，默认为0.95f
        /// </summary>
        public InArgument<Nullable<Single>> 相似度下限 { get; set; }
        public InArgument<Int32> 查找区域左偏移量 { get; set; }
        public InArgument<Int32> 查找区域上偏移量 { get; set; }
        public InArgument<Nullable<Int32>> 查找区域宽度 { get; set; }
        public InArgument<Nullable<Int32>> 查找区域高度 { get; set; }

        public OutArgument<TemplateMatch[]> 匹配项 { get; set; }
        public OutArgument<TemplateMatch> 最佳匹配项 { get; set; }
        public OutArgument<Point> 最佳匹配项坐标位置 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var sourceimage = 原图.Get(context);
            var sim = 相似度下限.Get(context);
            var width = 查找区域宽度.Get(context);
            var height = 查找区域高度.Get(context);
            var etm = new ExhaustiveTemplateMatching(sim==null?0.9f:sim.Value);
            var match = etm.ProcessImage(sourceimage, context.GetValue(匹配图), new Rectangle(查找区域左偏移量.Get(context), 查找区域上偏移量.Get(context), width == null ? sourceimage.Width : width.Value, height == null ? sourceimage.Height : height.Value));
            context.SetValue(匹配项, match);
            if (match != null && match.Length > 0)
            {
                var bestmatch = match.OrderByDescending(q => q.Similarity).First();
                context.SetValue(最佳匹配项, bestmatch);
                var bestmatchpos = bestmatch.Rectangle.Location;
                context.SetValue(最佳匹配项坐标位置, bestmatchpos);
            }
        }
    }
}
