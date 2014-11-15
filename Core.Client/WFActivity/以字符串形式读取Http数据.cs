using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Activities;
using System.Threading.Tasks;

namespace Core.WFActivity
{

    public sealed class 以字符串形式读取Http数据 : AsyncCodeActivity
    {
        /// <summary>
        /// 可以事先进行验证，如：response.EnsureSuccessStatusCode()
        /// 或修改请求的数据格式类型，如：response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        /// </summary>
        [RequiredArgument]
        public InArgument<HttpResponseMessage> 响应消息 { get; set; }

        [RequiredArgument]
        public OutArgument<string> 数据 { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            return context.GetValue(响应消息).Content.ReadAsStringAsync().ToApm(callback,state);
        }

        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            context.SetValue(数据, (result as Task<string>).Result);
        }
    }
}
