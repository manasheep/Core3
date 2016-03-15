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
    public class 内容审查器
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="审查规则列表">应用的所有审查规则</param>
        public 内容审查器(List<规则> 审查规则列表)
        {
            加载审查规则(审查规则列表);
        }

        /// <summary>
        /// 显示所有出现规则的类目
        /// </summary>
        public List<string> 类目列表
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
        private List<string> _类目列表;

        protected void 加载审查规则(List<规则> 审查规则列表)
        {
            类目列表 = new List<string>();

            var l = 审查规则列表;
            //追加繁体化的规则
            foreach (规则 f in l.ToArray())
            {
                l.Add(f.获取繁体版本对象());
            }
            //进行处理
            规则 = new Dictionary<string, List<规则>>();
            首字符 = new List<char>();
            尾字符 = new List<char>();
            foreach (规则 f in l)
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
                        if (!规则.Keys.Contains(ts)) 规则[ts] = new List<规则>();
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

        /// <summary>
        /// 内容审查遵循的处理规则列表。
        /// </summary>
        protected Dictionary<string, List<规则>> 规则
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
        private Dictionary<string, List<规则>> _规则;
        protected List<char> 首字符
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
        private List<char> _首字符;
        protected List<char> 尾字符
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
        private List<char> _尾字符;
        /// <summary>
        /// 所有规则中的最大取样长度
        /// </summary>
        protected int 最大长度
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
        private int _最大长度;

        /// <summary>
        /// 审查内容
        /// </summary>
        /// <param name="内容">待审查的内容</param>
        /// <param name="生成输出明细">指示是否在生成的报告中包含输出明细</param>
        /// <returns>报告</returns>
        public 报告 审查(string 内容, bool 生成输出明细)
        {
            if (!内容.验证有效性()) return null;

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
            var outlist = new ObservableCollection<匹配明细>();

            //捕获词语累计长度
            var getstr = 0;

            //当前处理索引位置
            var nowindex = 0;
            var st = new List<触发记录>();
            var ed = new List<触发记录>();
            #endregion

            #region 检测首尾字符匹配情况
            for (int i = 0; i < 内容.Length; i++)
            {
                if (首字符.Contains(内容[i])) st.Add(new 触发记录() { 索引位置 = i, 字符 = 内容[i] });
                if (尾字符.Contains(内容[i])) ed.Insert(0, new 触发记录() { 索引位置 = i, 字符 = 内容[i] });
            }
            #endregion

            #region 根据首尾匹配列表做进一步处理
            foreach (触发记录 起始字符 in st)
            {
                if (nowindex > 起始字符.索引位置) continue;
                var 起始字符1 = 起始字符;
                foreach (触发记录 结束字符 in ed.Where(f => f.索引位置 >= 起始字符1.索引位置 && f.索引位置 <= 起始字符1.索引位置 + 最大长度))
                {
                    var key = 起始字符.字符.ToString() + 结束字符.字符;
                    if (规则.Keys.Contains(key) && 规则[key].获取最大长度() >= 结束字符.索引位置 - 起始字符.索引位置)
                    {
                        var 结束字符1 = 结束字符;
                        var 起始字符2 = 起始字符;
                        foreach (规则 检测规则 in 规则[key].Where(f => 结束字符1.索引位置 - 起始字符2.索引位置 + 1 <= f.最大长度))
                        {
                            var s = 内容.Substring(起始字符.索引位置, 结束字符.索引位置 - 起始字符.索引位置 + 1);
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

                                if (生成输出明细) outlist.Add(new 匹配明细 { 应用规则 = 检测规则, 索引位置 = 起始字符.索引位置, 原文 = s, 精确分值 = t, 精确度 = d, 类目序号 = 类目列表.IndexOf(检测规则.类目) });

                                nowindex = 结束字符.索引位置 + 1;
                                goto end;
                            }
                        }
                    }
                }
            end: continue;
            }
            #endregion

            #region 赋值

            var obj = new 报告()
            {
                输入内容 = 内容,
                最高评分 = ms,
                累计评分 = cs,
                最高精确评分 = rms,
                累计精确评分 = rcs,
                输出明细 = outlist,
                捕获比例 = getstr * 1.0 / 内容.Length,
                平均精确度 = ad.Count > 0 ? ad.Average() : 0
            };
            return obj;

            #endregion
        }

    }
}
