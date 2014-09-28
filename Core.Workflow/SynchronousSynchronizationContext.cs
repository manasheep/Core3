using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Workflow
{
    /// <summary>
    /// 将WorkflowApplication实例的SynchronizationContext属性设为此类型实例之后，将会使工作流变为在当前线程内同步运行。
    /// 注意：如果使用了Delay活动，仍然会变成异步，所以如果需要延迟的话应该自定义活动并执行Thread.Sleep(...)使线程休眠。
    /// 注意：未测试对并行活动的兼容性。
    /// 参考：http://www.cnblogs.com/carysun/archive/2011/01/08/wf4-Synchronization.html#2006792
    /// 另一种同步思路参考：http://stevenwilliamalexander.wordpress.com/2010/11/16/integrating-a-persisted-wf40-workflow-with-mv/
    /// </summary>
    public class SynchronousSynchronizationContext : SynchronizationContext
    {
        public override void Post(SendOrPostCallback d, object state)
        {
            d(state);
        }
    }
}
