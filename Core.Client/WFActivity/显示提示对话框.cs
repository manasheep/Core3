using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Activities;
using System.Windows.Forms;

namespace Core.WFActivity
{
    [Designer(typeof(内容输入))]
    public sealed class 显示提示对话框 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> 内容 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            MessageBox.Show(context.GetValue(内容));
        }
    }
}
