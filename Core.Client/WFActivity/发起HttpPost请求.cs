using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Activities;
using System.Threading.Tasks;

namespace Core.WFActivity
{
    [Designer(typeof(网址及Http内容输入))]
    public sealed class 发起HttpPost请求 : AsyncCodeActivity
    {
        /// <summary>
        /// 目标网址
        /// </summary>
        [RequiredArgument]
        public InArgument<String> 网址 { get; set; }
        [RequiredArgument]
        public InArgument<HttpContent> Http内容 { get; set; }

        [RequiredArgument]
        public OutArgument<HttpResponseMessage> 响应消息 { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var hc = new HttpClient();
            var task = hc
                .PostAsync(context.GetValue(网址), context.GetValue(Http内容))
                .ToApm(callback, state);
            task.ContinueWith(q =>
                    hc.Dispose()
                );
            return task;
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            context.SetValue(响应消息, (result as Task<HttpResponseMessage>).Result);
        }
    }
}
