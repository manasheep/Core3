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
    [Designer(typeof(设置Http响应内容类型参数输入))]
    public sealed class 设置Http响应内容类型 : CodeActivity
    {
        [RequiredArgument]
        public InArgument<HttpResponseMessage> 响应消息 { get; set; }
        [RequiredArgument]
        public InArgument<文件对应MIME类型> MIME类型 { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            context.GetValue(响应消息).Content.Headers.ContentType = new MediaTypeHeaderValue(context.GetValue(MIME类型).GetDescription());
        }
    }
}
