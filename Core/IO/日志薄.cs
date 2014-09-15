using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.IO
{
    [Serializable]
    public class 日志薄 : 可序列化基类<日志薄>
    {
        public 日志薄()
            : base()
        {
            _日志列表 = new Dictionary<string, List<日志>>();
        }

        public 日志薄(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _日志列表 = info.GetValue("日志列表", typeof(Dictionary<string, List<日志>>)) as Dictionary<string, List<日志>>;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("日志列表", 日志列表, typeof(Dictionary<string, 日志薄>));
        }

        public virtual void 添加日志(string 标签, 日志 日志)
        {
            if (!日志列表.ContainsKey(标签))
            {
                日志列表.Add(标签, new List<日志>());
            }
            日志列表[标签].Add(日志);
        }

        public virtual List<日志> 获取全部日志()
        {
            var l = new List<日志>();
            foreach (string f in 日志列表.Keys)
            {
                l.AddRange(日志列表[f]);
            }
            return l;
        }

        public virtual List<日志> 获取特定日志(params string[] 标签)
        {
            var l = new List<日志>();
            foreach (string f in 日志列表.Keys)
            {
                if (标签.Contains(f)) l.AddRange(日志列表[f]);
            }
            return l;
        }

        public virtual List<日志> 获取特定日志(IEnumerable<string> 标签)
        {
            var l = new List<日志>();
            foreach (string f in 日志列表.Keys)
            {
                if (标签.Contains(f)) l.AddRange(日志列表[f]);
            }
            return l;
        }

        public Dictionary<string, List<日志>> 日志列表
        {
            get
            {
                return _日志列表;
            }
        }
        private Dictionary<string, List<日志>> _日志列表;

        public void 保存(string 保存路径)
        {
            this.序列化为二进制文件(保存路径);
        }

        public static 日志薄 读取(string 读取路径)
        {
            return 反序列化自二进制文件(读取路径);
        }
    }

    [Serializable]
    public class 日志 : 可序列化基类<日志>
    {
        public 日志()
            : base()
        {
            _生成时间 = DateTime.Now;
        }

        public 日志(string 标题, string 备注)
            : this()
        {
            this.标题 = 标题;
            this.备注 = 备注;
        }

        public 日志(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            标题 = info.GetString("标题");
            备注 = info.GetString("备注");
            _生成时间 = info.GetDateTime("生成时间");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("标题", 标题);
            info.AddValue("备注", 备注);
            info.AddValue("生成时间", 生成时间);
        }

        public string 标题
        {
            get
            {
                return _标题;
            }
            set
            {
                _标题 = value;
            }
        }
        private string _标题;

        public string 备注
        {
            get
            {
                return _备注;
            }
            set
            {
                _备注 = value;
            }
        }
        private string _备注;

        public DateTime 生成时间
        {
            get
            {
                return _生成时间;
            }
        }
        private DateTime _生成时间;
    }
}
