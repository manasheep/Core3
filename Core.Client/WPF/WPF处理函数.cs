using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Drawing;

namespace Core.WPF
{
    public static class WPF处理函数
    {
        /// <summary>
        /// 当列表为多选列表时，将所有项的选择状态逆转。
        /// 使用前需禁用ListBox的动态加载，否则将引发异常：VirtualizingStackPanel.IsVirtualizing="False"
        /// </summary>
        public static void 反向选择(this ListBox l)
        {
            for (int i = 0; i < l.Items.Count; i++)
            {
                var f = l.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                f.IsSelected = !f.IsSelected;
            }
            //foreach (object o in l.Items)
            //{
            //    var f = l.ItemContainerGenerator.ContainerFromItem(o) as ListBoxItem;
            //    f.IsSelected = !f.IsSelected;
            //}
        }

        /// <summary>
        /// 将可视对象转换为32位带透明格式的图片源，转换非界面显示的元素时建议使用DrawingVisual绘制好内部后保存，转换界面显示的元素时注意其边距属性可能会导致其超出画布，而显示空图像。
        /// </summary>
        public static RenderTargetBitmap 转换为图像(this DrawingVisual v)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)v.ContentBounds.Width, (int)v.ContentBounds.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(v);
            return bmp;
        }

        /// <summary>
        /// 将可视对象转换为32位带透明格式的图片源，转换非界面显示的元素时建议使用DrawingVisual绘制好内部后保存，转换界面显示的元素时注意其边距属性可能会导致其超出画布，而显示空图像。
        /// </summary>
        public static RenderTargetBitmap 转换为图像(this Visual v, int 宽度, int 高度)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap(宽度, 高度, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(v);
            return bmp;
        }

        /// <summary>
        /// 将可视对象保存为无损的PNG格式图片，保存非界面显示的元素时建议使用DrawingVisual绘制好内部后保存，保存界面显示的元素时注意其边距属性可能会导致其超出画布，而显示空图像。
        /// </summary>
        public static void 保存为图像文件(this Visual v, int 宽度, int 高度, string 存储路径)
        {
            保存为图像文件(转换为图像(v, 宽度, 高度), 存储路径);
        }

        /// <summary>
        /// 将可视对象保存为JPG格式图片，保存非界面显示的元素时建议使用DrawingVisual绘制好内部后保存，保存界面显示的元素时注意其边距属性可能会导致其超出画布，而显示空图像。
        /// </summary>
        /// <param name="质量">JPGE格式质量。其值的范围是 1 （最低质量） 到 100 （最高质量） （含)</param>
        public static void 保存为JPG图像文件(this Visual v, int 宽度, int 高度, string 存储路径, int 质量)
        {
            保存为JPG图像文件(转换为图像(v, 宽度, 高度), 存储路径, 质量);
        }

        /// <summary>
        /// 将图像保存为无损的PNG格式图片。
        /// </summary>
        public static void 保存为图像文件(this BitmapSource b, string 存储路径)
        {
            System.IO.FileStream fs = new System.IO.FileStream(存储路径, System.IO.FileMode.Create);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(b));
            encoder.Save(fs);
            fs.Close();
        }

        /// <summary>
        /// 将图像保存为JPG格式图片。
        /// </summary>
        /// <param name="质量">JPGE格式质量。其值的范围是 1 （最低质量） 到 100 （最高质量） （含)</param>
        public static void 保存为JPG图像文件(this BitmapSource b, string 存储路径, int 质量)
        {
            System.IO.FileStream fs = new System.IO.FileStream(存储路径, System.IO.FileMode.Create);
            BitmapEncoder encoder = new JpegBitmapEncoder()
            {
                QualityLevel = 质量
            };
            encoder.Frames.Add(BitmapFrame.Create(b));
            encoder.Save(fs);
            fs.Close();
        }

        /// <summary>
        /// 读取图像
        /// </summary>
        /// <param name="路径">路径</param>
        /// <returns>图像</returns>
        public static BitmapImage 读取图像(string 路径)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            var ms = new MemoryStream(File.ReadAllBytes(路径));
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            bitmap.Freeze();
            ms.Close();
            return bitmap;
        }

        /// <summary>
        /// 获取图像的指定区域的像素数组
        /// </summary>
        /// <param name="目标矩形区域">目标矩形区域</param>
        /// <returns>目标矩形区域内的像素数组</returns>
        public static byte[] 获取像素数组(this BitmapSource b, Int32Rect 目标矩形区域)
        {
            //计算Stride
            var stride = b.Format.BitsPerPixel * 目标矩形区域.Width / 8;
            //声明字节数组
            byte[] data = new byte[目标矩形区域.Height * stride];
            //调用CopyPixels
            b.CopyPixels(目标矩形区域, data, stride, 0);
            return data;
        }

        /// <summary>
        /// 获取图像的像素数组
        /// </summary>
        /// <returns>图像像素数组</returns>
        public static byte[] 获取像素数组(this BitmapSource b)
        {
            return 获取像素数组(b, new Int32Rect(0, 0, b.PixelWidth, b.PixelHeight));
        }

        /// <summary>
        /// 将带有ARGB通道数据的数组规范化为颜色多维数组，多维数组的第一列代表原像素所处行数，第二列为列数。注：慎用，内存开销巨大
        /// </summary>
        /// <param name="像素数组">通过获取像素数组方法获得的像素数组</param>
        /// <returns>规范化后的多维数组</returns>
        public static Color[,] 规范化32位像素数组(this BitmapSource b, byte[] 像素数组)
        {
            Color[,] array = new Color[b.PixelHeight, b.PixelWidth];
            for (int i = 0; i < 像素数组.Length; i += 4)
            {
                Color c = Color.FromArgb(像素数组[i + 3], 像素数组[i], 像素数组[i + 1], 像素数组[i + 2]);
                array[i / 4 / b.PixelWidth, i / 4 % b.PixelWidth] = c;
            }
            return array;
        }

        /// <summary>
        /// 将带有RGB通道数据的数组规范化为颜色多维数组，多维数组的第一列代表原像素所处行数，第二列为列数。注：慎用，内存开销巨大
        /// </summary>
        /// <param name="像素数组">通过获取像素数组方法获得的像素数组</param>
        /// <returns>规范化后的多维数组</returns>
        public static Color[,] 规范化24位像素数组(this BitmapSource b, byte[] 像素数组)
        {
            Color[,] array = new Color[b.PixelHeight, b.PixelWidth];
            for (int i = 0; i < 像素数组.Length; i += 3)
            {
                Color c = Color.FromRgb(像素数组[i], 像素数组[i + 1], 像素数组[i + 2]);
                array[i / 3 / b.PixelWidth, i / 3 % b.PixelWidth] = c;
            }
            return array;
        }

        /// <summary>
        /// 将像素数组规范化为颜色多维数组，多维数组的第一列代表原像素所处行数，第二列为列数。注：慎用，内存开销巨大
        /// </summary>
        /// <param name="像素数组">通过获取像素数组方法获得的像素数组</param>
        /// <returns>规范化后的多维数组</returns>
        public static Color[,] 规范化像素数组(this BitmapSource b, byte[] 像素数组)
        {
            switch (b.Format.BitsPerPixel)
            {
                case 32: return 规范化32位像素数组(b, 像素数组);
                case 24: return 规范化24位像素数组(b, 像素数组);
                default: throw new Exception("不支持" + b.Format.BitsPerPixel + "位图像的规范化");
            }
        }

        /// <summary>
        /// 将图像放大或缩小
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="指定宽度">指定的宽度，为0则保持原始值</param>
        /// <param name="指定高度">指定的高度，为0则保持原始值</param>
        /// <param name="缩放方式">缩放时采用的处理方式</param>
        /// <returns>缩放后的图像</returns>
        public static BitmapSource 缩放图像(this BitmapSource 图像, int 指定宽度, int 指定高度, 缩放方式 缩放方式)
        {
            var s = Drawing处理函数.计算缩放尺寸(缩放方式, 图像.PixelWidth, 图像.PixelHeight, 指定宽度, 指定高度);
            var img = 缩放图像(图像, s.Width, s.Height);
            if (缩放方式 == 缩放方式.强制裁剪)
            {
                var tw = 指定宽度 == 0 ? 图像.PixelWidth : 指定宽度;
                var th = 指定高度 == 0 ? 图像.PixelHeight : 指定高度;
                return 剪裁图像(img, img.PixelWidth / 2 - tw / 2, img.PixelHeight / 2 - th / 2, tw, th);
            }
            return img;
        }

        /// <summary>
        /// 将图像调整到指定尺寸
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="目标宽度">目标宽度</param>
        /// <param name="目标高度">目标高度</param>
        /// <returns>调整后的图像</returns>
        public static RenderTargetBitmap 缩放图像(this BitmapSource 图像, int 目标宽度, int 目标高度)
        {
            DrawingVisual dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawImage(图像, new Rect(new Point(0, 0), new Size(目标宽度, 目标高度)));
            }
            //这里DPI不知为什么必须要用96，如果不是96就会产生问题，比如用图片自身的DPI通常生成的图像就只有1/3左右大小占据左上角，其他部分都是空的
            RenderTargetBitmap rtb = new RenderTargetBitmap(目标宽度, 目标高度, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);
            return rtb;
        }

        /// <summary>
        /// 从图像中剪裁出指定区域为新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="起始X坐标">起始X坐标</param>
        /// <param name="起始Y坐标">起始Y坐标</param>
        /// <param name="宽度">宽度</param>
        /// <param name="高度">高度</param>
        /// <returns>剪裁后的图像</returns>
        public static BitmapSource 剪裁图像(this BitmapSource 图像, int 起始X坐标, int 起始Y坐标, int 宽度, int 高度)
        {
            var data = 获取像素数组(图像, new Int32Rect(起始X坐标, 起始Y坐标, 宽度, 高度));
            return 由像素数组重建图像(图像, data, 宽度, 高度);
        }

        /// <summary>
        /// 将图像生成的像素数组重新组建为新图像
        /// </summary>
        /// <param name="图像">曾用于生成像素数组的源图像</param>
        /// <param name="像素数组">像素数组</param>
        /// <param name="宽度">像素数组对应的图像区域宽度</param>
        /// <param name="高度">像素数组对应的图像区域高度</param>
        /// <param name="像素格式">使用的像素格式</param>
        /// <returns>新图像</returns>
        public static BitmapSource 由像素数组重建图像(this BitmapSource 图像, PixelFormat 像素格式, byte[] 像素数组, int 宽度, int 高度)
        {
            var stride = 图像.Format.BitsPerPixel * 宽度 / 8;
            return BitmapSource.Create(宽度, 高度, 图像.DpiX, 图像.DpiY, 像素格式, 图像.Palette, 像素数组, stride);
        }

        /// <summary>
        /// 将图像生成的像素数组重新组建为新图像
        /// </summary>
        /// <param name="图像">曾用于生成像素数组的源图像</param>
        /// <param name="像素数组">像素数组</param>
        /// <param name="宽度">像素数组对应的图像区域宽度</param>
        /// <param name="高度">像素数组对应的图像区域高度</param>
        /// <returns>新图像</returns>
        public static BitmapSource 由像素数组重建图像(this BitmapSource 图像, byte[] 像素数组, int 宽度, int 高度)
        {
            return 由像素数组重建图像(图像, 图像.Format, 像素数组, 宽度, 高度);
        }

        /// <summary>
        /// 将图像生成的像素数组重新组建为新图像
        /// </summary>
        /// <param name="图像">曾用于生成像素数组的源图像</param>
        /// <param name="规范化像素数组">规范化像素数组</param>
        /// <param name="宽度">像素数组对应的图像区域宽度</param>
        /// <param name="高度">像素数组对应的图像区域高度</param>
        /// <param name="像素格式">使用的像素格式</param>
        /// <returns>新图像</returns>
        public static BitmapSource 由像素数组重建图像(this BitmapSource 图像, PixelFormat 像素格式, Color[,] 规范化像素数组)
        {
            byte[] data = new byte[图像.Format.BitsPerPixel / 8 * 规范化像素数组.GetLength(0) * 规范化像素数组.GetLength(1)];
            int index = 0;
            for (int i = 0; i < 规范化像素数组.GetLength(0); i++)
            {
                for (int j = 0; j < 规范化像素数组.GetLength(1); j++)
                {
                    if (图像.Format.BitsPerPixel == 32 || 图像.Format.BitsPerPixel == 24)
                    {
                        data[index++] = 规范化像素数组[i, j].R;
                        data[index++] = 规范化像素数组[i, j].G;
                        data[index++] = 规范化像素数组[i, j].B;
                        if (图像.Format.BitsPerPixel == 32) data[index++] = 规范化像素数组[i, j].A;
                    }
                }
            }
            return 由像素数组重建图像(图像, data, 规范化像素数组.GetLength(1), 规范化像素数组.GetLength(0));
        }

        /// <summary>
        /// 将图像生成的像素数组重新组建为新图像
        /// </summary>
        /// <param name="图像">曾用于生成像素数组的源图像</param>
        /// <param name="规范化像素数组">规范化像素数组</param>
        /// <param name="宽度">像素数组对应的图像区域宽度</param>
        /// <param name="高度">像素数组对应的图像区域高度</param>
        /// <returns>新图像</returns>
        public static BitmapSource 由像素数组重建图像(this BitmapSource 图像, Color[,] 规范化像素数组)
        {
            return 由像素数组重建图像(图像, 图像.Format, 规范化像素数组);
        }

        /// <summary>
        /// 计算指定像素坐标位置的红色值在其生成的像素数组中所对应的索引位置。注：仅适用于Pbgra32像素格式。
        /// </summary>
        /// <param name="图像">图像</param>
        /// <param name="X轴位置">像素所在的X轴位置</param>
        /// <param name="Y轴位置">像素所在的Y轴位置</param>
        /// <returns>对应的索引位置</returns>
        public static int 计算目标位置像素红色值对应的像素数组索引位置(this BitmapSource 图像, int X轴位置, int Y轴位置)
        {
            return (Y轴位置 * 图像.PixelWidth + X轴位置) * (图像.Format.BitsPerPixel / 8) + 2;
        }

        /// <summary>
        /// 计算指定像素坐标位置的绿色值在其生成的像素数组中所对应的索引位置。注：仅适用于Pbgra32像素格式。
        /// </summary>
        /// <param name="图像">图像</param>
        /// <param name="X轴位置">像素所在的X轴位置</param>
        /// <param name="Y轴位置">像素所在的Y轴位置</param>
        /// <returns>对应的索引位置</returns>
        public static int 计算目标位置像素绿色值对应的像素数组索引位置(this BitmapSource 图像, int X轴位置, int Y轴位置)
        {
            return (Y轴位置 * 图像.PixelWidth + X轴位置) * (图像.Format.BitsPerPixel / 8) + 1;
        }

        /// <summary>
        /// 计算指定像素坐标位置的蓝色值在其生成的像素数组中所对应的索引位置。注：仅适用于Pbgra32像素格式。
        /// </summary>
        /// <param name="图像">图像</param>
        /// <param name="X轴位置">像素所在的X轴位置</param>
        /// <param name="Y轴位置">像素所在的Y轴位置</param>
        /// <returns>对应的索引位置</returns>
        public static int 计算目标位置像素蓝色值对应的像素数组索引位置(this BitmapSource 图像, int X轴位置, int Y轴位置)
        {
            return (Y轴位置 * 图像.PixelWidth + X轴位置) * (图像.Format.BitsPerPixel / 8);
        }

        /// <summary>
        /// 计算指定像素坐标位置的不透明度在其生成的像素数组中所对应的索引位置。注：仅适用于Pbgra32像素格式。
        /// </summary>
        /// <param name="图像">图像</param>
        /// <param name="X轴位置">像素所在的X轴位置</param>
        /// <param name="Y轴位置">像素所在的Y轴位置</param>
        /// <returns>对应的索引位置</returns>
        public static int 计算目标位置像素不透明度对应的像素数组索引位置(this BitmapSource 图像, int X轴位置, int Y轴位置)
        {
            return (Y轴位置 * 图像.PixelWidth + X轴位置) * (图像.Format.BitsPerPixel / 8) + 3;
        }

        /// <summary>
        /// 在像素数组中重设指定坐标位置的像素的不透明度。注：仅适用于Pbgra32像素格式，限于Pbgra32的数据形式，此操作是有损的，当不透明度倍率曾设为极小或0时再度重设较大的不透明度倍率将无法恢复正常颜色。
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="像素数组">像素数组</param>
        /// <param name="不透明度倍率">不透明度倍率</param>
        /// <param name="X轴位置">像素所在的X轴位置</param>
        /// <param name="Y轴位置">像素所在的Y轴位置</param>
        public static void 重设目标位置像素不透明度于像素数组(this BitmapSource 图像, byte[] 像素数组, float 不透明度倍率, int X轴位置, int Y轴位置)
        {
            var s = 计算目标位置像素蓝色值对应的像素数组索引位置(图像, X轴位置, Y轴位置);
            像素数组[s] = (byte)(像素数组[s++] * 不透明度倍率);
            像素数组[s] = (byte)(像素数组[s++] * 不透明度倍率);
            像素数组[s] = (byte)(像素数组[s++] * 不透明度倍率);
            像素数组[s] = (byte)(像素数组[s++] * 不透明度倍率);
        }

        /// <summary>
        /// 为图像添加水印，并生成为新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="水印图像">水印图像</param>
        /// <param name="水印方位">相对于源图像的方位，在不冲突的情况下可复选，比如“水印方位.右|水印方位.下”</param>
        /// <param name="水平边距">左侧或右侧的边距</param>
        /// <param name="垂直边距">上方或下方的边距</param>
        /// <returns>添加了水印的图片</returns>
        public static RenderTargetBitmap 添加水印(this BitmapSource 图像, BitmapSource 水印图像, 对齐方位 水印方位, int 水平边距, int 垂直边距, double 不透明度)
        {
            var x = 图像.PixelWidth / 2 - 水印图像.PixelWidth / 2;
            var y = 图像.PixelHeight / 2 - 水印图像.PixelHeight / 2;
            if ((水印方位 & 对齐方位.上) > 0)
            {
                y = 0 + 垂直边距;
            }
            if ((水印方位 & 对齐方位.下) > 0)
            {
                y = 图像.PixelHeight - 水印图像.PixelHeight - 垂直边距;
            }
            if ((水印方位 & 对齐方位.左) > 0)
            {
                x = 0 + 水平边距;
            }
            if ((水印方位 & 对齐方位.右) > 0)
            {
                x = 图像.PixelWidth - 水印图像.PixelWidth - 水平边距;
            }
            return 添加覆盖图像(图像, 水印图像, x, y, 不透明度);
        }

        /// <summary>
        /// 在图像上覆盖另一图像，并生成为新的图像
        /// </summary>
        /// <param name="原图像">原图</param>
        /// <param name="覆盖图像">覆盖图</param>
        /// <param name="起始X坐标">覆盖位置起始X坐标</param>
        /// <param name="起始Y坐标">覆盖位置起始Y坐标</param>
        /// <param name="宽度">覆盖图目标宽度</param>
        /// <param name="高度">覆盖图目标高度</param>
        /// <returns>覆盖了指定图像的图像</returns>
        public static RenderTargetBitmap 添加覆盖图像(this BitmapSource 原图像, BitmapSource 覆盖图像, double 起始X坐标, double 起始Y坐标, double 宽度, double 高度, double 不透明度)
        {
            if (覆盖图像.Format != PixelFormats.Pbgra32) throw new Exception("覆盖的图像像素格式为{0}，而此方法仅支持PixelFormats.Pbgra32像素格式".FormatWith(覆盖图像.Format));
            DrawingVisual dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawImage(原图像, new Rect(new Point(0, 0), new Size(原图像.PixelWidth, 原图像.PixelHeight)));
                dc.PushOpacity(不透明度);
                dc.DrawImage(覆盖图像, new Rect(new Point(起始X坐标, 起始Y坐标), new Size(宽度, 高度)));
            }
            //这里DPI不知为什么必须要用96，如果不是96就会产生问题，比如用图片自身的DPI通常生成的图像就只有1/3左右大小占据左上角，其他部分都是空的
            RenderTargetBitmap rtb = new RenderTargetBitmap(原图像.PixelWidth, 原图像.PixelHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);
            return rtb;
        }

        /// <summary>
        /// 在图像上覆盖另一图像，并生成为新的图像
        /// </summary>
        /// <param name="原图像">原图</param>
        /// <param name="覆盖图像">覆盖图</param>
        /// <param name="起始X坐标">覆盖位置起始X坐标</param>
        /// <param name="起始Y坐标">覆盖位置起始Y坐标</param>
        /// <returns>覆盖了指定图像的图像</returns>
        public static RenderTargetBitmap 添加覆盖图像(this BitmapSource 原图像, BitmapSource 覆盖图像, double 起始X坐标, double 起始Y坐标, double 不透明度)
        {
            return 添加覆盖图像(原图像, 覆盖图像, 起始X坐标, 起始Y坐标, 覆盖图像.PixelWidth, 覆盖图像.PixelHeight, 不透明度);
        }

        /// <summary>
        /// 生成一个以新像素格式排列的新图像
        /// </summary>
        /// <param name="原图像">原图像</param>
        /// <param name="目标像素格式">目标像素格式</param>
        /// <returns>以新像素格式排列的新图像</returns>
        public static BitmapSource 转换像素格式(this BitmapSource 原图像, PixelFormat 目标像素格式)
        {
            return new FormatConvertedBitmap(原图像, 目标像素格式, null, 0);
            //DrawingVisual dv = new DrawingVisual();
            //using (var dc = dv.RenderOpen())
            //{
            //    dc.DrawImage(原图像, new Rect(new Point(0, 0), new Size(原图像.PixelWidth, 原图像.PixelHeight)));
            //}
            ////这里DPI不知为什么必须要用96，如果不是96就会产生问题，比如用图片自身的DPI通常生成的图像就只有1/3左右大小占据左上角，其他部分都是空的
            //RenderTargetBitmap rtb = new RenderTargetBitmap(原图像.PixelWidth, 原图像.PixelHeight, 96, 96, 目标像素格式);
            //rtb.Render(dv);
            //return rtb;
        }

        /// <summary>
        /// 生成一个以Pbgra32像素格式排列的新图像
        /// </summary>
        /// <param name="原图像">原图像</param>
        /// <returns>以Pbgra32像素格式排列的新图像</returns>
        public static BitmapSource 转换像素格式为Pbgra32(this BitmapSource 原图像)
        {
            return 转换像素格式(原图像, PixelFormats.Pbgra32);
        }

        /// <summary>
        /// 放大或缩小画布
        /// </summary>
        /// <param name="图像">图像</param>
        /// <param name="新画布宽度">新画布宽度</param>
        /// <param name="新画布高度">新画布高度</param>
        /// <param name="对齐方位">原图在新画布中的对齐方位</param>
        /// <returns>放大或缩小画布后的图像</returns>
        public static BitmapSource 缩放画布(this BitmapSource 图像, int 新画布宽度, int 新画布高度, 对齐方位 对齐方位)
        {
            DrawingVisual dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {

            }
            RenderTargetBitmap rtb = new RenderTargetBitmap(新画布宽度, 新画布高度, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(dv);
            return rtb.添加水印(图像, 对齐方位, 0, 0, 1);
        }
    }
}
