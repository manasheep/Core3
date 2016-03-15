using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.ContentCheck
{
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

}
