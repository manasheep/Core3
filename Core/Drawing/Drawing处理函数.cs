using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace Core.Drawing
{
    /// <summary>
    /// 缩放图片时所使用的缩放方式
    /// </summary>
    public enum 缩放方式
    {
        保持长宽比, 强制拉伸, 强制裁剪
    }

    /// <summary>
    /// 用于设置水印的覆盖方位，可复选
    /// </summary>
    [Flags]
    public enum 对齐方位
    {
        上 = 1, 下 = 2, 左 = 4, 右 = 8, 中 = 16
    }

    public static class Drawing处理函数
    {
        /// <summary>
        /// 回调
        /// </summary>
        /// <returns></returns>
        public static bool 回调()
        {
            return false;
        }

        /// <summary>
        /// 将图像保存为JPG格式文件
        /// </summary>
        /// <param name="图像">传入图像</param>
        /// <param name="保存文件路径">存储路径</param>
        /// <param name="图像质量">一个0到100之间的整数值，数值越大则图像质量越高</param>
        public static void 保存为JPG文件(this Image 图像, string 保存文件路径, long 图像质量)
        {
            var ecd = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 图像质量);
            myEncoderParameters.Param[0] = myEncoderParameter;
            图像.Save(保存文件路径, ecd, myEncoderParameters);
        }

        /// <summary>
        /// 将图像保存为JPG格式文件
        /// </summary>
        /// <param name="图像">传入图像</param>
        /// <param name="写入流">写入流</param>
        /// <param name="图像质量">一个0到100之间的整数值，数值越大则图像质量越高</param>
        public static void 保存为JPG文件(this Image 图像, Stream 写入流, long 图像质量)
        {
            var ecd = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 图像质量);
            myEncoderParameters.Param[0] = myEncoderParameter;
            图像.Save(写入流, ecd, myEncoderParameters);
        }

        /// <summary>
        /// 将图像保存为PNG格式文件
        /// </summary>
        /// <param name="图像">传入图像</param>
        /// <param name="保存文件路径">存储路径</param>
        public static void 保存为PNG文件(this Image 图像, string 保存文件路径)
        {
            图像.Save(保存文件路径, ImageFormat.Png);
        }

        /// <summary>
        /// 将图像保存为PNG格式文件
        /// </summary>
        /// <param name="图像">传入图像</param>
        /// <param name="写入流">写入流</param>
        public static void 保存为PNG文件(this Image 图像, Stream 写入流)
        {
            图像.Save(写入流, ImageFormat.Png);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        /// <summary>
        /// 将图像放大或缩小，默认采用高质量缩放
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="指定宽度">指定的宽度，为0则不约束</param>
        /// <param name="指定高度">指定的高度，为0则不约束</param>
        /// <param name="缩放方式">缩放时采用的处理方式</param>
        /// <returns>缩放后的图像</returns>
        public static Bitmap 缩放图像(this Image 图像, int 指定宽度, int 指定高度, 缩放方式 缩放方式)
        {
            return 缩放图像(图像, 指定宽度, 指定高度, 缩放方式, InterpolationMode.HighQualityBicubic, SmoothingMode.HighQuality, CompositingQuality.HighQuality);
        }

        /// <summary>
        /// 将图像放大或缩小
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="指定宽度">指定的宽度，为0则保持原始值</param>
        /// <param name="指定高度">指定的高度，为0则保持原始值</param>
        /// <param name="缩放方式">缩放时采用的处理方式</param>
        /// <param name="插值算法">插值算法</param>
        /// <param name="平滑模式">平滑模式</param>
        /// <param name="合成质量">合成质量</param>
        /// <returns>缩放后的图像</returns>
        public static Bitmap 缩放图像(this Image 图像, int 指定宽度, int 指定高度, 缩放方式 缩放方式, InterpolationMode 插值算法, SmoothingMode 平滑模式, CompositingQuality 合成质量)
        {
            var s = 计算缩放尺寸(缩放方式, 图像.Width, 图像.Height, 指定宽度, 指定高度);
            var img = 缩放图像(图像, s, 插值算法, 平滑模式, 合成质量);
            if (缩放方式 == 缩放方式.强制裁剪)
            {
                var tw = 指定宽度 == 0 ? 图像.Width : 指定宽度;
                var th = 指定高度 == 0 ? 图像.Height : 指定高度;
                img = 剪裁图像(img, new Point(img.Width / 2 - tw / 2, img.Height / 2 - th / 2), new Size(tw, th));
            }
            return img;
        }

        /// <summary>
        /// 将图像调整到指定尺寸
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="缩放尺寸">目标尺寸</param>
        /// <param name="插值算法">插值算法</param>
        /// <param name="平滑模式">平滑模式</param>
        /// <param name="合成质量">合成质量</param>
        /// <returns>调整后的图像</returns>
        public static Bitmap 缩放图像(this Image 图像, Size 缩放尺寸, InterpolationMode 插值算法, SmoothingMode 平滑模式, CompositingQuality 合成质量)
        {
            Bitmap resizedBmp = new Bitmap(缩放尺寸.Width, 缩放尺寸.Height);
            Graphics g = Graphics.FromImage(resizedBmp);
            g.InterpolationMode = 插值算法;
            g.SmoothingMode = 平滑模式;
            g.CompositingQuality = 合成质量;
            g.DrawImage(图像, new Rectangle(0, 0, 缩放尺寸.Width, 缩放尺寸.Height), new Rectangle(0, 0, 图像.Width, 图像.Height), GraphicsUnit.Pixel);
            return resizedBmp;
        }

        /// <summary>
        /// 从图像中剪裁出指定区域为新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="起始坐标">剪裁区域的左上角坐标点</param>
        /// <param name="剪裁尺寸">剪裁区域尺寸</param>
        /// <returns>剪裁后的图像</returns>
        public static Bitmap 剪裁图像(this Image 图像, Point 起始坐标, Size 剪裁尺寸)
        {
            Bitmap resizedBmp = new Bitmap(剪裁尺寸.Width, 剪裁尺寸.Height);
            Graphics g = Graphics.FromImage(resizedBmp);
            g.DrawImage(图像, new Rectangle(0, 0, 剪裁尺寸.Width, 剪裁尺寸.Height), new Rectangle(起始坐标.X, 起始坐标.Y, 剪裁尺寸.Width, 剪裁尺寸.Height), GraphicsUnit.Pixel);
            return resizedBmp;
        }

        /// <summary>
        /// 从图像中剪裁出指定区域为新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="剪裁矩形">需从原图中剪裁的矩形</param>
        /// <returns>剪裁后的图像</returns>
        public static Bitmap 剪裁图像(this Image 图像, Rectangle 剪裁矩形)
        {
            return 剪裁图像(图像, 剪裁矩形.Location, 剪裁矩形.Size);
        }

        /// <summary>
        /// 从图像中剪裁出指定区域为新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="剪裁矩形">需从原图中剪裁的矩形</param>
        /// <returns>剪裁后的图像</returns>
        public static Bitmap 剪裁图像(this Image 图像, RectangleF 剪裁矩形)
        {
            Bitmap resizedBmp = new Bitmap((int)Math.Ceiling(剪裁矩形.Width), (int)Math.Ceiling(剪裁矩形.Height));
            Graphics g = Graphics.FromImage(resizedBmp);
            g.DrawImage(图像, new RectangleF(0, 0, 剪裁矩形.Width, 剪裁矩形.Height), 剪裁矩形, GraphicsUnit.Pixel);
            return resizedBmp;
        }

        /// <summary>
        /// 从图像中剪裁出指定区域为新的图像，此此重载形式为居中剪裁
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="起始坐标">剪裁区域的左上角坐标点</param>
        /// <param name="剪裁尺寸">剪裁区域尺寸</param>
        /// <returns>剪裁后的图像</returns>
        public static Bitmap 剪裁图像(this Image 图像, Size 剪裁尺寸)
        {
            return 剪裁图像(图像, new Point(图像.Width / 2 - 剪裁尺寸.Width / 2, 图像.Height / 2 - 剪裁尺寸.Height / 2), 剪裁尺寸);
        }

        /// <summary>
        /// 居中剪裁图像为小于或等于指定尺寸的最邻近的符合比例的尺寸，比如300*201将会返回300*200，因其符合3:2的比例。
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="横向最大比值">最大的比例值</param>
        /// <param name="纵向最大比值">最大的比例值</param>
        /// <param name="最大比值积">纵横比例值的最大乘积</param>
        /// <returns>剪裁后的图像</returns>
        public static Bitmap 剪裁图像为邻近的符合比例的尺寸(this Image 图像, int 横向最大比值, int 纵向最大比值, int 最大比值积)
        {
            return 剪裁图像(图像, 计算邻近的符合比例的尺寸(图像.Width, 图像.Height,  横向最大比值,  纵向最大比值,  最大比值积));
        }

        /// <summary>
        /// 计算应产生的图像缩放尺寸
        /// </summary>
        /// <param name="缩放方式">缩放时采用的处理方式</param>
        /// <param name="原始宽度">源图像宽度</param>
        /// <param name="原始高度">源图像高度</param>
        /// <param name="指定宽度">指定的宽度，为0则保持原始值</param>
        /// <param name="指定高度">指定的高度，为0则保持原始值</param>
        /// <returns>缩放后的尺寸</returns>
        public static Size 计算缩放尺寸(缩放方式 缩放方式, int 原始宽度, int 原始高度, int 指定宽度, int 指定高度)
        {
            var w = 原始宽度;
            var h = 原始高度;
            var tw = 指定宽度 == 0 ? w : 指定宽度;
            var th = 指定高度 == 0 ? h : 指定高度;
            switch (缩放方式)
            {
                case 缩放方式.保持长宽比:
                    if (tw * 1.00 / w < th * 1.00 / h)
                    {
                        h = (int)(tw * 1.00 / w * h);
                        w = tw;
                    }
                    else
                    {
                        w = (int)(th * 1.00 / h * w);
                        h = th;
                    }
                    break;
                case 缩放方式.强制拉伸:
                    w = tw;
                    h = th;
                    break;
                case 缩放方式.强制裁剪:
                    if (tw * 1.00 / w > th * 1.00 / h)
                    {
                        h = (int)(tw * 1.00 / w * h);
                        w = tw;
                    }
                    else
                    {
                        w = (int)(th * 1.00 / h * w);
                        h = th;
                    }
                    break;
                default:
                    break;
            }
            return new Size(w, h);
        }

        /// <summary>
        /// 为图像添加水印，并生成新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="水印图像文件路径">水印图像文件路径</param>
        /// <param name="水印方位">相对于源图像的方位，在不冲突的情况下可复选，比如“水印方位.右|水印方位.下”</param>
        /// <param name="水平边距">左侧或右侧的边距</param>
        /// <param name="垂直边距">上方或下方的边距</param>
        /// <returns>添加了水印的图片</returns>
        public static Bitmap 添加水印(this Image 图像, string 水印图像文件路径, 对齐方位 水印方位, int 水平边距, int 垂直边距)
        {
            return 添加水印(图像, Image.FromFile(水印图像文件路径), 水印方位, 水平边距, 垂直边距);
        }

        /// <summary>
        /// 为图像添加水印，并生成新的图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="水印图像">水印图像</param>
        /// <param name="水印方位">相对于源图像的方位，在不冲突的情况下可复选，比如“水印方位.右|水印方位.下”</param>
        /// <param name="水平边距">左侧或右侧的边距</param>
        /// <param name="垂直边距">上方或下方的边距</param>
        /// <returns>添加了水印的图片</returns>
        public static Bitmap 添加水印(this Image 图像, Image 水印图像, 对齐方位 水印方位, int 水平边距, int 垂直边距)
        {
            Bitmap Bmp = 图像.Clone() as Bitmap;
            Graphics g = Graphics.FromImage(Bmp);
            var x = Bmp.Width / 2 - 水印图像.Width / 2;
            var y = Bmp.Height / 2 - 水印图像.Height / 2;
            if ((水印方位 & 对齐方位.上) > 0)
            {
                y = 0 + 垂直边距;
            }
            if ((水印方位 & 对齐方位.下) > 0)
            {
                y = Bmp.Height - 水印图像.Height - 垂直边距;
            }
            if ((水印方位 & 对齐方位.左) > 0)
            {
                x = 0 + 水平边距;
            }
            if ((水印方位 & 对齐方位.右) > 0)
            {
                x = Bmp.Width - 水印图像.Width - 水平边距;
            }
            g.DrawImage(水印图像, new Rectangle(x, y, 水印图像.Width, 水印图像.Height), 0, 0, 水印图像.Width, 水印图像.Height, GraphicsUnit.Pixel);
            return Bmp;
        }

        /// <summary>
        /// 将源图像上覆盖以目标图像
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="覆盖图像">目标图像</param>
        /// <param name="左上角X坐标">起始X坐标</param>
        /// <param name="左上角Y坐标">起始Y坐标</param>
        public static void 覆盖图像(this Image 图像, Image 覆盖图像, int 左上角X坐标, int 左上角Y坐标)
        {
            Graphics g = Graphics.FromImage(图像);
            g.DrawImage(覆盖图像, 左上角X坐标, 左上角Y坐标);
            g.Dispose();
        }

        /// <summary>
        /// [已过时] 生成缩略图,返回缩略图的Image对象
        /// </summary>
        /// <param name="图像">需要处理的图像</param>
        /// <param name="宽">缩略图宽度</param>
        /// <param name="高">缩略图高度</param>
        /// <param name="等比缩放">是否按等比例进行缩放，否则将生成固定尺寸的缩略图</param>
        /// <returns>缩略图的Image对象</returns>
        public static Image 生成缩略图(Image 图像, int 宽, int 高, bool 等比缩放)
        {
            Size size = new Size(宽, 高);
            if (等比缩放) size = 计算缩略图尺寸(图像.Width, 图像.Height, size.Width, size.Height);
            Image img;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(回调);
            img = 图像.GetThumbnailImage(size.Width, size.Height, callb, IntPtr.Zero);
            return img;
        }

        /// <summary>
        /// [已过时] 保存缩略图
        /// </summary>
        /// <param name="图像">需要处理的图像</param>
        /// <param name="保存路径">文件存储路径</param>
        /// <param name="宽">宽度</param>
        /// <param name="高">高度</param>
        public static void 保存缩略图(Image 图像, string 保存路径, int 宽, int 高, bool 等比缩放)
        {
            switch (保存路径.AsPathString().Extension)
            {
                case ".png":
                    保存缩略图(图像, 保存路径, 宽, 高, ImageFormat.Png, 等比缩放);
                    break;
                case ".gif":
                    保存缩略图(图像, 保存路径, 宽, 高, ImageFormat.Gif, 等比缩放);
                    break;
                default:
                    保存缩略图(图像, 保存路径, 宽, 高, ImageFormat.Jpeg, 等比缩放);
                    break;
            }
        }

        /// <summary>
        /// [已过时] 生成缩略图并保存
        /// </summary>
        /// <param name="图像">需要处理的图像</param>
        /// <param name="保存路径">文件存储路径</param>
        /// <param name="宽">缩略图的宽度</param>
        /// <param name="高">缩略图的高度</param>
        /// <param name="图像格式">保存的图像格式</param>
        /// <returns>缩略图的Image对象</returns>
        public static void 保存缩略图(Image 图像, string 保存路径, int 宽, int 高, ImageFormat 图像格式, bool 等比缩放)
        {
            if ((图像.Width > 宽) || (图像.Height > 高))
            {
                Image img = 生成缩略图(图像, 宽, 高, 等比缩放);
                图像.Dispose();
                img.Save(保存路径, 图像格式);
                img.Dispose();
            }
        }

        /// <summary>
        /// [已过时] 为图像添加图片水印
        /// </summary>
        /// <param name="图像">要处理的图像</param>
        /// <param name="文件保存路径">处理后图片保存的路径</param>
        /// <param name="水印图片路径">要添加的水印图片路径</param>
        /// <param name="水印位置">水印的添加位置，范围为1-9，九宫格分布</param>
        /// <param name="质量">图像品质，范围为1-100</param>
        /// <param name="透明度">水印图片的透明度，范围为1-10，数值越小则透明度越高</param>
        public static void 添加图片水印(Image 图像, string 文件保存路径, string 水印图片路径, int 水印位置, int 质量, int 透明度)
        {
            Graphics g = Graphics.FromImage(图像);
            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image 水印图片 = new Bitmap(水印图片路径);

            if (水印图片.Height > 图像.Height / 3 || 水印图片.Width > 图像.Width / 3) 水印图片 = 生成缩略图(水印图片, 图像.Width / 3, 图像.Height / 3, true);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float transparency = 0.5F;
            if (透明度 >= 1 && 透明度 <= 10)
            {
                transparency = (透明度 / 10.0F);
            }

            float[][] colorMatrixElements = {
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
											};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;

            switch (水印位置)
            {
                case 1:
                    xpos = (int)(图像.Width * (float).01);
                    ypos = (int)(图像.Height * (float).01);
                    break;
                case 2:
                    xpos = (int)((图像.Width * (float).50) - (水印图片.Width / 2));
                    ypos = (int)(图像.Height * (float).01);
                    break;
                case 3:
                    xpos = (int)((图像.Width * (float).99) - (水印图片.Width));
                    ypos = (int)(图像.Height * (float).01);
                    break;
                case 4:
                    xpos = (int)(图像.Width * (float).01);
                    ypos = (int)((图像.Height * (float).50) - (水印图片.Height / 2));
                    break;
                case 5:
                    xpos = (int)((图像.Width * (float).50) - (水印图片.Width / 2));
                    ypos = (int)((图像.Height * (float).50) - (水印图片.Height / 2));
                    break;
                case 6:
                    xpos = (int)((图像.Width * (float).99) - (水印图片.Width));
                    ypos = (int)((图像.Height * (float).50) - (水印图片.Height / 2));
                    break;
                case 7:
                    xpos = (int)(图像.Width * (float).01);
                    ypos = (int)((图像.Height * (float).99) - 水印图片.Height);
                    break;
                case 8:
                    xpos = (int)((图像.Width * (float).50) - (水印图片.Width / 2));
                    ypos = (int)((图像.Height * (float).99) - 水印图片.Height);
                    break;
                case 9:
                    xpos = (int)((图像.Width * (float).99) - (水印图片.Width));
                    ypos = (int)((图像.Height * (float).99) - 水印图片.Height);
                    break;
            }

            g.DrawImage(水印图片, new Rectangle(xpos, ypos, 水印图片.Width, 水印图片.Height), 0, 0, 水印图片.Width, 水印图片.Height, GraphicsUnit.Pixel, imageAttributes);
            //g.DrawImage(watermark, new System.Drawing.Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, System.Drawing.GraphicsUnit.Pixel);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (质量 < 0 || 质量 > 100)
            {
                质量 = 80;
            }
            qualityParam[0] = 质量;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                图像.Save(文件保存路径, ici, encoderParams);
            }
            else
            {
                图像.Save(文件保存路径);
            }

            g.Dispose();
            图像.Dispose();
            水印图片.Dispose();
            imageAttributes.Dispose();
        }

        /// <summary>
        /// [已过时] 为图像添加文字水印
        /// </summary>
        /// <param name="图像">要处理的图像</param>
        /// <param name="文件保存路径">处理后图片保存的路径</param>
        /// <param name="水印位置">水印的添加位置，范围为1-9，九宫格分布</param>
        /// <param name="质量">图像品质，范围为1-100</param>
        /// <param name="水印文字">水印的文字内容</param>
        /// <param name="字体">字体名称</param>
        /// <param name="字号">水印图片的透明度，范围为1-10，数值越小则透明度越高</param>
        public static void 添加文字水印(Image 图像, string 文件保存路径, int 水印位置, int 质量, string 水印文字, string 字体, int 字号)
        {
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            //	.FromFile(filename);
            Graphics g = Graphics.FromImage(图像);
            Font drawFont = new Font(字体, 字号, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF crSize;
            crSize = g.MeasureString(水印文字, drawFont);

            float xpos = 0;
            float ypos = 0;

            switch (水印位置)
            {
                case 1:
                    xpos = (float)图像.Width * (float).01;
                    ypos = (float)图像.Height * (float).01;
                    break;
                case 2:
                    xpos = ((float)图像.Width * (float).50) - (crSize.Width / 2);
                    ypos = (float)图像.Height * (float).01;
                    break;
                case 3:
                    xpos = ((float)图像.Width * (float).99) - crSize.Width;
                    ypos = (float)图像.Height * (float).01;
                    break;
                case 4:
                    xpos = (float)图像.Width * (float).01;
                    ypos = ((float)图像.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 5:
                    xpos = ((float)图像.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)图像.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 6:
                    xpos = ((float)图像.Width * (float).99) - crSize.Width;
                    ypos = ((float)图像.Height * (float).50) - (crSize.Height / 2);
                    break;
                case 7:
                    xpos = (float)图像.Width * (float).01;
                    ypos = ((float)图像.Height * (float).99) - crSize.Height;
                    break;
                case 8:
                    xpos = ((float)图像.Width * (float).50) - (crSize.Width / 2);
                    ypos = ((float)图像.Height * (float).99) - crSize.Height;
                    break;
                case 9:
                    xpos = ((float)图像.Width * (float).99) - crSize.Width;
                    ypos = ((float)图像.Height * (float).99) - crSize.Height;
                    break;
            }

            //			System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
            //			StrFormat.Alignment = System.Drawing.StringAlignment.Center;
            //
            //			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.White), xpos + 1, ypos + 1, StrFormat);
            //			g.DrawString(watermarkText, drawFont, new System.Drawing.SolidBrush(System.Drawing.Color.Black), xpos, ypos, StrFormat);
            g.DrawString(水印文字, drawFont, new SolidBrush(Color.White), xpos + 1, ypos + 1);
            g.DrawString(水印文字, drawFont, new SolidBrush(Color.Black), xpos, ypos);

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ici = null;
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType.IndexOf("jpeg") > -1)
                {
                    ici = codec;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] qualityParam = new long[1];
            if (质量 < 0 || 质量 > 100)
            {
                质量 = 80;
            }
            qualityParam[0] = 质量;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
            encoderParams.Param[0] = encoderParam;

            if (ici != null)
            {
                图像.Save(文件保存路径, ici, encoderParams);
            }
            else
            {
                图像.Save(文件保存路径);
            }
            g.Dispose();
            //bmp.Dispose();
            图像.Dispose();
        }

        /// <summary>
        /// [已过时] 计算缩略图的等比尺寸
        /// </summary>
        /// <param name="宽">原始宽度</param>
        /// <param name="高">原始高度</param>
        /// <param name="最大宽度">最大新宽度</param>
        /// <param name="最大高度">最大新高度</param>
        /// <returns></returns>
        private static Size 计算缩略图尺寸(int 宽, int 高, int 最大宽度, int 最大高度)
        {
            decimal MAX_WIDTH = (decimal)最大宽度;
            decimal MAX_HEIGHT = (decimal)最大高度;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            decimal originalWidth = (decimal)宽;
            decimal originalHeight = (decimal)高;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = 宽;
                newHeight = 高;
            }

            return new Size(newWidth, newHeight);

        }

        /// <summary>
        /// 验证Web文件格式是否为常用图片格式
        /// </summary>
        public static bool 验证图片格式(string MIME文件类型)
        {
            string[] 常规格式 = { "jpg", "jpeg", "jpe", "bmp", "png", "gif" };
            bool b = false;
            foreach (string f in 常规格式)
            {
                if (MIME文件类型.IndexOf(f) >= 0) b = true;
            }
            return b;
        }

        static Random rand = new Random();

        /// <summary>
        /// 创建验证图片，并返回jpg格式文件的二进制数组形式
        /// </summary>
        public static byte[] 生成验证图片(string 验证码)
        {
            int iwidth = (int)(验证码.Length * 16);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
            Graphics g = Graphics.FromImage(image);
            //背景颜色
            g.Clear(Color.White);
            //字符颜色 
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Blue, Color.DarkGray, Color.Green, Color.OrangeRed, Color.Brown, Color.DarkCyan, Color.Purple };
            //Color[] c = { Color.Black, Color.DarkBlue, Color.DarkGray, Color.OrangeRed, Color.Brown, Color.DarkCyan };
            //Color[] c = { Color.FromArgb(208, 213, 216) };
            //定义字体         
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "Tahoma", "黑体", "幼圆", "宋体" };
            //string[] font = { "Arial", "黑体" };
            var C = c[rand.Next(c.Length)];
            //随机输出噪点 
            for (int i = 0; i < 32; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawRectangle(new Pen(C, 0), x, y, 1, 1);
            }

            //输出验证码字符 
            for (int i = 0; i < 验证码.Length; i++)
            {
                //int cindex = rand.Next(c.Length);
                int findex = rand.Next(font.Length);

                Font f = new System.Drawing.Font(font[findex], 13, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(C);
                int ii = 2;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(验证码.Substring(i, 1), f, b, 3 + (i * 15), ii);
            }
            //画一个边框 
            g.DrawRectangle(new Pen(C, 0), 0, 0, image.Width - 1, image.Height - 1);

            //输出到浏览器 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            var o = ms.ToArray();
            g.Dispose();
            image.Dispose();
            ms.Close();
            return o;
        }

        public static bool 验证矩形区域是否包含坐标点(this Rectangle 矩形,Point 坐标点)
        {
            return 坐标点.X >= 矩形.Left && 坐标点.X <= 矩形.Right && 坐标点.Y >= 矩形.Top && 坐标点.Y <= 矩形.Bottom;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void 创建缩略图(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 计算指定尺寸的比例，如果不合比例（超出最大比值）则返回null
        /// </summary>
        /// <param name="宽度">指定宽度</param>
        /// <param name="高度">指定高度</param>
        /// <param name="最大比值">最大的比例值</param>
        /// <returns>比例</returns>
        public static Size? 计算比例(int 宽度, int 高度, int 最大比值)
        {
            for (int i = 1; i <= 最大比值; i++)
            {
                for (int j = 1; j <= 最大比值; j++)
                {
                    if (宽度 * 1.00 / i == 高度 * 1.00 / j)
                    {
                        return new Size(i, j);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 计算图像的比例，如果不合比例（超出最大比值）则返回null
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="最大比值">最大的比例值</param>
        /// <returns>比例</returns>
        public static Size? 计算比例(this Image 图像, int 最大比值)
        {
            return 计算比例(图像.Width, 图像.Height, 最大比值);
        }

        /// <summary>
        /// 计算小于或等于指定尺寸的最邻近的符合比例的尺寸，比如300*201将会返回300*200，因其符合3:2的比例。
        /// </summary>
        /// <param name="宽度">指定宽度</param>
        /// <param name="高度">指定高度</param>
        /// <param name="横向最大比值">最大的比例值</param>
        /// <param name="纵向最大比值">最大的比例值</param>
        /// <param name="最大比值积">纵横比例值的最大乘积</param>
        /// <returns>邻近比例尺寸</returns>
        public static Size 计算邻近的符合比例的尺寸(int 宽度, int 高度, int 横向最大比值,int 纵向最大比值,int 最大比值积)
        {
            int offset = Int32.MaxValue;
            int ow = 0;
            int oh = 0;


            for (int w = 1; w <= 横向最大比值; w++)
            {
                for (int h = 1; h <= 纵向最大比值; h++)
                {
                    if (w * h > 最大比值积) continue;
                    for (int i = 1; i <= Math.Min(宽度, 高度); i++)
                    {
                        var tw = w * i;
                        if (tw > 宽度) continue;
                        var th = h * i;
                        if (th > 高度) continue;
                        var r = (宽度 - tw) + (高度 - th);
                        if (r < offset)
                        {
                            offset = r;
                            ow = tw;
                            oh = th;
                        }
                    }
                }
            }

            return new Size(ow, oh);
        }
    }
}
