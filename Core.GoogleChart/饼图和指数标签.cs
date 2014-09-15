using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.GoogleChart
{
    public interface 饼图和指数标签支持接口 : 参数支持接口
    { 
    
    }

    [Serializable]
    public class 饼图和指数标签 : 参数
    {
        public 饼图和指数标签()
            : base()
        {
            _标签列表 = new List<string>();
        }

        public override string 唯一参数标识
        {
            get { return "饼图和指数标签"; }
        }

        public override string 参数类型代码
        {
            get { return "chl"; }
        }

        public List<string> 标签列表
        {
            get
            {
                return _标签列表;
            }
        }
        private List<string> _标签列表;

        protected override string 生成参数值代码()
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in 标签列表)
            {
                if (s.Length > 0) s.Append('|');
                s.Append(f);
            }
            return s.ToString();
        }
    }
}
