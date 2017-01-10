using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Reflection;
using Core.Web;

public static partial class 通用扩展
{
    static Random R = new Random();

    #region 基本

    /// <summary>
    /// 追加集合到末尾，并返回自身
    /// </summary>
    public static List<T> AddRangeByLink<T>(this List<T> o, IEnumerable<T> items)
    {
        o.AddRange(items);
        return o;
    }

    /// <summary>
    /// 追加项到末尾，并返回自身
    /// </summary>
    public static List<T> AddByLink<T>(this List<T> o, T item)
    {
        o.Add(item);
        return o;
    }

    /// <summary>
    /// 连接集合中的所有数组，组成一个完整的大数组
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">数组集合</param>
    /// <returns>完整的大数组</returns>
    public static T[] ConnectAllArrays<T>(this IEnumerable<T[]> o)
    {
        var enumerable = o as T[][] ?? o.ToArray();
        var array = new T[enumerable.Sum(q => q.Length)];
        var x = 0;
        foreach (var f in enumerable)
        {
            Buffer.BlockCopy(f, 0, array, x, f.Length);
            x += f.Length;
        }
        return array;
    }

    /// <summary> 
    /// 将 Stream 转成 byte[] 
    /// </summary> 
    public static byte[] ToBytes(this Stream stream)
    {
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);

        // 设置当前流的位置为流的开始 
        stream.Seek(0, SeekOrigin.Begin);
        return bytes;
    }

    /// <summary> 
    /// 将 byte[] 转成 MemoryStream 
    /// </summary> 
    public static MemoryStream ToMemoryStream(this byte[] bytes)
    {
        return new MemoryStream(bytes);
    }

    /// <summary>
    /// 将对象强制转换为布尔类型并返回，如果对象为空则返回false
    /// </summary>
    /// <param name="o">目标对象</param>
    /// <returns>布尔值</returns>
    public static bool AsBoolean(this object o)
    {
        return o != null && (bool)o;
    }

    /// <summary>
    /// 执行并释放对象，同using(……)关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">目标对象</param>
    /// <param name="执行操作">要执行的操作</param>
    public static void UsingRun<T>(this T o, Action<T> 执行操作) where T : IDisposable
    {
        using (o)
        {
            执行操作(o);
        }
    }

    /// <summary>
    /// 执行并返回值，然后释放对象，同using(……)关键字
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <typeparam name="TR">返回值类型</typeparam>
    /// <param name="o">目标对象</param>
    /// <param name="执行操作">要执行的操作</param>
    public static TR UsingRunAndReturn<T, TR>(this T o, Func<T, TR> 执行操作) where T : IDisposable
    {
        using (o)
        {
            return 执行操作(o);
        }
    }

    /// <summary>
    /// 根据布尔值分别返回不同结果
    /// </summary>
    /// <typeparam name="T">返回结果类型</typeparam>
    /// <param name="b">布尔值变量</param>
    /// <param name="当为真时返回结果">当为真时返回结果</param>
    /// <param name="当为假时返回结果">当为假时返回结果</param>
    /// <returns>返回结果</returns>
    public static T SwitchReturn<T>(this bool b, T 当为真时返回结果, T 当为假时返回结果)
    {
        return b ? 当为真时返回结果 : 当为假时返回结果;
    }

    /// <summary>
    /// 根据可空布尔值分别返回不同结果
    /// </summary>
    /// <typeparam name="T">返回结果类型</typeparam>
    /// <param name="b">布尔值变量</param>
    /// <param name="当为真时返回结果">当为真时返回结果</param>
    /// <param name="当为假时返回结果">当为假时返回结果</param>
    /// <param name="当为空时返回结果">当为空时返回结果</param>
    /// <returns>返回结果</returns>
    public static T SwitchReturn<T>(this bool? b, T 当为真时返回结果, T 当为假时返回结果, T 当为空时返回结果)
    {
        return b == null ? 当为空时返回结果 : b.Value.SwitchReturn(当为真时返回结果, 当为假时返回结果);
    }

    /// <summary>
    /// 判断此GUID变量是否为默认值
    /// </summary>
    /// <param name="guid">GUID变量</param>
    /// <returns>是否为默认值</returns>
    public static bool IsDefaultValue(this Guid guid)
    {
        return guid == new Guid();
    }

    /// <summary>
    /// 获取集合的子集
    /// </summary>
    /// <param name="tc">目标集合</param>
    /// <param name="子集起始索引">子集由此索引开始</param>
    /// <param name="子集长度">子集从起始位置获得此数量的项目</param>
    /// <returns>子集</returns>
    public static IEnumerable GetSubset(this IEnumerable tc, int 子集起始索引, int 子集长度)
    {
        var x = 0;
        var max = 子集起始索引 + 子集长度;
        foreach (var item in tc)
        {
            if (x >= 子集起始索引)
            {
                if (x >= max) break;
                yield return item;
            }
            x++;
        }
    }

    /// <summary>
    /// 获取集合的子集
    /// </summary>
    /// <param name="tc">目标集合</param>
    /// <param name="子集起始索引">子集由此索引开始</param>
    /// <param name="子集长度">子集从起始位置获得此数量的项目</param>
    /// <returns>子集</returns>
    public static IEnumerable<T> GetSubset<T>(this IEnumerable<T> tc, int 子集起始索引, int 子集长度)
    {
        var x = 0;
        var max = 子集起始索引 + 子集长度;
        foreach (var item in tc)
        {
            if (x >= 子集起始索引)
            {
                if (x >= max) break;
                yield return item;
            }
            x++;
        }
    }

    /// <summary>
    /// 当对象非空时返回其ToString()方法，否则返回空字符串String.Empty
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>字符串形式</returns>
    public static string ToStringSafety(this object o)
    {
        if (o == null) return String.Empty;
        else return o.ToString();
    }

    /// <summary>
    /// 乱序步进循环操作
    /// </summary>
    /// <param name="i"></param>
    /// <param name="最大值">最大值</param>
    /// <param name="步进值">步进值</param>
    /// <param name="行为">循环操作行为</param>
    public static void RandomFor(this int i, int 最大值, int 步进值, Action<int> 行为)
    {
        var li = new List<int>();
        for (int j = i; j < 最大值; j += 步进值)
        {
            li.Add(j);
        }
        foreach (var f in li.Random())
        {
            行为(f);
        }
    }

    /// <summary>
    /// 添加对象的字符串形式到末尾新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="增加的对象">要添加的对象，取用其字符串形式</param>
    /// <returns>组合的字符串</returns>
    public static string AppendLine(this string s, object 增加的对象)
    {
        return s + "\r\n" + 增加的对象;
    }

    /// <summary>
    /// 添加多个对象的字符串形式到末尾，每个对象都置于新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="增加的对象">要添加的对象，取用其字符串形式</param>
    /// <returns>组合的字符串</returns>
    public static string AppendLine(this string s, params object[] 增加的对象)
    {
        var sb = new StringBuilder();
        sb.AppendLine(s);
        foreach (var f in 增加的对象)
        {
            sb.AppendLine(f == null ? "null" : f.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 判断是否为空引用或空集合
    /// </summary>
    /// <returns>是否为空引用或空集合</returns>
    public static bool IsNullOrEmpty(this IEnumerable o)
    {
        return o == null || o.GetEnumerator().MoveNext() == false;
    }

    /// <summary>
    /// 判断是否为空引用或空集合
    /// </summary>
    /// <returns>是否为空引用或空集合</returns>
    public static bool IsNullOrEmpty(this IEnumerator o)
    {
        return o == null || o.MoveNext() == false;
    }

    /// <summary>
    /// 输出为UTC时间的ISO8601格式字符串，例如：2010-08-20T15:00:00Z
    /// </summary>
    /// <param name="s"></param>
    /// <returns>UTC时间的ISO8601格式字符串</returns>
    public static string ToUtcStringByISO8601(this DateTime s)
    {
        return s.ToUniversalTime().ToString(@"yyyy-MM-dd\THH:mm:ss\Z");
    }

    /// <summary>
    /// 等同于：对象 as T
    /// </summary>
    public static T As<T>(this object o) where T : class
    {
        return o as T;
    }

    /// <summary>
    /// 同原生的Add方法，区别在于添加项后会返回自身，使得允许链式编程以连续添加项。
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="d"></param>
    /// <param name="键">键</param>
    /// <param name="值">值</param>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> AddByLink<TKey, TValue>(this IDictionary<TKey, TValue> d, TKey 键, TValue 值)
    {
        d.Add(键, 值);
        return d;
    }

    /// <summary>
    /// 添加内容到StringBuilder末尾，如果此StringBuilder中已有内容，则会先添加一个分隔符，再添加内容。
    /// </summary>
    /// <param name="添加内容">要添加到末尾的内容</param>
    /// <param name="分隔符">例如顿号、逗号、分号、空格等</param>
    /// <returns></returns>
    public static StringBuilder Append(this StringBuilder s, object 添加内容, string 分隔符)
    {
        return s.AppendAndSeparated(添加内容, 分隔符);
    }

    /// <summary>
    /// 添加内容到StringBuilder末尾，如果此StringBuilder中已有内容，则会先添加一个分隔符，再添加内容。
    /// </summary>
    /// <param name="添加内容">要添加到末尾的内容</param>
    /// <param name="分隔符">例如顿号、逗号、分号、空格等</param>
    /// <returns></returns>
    public static StringBuilder AppendAndSeparated(this StringBuilder s, object 添加内容, string 分隔符)
    {
        if (s.Length > 0) s.Append(分隔符);
        s.Append(添加内容);
        return s;
    }

    /// <summary>
    /// 将集合展开并以ToString形式拼接
    /// </summary>
    /// <param name="间隔字符">拼接时的间隔字符</param>
    /// <returns>拼接后的字符串</returns>
    public static string ExpandAndToString(this IEnumerable s, string 间隔字符)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var f in s)
        {
            if (sb.Length > 0) sb.Append(间隔字符);
            sb.Append(f.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// 如果字符串内容是Uri，则返回此Uri的ToString()形式，否则将视为本地路径，从而转换为“file:///”形式返回。
    /// </summary>
    public static string ToUrl(this string s)
    {
        try
        {
            return new Uri(s).ToString();
        }
        catch
        {
            return new Uri(s.AsPathString().ToLocalFileUri()).ToString();
        }
    }

    /// <summary>
    /// 反转逻辑值
    /// </summary>
    public static bool Reverse(this bool b)
    {
        return !b;
    }

    /// <summary>
    /// 检测字符串是否为null或空字符串
    /// </summary>
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrEmpty(s);
    }

    /// <summary>
    /// 检测字符串是否为null或空白字符串（Trim()之后长度为0）
    /// </summary>
    public static bool IsNullOrEmptyOrWhitespace(this string s)
    {
        return string.IsNullOrEmpty(s) || s.Trim().Length == 0;
    }

    /// <summary>
    /// 当字符串为null或空字符串时执行自定义表达式
    /// </summary>
    public static void IsNullOrEmptyThen(this string s, Action<string> 表达式)
    {
        if (string.IsNullOrEmpty(s)) 表达式(s);
    }

    /// <summary>
    /// 当字符串为null或空字符串时执行自定义表达式，并返回处理后的字符串；否则返回其自身
    /// </summary>
    public static string IsNullOrEmptyThen(this string s, Func<string, string> 表达式)
    {
        if (string.IsNullOrEmpty(s)) return 表达式(s);
        return s;
    }

    /// <summary>
    /// 当字符串为null或空字符串时返回指定的字符串；否则返回其自身
    /// </summary>
    public static string IsNullOrEmptyThen(this string s, string 替代字符串)
    {
        if (string.IsNullOrEmpty(s)) return 替代字符串;
        return s;
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, params object[] 格式化参数)
    {
        return string.Format(s, 格式化参数);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object 格式化参数1)
    {
        return string.Format(s, 格式化参数1);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object 格式化参数1, object 格式化参数2)
    {
        return string.Format(s, 格式化参数1, 格式化参数2);
    }

    /// <summary>
    /// 将字符串格式化并返回
    /// </summary>
    public static string FormatWith(this string s, object 格式化参数1, object 格式化参数2, object 格式化参数3)
    {
        return string.Format(s, 格式化参数1, 格式化参数2, 格式化参数3);
    }

    /// <summary>
    /// 验证是否匹配
    /// </summary>
    public static bool RegexIsMatch(this string s, string 表达式)
    {
        return Regex.IsMatch(s, 表达式);
    }

    /// <summary>
    /// 验证是否匹配
    /// </summary>
    public static bool RegexIsMatch(this string s, string 表达式, RegexOptions 选项)
    {
        return Regex.IsMatch(s, 表达式, 选项);
    }

    /// <summary>
    /// 获取一个匹配项
    /// </summary>
    public static Match RegexMatch(this string s, string 表达式)
    {
        return Regex.Match(s, 表达式);
    }

    /// <summary>
    /// 获取一个匹配项
    /// </summary>
    public static Match RegexMatch(this string s, string 表达式, RegexOptions 选项)
    {
        return Regex.Match(s, 表达式, 选项);
    }

    /// <summary>
    /// 获取所有匹配项
    /// </summary>
    public static MatchCollection RegexMatches(this string s, string 表达式)
    {
        return Regex.Matches(s, 表达式);
    }

    /// <summary>
    /// 获取所有匹配项
    /// </summary>
    public static MatchCollection RegexMatches(this string s, string 表达式, RegexOptions 选项)
    {
        return Regex.Matches(s, 表达式, 选项);
    }

    /// <summary>
    /// 以匹配项拆分字符串
    /// </summary>
    public static string[] RegexSplit(this string s, string 表达式)
    {
        return Regex.Split(s, 表达式);
    }

    /// <summary>
    /// 以匹配项拆分字符串
    /// </summary>
    public static string[] RegexSplit(this string s, string 表达式, RegexOptions 选项)
    {
        return Regex.Split(s, 表达式, 选项);
    }

    /// <summary>
    /// 替换匹配项为新值
    /// </summary>
    public static string RegexReplace(this string s, string 表达式, string 替换值)
    {
        return Regex.Replace(s, 表达式, 替换值);
    }

    /// <summary>
    /// 替换匹配项为新值
    /// </summary>
    public static string RegexReplace(this string s, string 表达式, string 替换值, RegexOptions 选项)
    {
        return Regex.Replace(s, 表达式, 替换值, 选项);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他任意某个字符串
    /// </summary>
    public static bool IsContainsAny(this string s, int 起始位置, int 检查字符总数, StringComparison 查询规则, params string[] 字符串)
    {
        return s.IsAnyMatch((a, b) => a.IndexOf(b, 起始位置, 检查字符总数, 查询规则) >= 0, 字符串);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他任意某个字符串，忽略大小写差异
    /// </summary>
    public static bool IsContainsAny(this string s, params string[] 字符串)
    {
        return s.IsContainsAny(0, s.Length, StringComparison.OrdinalIgnoreCase, 字符串);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他所有字符串
    /// </summary>
    public static bool IsContainsAll(this string s, int 起始位置, int 检查字符总数, StringComparison 查询规则, params string[] 字符串)
    {
        return s.IsAllMatch((a, b) => a.IndexOf(b, 起始位置, 检查字符总数, 查询规则) >= 0, 字符串);
    }

    /// <summary>
    /// 判断此字符串中是否包含其他所有字符串，忽略大小写差异
    /// </summary>
    public static bool IsContainsAll(this string s, params string[] 字符串)
    {
        return s.IsContainsAll(0, s.Length, StringComparison.OrdinalIgnoreCase, 字符串);
    }

    /// <summary>
    /// 执行动作后返回自身。适用于链式编程。
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">对象</param>
    /// <param name="act">一个无返回值动作</param>
    /// <returns>自身</returns>
    public static T Do<T>(this T o, Action<T> act)
    {
        act(o);
        return o;
    }

    /// <summary>
    /// 依据权重值调整概率，从集合中随机抽取一个项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">集合</param>
    /// <param name="getWeightedFunc">获取权重值的方法</param>
    /// <returns>随机抽取到的一个项</returns>
    public static T RandomExtractOneByWeighted<T>(this IEnumerable<T> o, Func<T, int> getWeightedFunc)
    {
        var enumerable = o.ToArray();
        var total = enumerable.Sum(getWeightedFunc);
        var r = R.Next(total);
        var v = 0;
        foreach (var f in enumerable)
        {
            var fw = getWeightedFunc(f);
            if (v <= r && v + fw > r)
            {
                return f;
            }
            v += fw;
        }
        return enumerable.FirstOrDefault();
    }

    /// <summary>
    /// 返回经随机排序后的集合
    /// </summary>
    public static IEnumerable<T> Random<T>(this IEnumerable<T> o)
    {
        //var c = o.Count();
        //var l = new List<int>();
        //for (int i = 0; i < c; i++)
        //{
        //    l.Add(i);
        //}
        //while (l.Count > 0)
        //{
        //    var i = l[R.Next(l.Count)];
        //    l.Remove(i);
        //    yield return o.ElementAt(i);
        //}
        return o.OrderBy(q => R.Next());
    }

    /// <summary>
    /// 判断一个值是否存在于提供的多个值中
    /// </summary>
    public static bool IsIn<T>(this T t, params T[] 判断依据)
    {
        return 判断依据.Contains(t);
    }

    /// <summary>
    /// 判断一个值是否存在于提供的多个值中
    /// </summary>
    public static bool IsIn<T>(this T t, IEnumerable<T> 判断依据)
    {
        return 判断依据.Contains(t);
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的某一项
    /// </summary>
    /// <param name="判断表达式">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAnyMatch<T, C>(this T t, Func<T, C, bool> 判断表达式, params C[] 判断依据)
    {
        return 判断依据.Any(f => 判断表达式(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的某一项
    /// </summary>
    /// <param name="判断表达式">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAnyMatch<T, C>(this T t, Func<T, C, bool> 判断表达式, IEnumerable<C> 判断依据)
    {
        return 判断依据.Any(f => 判断表达式(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的所有项
    /// </summary>
    /// <param name="判断表达式">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAllMatch<T, C>(this T t, Func<T, C, bool> 判断表达式, params C[] 判断依据)
    {
        return 判断依据.All(f => 判断表达式(t, f));
    }

    /// <summary>
    /// 根据表达式判定是否符合判断依据中的所有项
    /// </summary>
    /// <param name="判断表达式">第一个参数为原值，第二个参数为判断依据</param>
    public static bool IsAllMatch<T, C>(this T t, Func<T, C, bool> 判断表达式, IEnumerable<C> 判断依据)
    {
        return 判断依据.All(f => 判断表达式(t, f));
    }

    ///// <summary>
    ///// 判断一个值是否存在于提供的多个值中
    ///// </summary>
    //public static bool In<T>(this T t, T 值1, T 值2)
    //{
    //    return t.In(值1, 值2);
    //}

    ///// <summary>
    ///// 判断一个值是否存在于提供的多个值中
    ///// </summary>
    //public static bool In<T>(this T t, T 值1, T 值2, T 值3)
    //{
    //    return t.In(值1, 值2, 值3);
    //}

    ///// <summary>
    ///// 判断一个值是否存在于提供的多个值中
    ///// </summary>
    //public static bool In<T>(this T t, T 值1, T 值2, T 值3, T 值4)
    //{
    //    return t.In(值1, 值2, 值3, 值4);
    //}

    ///// <summary>
    ///// 判断一个值是否存在于提供的多个值中
    ///// </summary>
    //public static bool In<T>(this T t, T 值1, T 值2, T 值3, T 值4, T 值5)
    //{
    //    return t.In(值1, 值2, 值3, 值4, 值5);
    //}

    ///// <summary>
    ///// 判断一个值是否介于两值之间（与两值中的任意一个相等也返回true）
    ///// </summary>
    //public static bool InRange<T>(this IComparable<T> t, T 最小值, T 最大值)
    //{
    //    return t.CompareTo(最小值) >= 0 && t.CompareTo(最大值) <= 0;
    //}

    ///// <summary>
    ///// 判断一个值是否介于两值之间（与两值中的任意一个相等也返回true）
    ///// </summary>
    //public static bool InRange(this IComparable t, object 最小值, object 最大值)
    //{
    //    return t.CompareTo(最小值) >= 0 && t.CompareTo(最大值) <= 0;
    //}

    /// <summary>
    /// 判断值是否结语两值之间
    /// </summary>
    public static bool IsBetween<T>(this T t, T 最小值, T 最大值,
    bool 包含最小值, bool 包含最大值)
        where T : IComparable<T>
    {
        if (t == null) throw new ArgumentNullException("t");

        var lowerCompareResult = t.CompareTo(最小值);
        var upperCompareResult = t.CompareTo(最大值);

        return (包含最小值 && lowerCompareResult == 0) ||
            (包含最大值 && upperCompareResult == 0) ||
            (lowerCompareResult > 0 && upperCompareResult < 0);
    }

    /// <summary>
    /// 判断值是否结语两值之间（与两值中的任意一个相等也返回true）
    /// </summary>
    public static bool IsBetween<T>(this T t, T 最小值, T 最大值) where T : IComparable<T>
    {
        return t.IsBetween(最小值, 最大值, true, true);
    }

    /// <summary>
    /// 遍历集合，返回第一个符合判断条件的项目的索引位置
    /// </summary>
    public static int IndexOf<T>(this IEnumerable<T> source, Predicate<T> 判断条件)
    {
        int x = 0;
        foreach (T element in source)
        {
            if (判断条件(element)) return x;
            x++;
        }
        return -1;
    }

    /// <summary>
    /// 遍历集合，执行传入表达式
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> 操作)
    {
        foreach (T element in source)
            操作(element);
    }

    /// <summary>
    /// 遍历集合，执行传入表达式
    /// </summary>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> 操作)
    {
        int i = 0;
        foreach (T element in source)
            操作(element, i++);
    }

    /// <summary>
    /// 转换为List对象
    /// </summary>
    public static List<T> ToList<T>(this IEnumerable<T> source)
    {
        var l = new List<T>();
        foreach (var f in source)
        {
            l.Add(f);
        }
        return l;
    }

    ///// <summary>
    ///// 将对象的字符串形式输出到指定目录的log.txt文件中
    ///// </summary>
    //public static T Log<T>(this T t, string 输出目录)
    //{
    //    StreamWriter sw = new StreamWriter(输出目录.AsPathString().Combine("log.txt"), true);
    //    sw.WriteLine("{0} : {1}".FormatWith(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), t == null ? "[Null]" : t.ToString()));
    //    sw.Close();
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到指定目录的log.txt文件中
    ///// </summary>
    //public static T Log<T>(this T t, string 输出目录, Func<T, object> 表达式)
    //{
    //    表达式(t).Log(输出目录);
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    //public static T Trace<T>(this T t)
    //{
    //    System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString());
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    //public static T Trace<T>(this T t, Func<T, object> 表达式)
    //{
    //    var o = 表达式(t);
    //    System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString());
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    //public static T Trace<T>(this T t, string 分类)
    //{
    //    System.Diagnostics.Trace.WriteLine(t == null ? "[Null]" : t.ToString(), 分类);
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    //public static T Trace<T>(this T t, Func<T, object> 表达式, string 分类)
    //{
    //    var o = 表达式(t);
    //    System.Diagnostics.Trace.WriteLine(o == null ? "[Null]" : o.ToString(), 分类);
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    /////<param name="格式化字符串">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    //public static T TraceFormat<T>(this T t, string 格式化字符串)
    //{
    //    System.Diagnostics.Trace.WriteLine(string.Format(格式化字符串, t == null ? "[Null]" : t.ToString()));
    //    return t;
    //}

    ///// <summary>
    ///// 将对象的字符串形式输出到调试输出窗口中，并将该对象返回
    ///// </summary>
    /////<param name="格式化字符串">用于格式化的字符串，以{0}表示此对象的显示位置</param>
    //public static T TraceFormat<T>(this T t, string 格式化字符串, string 分类)
    //{
    //    System.Diagnostics.Trace.WriteLine(string.Format(格式化字符串, t == null ? "[Null]" : t.ToString()), 分类);
    //    return t;
    //}

    /// <summary>
    /// 展开为字符串
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">集合</param>
    /// <param name="间隔字符串">每个项之间的间隔符号</param>
    /// <param name="获取字符串表达式">由对象获取相应字符串</param>
    /// <returns>展开并组合后的字符串</returns>
    public static string ExpandToString<T>(this IEnumerable<T> o, string 间隔字符串, Func<T, string> 获取字符串表达式)
    {
        StringBuilder S = new StringBuilder();
        foreach (T f in o)
        {
            if (S.Length > 0) S.Append(间隔字符串);
            S.Append(获取字符串表达式(f));
        }
        return S.ToString();
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable o, Func<T, IEnumerable<T>> 递归项选取表达式)
    {
        return RecursionEachSelect(o, 递归项选取表达式, null);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable o, Func<T, IEnumerable<T>> 递归项选取表达式, Predicate<T> 检验表达式)
    {
        return RecursionEachSelect(o.Cast<T>(), 递归项选取表达式, 检验表达式);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> 递归项选取表达式)
    {
        return RecursionEachSelect(o, 递归项选取表达式, null);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> 递归项选取表达式, Predicate<T> 检验表达式)
    {
        return RecursionEachSelect(o, 递归项选取表达式, 检验表达式, true);
    }

    /// <summary>
    /// 循环所有子项，递归选取并返回所有后代项，针对IEnumerable泛型形式
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="检验失败是否继续">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionEachSelect<T>(this IEnumerable<T> o, Func<T, IEnumerable<T>> 递归项选取表达式, Predicate<T> 检验表达式, bool 检验失败是否继续)
    {
        foreach (var f in o)
        {
            if (检验表达式 == null || 检验表达式(f)) yield return f;
            else if (!检验失败是否继续)
            {
                yield break;
            }
            foreach (var d in RecursionSelect(f, 递归项选取表达式, 检验表达式))
            {
                yield return d;
            }
        }
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> 递归项选取表达式)
    {
        return RecursionSelect(o, 递归项选取表达式, null);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> 递归项选取表达式, Predicate<T> 检验表达式)
    {
        return RecursionSelect(o, 递归项选取表达式, 检验表达式, true);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="检验失败是否继续">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, T> 递归项选取表达式, Predicate<T> 检验表达式, bool 检验失败是否继续)
    {
        if (o == null) yield break;
        var f = 递归项选取表达式(o);
        if (检验表达式 == null || 检验表达式(f)) yield return f;
        else if (!检验失败是否继续)
        {
            yield break;
        }
        foreach (var d in RecursionSelect(f, 递归项选取表达式, 检验表达式, 检验失败是否继续))
        {
            yield return d;
        }
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> 递归项选取表达式)
    {
        return RecursionSelect(o, 递归项选取表达式, null);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> 递归项选取表达式, Predicate<T> 检验表达式)
    {
        return RecursionSelect(o, 递归项选取表达式, 检验表达式, true);
    }

    /// <summary>
    /// 递归选取并返回后代项
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="递归项选取表达式">通过此表达式选取要返回的子项</param>
    /// <param name="检验表达式">対返回项进行检验，返回代入此表达式后表达式成立的项</param>
    /// <param name="检验失败是否继续">指示表达式检验失败后是否继续递归选取接下来的项，在不指定此参数的重载形式中默认为true</param>
    /// <returns>选取的子项</returns>
    public static IEnumerable<T> RecursionSelect<T>(this T o, Func<T, IEnumerable<T>> 递归项选取表达式, Predicate<T> 检验表达式, bool 检验失败是否继续)
    {
        foreach (var f in 递归项选取表达式(o))
        {
            if (检验表达式 == null || 检验表达式(f)) yield return f;
            else if (!检验失败是否继续)
            {
                yield break;
            }
            foreach (var d in RecursionSelect(f, 递归项选取表达式, 检验表达式))
            {
                yield return d;
            }
        }
    }

    /// <summary>
    /// 清除列表内所有项，克隆目标集合的所有项到列表，以保持两个处内容一致。
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    /// <param name="克隆源">克隆目标集合</param>
    public static void CloneItemsForm<T>(this List<T> o, IEnumerable<T> 克隆源)
    {
        o.Clear();
        o.AddRange(克隆源);
    }

    /// <summary>
    /// 生成随机布尔值
    /// </summary>
    /// <returns>随机布尔值</returns>
    public static bool NextBool(this Random random)
    {
        return random.Next(2) == 0;
    }

    ///// <summary>
    ///// 生成随机枚举值
    ///// </summary>
    ///// <typeparam name="T">枚举类型</typeparam>
    ///// <returns>随机枚举值</returns>
    //public static T NextEnum<T>(this Random random)
    //where T : struct
    //{
    //    Type type = typeof(T);
    //    if (type.IsEnum == false) throw new InvalidOperationException();

    //    var array = Enum.GetValues(type);
    //    var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
    //    return (T)array.GetValue(index);
    //}

    /// <summary>
    /// 生成随机byte数组
    /// </summary>
    /// <param name="length">数组长度</param>
    /// <returns>随机byte数组</returns>
    public static byte[] NextBytes(this Random random, int length)
    {
        var data = new byte[length];
        random.NextBytes(data);
        return data;
    }

    /// <summary>
    /// 生成随机UInt16值
    /// </summary>
    /// <returns>随机UInt16值</returns>
    public static UInt16 NextUInt16(this Random random)
    {
        return BitConverter.ToUInt16(random.NextBytes(2), 0);
    }

    /// <summary>
    /// 生成随机Int16值
    /// </summary>
    /// <returns>随机Int16值</returns>
    public static Int16 NextInt16(this Random random)
    {
        return BitConverter.ToInt16(random.NextBytes(2), 0);
    }

    /// <summary>
    /// 生成随机Float值
    /// </summary>
    /// <returns>随机Float值</returns>
    public static float NextFloat(this Random random)
    {
        return BitConverter.ToSingle(random.NextBytes(4), 0);
    }

    /// <summary>
    /// 生成随机时间
    /// </summary>
    /// <param name="minValue">最小值</param>
    /// <param name="maxValue">最大值</param>
    /// <returns>随机时间</returns>
    public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
    {
        var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
        return new DateTime(ticks);
    }

    /// <summary>
    /// 生成随机时间
    /// </summary>
    /// <returns>随机时间</returns>
    public static DateTime NextDateTime(this Random random)
    {
        return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
    }

    //可以再编写生成随机字符串、随机颜色等功能的方法

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this double v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static long CeilingToLong(this double v)
    {
        return (long)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this float v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回大于或等于该值的最小整数
    /// </summary>
    public static int CeilingToInt(this decimal v)
    {
        return (int)Math.Ceiling(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this double v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static long FloorToLong(this double v)
    {
        return (long)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this float v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回小于或等于该值的最大整数
    /// </summary>
    public static int FloorToInt(this decimal v)
    {
        return (int)Math.Floor(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this double v)
    {
        return (int)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static long RoundToLong(this double v)
    {
        return (long)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this float v)
    {
        return (int)Math.Round(v);
    }

    /// <summary>
    /// 返回舍入后的最接近该值的整数
    /// </summary>
    public static int RoundToInt(this decimal v)
    {
        return (int)Math.Round(v);
    }

    #endregion

    #region 特殊字符串

    public interface ISpecialString
    {
        string Value { get; set; }
    }

    /// <summary>
    /// 特殊字符串基类
    /// </summary>
    public abstract class SpecialString : ISpecialString
    {
        public SpecialString()
        {

        }

        public SpecialString(string value)
        {
            _Value = value;
        }

        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        private string _Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    /// <summary>
    /// 转换为特殊字符串类型
    /// </summary>
    public static T As<T>(this string s) where T : SpecialString, new()
    {
        var o = new T();
        o.Value = s;
        return o;
    }

    public class UriString : SpecialString
    {
        /// <summary>
        /// 将绝对Uri与相对路径组合。
        /// 如果传入的是绝对路径，则原样返回。
        /// 通常用于处理网页内的相对路径超链接，如将“http://abc.com”或“http://abc.com/index.htm”与“info.htm”组合的话，就会生成“http://abc.com/info.htm”
        /// </summary>
        /// <param name="RelativePath">待组合的相对路径，可以是“../abc.htm”形式</param>
        public string CombineRelativePath(string RelativePath)
        {
            //try
            //{
            //    return new Uri(RelativePath).AbsoluteUri;
            //}
            //catch
            //{
            //var u = new Uri(Value);
            //return new Uri(u.LocalPath.EndsWith("/") ? u : new Uri(Path.GetDirectoryName(Value).Replace(@"\", "/").Replace(":/", "://")), RelativePath).AbsoluteUri;
            return new Uri(new Uri(Value), RelativePath).AbsoluteUri;
            //}
        }

        /// <summary>
        /// 转换本地文件Uri为本地路径格式。
        /// 如“file:///C:/abc/avatar.xml”将被转换为“C:\abc\avatar.xml”。
        /// 如果该Uri不是本地文件Uri，那么将抛出异常。
        /// </summary>
        public string ToLocalFilePath()
        {
            if (!Value.ToLower().StartsWith("file:///")) throw new Exception("这不是一个本地文件Uri");
            return Value.Substring(8).Replace("/", "\\");
        }

        /// <summary>
        /// 获取Url参数名值字典
        /// </summary>
        public Dictionary<string, string> UrlParameterDictionary
        {
            get
            {
                var u = new Uri(Value);
                var d = new Dictionary<string, string>();
                foreach (var f in Regex.Split(u.Query, @"\&"))
                {
                    if (f.IsNullOrEmpty())
                    {
                        continue;
                    }
                    var q = Regex.Split(f, @"\=");
                    d.Add(q[0], q[1]);
                }
                return d;
            }
        }

        /// <summary>
        /// 以新的名值参数字典值替代当前Url参数
        /// </summary>
        /// <param name="替代Url参数名值字典">新的参数字典，如已有相同参数则覆盖为此字典中的新值</param>
        /// <param name="追加原参数中不存在的参数">当前参数中不存在新字典中的某参数时，是否予以追加</param>
        /// <param name="舍弃替代参数中没有的参数">是否将新参数字典中没有的参数舍弃</param>
        /// <returns>新的完整Url</returns>
        public string ReplaceUrlParameters(Dictionary<string, string> 替代Url参数名值字典, bool 追加原参数中不存在的参数, bool 舍弃替代参数中没有的参数)
        {
            var u = UrlParameterDictionary;
            foreach (var f in 替代Url参数名值字典.Keys)
            {
                if (追加原参数中不存在的参数 && !f.IsAnyMatch((q, c) => q.ToLower() == c.ToLower(), u.Keys.Cast<string>().ToArray()))
                {
                    u.Add(f, 替代Url参数名值字典[f]);
                }
            }
            foreach (var f in u.Keys.ToArray())
            {
                var t = 替代Url参数名值字典.Keys.FirstOrDefault(q => q.ToLower() == f.ToLower());
                if (t != null) u[f] = 替代Url参数名值字典[t];
                else if (舍弃替代参数中没有的参数)
                {
                    u.Remove(f);
                }
            }
            StringBuilder s = new StringBuilder();
            foreach (var f in u.Keys)
            {
                if (s.Length > 0)
                {
                    s.Append('&');
                }
                s.Append(f + "=" + u[f]);
            }
            return Regex.Replace(Value, @"\?.*$", "") + "?" + s;
        }

        /// <summary>
        /// 添加一个参数，如果参数已存在，其将被替换为新值
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <param name="参数值">参数值</param>
        /// <returns>新的UriString对象</returns>
        public UriString AddUrlParameter(string 参数名, string 参数值)
        {
            var d = new Dictionary<string, string>();
            d.Add(参数名, 参数值);
            return ReplaceUrlParameters(d, true, false).AsUriString();
        }

        /// <summary>
        /// 替换一个参数为新值，如果当前此参数不存在，则忽略
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <param name="参数值">参数值</param>
        /// <returns>新的UriString对象</returns>
        public UriString ReplaceUrlParameter(string 参数名, string 参数值)
        {
            var d = new Dictionary<string, string>();
            d.Add(参数名, 参数值);
            return ReplaceUrlParameters(d, false, false).AsUriString();
        }

        /// <summary>
        /// 移除一个参数，如果参数本身即不存在，则忽略
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <returns>新的UriString对象</returns>
        public UriString RemoveUrlParameter(string 参数名)
        {
            var u = UrlParameterDictionary;
            foreach (var f in u.Keys)
            {
                if (f.ToLower() == 参数名.ToLower())
                {
                    u.Remove(f);
                    break;
                }
            }
            return ReplaceUrlParameters(u, false, true).AsUriString();
        }
    }

    public static UriString AsUriString(this string s)
    {
        return s.As<UriString>();
    }

    public class PathString : SpecialString
    {
        /// <summary>
        /// 转换本地路径为本地文件Uri格式。
        /// 如“C:\abc\avatar.xml”将被转换为“file:///C:/abc/avatar.xml”。
        /// </summary>
        public string ToLocalFileUri()
        {
            return "file:///" + Value.Replace("\\", "/");
        }

        ///// <summary>
        ///// 将绝对路径与相对路径组合。
        ///// 如果传入的是绝对路径，则原样返回。
        ///// 通常用于处文件相对路径计算，如将“C:\abc\”或“C:\abc\a.txt”与“info.htm”组合的话，就会生成“C:\abc\info.htm”
        ///// </summary>
        ///// <param name="RelativePath">待组合的相对路径，可以是“..\abc.htm”形式</param>
        //public string CombineRelativePath(string RelativePath)
        //{
        //    //return ToLocalFileUri().AsUriString().CombineRelativePath(RelativePath.Replace("\\", "/")).AsUriString().ToLocalFilePath();
        //    return Value.AsPathString().Combine(RelativePath).AsPathString().FullPath;
        //}

        ///// <summary>
        ///// 获取完整路径，等同于Path.GetFullPath()
        ///// </summary>
        //public string FullPath
        //{
        //    get
        //    {
        //        return Path.GetFullPath(Value);
        //    }
        //}
        //private string _FullPath;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return Path.GetFileName(Value);
            }
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension
        {
            get
            {
                return Path.GetExtension(Value);
            }
        }

        /// <summary>
        /// 所在目录名，等同于Path.GetDirectoryName()
        /// </summary>
        public string DirectoryName
        {
            get
            {
                return Path.GetDirectoryName(Value);
            }
        }

        ///// <summary>
        ///// 返回文件是否存在
        ///// </summary>
        //public bool FileExists
        //{
        //    get
        //    {
        //        return File.Exists(Value);
        //    }
        //}

        ///// <summary>
        ///// 返回目录是否存在
        ///// </summary>
        //public bool DirectoryExists
        //{
        //    get
        //    {
        //        return Directory.Exists(Value);
        //    }
        //}

        /// <summary>
        /// 返回是否为绝对路径
        /// </summary>
        public bool IsPathRooted
        {
            get
            {
                return Path.IsPathRooted(Value);
            }
        }

        /// <summary>
        /// 拼接路径
        /// </summary>
        public string Combine(string 待拼接路径)
        {
            return Path.Combine(Value, 待拼接路径);
        }

        /// <summary>
        /// 不带扩展名的文件名
        /// </summary>
        public string FileNameWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Value);
            }
        }
    }

    public static PathString AsPathString(this string s)
    {
        return s.As<PathString>();
    }

    #endregion

    #region 其它
    /// <summary>
    /// 执行Switch操作
    /// </summary>
    public static Switch<T> Switch<T>(this T v)
    {
        return new Switch<T>(v);
    }

    /// <summary>
    /// 执行Switch操作，并传入一个方法用于处理返回结果
    /// </summary>
    /// <param name="Do">处理返回结果的方法，该方法将在每次执行CaseReturn并匹配成功时或执行DefaultReturn时调用，方法的第一个参数是新传入的返回值，第二个参数是当前的返回值</param>
    public static Case<T, R> Switch<T, R>(this T v, Func<R, R, R> Do)
    {
        return new Case<T, R>(v, Do);
    }
    #endregion
}

public abstract partial class SwitchCaseBase<T>
{
    protected bool DefaultSet;

    protected void CheckDefaultSet()
    {
        if (DefaultSet) throw new Exception("Default操作必须在方法链末端执行，在其后不得执行Case操作。");
    }

    protected virtual T Value
    {
        get
        {
            return _Value;
        }
    }
    protected T _Value;

    protected virtual bool IsBroke
    {
        get
        {
            return _IsBroke;
        }
    }
    protected bool _IsBroke;

    internal void Break()
    {
        _IsBroke = true;
    }
}

public partial class Switch<T> : SwitchCaseBase<T>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Value">原始值</param>
    public Switch(T Value)
    {
        _Value = Value;
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> CaseReturn<R>(T Value, Func<T, R> Run)
    {
        return CaseReturn(f => f.Equals(Value), Run);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> CaseReturn<R>(T Value, R ReturnValue)
    {
        return CaseReturn(f => f.Equals(Value), f => ReturnValue);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> CaseReturn<R>(Predicate<T> Check, R ReturnValue)
    {
        return CaseReturn(Check, f => ReturnValue, true);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> CaseReturn<R>(Predicate<T> Check, Func<T, R> Run)
    {
        return CaseReturn(Check, Run, true);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn<R>(T Value, Func<T, R> Run, bool Break)
    {
        return CaseReturn(f => f.Equals(Value), Run, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="ReturnValue">返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn<R>(T Value, R ReturnValue, bool Break)
    {
        return CaseReturn(f => f.Equals(Value), f => ReturnValue, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="ReturnValue">返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn<R>(Predicate<T> Check, R ReturnValue, bool Break)
    {
        return CaseReturn(Check, f => ReturnValue, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param> 
    public Case<T, R> CaseReturn<R>(Predicate<T> Check, Func<T, R> Run, bool Break)
    {
        CheckDefaultSet();
        var r = new Case<T, R>(this.Value, this.IsBroke, this.DefaultSet);
        if (IsBroke)
        {
            return r;
        }
        if (Check(Value))
        {
            r.SetReturnValue(Run(Value));
            if (Break) r.Break();
        }
        return r;
    }

    /// <summary>
    /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> DefaultReturn<R>(R ReturnValue)
    {
        return DefaultReturn(f => ReturnValue);
    }

    /// <summary>
    /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> DefaultReturn<R>(Func<T, R> Run)
    {
        DefaultSet = true;
        var r = new Case<T, R>(this.Value, this.IsBroke, this.DefaultSet);
        if (IsBroke)
        {
            return r;
        }
        r.SetReturnValue(Run(this.Value));
        return r;
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public Switch<T> CaseRun(T Value, Action<T> Run)
    {
        return CaseRun(f => f.Equals(Value), Run);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Switch<T> CaseRun(T Value, Action<T> Run, bool Break)
    {
        return CaseRun(f => f.Equals(Value), Run, Break);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public Switch<T> CaseRun(Predicate<T> Check, Action<T> Run)
    {
        return CaseRun(Check, Run, true);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Switch<T> CaseRun(Predicate<T> Check, Action<T> Run, bool Break)
    {
        CheckDefaultSet();
        if (IsBroke) return this;
        if (Check(this.Value))
        {
            Run(this.Value);
            if (Break) _IsBroke = true;
        }
        return this;
    }

    /// <summary>
    /// 默认执行方法，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public void DefaultRun(Action<T> Run)
    {
        DefaultSet = true;
        if (IsBroke) return;
        Run(this.Value);
    }
}

public partial class Case<T, R> : SwitchCaseBase<T>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Value">原始值</param>
    public Case(T Value)
    {
        _Value = Value;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Value">原始值</param>
    /// <param name="Do">处理返回结果的方法，该方法将在每次执行CaseReturn并匹配成功时或执行DefaultReturn时调用，方法的第一个参数是新传入的返回值，第二个参数是当前的返回值</param>
    public Case(T Value, Func<R, R, R> Do)
    {
        _Value = Value;
        this.Do = Do;
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="Value">原始值</param>
    /// <param name="IsBroke">是否已结束</param>
    /// <param name="DefaultSet">是否已执行过默认操作</param>
    public Case(T Value, bool IsBroke, bool DefaultSet)
    {
        _Value = Value;
        _IsBroke = IsBroke;
        this.DefaultSet = DefaultSet;
    }

    protected Func<R, R, R> Do;

    /// <summary>
    /// 最终返回结果
    /// </summary>
    public R ReturnValue
    {
        get
        {
            return _ReturnValue;
        }
    }
    private R _ReturnValue;

    internal void SetReturnValue(R Value)
    {
        if (Do == null)
            _ReturnValue = Value;
        else
            _ReturnValue = Do(Value, ReturnValue);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public Case<T, R> CaseRun(T Value, Action<T> Run)
    {
        return CaseRun(f => f.Equals(Value), Run);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseRun(T Value, Action<T> Run, bool Break)
    {
        return CaseRun(f => f.Equals(Value), Run, Break);
    }


    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public Case<T, R> CaseRun(Predicate<T> Check, Action<T> Run)
    {
        return CaseRun(Check, Run, true);
    }

    /// <summary>
    /// 匹配并执行传入方法
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseRun(Predicate<T> Check, Action<T> Run, bool Break)
    {
        CheckDefaultSet();
        if (IsBroke) return this;
        if (Check(this.Value))
        {
            Run(this.Value);
            if (Break) _IsBroke = true;
        }
        return this;
    }

    /// <summary>
    /// 默认执行方法，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="Run">执行的方法，其参数为原始值</param>
    public Case<T, R> DefaultRun(Action<T> Run)
    {
        DefaultSet = true;
        if (IsBroke) return this;
        Run(this.Value);
        return this;
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> CaseReturn(T Value, Func<T, R> Run)
    {
        return CaseReturn(f => f.Equals(Value), Run);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> CaseReturn(T Value, R ReturnValue)
    {
        return CaseReturn(f => f.Equals(Value), f => ReturnValue);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> CaseReturn(Predicate<T> Check, Func<T, R> Run)
    {
        return CaseReturn(Check, Run, true);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> CaseReturn(Predicate<T> Check, R ReturnValue)
    {
        return CaseReturn(Check, f => ReturnValue, true);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn(T Value, Func<T, R> Run, bool Break)
    {
        return CaseReturn(f => f.Equals(Value), Run, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Value">匹配值</param>
    /// <param name="ReturnValue">返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn(T Value, R ReturnValue, bool Break)
    {
        return CaseReturn(f => f.Equals(Value), f => ReturnValue, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="ReturnValue">返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param>
    public Case<T, R> CaseReturn(Predicate<T> Check, R ReturnValue, bool Break)
    {
        return CaseReturn(Check, f => ReturnValue, Break);
    }

    /// <summary>
    /// 匹配并生成返回值。如果通过Switch指定了返回结果处理方法，那么在匹配成功时会将ReturnValue的值设为经过处理后的结果，否则将直接覆盖原有的ReturnValue值
    /// </summary>
    /// <param name="Check">匹配验证方法</param>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    /// <param name="Break">是否在匹配成功后结束</param> 
    public Case<T, R> CaseReturn(Predicate<T> Check, Func<T, R> Run, bool Break)
    {
        CheckDefaultSet();
        if (IsBroke)
        {
            return this;
        }
        if (Check(Value))
        {
            SetReturnValue(Run(Value));
            if (Break) this.Break();
        }
        return this;
    }

    /// <summary>
    /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="ReturnValue">返回结果</param>
    public Case<T, R> DefaultReturn(R ReturnValue)
    {
        return DefaultReturn(f => ReturnValue);
    }

    /// <summary>
    /// 默认生成的返回值，该方法必须在Switch操作方法链末端执行，其后不得再执行Case操作，但可以执行其它的Default操作
    /// </summary>
    /// <param name="Run">执行的方法，其参数为原始值，其返回值即为返回结果</param>
    public Case<T, R> DefaultReturn(Func<T, R> Run)
    {
        DefaultSet = true;
        if (IsBroke)
        {
            return this;
        }
        SetReturnValue(Run(this.Value));
        return this;
    }
}

/// <summary>
/// 枚举注释特性
/// </summary>
public partial class RemarkAttribute : Attribute
{
    private string _remark;
    public RemarkAttribute(string _remark)
    {
        this._remark = _remark;
    }
    public string Remark
    {
        get { return _remark; }
        set { _remark = value; }
    }

    ///// <summary>
    ///// 获取枚举项的注释信息。
    ///// </summary>
    //public static string GetEnumRemark(System.Enum _enum)
    //{
    //    return _enum.GetRemark();
    //}
}