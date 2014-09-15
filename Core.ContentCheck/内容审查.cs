using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using Core.Text;
using Core.IO;

namespace Core.ContentCheck
{
    [Serializable]
    public class 内容审查
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="内容"> 用于审查的内容。</param>
        public 内容审查(string 内容)
        {
            _输入内容 = 内容;
            读取审查规则();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="内容"> 用于审查的内容。</param>
        /// <param name="规则文件所在目录">规则文件所在目录</param>
        public 内容审查(string 内容, string 规则文件所在目录)
        {
            _输入内容 = 内容;
            规则文件目录 = 规则文件所在目录;
        }

        /// <summary>
        /// 规则文件存放的目录
        /// </summary>
        public static string 规则文件目录
        {
            get
            {
                return _规则文件目录;
            }
            set
            {
                if (_规则文件目录 != value)
                {
                    _规则文件目录 = value;
                    加载审查规则();
                }
            }
        }
        private static string _规则文件目录 = "内容审查规则\\";

        private static void 读取审查规则()
        {
            if (规则 != null) return;
            加载审查规则();
        }

        /// <summary>
        /// 显示所有出现规则的类目
        /// </summary>
        public static List<string> 类目列表
        {
            get
            {
                return _类目列表;
            }
            set
            {
                _类目列表 = value;
            }
        }
        private static List<string> _类目列表;

        public static void 加载审查规则()
        {
            类目列表 = new List<string>();

            if (Directory.Exists(规则文件目录))
            {
                //读取数据
                var l = new List<内容审查规则>();
                foreach (string f in Directory.GetFiles(规则文件目录, "*.stl"))
                {
                    l.AddRange(IO处理函数.反序列化对象自XML文件<List<内容审查规则>>(f));
                }
                //追加繁体化的规则
                foreach (内容审查规则 f in l.ToArray())
                {
                    l.Add(f.获取繁体版本对象());
                }
                //进行处理
                规则 = new Dictionary<string, List<内容审查规则>>();
                首字符 = new List<char>();
                尾字符 = new List<char>();
                foreach (内容审查规则 f in l)
                {
                    最大长度 = Math.Max(f.最大长度, 最大长度);
                    foreach (char st in f.首字符)
                    {
                        if (首字符.IndexOf(st) < 0) 首字符.Add(st);
                        //将首字符与尾字符组合成一个规则键值，并添入规则，便于快速定位规则区间组，减少遍历次数
                        foreach (char ed in f.尾字符)
                        {
                            if (尾字符.IndexOf(ed) < 0) 尾字符.Add(ed);
                            var ts = st.ToString() + ed;
                            if (!规则.Keys.Contains(ts)) 规则[ts] = new List<内容审查规则>();
                            规则[ts].Add(f);
                        }
                    }
                    if (类目列表.IndexOf(f.类目) < 0) 类目列表.Add(f.类目);
                }
                //排序，长度较大的排在前面，以优先处理多字符的规则
                foreach (string f in 规则.Keys.ToArray())
                {
                    规则[f] = 规则[f].OrderByDescending(d => d.最大长度).ToList();
                }
            }
            else
            {
                throw new Exception("审查规则配置目录路径不存在：" + 规则文件目录);
            }
        }

        /// <summary>
        /// 内容审查遵循的处理规则列表。
        /// </summary>
        public static Dictionary<string, List<内容审查规则>> 规则
        {
            get
            {
                return _规则;
            }
            set
            {
                _规则 = value;
            }
        }
        private static Dictionary<string, List<内容审查规则>> _规则;
        protected static List<char> 首字符
        {
            get
            {
                return _首字符;
            }
            set
            {
                _首字符 = value;
            }
        }
        private static List<char> _首字符;
        protected static List<char> 尾字符
        {
            get
            {
                return _尾字符;
            }
            set
            {
                _尾字符 = value;
            }
        }
        private static List<char> _尾字符;
        /// <summary>
        /// 所有规则中的最大取样长度
        /// </summary>
        protected static int 最大长度
        {
            get
            {
                return _最大长度;
            }
            set
            {
                _最大长度 = value;
            }
        }
        private static int _最大长度;

        /// <summary>
        /// 用于审查的内容。由构造函数制定。
        /// </summary>
        public string 输入内容 { get { return _输入内容; } }
        string _输入内容;
        /// <summary>
        /// 输出的审查明细列表。在执行审查时生成。
        /// </summary>
        public ObservableCollection<内容审查明细> 输出明细 { get { return _输出明细; } }
        private ObservableCollection<内容审查明细> _输出明细;

        /// <summary>
        /// 对内容中捕获词语的最高评分。在执行审查时生成。
        /// </summary>
        public int 最高评分 { get { return _最高评分; } }
        private int _最高评分;

        /// <summary>
        /// 对内容中捕获词语的累计评分。在执行审查时生成。
        /// </summary>
        public int 累计评分 { get { return _累计评分; } }
        private int _累计评分;

        /// <summary>
        /// 对内容中捕获词语的最高精确评分，依据匹配的精确度比值计算得出。在执行审查时生成。
        /// </summary>
        public double 最高精确评分 { get { return _最高精确评分; } }
        private double _最高精确评分;

        /// <summary>
        /// 对内容中捕获词语的累计精确评分，依据匹配的精确度比值计算得出。在执行审查时生成。
        /// </summary>
        public double 累计精确评分 { get { return _累计精确评分; } }
        private double _累计精确评分;

        /// <summary>
        /// 匹配的平均精确度，取值范围为0到1之间。在执行审查时生成。
        /// </summary>
        public double 平均精确度 { get { return _平均精确度; } }
        private double _平均精确度;

        /// <summary>
        /// 捕获词语字数与全文字总数的比例，取值范围为0到1之间。在执行审查时生成。
        /// </summary>
        public double 捕获比例 { get { return _捕获比例; } }
        private double _捕获比例;

        /// <summary>
        /// 审查内容，并生成输出明细及内容
        /// </summary>
        public void 审查()
        {
            审查(true);
        }

        /// <summary>
        /// 审查内容
        /// </summary>
        /// <param name="生成输出明细">指示是否生成输出明细</param>
        public void 审查(bool 生成输出明细)
        {
            if (!输入内容.验证有效性()) return;

            #region 初始化
            //最高评分
            int ms = 0;
            //累计评分
            int cs = 0;
            //最高精确评分
            double rms = 0;
            //最高累计评分
            double rcs = 0;
            //精确度列表
            List<double> ad = new List<double>();
            //输出明细
            var outlist = new ObservableCollection<内容审查明细>();

            //捕获词语累计长度
            var getstr = 0;

            //当前处理索引位置
            var nowindex = 0;
            var st = new List<内容审查触发记录>();
            var ed = new List<内容审查触发记录>();
            #endregion

            #region 检测首尾字符匹配情况
            for (int i = 0; i < 输入内容.Length; i++)
            {
                if (首字符.Contains(输入内容[i])) st.Add(new 内容审查触发记录() { 索引位置 = i, 字符 = 输入内容[i] });
                if (尾字符.Contains(输入内容[i])) ed.Insert(0, new 内容审查触发记录() { 索引位置 = i, 字符 = 输入内容[i] });
            }
            #endregion

            #region 根据首尾匹配列表做进一步处理
            foreach (内容审查触发记录 起始字符 in st)
            {
                if (nowindex > 起始字符.索引位置) continue;
                foreach (内容审查触发记录 结束字符 in ed.Where(f => f.索引位置 >= 起始字符.索引位置 && f.索引位置 <= 起始字符.索引位置 + 最大长度))
                {
                    var key = 起始字符.字符.ToString() + 结束字符.字符;
                    if (规则.Keys.Contains(key) && 规则[key].获取最大长度() >= 结束字符.索引位置 - 起始字符.索引位置)
                        foreach (内容审查规则 检测规则 in 规则[key].Where(f => 结束字符.索引位置 - 起始字符.索引位置 + 1 <= f.最大长度))
                        {
                            var s = 输入内容.Substring(起始字符.索引位置, 结束字符.索引位置 - 起始字符.索引位置 + 1);
                            //string.Format("[{1}-{2}] {0} : {3}",s,起始字符.字符,结束字符.字符,检测规则.表达式).调试输出();

                            //处理
                            if (检测规则.获取正则表达式对象().IsMatch(s))
                            {
                                cs += 检测规则.分值;
                                ms = Math.Max(ms, 检测规则.分值);
                                var d = 检测规则.计算精确度(s.Length);
                                ad.Add(d);
                                var t = 检测规则.计算精确分值(d);
                                rcs += t;
                                rms = Math.Max(rms, t);

                                getstr += s.Length;

                                if (生成输出明细) outlist.Add(new 内容审查明细 { 应用规则 = 检测规则, 索引位置 = 起始字符.索引位置, 原文 = s, 精确分值 = t, 精确度 = d, 类目序号 = 类目列表.IndexOf(检测规则.类目) });

                                nowindex = 结束字符.索引位置 + 1;
                                goto end;
                            }
                        }
                }
            end: continue;
            }
            #endregion

            #region 赋值
            _最高评分 = ms;
            _累计评分 = cs;
            _最高精确评分 = rms;
            _累计精确评分 = rcs;
            if (ad.Count > 0) _平均精确度 = ad.Average();
            _输出明细 = outlist;
            _捕获比例 = getstr * 1.0 / 输入内容.Length;
            #endregion
        }

    }

    /// <summary>
    /// 用于记录扫描原文时搜索到的首尾触发字符信息
    /// </summary>
    [Serializable]
    public struct 内容审查触发记录
    {
        /// <summary>
        /// 触发的字符
        /// </summary>
        public char 字符
        {
            get
            {
                return _字符;
            }
            set
            {
                _字符 = value;
            }
        }
        private char _字符;
        /// <summary>
        /// 在原文中的索引位置
        /// </summary>
        public int 索引位置
        {
            get
            {
                return _索引位置;
            }
            set
            {
                _索引位置 = value;
            }
        }
        private int _索引位置;
    }

    [Serializable]
    public struct 内容审查明细
    {
        /// <summary>
        /// 原文匹配内容片段
        /// </summary>
        public string 原文
        {
            get
            {
                return _原文;
            }
            set
            {
                _原文 = value;
            }
        }
        private string _原文;
        /// <summary>
        /// 在原文中的位置
        /// </summary>
        public int 索引位置
        {
            get
            {
                return _索引位置;
            }
            set
            {
                _索引位置 = value;
            }
        }
        private int _索引位置;
        /// <summary>
        /// 危险度精确分值，依据匹配的精确度比值计算得出
        /// </summary>
        public double 精确分值
        {
            get
            {
                return _精确分值;
            }
            set
            {
                _精确分值 = value;
            }
        }
        private double _精确分值;
        /// <summary>
        /// 依据实际长度和精确长度的比值计算出的匹配疑似度，取值范围为0到1之间
        /// </summary>
        public double 精确度
        {
            get
            {
                return _精确度;
            }
            set
            {
                _精确度 = value;
            }
        }
        private double _精确度;
        /// <summary>
        /// 匹配项所应用的规则
        /// </summary>
        public 内容审查规则 应用规则
        {
            get
            {
                return _应用规则;
            }
            set
            {
                _应用规则 = value;
            }
        }
        private 内容审查规则 _应用规则;
        /// <summary>
        /// 类目所属的索引序号
        /// </summary>
        public int 类目序号
        {
            get
            {
                return _类目序号;
            }
            set
            {
                _类目序号 = value;
            }
        }
        private int _类目序号;
    }

    [Serializable]
    [XmlInclude(typeof(ObservableCollection<char>))]
    [XmlInclude(typeof(简单内容审查规则))]
    [XmlInclude(typeof(自定义内容审查规则))]
    public abstract class 内容审查规则 : INotifyPropertyChanged
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 内容审查规则()
        {
            缓存 = new Hashtable();
            重置变更状态();
        }

        #region 实现INotifyPropertyChanged

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
            _变更 = true;
            变更时间 = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// 内部缓存表
        /// </summary>
        private Hashtable 缓存;
        /// <summary>
        /// 记录缓存的保存时间
        /// </summary>
        private DateTime 缓存时间;
        /// <summary>
        /// 记录内容变更的触发时间
        /// </summary>
        private DateTime 变更时间;

        /// <summary>
        /// 将值存入缓存
        /// </summary>
        protected void 存入缓存(string 键, object 值)
        {
            缓存[键] = 值;
            缓存时间 = DateTime.Now;
        }

        /// <summary>
        /// 从缓存中取出值，如果键值不存在或已过期则返回null
        /// </summary>
        protected object 取出缓存(string 键)
        {
            if (缓存时间 < 变更时间) return null;
            return 缓存[键];
        }

        /// <summary>
        ///根据实际长度与精确长度的比值计算出精确度，取值范围为0到1之间。
        /// </summary>
        /// <param name="实际长度">实际匹配到的内容长度</param>
        /// <returns>精确度</returns>
        public double 计算精确度(int 实际长度)
        {
            return 精确长度 * 1.00 / 实际长度;
        }

        /// <summary>
        ///根据实际长度与精确长度的比值计算出精确分值
        /// </summary>
        /// <param name="实际长度">实际匹配到的内容长度</param>
        /// <returns>精确分值</returns>
        public double 计算精确分值(int 实际长度)
        {
            return 计算精确度(实际长度) * 分值;
        }

        /// <summary>
        ///根据提供的精确度计算出精确分值
        /// </summary>
        /// <param name="精确度">匹配的精确度，取值为0到1之间</param>
        /// <returns>精确分值</returns>
        public double 计算精确分值(double 精确度)
        {
            return 精确度 * 分值;
        }

        /// <summary>
        /// 匹配表达式
        /// </summary>
        public abstract string 表达式 { get; }
        /// <summary>
        /// 获取对应的正则表达式对象
        /// </summary>
        public abstract Regex 获取正则表达式对象();
        /// <summary>
        /// 首字符列表
        /// </summary>
        [XmlIgnore]
        public abstract ObservableCollection<char> 首字符 { get; }
        /// <summary>
        /// 尾字符列表
        /// </summary>
        [XmlIgnore]
        public abstract ObservableCollection<char> 尾字符 { get; }
        /// <summary>
        /// 最大取样长度
        /// </summary>
        public abstract int 最大长度 { get; }
        /// <summary>
        /// 精确取样长度
        /// </summary>
        public abstract int 精确长度 { get; }
        /// <summary>
        /// 克隆此对象
        /// </summary>
        public abstract 内容审查规则 克隆();
        /// <summary>
        /// 获取此对象的繁体内容版本
        /// </summary>
        public abstract 内容审查规则 获取繁体版本对象();
        /// <summary>
        /// 匹配成功后给予的分值
        /// </summary>
        public int 分值
        {
            get
            {
                return _分值;
            }
            set
            {
                _分值 = value;
                OnPropertyChanged("分值");
            }
        }
        private int _分值;

        /// <summary>
        /// 指示规则所属的类目(.stl文件)，仅供读取，其值应在输出.stl文件时写入
        /// </summary>
        public string 类目
        {
            get
            {
                return _类目;
            }
            set
            {
                _类目 = value;
            }
        }
        private string _类目;

        /// <summary>
        /// 指定此规则的所属类目
        /// </summary>
        /// <param name="类目名称">类目名</param>
        public void 设置类目(string 类目名称)
        {
            _类目 = 类目名称;
        }

        /// <summary>
        /// 指示该项是否已变更
        /// </summary>
        public bool 变更
        {
            get
            {
                return _变更;
            }
        }
        private bool _变更;

        /// <summary>
        /// 将变更状态设为未变更
        /// </summary>
        public void 重置变更状态()
        {
            _变更 = false;
        }
    }

    [Serializable]
    [XmlInclude(typeof(ObservableCollection<char>))]
    public class 简单内容审查规则 : 内容审查规则
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 简单内容审查规则()
            : base()
        {

        }

        /// <summary>
        /// 查询的关键词
        /// </summary>
        public string 关键词
        {
            get
            {
                return _关键词;
            }
            set
            {
                _关键词 = value;
                OnPropertyChanged("关键词");
            }
        }
        private string _关键词;

        /// <summary>
        /// 关键词每个字之间允许存在的干扰字符数
        /// </summary>
        public int 间隔容差
        {
            get
            {
                return _间隔容差;
            }
            set
            {
                _间隔容差 = value;
                OnPropertyChanged("间隔容差");
            }
        }
        private int _间隔容差;

        /// <summary>
        /// 匹配表达式
        /// </summary>
        public override string 表达式
        {
            get
            {
                if (关键词 == null)
                {
                    return "";
                }
                var g = 取出缓存("表达式") as string;
                if (g == null)
                {
                    //"未使用缓存".调试输出();
                    if (间隔容差 > 0)
                    {
                        StringBuilder s = new StringBuilder();
                        int x = 0;
                        foreach (char f in 关键词)
                        {
                            if (x > 0) s.AppendFormat(@"[\s\S]{{0,{0}}}?", 间隔容差);
                            s.Append(f.ToString().进行正则表达式转义());
                            x++;
                        }
                        var t1 = s.ToString();
                        存入缓存("表达式", t1);
                        return t1;
                    }
                    var t2 = 关键词.进行正则表达式转义();
                    存入缓存("表达式", t2);
                    return t2;
                }
                else
                {
                    //"使用缓存".调试输出();
                    return g;
                }
            }
        }

        /// <summary>
        /// 最大取样长度
        /// </summary>
        public override int 最大长度
        {
            get
            {
                if (关键词 == null) return 0;
                return 关键词.Length + 间隔容差 * (关键词.Length - 1);
            }
        }

        /// <summary>
        /// 精确取样长度
        /// </summary>
        public override int 精确长度
        {
            get
            {
                if (关键词 == null) return 0;
                return 关键词.Length;
            }
        }

        /// <summary>
        /// 首字符列表
        /// </summary>
        [XmlIgnore]
        public override ObservableCollection<char> 首字符
        {
            get
            {
                var g = 取出缓存("首字符") as ObservableCollection<char>;
                if (g == null)
                {
                    var t = new ObservableCollection<char> { 关键词.获取首字符() };
                    存入缓存("首字符", t);
                    return t;
                }
                else return g;
            }
        }

        /// <summary>
        /// 尾字符列表
        /// </summary>
        [XmlIgnore]
        public override ObservableCollection<char> 尾字符
        {
            get
            {
                var g = 取出缓存("尾字符") as ObservableCollection<char>;
                if (g == null)
                {
                    var t = new ObservableCollection<char> { 关键词.获取尾字符() };
                    存入缓存("尾字符", t);
                    return t;
                }
                else return g;
            }
        }

        public override 内容审查规则 克隆()
        {
            return new 简单内容审查规则() { 分值 = this.分值, 关键词 = this.关键词, 间隔容差 = this.间隔容差 };
        }

        public override 内容审查规则 获取繁体版本对象()
        {
            var s = this.克隆() as 简单内容审查规则;
            s.关键词 = s.关键词.转换为繁体中文();
            return s;
        }

        /// <summary>
        /// 生成一个正则表达式对象
        /// </summary>
        public override Regex 获取正则表达式对象()
        {
            var g = 取出缓存("正则表达式对象");
            if (g == null)
            {
                var r = new Regex(表达式, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                存入缓存("正则表达式对象", r);
                return r;
            }
            else return g as Regex;
        }
    }

    [Serializable]
    [XmlInclude(typeof(ObservableCollection<char>))]
    public class 自定义内容审查规则 : 内容审查规则
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 自定义内容审查规则()
            : base()
        {
            自定义首字符列表 = new ObservableCollection<char>();
            自定义尾字符列表 = new ObservableCollection<char>();
            初始化集合更改事件();
        }

        /// <summary>
        /// 应在集合内容初始化或反序列化之后执行，以注册更改通知
        /// </summary>
        public void 初始化集合更改事件()
        {
            首字符.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(首字符_CollectionChanged);
            尾字符.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(尾字符_CollectionChanged);
            首字符.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(首字符_CollectionChanged);
            尾字符.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(尾字符_CollectionChanged);
        }

        void 尾字符_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("尾字符");
        }

        void 首字符_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("首字符");
        }

        /// <summary>
        /// 正则表达式的选项
        /// </summary>
        public RegexOptions 表达式选项
        {
            get
            {
                return _表达式选项;
            }
            set
            {
                _表达式选项 = value;
                OnPropertyChanged("表达式选项");
            }
        }
        private RegexOptions _表达式选项;

        /// <summary>
        /// 自定义的表达式
        /// </summary>
        public string 自定义表达式
        {
            get
            {
                return _自定义表达式;
            }
            set
            {
                _自定义表达式 = value;
                OnPropertyChanged("自定义表达式");
            }
        }
        private string _自定义表达式;

        /// <summary>
        /// 匹配表达式
        /// </summary>
        public override string 表达式
        {
            get
            {
                return 自定义表达式;
            }
        }
        /// <summary>
        /// 最大取样长度
        /// </summary>
        public override int 最大长度
        {
            get { return 自定义最大长度; }
        }
        /// <summary>
        /// 最大取样长度
        /// </summary>
        public int 自定义最大长度
        {
            get
            {
                return _自定义最大长度;
            }
            set
            {
                _自定义最大长度 = value;
                OnPropertyChanged("自定义最大长度");
            }
        }
        private int _自定义最大长度;
        /// <summary>
        /// 精确取样长度
        /// </summary>
        public override int 精确长度
        {
            get { return 自定义精确长度; }
        }
        /// <summary>
        /// 精确取样长度
        /// </summary>
        public int 自定义精确长度
        {
            get
            {
                return _自定义精确长度;
            }
            set
            {
                _自定义精确长度 = value;
                OnPropertyChanged("自定义精确长度");
            }
        }
        private int _自定义精确长度;
        /// <summary>
        /// 首字符列表
        /// </summary>
        [XmlIgnore]
        public override ObservableCollection<char> 首字符
        {
            get
            {
                return 自定义首字符列表;
            }
        }
        public ObservableCollection<char> 自定义首字符列表;
        /// <summary>
        /// 尾字符列表
        /// </summary>
        [XmlIgnore]
        public override ObservableCollection<char> 尾字符
        {
            get
            {
                return 自定义尾字符列表;
            }
        }
        public ObservableCollection<char> 自定义尾字符列表;
        /// <summary>
        /// 生成一个正则表达式对象
        /// </summary>
        public override Regex 获取正则表达式对象()
        {
            var g = 取出缓存("正则表达式对象");
            if (g == null)
            {
                var r = new Regex(表达式, 表达式选项 | RegexOptions.Compiled);
                存入缓存("正则表达式对象", r);
                return r;
            }
            else return g as Regex;
        }

        public override 内容审查规则 克隆()
        {
            return new 自定义内容审查规则() { 自定义表达式 = this.自定义表达式, 分值 = this.分值, 表达式选项 = this.表达式选项, 自定义最大长度 = this.自定义最大长度, 自定义首字符列表 = new ObservableCollection<char>(this.自定义首字符列表.ToList()), 自定义尾字符列表 = new ObservableCollection<char>(this.自定义尾字符列表.ToList()) };
        }

        public override 内容审查规则 获取繁体版本对象()
        {
            var s = this.克隆() as 自定义内容审查规则;
            s.自定义表达式 = s.自定义表达式.转换为繁体中文();
            for (int i = 0; i < s.自定义首字符列表.Count; i++)
            {
                s.自定义首字符列表[i] = s.自定义首字符列表[i].ToString().转换为繁体中文()[0];
            }
            for (int i = 0; i < s.自定义尾字符列表.Count; i++)
            {
                s.自定义尾字符列表[i] = s.自定义尾字符列表[i].ToString().转换为繁体中文()[0];
            }
            return s;
        }
    }
}
