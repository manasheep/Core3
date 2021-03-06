﻿using System;
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
    public class 自定义规则 : 规则
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 自定义规则()
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

        public override 规则 克隆()
        {
            return new 自定义规则() { 自定义表达式 = this.自定义表达式, 分值 = this.分值, 表达式选项 = this.表达式选项, 自定义最大长度 = this.自定义最大长度, 自定义首字符列表 = new ObservableCollection<char>(this.自定义首字符列表.ToList()), 自定义尾字符列表 = new ObservableCollection<char>(this.自定义尾字符列表.ToList()) };
        }

        public override 规则 获取繁体版本对象()
        {
            var s = this.克隆() as 自定义规则;
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
