using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.WF
{
    /// <summary>
    /// 同步执行工作流线程的上下文
    /// </summary>
   public class SynchronousSynchronizationContext : SynchronizationContext
    {
       public override void Post(SendOrPostCallback d, object state)
       {
           d(state);
       }
    }
}
