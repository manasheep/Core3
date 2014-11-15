using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Activities;
using Core.Net;

namespace Core.WFActivity
{
    [Designer(typeof(构建字符串形式的Http内容参数输入))]
    public sealed class 构建字符串形式的Http内容 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<String> 内容 { get; set; }
        [RequiredArgument]
        public InArgument<文件对应MIME类型> MIME类型 { get; set; }

        [RequiredArgument]
        public OutArgument<StringContent> 字符串形式的Http内容 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            var httpContent = new StringContent(context.GetValue(内容));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(context.GetValue(MIME类型).GetDescription());
            context.SetValue(字符串形式的Http内容, httpContent);
        }
    }
}
