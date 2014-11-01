using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(窗口句柄输入))]
    public sealed class 将窗口显示到桌面前端 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IntPtr> 窗口句柄 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            Client处理函数.将窗口显示到桌面前端(context.GetValue(窗口句柄));
        }
    }
}
