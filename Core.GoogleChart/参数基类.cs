using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Core.GoogleChart
{
    [Serializable]
    public abstract class 参数
    {
        public 参数()
        {
            _依赖参数标识列表 = new List<string>();
        }

        /// <summary>
        /// 用于参数间互斥，避免重复定义同类参数，为空则不进行互斥判断
        /// </summary>
        public abstract string 唯一参数标识 { get; }

        /// <summary>
        /// 用于声明参数依赖，如果在生成代码时没有找到其中任意一个依赖项则报错，为空则不依赖任何其他参数
        /// </summary>
        public List<string> 依赖参数标识列表
        {
            get
            {
                return _依赖参数标识列表;
            }
        }
        private List<string> _依赖参数标识列表;

        /// <summary>
        /// URL中的参数名称
        /// </summary>
        public abstract string 参数类型代码 { get; }

        /// <summary>
        /// 生成URL中的参数值代码
        /// </summary>
        protected abstract string 生成参数值代码();

        /// <summary>
        /// 生成完整的URL参数名值代码
        /// </summary>
        public virtual string 生成代码()
        {
            return 参数类型代码 + "=" + 生成参数值代码();
        }
    }

    public interface 参数支持接口
    {
        List<参数> 参数列表 { get; }
    }
}
