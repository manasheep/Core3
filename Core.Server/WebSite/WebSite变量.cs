using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace Core.WebSite
{
    public static class WebSite变量
    {
        /// <summary>
        /// 获取当前会话对象
        /// </summary>
        public static HttpSessionState Session
        {
            get { return Current.Session; }
        }

        /// <summary>
        /// 获取当前服务器对象
        /// </summary>
        public static HttpServerUtility Server
        {
            get { return Current.Server; }
        }

        /// <summary>
        /// 获取当前HTTP请求信息对象，其中包含会话、全局、服务器等环境变量
        /// </summary>
        public static HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        /// <summary>
        /// 获取当前应用程序的缓存
        /// </summary>
        public static Cache Cache
        {
            get { return HttpRuntime.Cache; }
        }

        /// <summary>
        /// 获取服务器根目录路径的物理路径
        /// </summary>
        public static string 服务器根目录路径
        {
            get { return HttpRuntime.AppDomainAppPath; }
        }
    }
}
