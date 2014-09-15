using System;

namespace Core.Maths
{
    public static partial class MathEx
    {
        /// <summary>
        /// 按参考值与实际值之间的比例进行缩放，通常用于做图像等比适配计算。
        /// </summary>
        /// <param name="value">待缩放值</param>
        /// <param name="refer">参考基准值</param>
        /// <param name="actual">实际值</param>
        /// <returns>缩放后的值</returns>
        public static double scaling(double value, double refer, double actual)
        {
            return actual / refer * value;
        }

        /// <summary>
        /// 根据勾股定理，计算两点间的距离长度
        /// </summary>
        /// <param name="x1">坐标1的X值</param>
        /// <param name="y1">坐标1的Y值</param>
        /// <param name="x2">坐标2的X值</param>
        /// <param name="y2">坐标2的Y值</param>
        /// <returns>两点间的直线距离长度</returns>
        public static double distance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 将角度值转换为弧度
        /// </summary>
        /// <param name="angle">一个0至360之间的值，表示角度</param>
        /// <returns>弧度值，一个0至2*PI之间的值</returns>
        public static double anglesToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

        /// <summary>
        /// 将弧度值转换为角度
        /// </summary>
        /// <param name="radian">一个0至2*PI之间的值，表示弧度</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double radiansToAngles(double radian)
        {
            return radian * (180 / Math.PI);
        }

        /// <summary>
        /// 接收角度值为参数的正弦函数
        /// </summary>
        /// <param name="angle">一个0至360之间的值，表示角度</param>
        /// <returns>正弦值</returns>
        public static double sinAngle(double angle)
        {
            return Math.Sin(anglesToRadians(angle));
        }

        /// <summary>
        /// 接收角度值为参数的余弦函数
        /// </summary>
        /// <param name="angle">一个0至360之间的值，表示角度</param>
        /// <returns>余弦值</returns>
        public static double cosAngle(double angle)
        {
            return Math.Cos(anglesToRadians(angle));
        }

        /// <summary>
        /// 接收角度值为参数的正切函数
        /// </summary>
        /// <param name="angle">一个0至360之间的值，表示角度</param>
        /// <returns>正切值</returns>
        public static double tanAngle(double angle)
        {
            return Math.Tan(anglesToRadians(angle));
        }

        /// <summary>
        /// 返回角度值的反正切函数
        /// </summary>
        /// <param name="y">坐标Y值</param>
        /// <param name="x">坐标X值</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double atan2Angle(double y, double x)
        {
            return radiansToAngles(Math.Atan2(y, x));
        }

        /// <summary>
        /// 通过反正切计算两点间直线的倾斜角度
        /// </summary>
        /// <param name="x1">坐标1的X值</param>
        /// <param name="y1">坐标1的Y值</param>
        /// <param name="x2">坐标2的X值</param>
        /// <param name="y2">坐标2的Y值</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double angleOfLine(double x1, double y1, double x2, double y2)
        {
            return atan2Angle(y2 - y1, x2 - x1);
        }

        /// <summary>
        /// 返回角度值的反余弦函数
        /// </summary>
        /// <param name="ratio">余弦比例值，一个-1至1之间的值</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double acosAngle(double ratio)
        {
            return radiansToAngles(Math.Acos(ratio));
        }

        /// <summary>
        /// 返回角度值的反正弦函数
        /// </summary>
        /// <param name="ratio">正弦比例值，一个-1至1之间的值</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double asinAngle(double ratio)
        {
            return radiansToAngles(Math.Asin(ratio));
        }

        /// <summary>
        /// 角度标准化函数，修正角度值为0至360之间的值
        /// </summary>
        /// <param name="angle">待修正的角度值</param>
        /// <returns>角度值，一个0至360之间的值</returns>
        public static double fixAngle(double angle)
        {
            double a = angle % 360;
            return a < 0 ? a + 360 : a;
        }

        /// <summary>
        /// 计算两个角度之间的最短方向的夹角角度
        /// </summary>
        /// <param name="angle1">角度1</param>
        /// <param name="angle2">角度2</param>
        /// <returns>最短方向的夹角角度</returns>
        public static double calculationAngleDistance(double angle1, double angle2)
        {
            double a = fixAngle(angle1);
            double b = fixAngle(angle2);
            double big = Math.Max(a, b);
            double small = Math.Min(a, b);
            return Math.Min(big - small, 360 - big + small);
        }
    }
}
