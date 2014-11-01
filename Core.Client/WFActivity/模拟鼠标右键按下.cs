using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{

    [Designer(typeof(整数笛卡尔坐标输入))]
    public sealed class 模拟鼠标右键按下 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<int> X坐标 { get; set; }
        [RequiredArgument]
        public InArgument<int> Y坐标 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            Client处理函数.模拟鼠标右键按下(context.GetValue(X坐标), context.GetValue(Y坐标));
        }
    }
}
