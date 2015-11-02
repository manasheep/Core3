using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.JScript.Vsa;

namespace Core.Maths
{
    public partial class Maths处理函数
    {
        /// <summary>
        /// 计算字符串算式，并返回结果
        /// </summary>
        /// <param name="算式">如:(356+258)*3/0.3%2</param>
        /// <returns>计算结果</returns>
        public static double 计算字符串算式(string 算式)
        {
            var ve = VsaEngine.CreateEngine();
            //获取计算结果 ret
            object ret = Microsoft.JScript.Eval.JScriptEvaluate(算式, ve);
            //返回结果
            return Convert.ToDouble(ret);
        }
    }
}
