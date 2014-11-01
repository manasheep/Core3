using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;
using System.Text.RegularExpressions;

namespace Core.WFActivity
{

    [Designer(typeof(正则表达式输入))]
    public sealed class 通过正则表达式获取窗口句柄 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<Regex> 表达式 { get; set; }

        [RequiredArgument]
        public OutArgument<IntPtr> 窗口句柄 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            context.SetValue(窗口句柄, Client处理函数.获取窗口句柄(context.GetValue(表达式)));
        }
    }
}
