using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.Windows;

namespace Core.Environment
{
   public static class ClientEnvironment变量
    {
        /// <summary>
        /// 获取第一个CPU的序列号
        /// </summary>
        public static string CPU序列号
        {
            get
            {
                ManagementClass cimobject = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject f in moc) return f.Properties["ProcessorId"].Value.ToString();
                return null;
            }
        }

        /// <summary>
        /// 获取第一个硬盘的编号
        /// </summary>
        public static string 硬盘编号
        {
            get
            {
                ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject f in moc) return f.Properties["Model"].Value.ToString();
                return null;
            }
        }

        /// <summary>
        /// 获取第一个网卡的MAC地址
        /// </summary>
        public static string 网卡MAC地址
        {
            get
            {
                ManagementClass cimobject = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject f in moc) if ((bool)f["IPEnabled"]) return f["MacAddress"].ToString();
                return null;
            }
        }

        /// <summary>
        /// 获得计算机名
        /// </summary>
        /// <value>计算机名</value>
        public static string 计算机名
        {
            get { return SystemInformation.ComputerName; }
        }

        /// <summary>
        /// 获得用户所在的域名
        /// </summary>
        /// <value>用户所在的域名</value>
        public static string 用户所在的域名
        {
            get { return SystemInformation.UserDomainName; }
        }

        /// <summary>
        /// 获取鼠标当前所在的屏幕坐标值
        /// </summary>
        public static System.Drawing.Point 鼠标坐标
        {
            get { return Control.MousePosition; }
        }

        public static double 桌面分辨率宽度
        {
            get { return SystemParameters.PrimaryScreenWidth; }
        }

        public static double 桌面分辨率高度
        {
            get { return SystemParameters.PrimaryScreenHeight; }
        }

        /// <summary>
        /// 获得程序所在目录路径
        /// </summary>
        /// <value>程序所在目录路径</value>
        public static string 程序所在目录路径
        {
            get { return System.Windows.Forms.Application.StartupPath; }
        }

        /// <summary>
        /// 获得程序版本号
        /// </summary>
        /// <value>程序版本号</value>
        public static string 程序版本号
        {
            get { return System.Windows.Forms.Application.ProductVersion; }
        }

        /// <summary>
        /// 获得程序名称
        /// </summary>
        /// <value>程序名称</value>
        public static string 程序名称
        {
            get { return System.Windows.Forms.Application.ProductName; }
        }

    }
}
