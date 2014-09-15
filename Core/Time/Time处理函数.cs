using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Text;
using Core.Maths;

namespace Core.Time
{
   public static class Time处理函数
    {
        /// <summary>
        /// 转换为常用日期格式方法，如：2006年11月3日 21:35:09
        /// </summary>
        public static string 转换为常用日期时间格式(this DateTime t)
        {
            return t.ToString(Text常量.常用日期时间格式);
        }

        /// <summary>
        /// 转换为常用日期格式方法，如：2006年11月3日
        /// </summary>
        public static string 转换为常用日期格式(this DateTime t)
        {
            return t.ToString(Text常量.常用日期格式);
        }

        /// <summary>
        /// 转换为标准日期时间格式方法，如：2006-11-03 21:35:09
        /// </summary>
        public static string 转换为标准日期时间格式(this DateTime t)
        {
            return t.ToString(Text常量.标准日期时间格式);
        }

        /// <summary>
        /// 转换为标准日期格式方法，如：2006-11-03
        /// </summary>
        public static string 转换为标准日期格式(this DateTime t)
        {
            return t.ToString(Text常量.标准日期格式);
        }

        /// <summary>
        /// 转换为标准时间格式方法，如：21:35:09
        /// </summary>
        public static string 转换为标准时间格式(this DateTime t)
        {
            return t.ToString(Text常量.标准时间格式);
        }

        /// <summary>
        /// 转换为日期时间编码，如：20080802191521986
        /// </summary>
        public static string 转换为日期时间编码格式(this DateTime t)
        {
            return string.Format("{0}{1:d2}{2:d2}{3:d2}{4:d2}{5:d2}{6:d3}", t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second, t.Millisecond);
        }

        /// <summary>
        /// 如48:55:12
        /// </summary>
        public static string 转换为标准时间格式(this TimeSpan t)
        {
            return string.Format("{0}:{1}:{2}", (int)t.TotalHours, t.Minutes, t.Seconds);
        }

        /// <summary>
        /// 如48小时55分12秒
        /// </summary>
        public static string 转换为常用时间格式(this TimeSpan t)
        {
            return string.Format("{0}小时{1}分{2}秒", (int)t.TotalHours, t.Minutes, t.Seconds);
        }

        /// <summary>
        /// 输出友好形式的时间字符串，如：65天17小时56分 或 12秒357毫秒 或 229分钟49秒
        /// </summary>
        /// <param name="精确度">指示统计的精确程度，该值不可高于粒度</param>
        /// <param name="粒度">指示统计的最大粒度，该值不可低于精确度</param>
        /// <param name="保留零值位前缀">指示是否保留为0的前置高粒度位，如：0小时0分钟29秒311毫秒，否则显示为：29秒311毫秒</param>
        /// <param name="仅保留最高有效位">指示是否只保留最高位，如：6天 或 120分钟</param>
        /// <returns>友好形式的时间字符串</returns>
        public static string 转换为友好的日期时间格式(this TimeSpan t, 时间粒度 精确度, 时间粒度 粒度, bool 保留零值位前缀, bool 仅保留最高有效位)
        {
            return 转换为友好的日期时间格式(t, 精确度, 粒度, false, 保留零值位前缀, 仅保留最高有效位);
        }

        /// <summary>
        /// 输出友好形式的时间字符串，如：65天17小时56分 或 12秒357毫秒 或 229分钟49秒
        /// </summary>
        /// <param name="精确度">指示统计的精确程度，该值不可高于粒度</param>
        /// <param name="粒度">指示统计的最大粒度，该值不可低于精确度</param>
        /// <param name="四舍五入">当精确度和粒度相等时，指示值是否应当进行四舍五入</param>
        /// <param name="保留零值位前缀">指示是否保留为0的前置高粒度位，如：0小时0分钟29秒311毫秒，否则显示为：29秒311毫秒</param>
        /// <param name="仅保留最高有效位">指示是否只保留最高位，如：6天 或 120分钟 或 9年</param>
        /// <returns>友好形式的时间字符串</returns>
        public static string 转换为友好的日期时间格式(this TimeSpan t, 时间粒度 精确度, 时间粒度 粒度, bool 四舍五入, bool 保留零值位前缀, bool 仅保留最高有效位)
        {
            if (粒度 < 精确度) throw new Exception("粒度不得小于精确度");
            StringBuilder s = new StringBuilder();
            s.Append(输出(时间粒度.年, "年", t.Years(), t.TotalYears(), 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.月, "个月", t.Months(), t.TotalMonths(), 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.星期, "星期", t.Weeks(), t.TotalWeeks(), 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.天, "天", t.Days(), t.TotalDays, 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.小时, "小时", t.Hours, t.TotalHours, 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.分钟, "分钟", t.Minutes, t.TotalMinutes, 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.秒, "秒", t.Seconds, t.TotalSeconds, 精确度, 粒度, 四舍五入, 保留零值位前缀));
            if (仅保留最高有效位 && s.Length > 0) goto Output;
            s.Append(输出(时间粒度.毫秒, "毫秒", t.Milliseconds, t.TotalMilliseconds, 精确度, 粒度, 四舍五入, 保留零值位前缀));
        //if (仅保留最高有效位 && s.Length > 0) goto Output;
        Output: return s.ToString();
        }

        static string 输出(时间粒度 处理级别, string 显示名, int 处理位显示值, double 处理位总值, 时间粒度 精确度, 时间粒度 粒度, bool 四舍五入, bool 保留零值前缀)
        {
            if (精确度 > 处理级别 || 粒度 < 处理级别 || (!保留零值前缀 && 处理位总值 < 1)) return null;
            if (粒度 > 处理级别) return 处理位显示值 + 显示名;
            return (四舍五入 && 精确度 == 粒度 ? 处理位总值.四舍五入(0) : (int)处理位总值) + 显示名;
        }

        public static int Days(this TimeSpan t)
        {
            return t.Days % 365 % 30 % 7;
        }

        public static int Weeks(this TimeSpan t)
        {
            return t.Days % 365 % 30 / 7;
        }

        public static int Months(this TimeSpan t)
        {
            return t.Days % 365 / 30;
        }

        public static int Years(this TimeSpan t)
        {
            return t.Days / 365;
        }

        public static double TotalWeeks(this TimeSpan t)
        {
            return t.TotalDays / 7;
        }

        public static double TotalMonths(this TimeSpan t)
        {
            return t.TotalDays / 30;
        }

        public static double TotalYears(this TimeSpan t)
        {
            return t.TotalDays / 365;
        }


    }

   public enum 时间粒度
   {
       毫秒, 秒, 分钟, 小时, 天, 星期, 月, 年
   }
}
