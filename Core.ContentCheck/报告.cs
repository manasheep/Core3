using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Core.ContentCheck
{
    public class 报告
    {
        /// <summary>
        /// 用于审查的内容。由构造函数制定。
        /// </summary>
        public string 输入内容 { get; set; }

        /// <summary>
        /// 输出的审查明细列表。在执行审查时生成。
        /// </summary>
        public List<匹配明细> 输出明细 { get; set; }

        /// <summary>
        /// 对内容中捕获词语的最高评分。在执行审查时生成。
        /// </summary>
        public int 最高评分 { get; set; }

        /// <summary>
        /// 对内容中捕获词语的累计评分。在执行审查时生成。
        /// </summary>
        public int 累计评分 { get; set; }

        /// <summary>
        /// 对内容中捕获词语的最高精确评分，依据匹配的精确度比值计算得出。在执行审查时生成。
        /// </summary>
        public double 最高精确评分 { get; set; }

        /// <summary>
        /// 对内容中捕获词语的累计精确评分，依据匹配的精确度比值计算得出。在执行审查时生成。
        /// </summary>
        public double 累计精确评分 { get; set; }

        /// <summary>
        /// 匹配的平均精确度，取值范围为0到1之间。在执行审查时生成。
        /// </summary>
        public double 平均精确度 { get; set; }

        /// <summary>
        /// 捕获词语字数与全文字总数的比例，取值范围为0到1之间。在执行审查时生成。
        /// </summary>
        public double 捕获比例 { get; set; }

    }
}
