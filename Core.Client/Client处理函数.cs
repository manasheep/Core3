using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace Core
{
    public static class Client处理函数
    {
        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);
        [DllImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpText, int nCount);

        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(uint dwErrCode);

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string classname, string captionName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindowEx(IntPtr parent, IntPtr child, string classname, string captionName);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32")]
        static extern bool SetWindowText(IntPtr hwnd, string windowName);

        public static void 将窗口显示到桌面前端(IntPtr 窗口句柄)
        {
            SetForegroundWindow(窗口句柄);
        }

        public static IntPtr 获取窗口句柄(Regex 窗口标题文字匹配表达式)
        {
            var outptr = IntPtr.Zero;
            EnumWindows((IntPtr p, uint u) =>
            {
                var sb = new StringBuilder(50);
                GetWindowText(p, sb, sb.Capacity);
                var s = sb.ToString();
                if (窗口标题文字匹配表达式.IsMatch(s))
                {
                    outptr = p;
                }
                return true;
            }, 0);
            return outptr;
        }

        public static IntPtr 获取窗口句柄(string 窗口标题)
        {
            return FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, null, 窗口标题);
        }

        public static IntPtr 获取窗口句柄(string 窗口类名, string 窗口标题)
        {
            return FindWindowEx(System.IntPtr.Zero, System.IntPtr.Zero, 窗口类名, 窗口标题);
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        public static void 设置窗口坐标位置(IntPtr 窗口句柄, int x, int y, bool 强制前端显示)
        {
            uint flags = 0x0010 | 0x0001;
            //组合上0x0002参数将忽略x、y参数，即不改变窗口位置
            if (强制前端显示) flags = 0x0040 | 0x0001;
            SetWindowPos(窗口句柄, (IntPtr)(强制前端显示 ? -1 : 1), x, y, 0, 0, flags);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }
        public static RECT 获取窗口矩形(IntPtr 窗口句柄)
        {
            IntPtr awin = 窗口句柄;
            RECT rect = new RECT();
            GetWindowRect(awin, ref rect);
            return rect;
        }

        private const int GWL_STYLE = -16;
        private const long WS_CAPTION = 0x00C00000L;
        private const long WS_CAPTION_2 = 0X00C0000L;
        [DllImport("User32.dll")]
        private static extern long GetWindowLong(IntPtr handle, int style);
        [DllImport("User32.dll")]
        private static extern void SetWindowLong(IntPtr handle, int oldStyle, long newStyle);
        public static void 设为无边框窗口(IntPtr 窗口句柄)
        {
            long oldstyle = GetWindowLong(窗口句柄, GWL_STYLE);
            SetWindowLong(窗口句柄, GWL_STYLE, oldstyle & (~(WS_CAPTION | WS_CAPTION_2)));
        }

        public static Bitmap 获取窗口截图(IntPtr 窗口句柄)
        {
            var r = 获取窗口矩形(窗口句柄);
            //记录日志("识别游戏窗口矩形区","{0},{1} {2},{3}".FormatWith(r.Left,r.Top,r.Right-r.Left,r.Bottom-r.Top));
            return 获取截图(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top, PixelFormat.Format24bppRgb);
        }

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key click flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bSCan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        private static extern byte MapVirtualKey(byte wCode, int wMap);

        public static void 模拟按下按键(VirtualKeyCode 虚拟按键代码)
        {
            var code = (byte)虚拟按键代码;
            keybd_event(code, 0, 0, 0);
        }

        public static void 模拟弹起按键(VirtualKeyCode 虚拟按键代码)
        {
            var code = (byte) 虚拟按键代码;
            keybd_event(code, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void 模拟单击按键(VirtualKeyCode 虚拟按键代码)
        {
            var code = (byte)虚拟按键代码;
            keybd_event(code, 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        /// <summary>
        /// 模拟键盘按键输入，具体指令参看：http://www.cnblogs.com/sydeveloper/archive/2013/02/25/2932571.html
        /// 注意：此功能在某些情况下不能正常工作，建议使用调用WindowsAPI版本的方法
        /// </summary>
        /// <param name="按键输入内容">输入内容</param>
        public static void 模拟键盘输入(string 按键输入内容)
        {
            SendKeys.SendWait(按键输入内容);
        }

        public static Bitmap 获取截图(int x, int y, int width, int height, PixelFormat 像素格式)
        {
            Bitmap myImage = new Bitmap(width, height, 像素格式);
            Graphics g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(x, y), new Point(0, 0), new Size(myImage.Width, myImage.Height));
            IntPtr dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);
            return myImage;
        }

        public static Bitmap 获取截图(Rectangle rect, PixelFormat 像素格式)
        {
            return 获取截图(rect.X, rect.Y, rect.Width, rect.Height, 像素格式);
        }

        /// <summary>
        /// 鼠标控制参数
        /// </summary>
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        /// <summary>
        /// 鼠标的位置
        /// </summary>
        private struct PONITAPI
        {
            public int x, y;
        }
        [DllImport("user32.dll")]
        private static extern int GetCursorPos(ref PONITAPI p);
        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public static void 模拟鼠标单击(int x, int y)
        {
            模拟鼠标移动(x, y);
            Thread.Sleep(55);
            模拟鼠标左键按下(x, y);
            Thread.Sleep(55);
            模拟鼠标左键抬起(x, y);
        }

        public static void 模拟鼠标左键按下(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
        }
        public static void 模拟鼠标左键抬起(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
        public static void 模拟鼠标右键按下(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTDOWN, x, y, 0, 0);
        }
        public static void 模拟鼠标右键抬起(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_RIGHTUP, x, y, 0, 0);
        }

        public static void 模拟鼠标右键单击(int x, int y)
        {
            模拟鼠标移动(x, y);
            Thread.Sleep(55);
            模拟鼠标右键按下(x, y);
            Thread.Sleep(55);
            模拟鼠标右键抬起(x, y);
        }

        public static void 模拟鼠标拖拽(int x, int y, int x2, int y2)
        {
            模拟鼠标移动(x, y);
            Thread.Sleep(55);
            模拟鼠标左键按下(x, y);
            Thread.Sleep(55);
            模拟鼠标移动(x2, y2);
            Thread.Sleep(55);
            模拟鼠标左键抬起(x, y);
        }

        public static void 模拟鼠标移动(int x, int y)
        {
            SetCursorPos(x, y);
        }

        //注册热键的api
        [DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint control, Keys vk);
        //解除注册热键的api
        [DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        /// <summary>
        /// 注册系统热键
        /// </summary>
        /// <param name="窗口句柄">本程序的窗口句柄，用于接收热键，通过窗体重写WndProc(ref Message m)方法实现接收热键事件，判断当m.Msg为0x0312时触发热键行为，热键编号通过m.WParam获得</param>
        /// <param name="编号">热键独立编号</param>
        /// <param name="辅助键">None = 0,   Alt = 1,  crtl= 2,  Shift = 4,   Windows = 8，多个辅助键用与运算组合</param>
        /// <param name="按键">按键</param>
        public static void 注册系统热键(IntPtr 窗口句柄, int 编号, uint 辅助键, Keys 按键)
        {
            RegisterHotKey(窗口句柄, 编号, 辅助键, 按键);
        }
        public static void 注销系统按键(IntPtr 窗口句柄, int 编号)
        {
            UnregisterHotKey(窗口句柄, 编号);
        }
        /// <summary>
        /// 获取触发热键编号，如果没有触发则返回-1
        /// </summary>
        /// <param name="消息">窗体重写WndProc(ref Message m)方法中得到的参数</param>
        /// <returns>触发热键编号，如果没有触发则返回-1</returns>
        public static int 获取触发热键编号(Message 消息)
        {
            if (消息.Msg == 0x0312)
            {
                return (int)消息.WParam;
            }
            else return -1;
        }

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
