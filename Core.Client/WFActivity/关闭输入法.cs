using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    public sealed class 关闭输入法 : CodeActivity
    {
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            //Client处理函数.设置线程联接状态(Client处理函数.获取目标窗口线程ID(Client处理函数.获取当前激活窗口句柄().Trace("当前激活窗口句柄")).Trace("当前激活窗口线程ID"), true);
            //var p = Client处理函数.获取当前激活控件句柄().Trace("当前激活控件句柄");
            //var himc = ImmGetContext(p).Trace("当前输入法上下文");
            //ImmSetOpenStatus(himc, false);
            //Client处理函数.设置线程联接状态(Client处理函数.获取目标窗口线程ID(Client处理函数.获取当前激活窗口句柄()), false);

            //ImmDisableIME(Client处理函数.获取目标窗口线程ID(Client处理函数.获取当前激活窗口句柄().Trace("当前激活窗口句柄")).Trace("当前激活窗口线程ID"));

            //目前这两种方法都完全不起作用，有待继续研究
            throw new Exception("关闭输入法 活动目前还不起作用，请勿使用");
        }

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmDisableIME(IntPtr idThread);
        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);
        [DllImport("Imm32.dll")]
        public static extern Boolean ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);
    }
}
