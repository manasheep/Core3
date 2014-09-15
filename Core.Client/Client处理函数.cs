using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Core
{
    public static class Client处理函数
    {
        /// <summary>
        /// 使用资源管理器浏览并选定文件
        /// </summary>
        public static void 使用资源管理器浏览并选定文件(string 文件路径)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            proc.StartInfo.Arguments = @"/select," + 文件路径;
            proc.Start();
        }

        /// <summary>
        /// 使用资源管理器浏览目录
        /// </summary>
        public static void 使用资源管理器浏览目录(string 目录路径)
        {
            System.Diagnostics.Process.Start("explorer.exe", 目录路径);
        }

        /// <summary>
        /// 使用默认浏览器打开指定网址
        /// </summary>
        /// <param name="网址">要打开的网址</param>
        public static void 使用浏览器打开网址(string 网址)
        {
            System.Diagnostics.Process.Start(网址);
        }

        /// <summary>
        /// 用于检测是否没有相同实例在运行
        /// </summary>
        /// <param name="程序标识字串">用于标识程序的字符串</param>
        /// <returns>是否没有相同实例在运行</returns>
        public static bool 程序互斥判断(string 程序标识字串)
        {
            bool B;
            Mutex M = new Mutex(true, 程序标识字串, out B);
            return B;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FileInfoStruct
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public enum FileInfoFlags : int
        {
            SHGFI_ICON = 0x000000100,  //  get icon 
            SHGFI_DISPLAYNAME = 0x000000200,  //  get display name 
            SHGFI_TYPENAME = 0x000000400,  //  get type name 
            SHGFI_ATTRIBUTES = 0x000000800,  //  get attributes 
            SHGFI_ICONLOCATION = 0x000001000,  //  get icon location 
            SHGFI_EXETYPE = 0x000002000,  //  return exe type 
            SHGFI_SYSICONINDEX = 0x000004000,  //  get system icon index 
            SHGFI_LINKOVERLAY = 0x000008000,  //  put a link overlay on icon 
            SHGFI_SELECTED = 0x000010000,  //  show icon in selected state 
            SHGFI_ATTR_SPECIFIED = 0x000020000,  //  get only specified attributes 
            SHGFI_LARGEICON = 0x000000000,  //  get large icon 
            SHGFI_SMALLICON = 0x000000001,  //  get small icon 
            SHGFI_OPENICON = 0x000000002,  //  get open icon 
            SHGFI_SHELLICONSIZE = 0x000000004,  //  get shell size icon 
            SHGFI_PIDL = 0x000000008,  //  pszPath is a pidl 
            SHGFI_USEFILEATTRIBUTES = 0x000000010,  //  use passed dwFileAttribute 
            SHGFI_ADDOVERLAYS = 0x000000020,  //  apply the appropriate overlays 
            SHGFI_OVERLAYINDEX = 0x000000040   //  Get the index of the overlay 
        }

        public enum FileAttributeFlags : int
        {
            FILE_ATTRIBUTE_READONLY = 0x00000001,
            FILE_ATTRIBUTE_HIDDEN = 0x00000002,
            FILE_ATTRIBUTE_SYSTEM = 0x00000004,
            FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
            FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
            FILE_ATTRIBUTE_DEVICE = 0x00000040,
            FILE_ATTRIBUTE_NORMAL = 0x00000080,
            FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
            FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
            FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
            FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
            FILE_ATTRIBUTE_OFFLINE = 0x00001000,
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
            FILE_ATTRIBUTE_ENCRYPTED = 0x00004000
        }

        [DllImport("shell32.dll ", EntryPoint = "SHGetFileInfo")]
        private static extern int GetFileInfo(string pszPath, int dwFileAttributes,
            ref  FileInfoStruct psfi, int cbFileInfo, int uFlags);

        /// <summary>
        /// 获取文件或目录对应的系统图标
        /// </summary>
        /// <param name="路径">文件或目录路径</param>
        /// <param name="大图标">指示获取大图标或小图标</param>
        /// <returns>对应图标</returns>
        public static Icon 获取系统图标(string 路径, bool 大图标)
        {
            return 大图标 ? GetLargeIcon(路径) : GetSmallIcon(路径);
        }

        private static Icon GetLargeIcon(string 路径)
        {
            FileInfoStruct _info = new FileInfoStruct();
            GetFileInfo(路径, 0, ref  _info, Marshal.SizeOf(_info),
                (int)(FileInfoFlags.SHGFI_ICON | FileInfoFlags.SHGFI_LARGEICON));
            try
            {
                return Icon.FromHandle(_info.hIcon);
            }
            catch
            {
                return null;
            }
        }

        private static Icon GetSmallIcon(string 路径)
        {
            FileInfoStruct _info = new FileInfoStruct();
            GetFileInfo(路径, 0, ref  _info, Marshal.SizeOf(_info),
                (int)(FileInfoFlags.SHGFI_ICON | FileInfoFlags.SHGFI_SMALLICON));
            try
            {
                return Icon.FromHandle(_info.hIcon);
            }
            catch
            {
                return null;
            }
        }
    }
}
