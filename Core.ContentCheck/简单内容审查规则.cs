using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Core.Text;

namespace Core.ContentCheck
{
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
}
