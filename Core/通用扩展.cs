using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core.Encryption;
using Core.Reflection;

public static partial class 通用扩展
{
    #region 基本

    /// <summary>     
    /// 获取表达式选取的目标属性的名称
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="表达式">获取属性的表达式</param>     
    /// <returns>属性的名称</returns>     
    public static string GetPropertyName<T, PT>(this T o, Expression<Func<T, PT>> 表达式)
    {
        return Reflection处理函数.GetPropertyName(表达式);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的名称
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="表达式">获取属性的表达式</param>     
    /// <returns>属性的名称</returns>     
    public static string GetPropertyName<T, PT>(this Expression<Func<T, PT>> 表达式)
    {
        return Reflection处理函数.GetPropertyName(表达式);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的类型
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="表达式">获取属性的表达式</param>     
    /// <returns>属性的类型</returns>     
    public static Type GetPropertyType<T, PT>(this T o, Expression<Func<T, PT>> 表达式)
    {
        return Reflection处理函数.GetPropertyType(表达式);
    }

    /// <summary>     
    /// 获取表达式选取的目标属性的类型
    /// </summary>     
    /// <typeparam name="T">元素类型</typeparam>
    /// <typeparam name="PT">属性类型</typeparam>
    /// <param name="表达式">获取属性的表达式</param>     
    /// <returns>属性的类型</returns>     
    public static Type GetPropertyType<T, PT>(this Expression<Func<T, PT>> 表达式)
    {
        return Reflection处理函数.GetPropertyType(表达式);
    }

    /// <summary>
    /// 获取枚举的注释特性（DescriptionAttribute）值
    /// </summary>
    /// <param name="o">枚举值</param>
    /// <returns>注释</returns>
    public static string GetDescription(this Enum o)
    {
        var enumType = o.GetType();
        var name = Enum.GetName(enumType, Convert.ToInt32(o));
        if (name == null)
            return string.Empty;
        object[] objs = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (objs.Length == 0)
        {
            return string.Empty;
        }
        else
        {
            var attr = objs[0] as DescriptionAttribute;
            return attr.Description;
        }
    }

    /// <summary>
    /// 转换为异步编程模型（Asynchronous Programming Model），用于WF的AsyncCodeActivity中的BeginExecute方法中使用，如果不进行此转换，通常就会因不包含AsyncState属性而引发InvalidOperationException
    /// 代码源自：http://tweetycodingxp.blogspot.jp/2013/06/using-task-based-asynchronous-pattern.html
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="task">原Task对象</param>
    /// <param name="callback">BeginExecute方法中的callback参数</param>
    /// <param name="state">BeginExecute方法中的state参数</param>
    /// <returns>异步编程模型形式的Task</returns>
    public static Task<TResult> ToApm<TResult>(this Task<TResult> task, AsyncCallback callback, object state)
    {
        if (task.AsyncState == state)
        {
            if (callback != null)
            {
                task.ContinueWith(delegate { callback(task); },
                                  CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
            }
            return task;
        }

        var tcs = new TaskCompletionSource<TResult>(state);
        task.ContinueWith(obj =>
        {
            if (task.IsFaulted) tcs.TrySetException(task.Exception.InnerExceptions);
            else if (task.IsCanceled) tcs.TrySetCanceled();
            else tcs.TrySetResult(task.Result);

            if (callback != null) callback(tcs.Task);
        }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
        return tcs.Task;
    }

    ////数值类型数组缓存
    //private static Type[] ta = new[] { typeof(Int16), typeof(Int32), typeof(Int64), typeof(Single), typeof(Double), typeof(UInt16), typeof(UInt32), typeof(UInt64), typeof(Byte), typeof(Decimal), typeof(SByte), typeof(UIntPtr), typeof(IntPtr)};
    ///// <summary>
    ///// 判断是否为数值类型。目前已知不支持BigInteger。这种方法是以空间换时间的缓存策略，有大量查询时性能会更优，反之则拖慢初始化。
    ///// </summary>
    ///// <param name="t">要判断的类型</param>
    ///// <returns>是否为数值类型</returns>
    //public static bool IsNumericType(this Type t)
    //{
    //    return Array.IndexOf(ta, t) >= 0;
    //}

    /// <summary>
    /// 判断是否为数值类型。目前已知不支持BigInteger。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为数值类型</returns>
    public static bool IsNumericType(this Type t)
    {
        var tc = Type.GetTypeCode(t);
        return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
    }

    /// <summary>
    /// 判断是否为可空数值类型。目前已知不支持BigInteger。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为可空数值类型</returns>
    public static bool IsNumericOrNullableNumericType(this Type t)
    {
        return t.IsNumericType() || (t.IsNullableType() && t.GetGenericArguments()[0].IsNumericType());
    }

    /// <summary>
    /// 判断是否为可空类型。
    /// 注意，直接调用可空对象的.GetType()方法返回的会是其泛型值的实际类型，用其进行此判断肯定返回false。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为可空类型</returns>
    public static bool IsNullableType(this Type t)
    {
        return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>
    /// 通过反射获取对象的指定属性值
    /// </summary>
    /// <param name="o"></param>
    /// <param name="属性名">属性名</param>
    /// <returns>属性值</returns>
    public static object GetPropertyValue(this object o, string 属性名)
    {
        var t = o.GetType();
        return t.GetProperty(属性名).GetValue(o, null);
    }

    /// <summary>
    /// 通过反射获取对象的指定字段值
    /// </summary>
    /// <param name="o"></param>
    /// <param name="字段名">字段名</param>
    /// <returns>字段值</returns>
    public static object GetFieldValue(this object o, string 字段名)
    {
        var t = o.GetType();
        return t.GetField(字段名).GetValue(o);
    }

    /// <summary>
    /// 通过反射获得类型名称及全部属性名值的输出
    /// </summary>
    /// <param name="o"></param>
    /// <returns>类型名称及全部属性名值的输出</returns>
    public static string GetTypeNameAndAllPropertyInfo(this object o)
    {
        StringBuilder sb = new StringBuilder();
        var t = o.GetType();
        sb.AppendLine(t.FullName + "类型对象");
        foreach (System.Reflection.PropertyInfo p in t.GetProperties())
        {
            sb.AppendLine(p.Name + ":" + p.GetValue(o, null));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 返回该字符串的CRC32代码
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的CRC32代码</returns>
    // ReSharper disable once InconsistentNaming
    public static ulong GetCRC32Code(this string s)
    {
        return new CRC32().StringCRC(s);
    }

    /// <summary>
    /// 返回该字符串的哈希字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的哈希字符串</returns>
    public static string GetHashString(this string s)
    {
        return Cryptography.GetPWDHash(s);
    }

    /// <summary>
    /// 返回该字符串的加密字符串（对称加密）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的加密字符串</returns>
    public static string GetEncodeCryptographyString(this string s)
    {
        return Cryptography.GetEncrypt(s);
    }

    /// <summary>
    /// 返回该字符串的解密字符串（对称解密）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的解密字符串</returns>
    public static string GetDecodeCryptographyString(this string s)
    {
        return Cryptography.GetDecrypt(s);
    }

    /// <summary>
    /// 返回该字符串的MD5编码字符串
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的MD5编码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetMD5String(this string s)
    {
        return MD5.HashString(s);
    }

    /// <summary>
    /// 返回该字符串的BASE64编码字符串（编码为ASCII文本，用于网络传输）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的BASE64编码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetEncodeBASE64String(this string s)
    {
        return BASE64.EncryptString(s);
    }

    /// <summary>
    /// 返回该字符串的BASE64解码字符串（解码自ASCII文本，用于网络传输）
    /// </summary>
    /// <param name="s"></param>
    /// <returns>该字符串的BASE64解码字符串</returns>
    // ReSharper disable once InconsistentNaming
    public static string GetDecodeBASE64String(this string s)
    {
        return BASE64.DecryptString(s);
    }

    /// <summary>
    /// 将对象的字符串形式输出到指定目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t, string 输出目录)
    {
        StreamWriter sw = new StreamWriter(输出目录.AsPathString().Combine("log.txt"), true);
        sw.WriteLine("{0} : {1}".FormatWith(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), t == null ? "[Null]" : t.ToString()));
        sw.Close();
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到指定目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t, string 输出目录, Func<T, object> 表达式)
    {
        表达式(t).Log(输出目录);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t)
    {
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, Func<T, object> 表达式)
    {
        var o = 表达式(t);
        System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString());
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, string 分类)
    {
        System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString(), 分类);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    public static T Trace<T>(this T t, Func<T, object> 表达式, string 分类)
    {
        var o = 表达式(t);
        System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString(), 分类);
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    ///<param name="格式化字符串">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    public static T TraceFormat<T>(this T t, string 格式化字符串)
    {
        System.Diagnostics.Trace.WriteLine(string.Format(格式化字符串, t == null ? "[Null]" : t.ToString()));
        return t;
    }

    /// <summary>
    /// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    /// </summary>
    ///<param name="格式化字符串">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    public static T TraceFormat<T>(this T t, string 格式化字符串, string 分类)
    {
        System.Diagnostics.Trace.WriteLine(string.Format(格式化字符串, t == null ? "[Null]" : t.ToString()), 分类);
        return t;
    }

    /// <summary>
    /// 生成随机枚举值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <returns>随机枚举值</returns>
    public static T NextEnum<T>(this Random random)
    where T : struct
    {
        Type type = typeof(T);
        if (type.IsEnum == false) throw new InvalidOperationException();

        var array = Enum.GetValues(type);
        var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
        return (T)array.GetValue(index);
    }

    #endregion
}

