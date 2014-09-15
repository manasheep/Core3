using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using CIO = Core.IO;
using System.Runtime.Serialization;
using Core.Text;
using Core.Collection;

namespace Core.Net
{
    public enum 采集状态
    {
        待机, 采集中, 正在请求停止
    }

    [Serializable]
    public class 蜘蛛巢 : CIO.可序列化基类<蜘蛛巢>
    {
        public 蜘蛛巢()
            : base()
        {
            _蜘蛛群 = new List<蜘蛛>();
            _线程池 = new Dictionary<string, Thread>();
            _排除扩展名 = new List<string>();
            _限定内容类型 = new List<string>();
        }

        public 蜘蛛巢(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.过往运行时间 = ((TimeSpan)info.GetValue("过往运行时间", typeof(TimeSpan))).Add((TimeSpan)info.GetValue("本次运行时间", typeof(TimeSpan)));
            this._上次保存时间 = (DateTime)info.GetValue("上次保存时间", typeof(DateTime));
            //this._蜘蛛群 = info.GetValue("蜘蛛群", typeof(List<蜘蛛>)) as List<蜘蛛>;
            _线程池 = new Dictionary<string, Thread>();
            this._限定文件大小 = info.GetInt32("限定文件大小");
            this._限定内容类型 = info.GetValue("限定内容类型", typeof(List<string>)) as List<string>;
            this._排除扩展名 = info.GetValue("排除扩展名", typeof(List<string>)) as List<string>;
            _蜘蛛群 = new List<蜘蛛>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            _上次保存时间 = DateTime.Now;
            //info.AddValue("蜘蛛群", 蜘蛛群, typeof(List<蜘蛛>));
            info.AddValue("过往运行时间", 过往运行时间, typeof(TimeSpan));
            info.AddValue("本次运行时间", 本次运行时间, typeof(TimeSpan));
            info.AddValue("上次保存时间", 上次保存时间, typeof(DateTime));
            info.AddValue("限定文件大小", 限定文件大小);
            info.AddValue("限定内容类型", 限定内容类型, typeof(List<string>));
            info.AddValue("排除扩展名", 排除扩展名, typeof(List<string>));
        }

        public void 保存(string 保存路径)
        {
            this.序列化为二进制文件(保存路径);
        }

        public static 蜘蛛巢 读取(string 读取路径)
        {
            return 反序列化自二进制文件(读取路径);
        }

        /// <summary>
        /// 采集状态变更事件代理
        /// </summary>
        public delegate void 采集状态变更代理(object sender, 采集状态 新状态);

        /// <summary>
        /// 采集状态变更事件
        /// </summary>
        public event 采集状态变更代理 采集状态变更事件;

        protected virtual void 触发采集状态变更事件(采集状态 新状态)
        {
            if (采集状态变更事件 != null) 采集状态变更事件(this, 新状态);
            if (新状态 == 采集状态.正在请求停止)
                foreach (蜘蛛 f in 蜘蛛群)
                {
                    if (f.状态 == 采集状态.采集中) f.状态 = 新状态;
                }
        }

        /// <summary>
        /// 开始采集事件代理
        /// </summary>
        public delegate void 开始采集代理(object sender);

        /// <summary>
        /// 开始采集事件
        /// </summary>
        public event 开始采集代理 开始采集事件;

        protected virtual void 触发开始采集事件()
        {
            if (开始采集事件 != null) 开始采集事件(this);
        }

        /// <summary>
        /// 启动线程事件代理
        /// </summary>
        public delegate void 启动线程代理(object sender, 蜘蛛 蜘蛛, Thread 线程);

        /// <summary>
        /// 启动线程事件
        /// </summary>
        public event 启动线程代理 启动线程事件;

        protected virtual void 触发启动线程事件(蜘蛛 蜘蛛, Thread 线程)
        {
            if (启动线程事件 != null) 启动线程事件(this, 蜘蛛, 线程);
        }

        /// <summary>
        /// 采集完毕事件代理
        /// </summary>
        public delegate void 采集完毕代理(object sender);

        /// <summary>
        /// 采集完毕事件
        /// </summary>
        public event 采集完毕代理 采集完毕事件;

        protected virtual void 触发采集完毕事件()
        {
            if (采集完毕事件 != null) 采集完毕事件(this);
        }

        /// <summary>
        /// 指示忽略的网页扩展名，如：jpg或zip或avi
        /// </summary>
        public List<string> 排除扩展名
        {
            get
            {
                return _排除扩展名;
            }
        }
        private List<string> _排除扩展名;

        /// <summary>
        /// 指示忽略超过限定大小的文件，单位为kb，为0则不限制
        /// </summary>
        public int 限定文件大小
        {
            get
            {
                return _限定文件大小;
            }
            set
            {
                _限定文件大小 = value;
            }
        }
        private int _限定文件大小;

        /// <summary>
        /// 指示忽略限定内容类型以外的文件，如：text/html或image/jpeg
        /// </summary>
        public List<string> 限定内容类型
        {
            get
            {
                return _限定内容类型;
            }
            set
            {
                _限定内容类型 = value;
            }
        }
        private List<string> _限定内容类型;

        public DateTime 启动时间
        {
            get
            {
                return _启动时间;
            }
        }
        private DateTime _启动时间;

        public DateTime 上次保存时间
        {
            get
            {
                return _上次保存时间;
            }
        }
        private DateTime _上次保存时间;

        /// <summary>
        /// 自此次启动之前的总运行时间
        /// </summary>
        protected TimeSpan 过往运行时间
        {
            get
            {
                return _过往运行时间;
            }
            set
            {
                _过往运行时间 = value;
            }
        }
        private TimeSpan _过往运行时间;

        /// <summary>
        /// 此次启动以来的运行时间
        /// </summary>
        protected TimeSpan 本次运行时间
        {
            get
            {
                if (状态 == 采集状态.待机) return TimeSpan.Zero;
                else return DateTime.Now.Subtract(启动时间);
            }
        }

        public TimeSpan 总运行时间
        {
            get
            {
                return 过往运行时间.Add(本次运行时间);
            }
        }

        /// <summary>
        /// 应在采集停止前执行
        /// </summary>
        protected virtual void 更新过往运行时间()
        {
            过往运行时间 = 总运行时间;
        }

        public 采集状态 状态
        {
            get
            {
                return _状态;
            }
            set
            {
                if (value == 采集状态.待机) 更新过往运行时间();
                _状态 = value;
                触发采集状态变更事件(value);
            }
        }
        private 采集状态 _状态;

        public virtual void 开始采集(IEnumerable<蜘蛛> 蜘蛛)
        {
            触发开始采集事件();
            _启动时间 = DateTime.Now;
            状态 = 采集状态.采集中;
            委派目标(蜘蛛);
            foreach (蜘蛛 f in 蜘蛛群)
            {
                f.采集完毕事件 -= new 蜘蛛.采集完毕代理(采集完毕处理);
                f.采集完毕事件 += new 蜘蛛.采集完毕代理(采集完毕处理);
                f.巢穴 = this;
                var t = new Thread(f.开始采集);
                t.Name = f.名称;
                t.IsBackground = true;
                线程池.添加或更新(f.名称, t);
                触发启动线程事件(f, t);
                t.Start();
            }
        }

        protected Dictionary<string, Thread> 线程池
        {
            get
            {
                return _线程池;
            }
        }
        private Dictionary<string, Thread> _线程池;

        void 采集完毕处理(object sender, int 跳过网址数量)
        {
            if (!繁忙中)
            {
                状态 = 采集状态.待机;
                触发采集完毕事件();
            }
        }

        protected virtual void 委派目标(IEnumerable<蜘蛛> 蜘蛛)
        {
            蜘蛛群.Clear();
            foreach (蜘蛛 f in 蜘蛛)
            {
                f.名称 = "采集线程" + (蜘蛛群.Count + 1);
                if (f.发现网址列表.Count == 0) f.发现网址列表.Add(new Uri(f.入口网址));
                蜘蛛群.Add(f);
            }
        }

        public bool 繁忙中
        {
            get
            {
                return !蜘蛛群.All(f => f.状态 == 采集状态.待机);
            }
        }

        public List<蜘蛛> 蜘蛛群
        {
            get
            {
                return _蜘蛛群;
            }
        }
        private List<蜘蛛> _蜘蛛群;
    }

    [Serializable]
    public class 蜘蛛 : CIO.可序列化基类<蜘蛛>
    {
        static 蜘蛛()
        {
            编码检索表达式 = new Regex(@"<meta[^>]+?charset=([\w\d-]+)[^>]+?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            超链接检索表达式 = new Regex(@"href=['""]?(?!\s*(javascript:)|#|&|mailto:)(?<地址>[^>\s""']+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            URL检索表达式 = new Regex(@"(?<协议>http|https)://(?<地址>[%\w\#\&\.\/\?=:\-_\|\+]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        void 初始化()
        {
            _网页客户端 = new HttpClient();
            _网页客户端.Headers.Add("Accept", "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
            _网页客户端.Headers.Add("Accept-Language", "zh-cn");
            _网页客户端.Headers.Add("UA-CPU", "x86");
            _网页客户端.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            _随机数 = new Random();
        }

        public 蜘蛛()
        {
            爬行路线长度上限 = 300;
            _爬行路线列表 = new List<Uri>();
            _发现网址列表 = new List<Uri>();
            _限定域名 = new List<string>();
            初始化();
        }

        public 蜘蛛(string 名称, Uri 入口网址)
            : this()
        {
            _名称 = 名称;
            _入口网址 = 入口网址.AbsoluteUri;
            发现网址列表.Add(入口网址);
            初始化();
        }

        public 蜘蛛(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this._名称 = info.GetString("名称");
            this._总访问网址量 = info.GetInt64("总访问网址量");
            this._总下载量 = info.GetInt64("总下载量");
            this._入口网址 = info.GetString("入口网址");
            this._爬行路线列表 = info.GetValue("爬行路线列表", typeof(List<Uri>)) as List<Uri>;
            this._发现网址列表 = info.GetValue("发现网址列表", typeof(List<Uri>)) as List<Uri>;
            this._限定域名 = info.GetValue("限定域名", typeof(List<string>)) as List<string>;
            this.爬行间歇时间 = info.GetInt32("爬行间歇时间");
            this._爬行路线长度上限 = info.GetInt32("爬行路线长度上限");
            初始化();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("名称", 名称);
            info.AddValue("总访问网址量", 总访问网址量);
            info.AddValue("总下载量", 总下载量);
            info.AddValue("入口网址", 入口网址);
            info.AddValue("爬行路线列表", 爬行路线列表, typeof(List<Uri>));
            info.AddValue("发现网址列表", 发现网址列表, typeof(List<Uri>));
            info.AddValue("限定域名", 限定域名, typeof(List<string>));
            info.AddValue("爬行路线长度上限", 爬行路线长度上限);
            info.AddValue("爬行间歇时间", 爬行间歇时间);
        }

        public override string ToString()
        {
            return 入口网址;
        }

        public int 爬行间歇时间
        {
            get
            {
                return _爬行间歇时间;
            }
            set
            {
                _爬行间歇时间 = value;
            }
        }
        private int _爬行间歇时间;

        public int 爬行路线长度上限
        {
            get
            {
                return _爬行路线长度上限;
            }
            set
            {
                _爬行路线长度上限 = value;
            }
        }
        private int _爬行路线长度上限;

        public string 入口网址
        {
            get
            {
                return _入口网址;
            }
            set
            {
                _入口网址 = value;
            }
        }
        private string _入口网址;

        /// <summary>
        /// 采集完毕事件代理
        /// </summary>
        public delegate void 采集完毕代理(object sender, int 跳过网址数);

        /// <summary>
        /// 采集完毕事件
        /// </summary>
        public event 采集完毕代理 采集完毕事件;

        protected virtual void 触发采集完毕事件(int 跳过网址数)
        {
            状态 = 采集状态.待机;
            if (采集完毕事件 != null) 采集完毕事件(this, 跳过网址数);
        }

        /// <summary>
        /// 发生异常事件代理
        /// </summary>
        public delegate void 发生异常代理(object sender, Exception 异常);

        /// <summary>
        /// 发生异常事件
        /// </summary>
        public event 发生异常代理 发生异常事件;

        protected virtual void 触发发生异常事件(Exception 异常)
        {
            if (发生异常事件 != null) 发生异常事件(this, 异常);
        }


        /// <summary>
        /// 检验网址的域名是否在限定域名之内，返回是否允许访问
        /// </summary>
        /// <param name="网址">网址</param>
        /// <returns>是否允许访问</returns>
        public virtual bool 检验域名(Uri 网址)
        {
            if (限定域名.Count == 0) return true;
            foreach (string f in 限定域名)
            {
                if (Regex.IsMatch(网址.Host, "^" + f.Replace("*", @"[\S]*").Replace(".", @"\.") + "$", RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 限定只访问限定域名内的网址，如：bbs.163.com或*.163.com或163.com
        /// 为空则不进行任何限制
        /// </summary>
        public List<string> 限定域名
        {
            get
            {
                return _限定域名;
            }
        }
        private List<string> _限定域名;

        /// <summary>
        /// 排除网址事件代理
        /// </summary>
        public delegate void 排除网址代理(object sender, Uri 排除的网址);

        /// <summary>
        /// 排除网址事件
        /// </summary>
        public event 排除网址代理 排除网址事件;

        protected virtual void 触发排除网址事件(Uri 排除的网址)
        {
            if (排除网址事件 != null) 排除网址事件(this, 排除的网址);
        }

        /// <summary>
        /// 发生访问异常事件代理
        /// </summary>
        public delegate void 发生访问异常代理(object sender, Uri 访问目标网址, WebException 异常);

        /// <summary>
        /// 发生访问异常事件
        /// </summary>
        public event 发生访问异常代理 发生访问异常事件;

        protected virtual void 触发发生访问异常事件(Uri 访问目标网址, WebException 异常)
        {
            if (发生访问异常事件 != null) 发生访问异常事件(this, 访问目标网址, 异常);
        }

        /// <summary>
        /// 成功读取网址事件代理
        /// </summary>
        public delegate void 成功读取网址代理(object sender, Uri 已读取网址, byte[] 原始数据, string 内容, IEnumerable<Uri> 获取超链接列表);

        /// <summary>
        /// 成功读取网址事件
        /// </summary>
        public event 成功读取网址代理 成功读取网址事件;

        protected virtual void 触发成功读取网址事件(Uri 已读取网址, byte[] 原始数据, string 内容, IEnumerable<Uri> 获取超链接列表)
        {
            if (成功读取网址事件 != null) 成功读取网址事件(this, 已读取网址, 原始数据, 内容, 获取超链接列表);
        }

        /// <summary>
        /// 开始采集事件代理
        /// </summary>
        public delegate void 开始采集代理(object sender);

        /// <summary>
        /// 开始采集事件
        /// </summary>
        public event 开始采集代理 开始采集事件;

        protected virtual void 触发开始采集事件()
        {
            if (开始采集事件 != null) 开始采集事件(this);
        }

        /// <summary>
        /// 指示总计下载尺寸，单位为kb
        /// </summary>
        public long 总下载量
        {
            get
            {
                return _总下载量;
            }
        }
        private long _总下载量;

        public long 总访问网址量
        {
            get
            {
                return _总访问网址量;
            }
        }
        private long _总访问网址量;

        private static Regex 编码检索表达式
        {
            get
            {
                return _编码检索表达式;
            }
            set
            {
                _编码检索表达式 = value;
            }
        }
        private static Regex _编码检索表达式;

        private static Regex 超链接检索表达式
        {
            get
            {
                return _超链接检索表达式;
            }
            set
            {
                _超链接检索表达式 = value;
            }
        }
        private static Regex _超链接检索表达式;

        private static Regex URL检索表达式
        {
            get
            {
                return _URL检索表达式;
            }
            set
            {
                _URL检索表达式 = value;
            }
        }
        private static Regex _URL检索表达式;

        public virtual void 开始采集()
        {
            触发开始采集事件();
            状态 = 采集状态.采集中;
            var x = 0;
        Loop:
            var u = 获取网址();
            if (u == null) return;
            try
            {
                if (!检验文件大小及内容类型(u))
                {
                    触发排除网址事件(u);
                    x++;
                }
                var d = 网页客户端.DownloadData(u);
                _总下载量 += d.Count() / 1024;
                var s = 获取内容(d);
                var g = 获取链接网址(u, s);
                触发成功读取网址事件(u, d, s, g);
                添加到爬行路线列表(u);
                更新发现网址列表(u, g);
            }
            catch (WebException e)
            {
                触发发生异常事件(e);
                触发发生访问异常事件(u, e);
                x++;
            }
            _总访问网址量++;
            if (状态 == 采集状态.采集中) goto Loop;
            触发采集完毕事件(x);
        }

        public virtual void 请求停止()
        {
            状态 = 采集状态.正在请求停止;
        }

        public 采集状态 状态
        {
            get
            {
                return _状态;
            }
            set
            {
                _状态 = value;
            }
        }
        private 采集状态 _状态;

        protected virtual void 添加到爬行路线列表(Uri 网址)
        {
            爬行路线列表.Add(网址);
            if (爬行路线列表.Count > 爬行路线长度上限) 爬行路线列表.RemoveRange(0, 爬行路线列表.Count - 爬行路线长度上限);
        }

        protected Random 随机数
        {
            get
            {
                return _随机数;
            }
        }
        private Random _随机数;

        protected virtual Uri 获取网址()
        {
            if (发现网址列表.Count > 0)
            {
                var s = 发现网址列表[随机数.Next(发现网址列表.Count)];
                发现网址列表.Remove(s);
                return s;
            }
            else if (爬行路线列表.Count > 0)
            {
                var s = 爬行路线列表[随机数.Next(爬行路线列表.Count)];
                爬行路线列表.Remove(s);
                return s;
            }
            return null;
        }

        /// <summary>
        /// 验证网址指向的文件是否超过限定大小，或不在限定内容类型之内，返回是否允许访问
        /// </summary>
        /// <param name="网址">网址</param>
        /// <returns>是否允许访问</returns>
        protected virtual bool 检验文件大小及内容类型(Uri 网址)
        {
            //if (限定文件大小 == 0 && 限定内容类型.Count == 0) return true;
            var d = 网页客户端.获取WebRequest(网址).GetResponse();
            var s = 巢穴.限定文件大小 == 0 || d.ContentLength / 1024 <= 巢穴.限定文件大小;
            var c = 巢穴.限定内容类型.Count == 0;
            var t = d.ContentType.ToLower();
            foreach (string f in 巢穴.限定内容类型)
            {
                if (t.IndexOf(f.ToLower()) >= 0) c = true;
            }
            d.Close();
            return s && c;
        }

        public List<Uri> 爬行路线列表
        {
            get
            {
                return _爬行路线列表;
            }
        }
        private List<Uri> _爬行路线列表;

        /// <summary>
        /// 网址更新到发现列表中，排除其中已访问过的或不合规则的网址，返回排除的网址数量
        /// </summary>
        /// <param name="网址列表">网址列表</param>
        /// <returns>排除的网址数量</returns>
        protected virtual void 更新发现网址列表(Uri 当前网址, IEnumerable<Uri> 网址列表)
        {
            发现网址列表.Clear();
            发现网址列表.AddRange(网址列表);
        }

        /// <summary>
        /// 检验网址是否属于被排除扩展名之列，返回是否允许访问
        /// </summary>
        /// <param name="网址">网址</param>
        /// <returns>是否允许访问</returns>
        protected virtual bool 检验扩展名(Uri 网址)
        {
            bool b = true;
            var s = Path.GetExtension(网址.AbsoluteUri);
            if (s.验证有效性())
                foreach (string f in 巢穴.排除扩展名)
                {
                    if (s.Substring(1).ToLower() == f.ToLower()) return false;
                }
            return b;
        }

        protected virtual bool 检验网址(Uri 网址)
        {
            return 网址 != null && 网址.AbsoluteUri.ToLower().StartsWith("http") && 检验域名(网址) && 检验扩展名(网址) && !爬行路线列表.Any(t => t.AbsoluteUri == 网址.AbsoluteUri);
        }

        protected virtual List<Uri> 获取链接网址(Uri 当前网址, string 内容)
        {
            var l = new List<Uri>();
            var m = 超链接检索表达式.Matches(内容).Cast<Match>();
            var r = 随机数.Next(m.Count());
            foreach (Match f in m.OrderBy(t => r - t.Index))
            {
                var u = 转换为Uri(当前网址, f.Groups["地址"].Value);
                if (检验网址(u) && !l.Any(t => t.AbsoluteUri == u.AbsoluteUri))
                {
                    l.Add(u);
                    if (l.Count > 10) return l;
                }
            }
            var n = URL检索表达式.Matches(内容).Cast<Match>();
            r = 随机数.Next(m.Count());
            foreach (Match f in n.OrderBy(t => r - t.Index))
            {
                var u = 转换为Uri(当前网址, f.Value);
                if (检验网址(u) && !l.Any(t => t.AbsoluteUri == u.AbsoluteUri))
                {
                    l.Add(u);
                    if (l.Count > 10) return l;
                }
            }
            return l;
        }

        protected virtual Uri 转换为Uri(Uri 当前网址, string 待处理网址)
        {
            try
            {
                try
                {
                    return new Uri(待处理网址);
                }
                catch
                {
                    return new Uri(当前网址, 待处理网址);
                }
            }
            catch (Exception e)
            {
                触发发生异常事件(e);
                return null;
            }
        }

        protected virtual string 获取内容(byte[] 数据)
        {
            if (数据 == null) return null;
            var s = Encoding.UTF8.GetString(数据);
            var m = 编码检索表达式.Match(s);
            var e = m.Groups[1].Value;
            try
            {
                var ed = Encoding.GetEncoding(e.验证及替代("UTF-8"));
                return ed != Encoding.UTF8 ? ed.GetString(数据) : s;
            }
            catch { return s; }
        }

        public string 名称
        {
            get
            {
                return _名称;
            }
            set
            {
                _名称 = value;
            }
        }
        private string _名称;

        public 蜘蛛巢 巢穴
        {
            get
            {
                return _巢穴;
            }
            set
            {
                _巢穴 = value;
            }
        }
        private 蜘蛛巢 _巢穴;

        private HttpClient 网页客户端
        {
            get
            {
                return _网页客户端;
            }
        }
        private HttpClient _网页客户端;

        public List<Uri> 发现网址列表
        {
            get
            {
                return _发现网址列表;
            }
        }
        private List<Uri> _发现网址列表;
    }

    /// <summary>
    /// 支持 Session 和 Cookie 的 WebClient。
    /// </summary>
    public class HttpClient : WebClient
    {
        // Cookie 容器
        private CookieContainer cookieContainer;

        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        public HttpClient()
        {
            this.cookieContainer = new CookieContainer();
        }

        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        /// <param name="cookies">Cookie 容器</param>
        public HttpClient(CookieContainer cookies)
        {
            this.cookieContainer = cookies;
        }

        /// <summary>
        /// Cookie 容器
        /// </summary>
        public CookieContainer Cookies
        {
            get { return this.cookieContainer; }
            set { this.cookieContainer = value; }
        }

        public WebRequest 获取WebRequest(Uri 网址)
        {
            return GetWebRequest(网址);
        }

        /// <summary>
        /// 返回带有 Cookie 的 HttpWebRequest。
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = cookieContainer;
            }
            return request;
        }

        /// <summary>
        /// 向指定的 URL POST 数据，并返回页面
        /// </summary>
        /// <param name="uriString">POST URL</param>
        /// <param name="postString">POST 的 数据</param>
        /// <param name="postStringEncoding">POST 数据的 CharSet</param>
        /// <param name="dataEncoding">页面的 CharSet</param>
        /// <returns>页面的源文件</returns>
        public string PostData(string uriString, string postString, string postStringEncoding, string dataEncoding, out string msg)
        {
            try
            {
                // 将 Post 字符串转换成字节数组
                byte[] postData = Encoding.GetEncoding(postStringEncoding).GetBytes(postString);
                this.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                // 上传数据，返回页面的字节数组
                byte[] responseData = this.UploadData(uriString, "POST", postData);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding(dataEncoding).GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得指定 URL 的源文件
        /// </summary>
        /// <param name="uriString">页面 URL</param>
        /// <param name="dataEncoding">页面的 CharSet</param>
        /// <returns>页面的源文件</returns>
        public string GetSrc(string uriString, string dataEncoding, out string msg)
        {
            try
            {
                // 返回页面的字节数组
                byte[] responseData = this.DownloadData(uriString);
                // 将返回的将字节数组转换成字符串(HTML);
                string srcString = Encoding.GetEncoding(dataEncoding).GetString(responseData);
                srcString = srcString.Replace("\t", "");
                srcString = srcString.Replace("\r", "");
                srcString = srcString.Replace("\n", "");
                msg = string.Empty;
                return srcString;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 从指定的 URL 下载文件到本地
        /// </summary>
        /// <param name="urlString">文件 URL</param>
        /// <param name="fileName">本地文件的完成路径</param>
        /// <returns></returns>
        public bool GetFile(string urlString, string fileName, out string msg)
        {
            try
            {
                this.DownloadFile(urlString, fileName);
                msg = string.Empty;
                return true;
            }
            catch (WebException we)
            {
                msg = we.Message;
                return false;
            }
        }
    }
}
