using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;
using System.Threading;

namespace Core.WFActivity
{

    [Designer(typeof(整数笛卡尔坐标输入))]
    public sealed class 模拟鼠标左键单击 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<int> X坐标 { get; set; }
        [RequiredArgument]
        public InArgument<int> Y坐标 { get; set; }

        [DefaultValue(50)]
        [Description("单位为毫秒")]
        public InArgument<int> 操作间隔延迟时间 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var ts = context.GetValue(操作间隔延迟时间);
            if (ts <= 0) ts = 50;
            Client处理函数.模拟鼠标移动(context.GetValue(X坐标), context.GetValue(Y坐标));
            Thread.Sleep(ts);
            Client处理函数.模拟鼠标左键按下(context.GetValue(X坐标), context.GetValue(Y坐标));
            Thread.Sleep(ts);
            Client处理函数.模拟鼠标左键抬起(context.GetValue(X坐标), context.GetValue(Y坐标));
        }
    }
}
