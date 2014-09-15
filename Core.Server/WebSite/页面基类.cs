using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Core.WebSite
{
    /// <summary>
    /// 网页页面基类
    /// </summary>
    public abstract class 页面基类 : Page
    {
        public 页面基类()
        {

        }

        /// <summary>
        /// 获得当前页面的文件名
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public string 文件名
        {
            get
            {
                return WebSite处理函数.获取页面名称();
            }
        }

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public bool 是否为Post请求
        {
            get
            {
                return WebSite处理函数.是否为Post请求();
            }
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public bool 是否为Get请求
        {
            get
            {
                return WebSite处理函数.是否为Get请求();
            }
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public string 客户端IP地址
        {
            get
            {
                return WebSite处理函数.获取客户端IP地址();
            }
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public string 前一个页面地址
        {
            get
            {
                return WebSite处理函数.获取前一个页面地址();
            }
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))。例如：/default.aspx?type=3
        /// </summary>
        /// <returns>原始 URL</returns>
        public string 原始URL地址
        {
            get
            {
                return WebSite处理函数.获取原始URL地址();
            }
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public bool 是否为浏览器访问
        {
            get
            {
                return WebSite处理函数.是否为浏览器访问();
            }
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public bool 是否为搜索引擎链接
        {
            get
            {
                return WebSite处理函数.是否为搜索引擎链接();
            }
        }

        /// <summary>
        /// 获得当前完整Url地址，如：http://www.abc.com/default.aspx?type=3
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public string 完整URL地址
        {
            get
            {
                return WebSite处理函数.获取完整URL地址();
            }
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="参数名">Url参数名</param>
        /// <returns>Url参数的值</returns>
        public string 获取URL参数值(string 参数名)
        {
            return WebSite处理函数.获取URL参数值(参数名);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="参数名">表单参数名</param>
        /// <returns>表单参数的值</returns>
        public string 获取表单参数值(string 参数名)
        {
            return WebSite处理函数.获取表单参数值(参数名);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <returns>Url或表单参数的值</returns>
        public string 获取表单或URL参数值(string 参数名)
        {
            return WebSite处理函数.获取表单或URL参数值(参数名);
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public int 参数总数
        {
            get
            {
                return WebSite处理函数.获取参数总数();
            }
        }

        /// <summary>
        /// 将动态页面转换为静态HTML页
        /// </summary>
        public void 转换为静态HTML页面(string 输出文件路径)
        {
            WebSite处理函数.转换为静态HTML页面(文件名, 输出文件路径);
        }

        /// <summary>
        /// 通过客户端脚本向客户端显示对话框
        /// </summary>
        /// <param name="内容">对话框内容</param>
        public void 显示对话框(string 内容)
        {
            Page.显示对话框(内容);
        }
    }
}
