using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(内容输入))]
    public sealed class 模拟键盘输入 : CodeActivity
    {
        /// <summary>
        /// 输入内容，具体指令参看：http://www.cnblogs.com/sydeveloper/archive/2013/02/25/2932571.html
        /// </summary>
        [RequiredArgument]
        public InArgument<string> 内容 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            Client处理函数.模拟键盘输入(context.GetValue(内容));
        }
    }
}
