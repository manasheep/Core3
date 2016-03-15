using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Core.ContentCheck
{
    [Serializable]
    [XmlInclude(typeof(List<char>))]
    [XmlInclude(typeof(简单规则))]
    [XmlInclude(typeof(自定义规则))]
    public abstract class 规则
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public 规则()
        {
            缓存 = new Hashtable();
        }

        /// <summary>
        /// 内部缓存表
        /// </summary>
        private Hashtable 缓存;

        /// <summary>
        /// 将值存入缓存
        /// </summary>
        protected void 存入缓存(string 键, object 值)
        {
            缓存[键] = 值;
        }

        /// <summary>
        /// 从缓存中取出值，如果键值不存在或已过期则返回null
        /// </summary>
        protected object 取出缓存(string 键)
        {
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
        public abstract List<char> 首字符 { get; }
        /// <summary>
        /// 尾字符列表
        /// </summary>
        [XmlIgnore]
        public abstract List<char> 尾字符 { get; }
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
        public abstract 规则 克隆();
        /// <summary>
        /// 获取此对象的繁体内容版本
        /// </summary>
        public abstract 规则 获取繁体版本对象();
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

    }
}
