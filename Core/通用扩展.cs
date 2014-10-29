using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Reflection;
using Core.Encryption;

public static partial class 通用扩展
{
    #region 基本

    /// <summary>
    /// 判断是否为数值类型。
    /// </summary>
    /// <param name="t">要判断的类型</param>
    /// <returns>是否为数值类型</returns>
    public static bool IsNumericType(this Type t)
    {
        var tc = Type.GetTypeCode(t);
        return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
    }

    /// <summary>
    /// 判断是否为可空数值类型。
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

