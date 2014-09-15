using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;

namespace Core.Environment
{
    public static class Environment变量
    {
        /// <summary>
        /// 获取系统当前的用户名
        /// </summary>
        public static string 用户名
        {
            get { return System.Environment.UserName; }
        }

        /// <summary>
        /// 获取系统当前的公共语言运行库版本
        /// </summary>
        public static string 运行库版本
        {
            get { return System.Environment.Version.ToString(); }
        }

        /// <summary>
        /// 获得操作系统版本
        /// </summary>
        /// <值>操作系统版本</值>
        public static string 操作系统版本
        {
            get { return System.Environment.OSVersion.Version.ToString(); }
        }

        /// <summary>
        /// 获得系统目录路径
        /// </summary>
        /// <值>系统目录路径</值>
        public static string 系统目录路径
        {
            get { return System.Environment.SystemDirectory; }
        }

        /// <summary>
        /// 获得系统临时文件夹目录路径
        /// </summary>
        public static string 临时文件目录路径
        {
            get { return Path.GetTempPath(); }
        }

        /// <summary>
        /// 获得系统桌面的物理目录路径
        /// </summary>
        public static string 桌面目录路径
        {
            get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory); }
        }

        /// <summary>
        /// 获得系统启动目录的路径
        /// </summary>
        public static string 启动目录路径
        {
            get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Startup); }
        }

        /// <summary>
        /// 获得系统启动时间，以毫秒为单位
        /// </summary>
        /// <值>系统启动时间</值>
        public static int 系统启动时间
        {
            get { return System.Environment.TickCount; }
        }

        /// <summary>
        /// 从App.config或Web.config中读取应用程序数据库连接字符串配置信息
        /// </summary>
        public static ConnectionStringSettingsCollection 应用程序数据库连接字符串配置信息
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        /// <summary>
        /// 从App.config或Web.config中读取应用程序配置信息
        /// </summary>
        public static NameValueCollection 应用程序配置信息
        {
            get { return ConfigurationManager.AppSettings; }
        }
    }
}
