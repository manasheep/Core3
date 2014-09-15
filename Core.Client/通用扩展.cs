using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using Timer = System.Threading.Timer;

public static partial class 通用扩展
{
    /// <summary>
    /// 将对象的字符串形式输出到程序启动目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t, Func<T, object> 表达式)
    {
        return t.Log(Application.StartupPath, 表达式);
    }

    /// <summary>
    /// 将对象的字符串形式输出到程序启动目录的log.txt文件中
    /// </summary>
    public static T Log<T>(this T t)
    {
        return t.Log(Application.StartupPath);
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

    /// <summary>
    /// 释放内存
    /// </summary>
    public static void FlushMemory(this Object o)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
        {
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }
    }

    /// <summary>
    /// 延迟执行操作
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="o">操作对象</param>
    /// <param name="延迟毫秒数">延迟毫秒数</param>
    /// <param name="执行内容">执行内容</param>
    public static void DelayRun<T>(this T o, int 延迟毫秒数, Action<T> 执行内容)
    {
        //Timer t = new Timer(r => { 执行内容(o); }, null, 延迟毫秒数, Timeout.Infinite);

        System.Timers.Timer t = new System.Timers.Timer();
        t.Interval = 延迟毫秒数;
        t.Elapsed += (sender, e) =>
        {
            执行内容(o);
            t.Dispose();
        };
        t.Enabled = true;

        //定时器 d = new 定时器(延迟毫秒数);
        //d.执行完毕事件 += (s, op, b) => { 执行内容(o); d.Dispose(); };
        //d.执行(null);
    }

    ///// <summary>
    ///// 异步延迟执行操作
    ///// </summary>
    ///// <typeparam name="T">类型</typeparam>
    ///// <param name="o">操作对象</param>
    ///// <param name="延迟毫秒数">延迟毫秒数</param>
    ///// <param name="执行内容">执行内容</param>
    //public static Task<T> DelayRunAsync<T>(this T o, int 延迟毫秒数, Action<T> 执行内容)
    //{
    //    Thread.Sleep(延迟毫秒数);
    //    执行内容(o);
    //    return Task.FromResult(o);
    //}
}
