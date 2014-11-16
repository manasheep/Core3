using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Activities;

namespace Core.WFActivity
{
    [Designer(typeof(文件路径输入))]
    public sealed class 删除文件 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<string> 文件路径 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            File.Delete( context.GetValue(文件路径));
        }
    }
}
