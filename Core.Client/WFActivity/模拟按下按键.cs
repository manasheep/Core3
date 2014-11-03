using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(虚拟按键代码输入))]
    public sealed class 模拟按下按键 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<VirtualKeyCode> 虚拟按键代码 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            Client处理函数.模拟按下按键(context.GetValue(虚拟按键代码));
        }
    }
}
