using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;

namespace Core.Workflow
{
    public static class 工作流扩展
    {
        /// <summary>
        /// 将SynchronizationContext属性设为SynchronousSynchronizationContext类型实例之后，以使工作流变为在当前线程内同步运行。
        /// 注意：如果使用了Delay活动，仍然会变成异步，所以如果需要延迟的话应该自定义活动并执行Thread.Sleep(...)使线程休眠。
        /// 注意：未测试对并行活动的兼容性。        
        /// </summary>
        /// <param name="o"></param>
        public static void SetToSynchronous(this WorkflowApplication o)
        {
            o.SynchronizationContext = new SynchronousSynchronizationContext();
        }
    }
}
