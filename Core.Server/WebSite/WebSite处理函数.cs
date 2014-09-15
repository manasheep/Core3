using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Text;
using System.Web;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Configuration;
using System.Net;
using Core.Net;
using System.IO;
using System.Web.UI;
using Core.Web;
using Core.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Core.WebSite
{
    public static class WebSite处理函数
    {

        /// <summary>
        /// 生成分页浏览的页面索引
        /// </summary>
        /// <param name="总页数">总页数</param>
        /// <param name="当前页">当前页码(以0起始)</param>
        /// <param name="外围">外围格式化字符串,期间包含所有项目,使用{0}代表内部项目</param>
        /// <param name="常规项目">代表一般的项目格式化字符串,使用{0}代表页码文字说明部分,使用{1}代表链接部分</param>
        /// <param name="当前项目">代表当前页面的项目格式化字符串,使用{0}代表页码文字说明部分,使用{1}代表链接部分</param>
        /// <param name="上一页项目">代表上一页项目格式化字符串,使用{1}代表链接部分</param>
        /// <param name="下一页项目">代表下一页项目格式化字符串,使用{1}代表链接部分</param>
        /// <returns>HTML代码</returns>
        public static string 生成页面索引HTML代码(int 总页数, int 当前页, string 外围, string 常规项目, string 当前项目, string 上一页项目, string 下一页项目)
        {
            StringBuilder S = new StringBuilder();
            if (当前页 > 0) S.AppendLine(String.Format(上一页项目, "", 当前页 - 1));
            for (int i = 0; i < 总页数; i++)
            {
                if (i == 0 || i == 总页数 - 1 || (i < 当前页 + 5 && i > 当前页 - 5)) S.AppendLine(String.Format(i == 当前页 ? 当前项目 : 常规项目, i + 1, i));
                else if (i == 当前页 - 5 || i == 当前页 + 5) S.AppendLine(String.Format(常规项目, "...", i));
            }
            if (当前页 < 总页数 - 1) S.AppendLine(String.Format(下一页项目, "", 当前页 + 1));
            return String.Format(外围, S);
        }

        /// <summary>
        /// 生成分页浏览的页面索引
        /// </summary>
        /// <param name="总页数">总页数</param>
        /// <param name="当前页">当前页码(以0起始)</param>
        /// <param name="列表类">列表CSS类</param>
        /// <param name="常规项目类">常规项目CSS类</param>
        /// <param name="当前项目类">当前项目CSS类</param>
        /// <param name="上一页字符">上一页链接字符</param>
        /// <param name="下一页字符">下一页链接字符</param>
        /// <param name="页面链接代码">用于显示内容的页面链接代码，使用{1}代表链接中的页码</param>
        /// <returns>HTML代码</returns>
        public static string 生成页面索引HTML代码(int 总页数, int 当前页, string 列表类, string 常规项目类, string 当前项目类, string 上一页字符, string 上一页项目类, string 下一页字符, string 下一页项目类, string 页面链接代码)
        {
            return 生成页面索引HTML代码(
                总页数,
                当前页,
@"<ul class=""" + 列表类 + @""">{0}</ul>",
@"<li class=""" + 常规项目类 + @"""><a href=""" + 页面链接代码 + @""" >{0}</a></li>",
@"<li class=""" + 当前项目类 + @"""><a href=""" + 页面链接代码 + @""" >{0}</a></li>",
@"<li class=""" + 上一页项目类 + @"""><a href=""" + 页面链接代码 + @""" >" + 上一页字符 + "</a></li>",
@"<li class=""" + 下一页项目类 + @"""><a href=""" + 页面链接代码 + @""" >" + 下一页字符 + "</a></li>"
);
        }

        /// <summary>
        /// 生成分页浏览的页面索引，其中自动保留当前请求的URL参数
        /// </summary>
        /// <param name="总页数">总页数</param>
        /// <param name="当前页">当前页码(以0起始)</param>
        /// <param name="列表类">列表CSS类</param>
        /// <param name="常规项目类">常规项目CSS类</param>
        /// <param name="当前项目类">当前项目CSS类</param>
        /// <param name="上一页字符">上一页链接字符</param>
        /// <param name="下一页字符">下一页链接字符</param>
        /// <param name="页面名称">显示内容的页面名称，如"MyList.aspx"</param>
        /// <param name="URL页面标识参数">用于判断当前页面的URL参数名称，如"page"</param>
        /// <returns>HTML代码</returns>
        public static string 生成页面索引HTML代码(int 总页数, int 当前页, string 列表类, string 常规项目类, string 当前项目类, string 上一页字符, string 上一页项目类, string 下一页字符, string 下一页项目类, string 页面名称, string URL页面标识参数)
        {
            bool b = true;
            StringBuilder S = new StringBuilder();
            foreach (string f in WebSite变量.Current.Request.QueryString.AllKeys)
            {
                if (S.Length > 0) S.Append("&");
                if (f.ToLower() == URL页面标识参数.ToLower())
                {
                    b = false;
                    S.Append(f + "={1}");
                }
                else S.Append(f + "=" + WebSite变量.Current.Request.QueryString[f]);
            }
            if (b)
            {
                if (S.Length > 0) S.Append("&");
                S.Append(URL页面标识参数 + "={1}");
            }
            return 生成页面索引HTML代码(总页数, 当前页, 列表类, 常规项目类, 当前项目类, 上一页字符, 上一页项目类, 下一页字符, 下一页项目类, 页面名称 + "?" + S);
        }

        /// <summary>
        /// 依据当前请求的URL参数，生成其参数部分的字符串
        /// </summary>
        public static string 生成URL参数字串()
        {
            StringBuilder S = new StringBuilder();
            foreach (string f in WebSite变量.Current.Request.QueryString.AllKeys)
            {
                if (S.Length > 0) S.Append("&");
                S.Append(f + "=" + WebSite变量.Current.Request.QueryString[f]);
            }
            return S.ToString();
        }

        /// <summary>
        /// 依据当前请求的URL参数，生成其参数部分的字符串，替代项字典中的键值对将被替换为新值
        /// </summary>
        /// <param name="替代项字典">用于对当前选项的值进行替换</param>
        /// <param name="追加未替代项">如果替代项不存在于当前Url参数中，则追加该项为新参数</param>
        public static string 生成URL参数字串(Dictionary<string, string> 替代项字典, bool 追加未替代项)
        {
            var fl = new List<string>();
            StringBuilder S = new StringBuilder();
            foreach (string f in WebSite变量.Current.Request.QueryString.AllKeys)
            {
                if (S.Length > 0) S.Append("&");
                var b = true;
                foreach (var q in 替代项字典.Keys)
                {
                    if (q.ToLower() == f.ToLower())
                    {
                        S.Append(f + "=" + 替代项字典[q]);
                        fl.Add(q);
                        b = false;
                        break;
                    }
                }
                if (b) S.Append(f + "=" + WebSite变量.Current.Request.QueryString[f]);
            }
            if (追加未替代项)
            {
                foreach (var f in 替代项字典.Keys)
                {
                    if (!f.IsIn(fl))
                    {
                        S.AppendAndSeparated(f + "=" + 替代项字典[f], "&");
                    }
                }
            }
            return S.ToString();
        }

        /// <summary>
        /// 读取Web.Config中的邮件发送节点
        /// </summary>
        /// <returns>邮件发送节点</returns>
        public static SmtpSection 读取网站配置文件邮件发送节点()
        {
            return NetSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("/")).MailSettings.Smtp;
        }

        /// <summary> 
        /// 设置应用程序配置appSettings节点，如果已经存在此节点，则会修改该节点的值，否则添加此节点
        /// </summary> 
        /// <param name="键">名称</param> 
        /// <param name="值">值</param> 
        public static void 设置网站配置文件程序配置节点值(string 键, string 值)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("/");
            AppSettingsSection appSetting = (AppSettingsSection)config.GetSection("appSettings");
            if (appSetting.Settings[键] == null)//如果不存在此节点，则添加 
            {
                appSetting.Settings.Add(键, 值);
            }
            else//如果存在此节点，则修改 
            {
                appSetting.Settings[键].Value = 值;
            }
            config.Save();
        }

        /// <summary>
        /// 通过相对路径，来获取其绝对路径
        /// </summary>
        /// <param name="路径">相对于服务器根目录的路径</param>
        /// <returns>物理路径</returns>
        public static string 获取服务器内容绝对路径(string 路径)
        {
            return WebSite变量.Current.Server.MapPath(路径);
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>IP地址列表</returns>
        public static IPAddress[] 获取本机IP地址()
        {
            return Net处理函数.获取IP地址(获取主机名());
        }

        /// <summary>
        /// 获取客户端浏览器信息，可从中检索浏览器名称、版本、功能及屏幕色彩、分辨率等信息
        /// </summary>
        public static HttpBrowserCapabilities 获取客户端浏览器信息()
        {
            return WebSite变量.Current.Request.Browser;
        }

        /// <summary>
        /// 获得当前页面客户端的IP，如为"0.0.0.0"表示未获取到，为"255.255.255.255"表示获取过程中遇到异常
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string 获取客户端IP地址()
        {
            try
            {
                string ip =HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ip.IsNullOrEmpty() || ip.ToLower().IndexOf("unknown") > -1)
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    if (ip.IndexOf(',') > -1)
                    {
                        ip = ip.Substring(0, ip.IndexOf(','));
                    }
                    if (ip.IndexOf(';') > -1)
                    {
                        ip = ip.Substring(0, ip.IndexOf(';'));
                    }
                }

                Regex regex = new Regex("[^0-9.]");
                if (ip.IsNullOrEmpty() || regex.IsMatch(ip))
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                    if (ip.IsNullOrEmpty() || regex.IsMatch(ip))
                    {
                        ip = "0.0.0.0";
                    }
                }
                return ip;
            }
            catch (Exception e)
            {
                return "255.255.255.255";
            }
        }

        /// <summary>
        /// 判断传入字符串是否为IP地址
        /// </summary>
        /// <param name="IP">传入字串</param>
        /// <returns></returns>
        public static bool 验证IP地址(this string IP)
        {
            return Regex.IsMatch(IP, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="电子邮件地址">要判断的电子邮件地址</param>
        /// <returns>判断结果</returns>
        public static bool 验证电子邮件地址(this string 电子邮件地址)
        {
            return Regex.IsMatch(电子邮件地址, RegularExpressions常量.电子邮件地址格式正则匹配代码);
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="字符串">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool 验证字符串是否包含危险的SQL操作字符(this string 字符串)
        {
            return Regex.IsMatch(字符串, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串，通常用于用户信息的检测
        /// </summary>
        /// <param name="字符串">要判断的字符串</param>
        /// <returns>判断结果</returns>
        public static bool 验证字符串是否包含危险的链接字符(this string 字符串)
        {
            return Regex.IsMatch(字符串, @"/^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|$guestexp/is");
        }

        /// <summary>
        /// 写入Cookies值
        /// </summary>
        /// <param name="名称">名称</param>
        /// <param name="值">值</param>
        public static void 写入Cookies值(string 名称, string 值)
        {
            HttpCookie cookie = WebSite变量.Current.Request.Cookies[名称];
            if (cookie == null)
            {
                cookie = new HttpCookie(名称);
            }
            cookie.Value = 值;
            WebSite变量.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 写入Cookies值
        /// </summary>
        /// <param name="名称">名称</param>
        /// <param name="值">值</param>
        /// <param name="有效期">Cookies的有效时间，单位为分钟</param>
        public static void 写入Cookies值(string 名称, string 值, int 有效期)
        {
            HttpCookie cookie = WebSite变量.Current.Request.Cookies[名称];
            if (cookie == null)
            {
                cookie = new HttpCookie(名称);
            }
            cookie.Value = 值;
            cookie.Expires = DateTime.Now.AddMinutes(有效期);
            WebSite变量.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 读取Cookies值
        /// </summary>
        /// <param name="名称">值名称</param>
        /// <returns>Cookies值</returns>
        public static string 读取Cookies值(string 名称)
        {
            if (WebSite变量.Current.Request.Cookies != null && WebSite变量.Current.Request.Cookies[名称] != null)
            {
                return WebSite变量.Current.Request.Cookies[名称].Value.ToString();
            }

            return "";
        }

        /// <summary>
        /// 将动态页面转换为静态HTML页
        /// </summary>
        /// <param name="转换文件路径">转换文件路径，使用相对路径</param>
        /// <param name="输出文件路径">输出文件路径，使用相对路径</param>
        public static void 转换为静态HTML页面(string 转换文件路径, string 输出文件路径)
        {
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(转换文件路径, writer);
            FileStream fs;
            if (File.Exists(page.Server.MapPath("") + "\\" + 输出文件路径))
            {
                File.Delete(page.Server.MapPath("") + "\\" + 输出文件路径);
                fs = File.Create(page.Server.MapPath("") + "\\" + 输出文件路径);
            }
            else
            {
                fs = File.Create(page.Server.MapPath("") + "\\" + 输出文件路径);
            }
            byte[] bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }

        /// <summary>
        /// 在页面响应中以指定的内容类型输出指定文件文件
        /// </summary>
        /// <param name="文件路径">文件路径</param>
        /// <param name="文件名">输出的文件名</param>
        /// <param name="文件类型">将文件输出时设置的ContentType</param>
        public static void 输出文件(string 文件路径, string 文件名, string 文件类型)
        {
            Stream iStream = null;

            // 缓冲区为10k
            byte[] buffer = new Byte[10000];

            // 文件长度
            int length;

            // 需要读的数据长度
            long dataToRead;

            try
            {
                // 打开文件
                iStream = new FileStream(文件路径, FileMode.Open, FileAccess.Read, FileShare.Read);


                // 需要读的数据长度
                dataToRead = iStream.Length;

                WebSite变量.Current.Response.ContentType = 文件类型;
                WebSite变量.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + 文件名.Trim().进行URL编码().Replace("+", " "));

                while (dataToRead > 0)
                {
                    // 检查客户端是否还处于连接状态
                    if (WebSite变量.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        WebSite变量.Current.Response.OutputStream.Write(buffer, 0, length);
                        WebSite变量.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再连接则跳出死循环
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                WebSite变量.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 关闭文件
                    iStream.Close();
                }
            }
            WebSite变量.Current.Response.End();
        }

        /// <summary>
        /// 判断文件是否为浏览器可以直接显示的图片文件类型
        /// </summary>
        /// <param name="路径">文件路径</param>
        /// <returns>是否可以直接显示</returns>
        public static bool 是否为浏览器支持的图像(string 路径)
        {
            路径 = 路径.Trim();
            if (路径.EndsWith(".") || 路径.IndexOf(".") == -1)
            {
                return false;
            }
            string extname = 路径.Substring(路径.LastIndexOf(".") + 1).ToLower();
            return (extname == "jpg" || extname == "jpeg" || extname == "png" || extname == "bmp" || extname == "gif");
        }


        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool 是否为Post请求()
        {
            return WebSite变量.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool 是否为Get请求()
        {
            return WebSite变量.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 得到当前完整主机名
        /// </summary>
        /// <returns>完整的主机名</returns>
        public static string 获取完整主机名()
        {
            HttpRequest request = WebSite变量.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机名
        /// </summary>
        /// <returns>主机名</returns>
        public static string 获取主机名()
        {
            return WebSite变量.Current.Request.Url.Host;
        }

        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="变量名">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string 获取服务器变量(string 变量名)
        {
            if (WebSite变量.Current.Request.ServerVariables[变量名] == null)
            {
                return "";
            }
            return WebSite变量.Current.Request.ServerVariables[变量名].ToString();
        }

        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns>原始 URL</returns>
        public static string 获取原始URL地址()
        {
            return WebSite变量.Current.Request.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool 是否为浏览器访问()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "firefox", "chrome" };
            string curBrowser = WebSite变量.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string 获取页面名称()
        {
            string[] urlArr = WebSite变量.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 保存用户上传的文件
        /// </summary>
        /// <param name="路径">保存路径</param>
        public static void 保存上传文件(string 路径)
        {
            if (WebSite变量.Current.Request.Files.Count > 0)
            {
                WebSite变量.Current.Request.Files[0].SaveAs(路径);
            }
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string 获取前一个页面地址()
        {
            string retVal = null;

            try
            {
                retVal = WebSite变量.Current.Request.UrlReferrer.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool 是否为搜索引擎链接()
        {
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom" };
            string tmpReferrer = 获取前一个页面地址().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string 获取完整URL地址()
        {
            return WebSite变量.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="参数名">Url参数名</param>
        /// <returns>Url参数的值</returns>
        public static string 获取URL参数值(string 参数名)
        {
            if (WebSite变量.Current.Request.QueryString[参数名] == null)
            {
                return "";
            }
            return WebSite变量.Current.Request.QueryString[参数名];
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="参数名">表单参数名</param>
        /// <returns>表单参数的值</returns>
        public static string 获取表单参数值(string 参数名)
        {
            if (WebSite变量.Current.Request.Form[参数名] == null)
            {
                return "";
            }
            return WebSite变量.Current.Request.Form[参数名];
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="参数名">参数名</param>
        /// <returns>Url或表单参数的值</returns>
        public static string 获取表单或URL参数值(string 参数名)
        {
            if ("".Equals(获取URL参数值(参数名)))
            {
                return 获取表单参数值(参数名);
            }
            else
            {
                return 获取URL参数值(参数名);
            }
        }

        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int 获取参数总数()
        {
            return WebSite变量.Current.Request.Form.Count + WebSite变量.Current.Request.QueryString.Count;
        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool 是否为跨站提交()
        {
            // 如果不是提交则为true
            if (!是否为Post请求())
            {
                return true;
            }
            return 是否为跨站提交(获取前一个页面地址(), 获取主机名());
        }

        /// <summary>
        /// 返回当前页面是否是跨站提交
        /// </summary>
        /// <param name="前一页面地址">前一页面地址</param>
        /// <param name="本机主机名">本机主机名</param>
        /// <returns>当前页面是否是跨站提交</returns>
        public static bool 是否为跨站提交(string 前一页面地址, string 本机主机名)
        {
            if (前一页面地址.Length < 7)
            {
                return true;
            }
            // 移除http://
            string s = 前一页面地址.Remove(0, 7);
            if (s.IndexOf(":") > -1)
                s = s.Substring(0, s.IndexOf(":"));
            else
                s = s.Substring(0, s.IndexOf('/'));
            return s != 本机主机名;
        }

        /// <summary>
        /// 读取Web.Config文件中appSettings节点中设置的变量，其定义格式为：&lt;appSettings&gt;&lt;add 键=&quot;变量名&quot; 值=&quot;变量值&quot;/&gt;&lt;/appSettings&gt;
        /// </summary>
        /// <param name="键">变量名</param>
        /// <returns>变量值</returns>
        public static string 读取网站配置文件程序配置节点值(string 键)
        {
            return WebConfigurationManager.AppSettings[键];
        }


        #region Page
        /// <summary>
        /// 添加网页关键字，供搜索引擎检索
        /// </summary>
        public static void 添加关键字(this Page o, string 内容)
        {
            HtmlMeta MetaKeyWord = new HtmlMeta();
            MetaKeyWord.Name = "keywords";
            MetaKeyWord.Content = 内容;
            o.Header.Controls.Add(MetaKeyWord);
        }

        /// <summary>
        /// 添加网页摘要信息
        /// </summary>
        public static void 添加摘要(this Page o, string 内容)
        {
            HtmlMeta MetaInfo = new HtmlMeta();
            MetaInfo.Name = "description";
            MetaInfo.Content = 内容;
            o.Header.Controls.Add(MetaInfo);
        }

        /// <summary>
        /// 添加收藏夹及快捷方式显示的图标，图标内除16×16尺寸的格式外，最好还具备大图标，用于桌面快捷方式显示。
        /// </summary>
        /// <param name="图标路径">图标文件路径，最好使用绝对URL路径，否则可能不会显示</param>
        public static void 添加收藏及快捷方式图标(this Page o, string 图标路径)
        {
            HtmlLink LinkICO = new HtmlLink();
            LinkICO.Href = 图标路径;
            LinkICO.Attributes.Add("rel", "Shortcut Icon");
            o.Header.Controls.Add(LinkICO);
            LinkICO = new HtmlLink();
            LinkICO.Href = 图标路径;
            LinkICO.Attributes.Add("rel", "Bookmark");
            o.Header.Controls.Add(LinkICO);
        }

        /// <summary>
        /// 添加地址栏图标，图标大小为16×16
        /// </summary>
        /// <param name="图标路径">图标文件路径，最好使用绝对URL路径，否则可能不会显示</param>
        /// <param name="是否为GIF格式">指示图标是否为gif动画格式，否则为ico格式。IE7不支持gif</param>
        public static void 添加地址栏图标(this Page o, string 图标路径, bool 是否为GIF格式)
        {
            HtmlLink LinkICO = new HtmlLink();
            LinkICO.Href = 图标路径;
            LinkICO.Attributes.Add("rel", "icon");
            LinkICO.Attributes.Add("type", 是否为GIF格式 ? "image/gif" : "image/x-icon");
            o.Header.Controls.Add(LinkICO);
        }

        /// <summary>
        /// 添加页面CSS样式表，可多次添加
        /// </summary>
        public static void 添加CSS样式表(this Page o, string 样式表路径)
        {
            添加CSS样式表(o, 样式表路径, "all");
        }

        /// <summary>
        /// 添加页面CSS样式表，可多次添加
        /// </summary>
        /// <param name="样式表路径">样式表对应的媒体类型，通常为"all"</param>
        public static void 添加CSS样式表(this Page o, string 样式表路径, string 媒体类型)
        {
            HtmlLink LinkCSS = new HtmlLink();
            LinkCSS.Href = 样式表路径;
            LinkCSS.Attributes.Add("rel", "stylesheet");
            LinkCSS.Attributes.Add("type", "text/css");
            LinkCSS.Attributes.Add("media", 媒体类型);
            o.Header.Controls.Add(LinkCSS);
        }

        /// <summary>
        /// 添加页面相关RSS信源，可多次添加
        /// </summary>
        public static void 添加RSS信源声明(this Page o, string 信源路径, string 名称)
        {
            HtmlLink LinkRSS = new HtmlLink();
            LinkRSS.Href = 信源路径;
            LinkRSS.Attributes.Add("rel", "alternate");
            LinkRSS.Attributes.Add("type", "application/rss+xml");
            LinkRSS.Attributes.Add("title", 名称);
            o.Header.Controls.Add(LinkRSS);
        }

        /// <summary>
        /// 添加页面自动跳转
        /// </summary>
        /// <param name="停留时间">单位为秒</param>
        /// <param name="跳转地址">跳转目标URL地址，为空则刷新本页</param>
        public static void 添加自动跳转(this Page o, int 停留时间, string 跳转地址)
        {
            HtmlMeta MetaInfo = new HtmlMeta();
            MetaInfo.HttpEquiv = "refresh";
            MetaInfo.Content = String.Format("{0}{1}", 停留时间, 跳转地址.验证有效性() ? ";URL=" + 跳转地址 : "");
            o.Header.Controls.Add(MetaInfo);
        }


        /// <summary>
        /// 页面转向到指定URL地址
        /// </summary>
        public static void 转向(this Page o, string 地址)
        {
            o.Response.Redirect(地址, true);
        }

        /// <summary>
        /// 将页面设为禁用缓存
        /// </summary>
        public static void 设置禁用页面缓存(this Page o)
        {
            o.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        /// <summary>
        /// 通过客户端脚本向客户端显示对话框
        /// </summary>
        /// <param name="内容">对话框内容</param>
        public static void 显示对话框(this Page o, string 内容)
        {
            o.Controls.Add(new Literal { Text = "<script>alert('" + 内容 + "')</script>" });
        }

        #endregion

        #region Button

        /// <summary>
        /// 通过客户端脚本向客户端显示对话框以确认操作
        /// </summary>
        /// <param name="内容">对话框内容</param>
        public static void 添加确认对话框(this Button o, string 内容)
        {
            o.Attributes.Add("onclick", "return confirm('{0}');".FormatWith(内容));
        }

        #endregion

        #region Page.Request.QueryString
        public static int 获取整数(this NameValueCollection o, string 参数名)
        {
            var x = 0;
            Int32.TryParse(o[参数名], out x);
            return x;
        }
        #endregion

        #region Membership
        /// <summary>
        /// 检验当前用户是否属于指定角色
        /// </summary>
        /// <param name="角色名">角色名</param>
        /// <returns>是否属于指定角色</returns>
        public static bool 角色验证(this MembershipUser o, string 角色名)
        {
            return Roles.IsUserInRole(o.UserName, 角色名);
        }

        /// <summary>
        /// 获取当前用户所属的角色列表
        /// </summary>
        /// <returns>角色列表</returns>
        public static string[] 获取所属角色列表(this MembershipUser o)
        {
            return Roles.GetRolesForUser(o.UserName);
        }

        /// <summary>
        /// 为用户添加角色
        /// </summary>
        /// <param name="角色名">角色名</param>
        public static void 添加角色(this MembershipUser o, string 角色名)
        {
            Roles.AddUserToRole(o.UserName, 角色名);
        }

        /// <summary>
        /// 为用户移除角色
        /// </summary>
        /// <param name="角色名">角色名</param>
        public static void 移除角色(this MembershipUser o, string 角色名)
        {
            Roles.RemoveUserFromRole(o.UserName, 角色名);
        }
        #endregion

        #region System.Web.UI.Control
        public static T 查找子控件<T>(this System.Web.UI.Control o, string 子控件ID)
            where T : System.Web.UI.Control
        {
            return o.FindControl(子控件ID) as T;
        }

        #endregion

        #region BaseDataBoundControl

        /// <summary>
        /// 为控件手动指派数据源
        /// </summary>
        /// <param name="数据源">数据源</param>
        public static void 设置数据源<T>(this BaseDataBoundControl o, IEnumerable<T> 数据源)
        {
            o.DataSource = 数据源;
            o.DataBind();
        }

        #endregion

        /// <summary>
        /// 为控件手动指派数据源
        /// </summary>
        /// <param name="数据源">数据源</param>
        public static void 设置数据源<T>(this ListControl o, IEnumerable<T> 数据源, string 显示属性名, string 值属性名)
        {
            o.DataTextField = 显示属性名;
            o.DataValueField = 值属性名;
            o.DataSource = 数据源;
            o.DataBind();
        }

        #region ListView

        ///// <summary>
        ///// 获取触发事件对应的对象ID，如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效。
        ///// </summary>
        ///// <param name="所在ListView控件">触发此事件的ListView控件</param>
        //public static Guid 获取GUID类型的操作对象ID(this ListViewDeleteEventArgs e, ListView 所在ListView控件)
        //{
        //    return new Guid(所在ListView控件.DataKeys[e.ItemIndex].Value.ToString());
        //}

        ///// <summary>
        ///// 获取触发事件对应的对象ID，如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效。
        ///// </summary>
        ///// <param name="所在ListView控件">触发此事件的ListView控件</param>
        //public static int 获取INT类型的操作对象ID(this ListViewDeleteEventArgs e, ListView 所在ListView控件)
        //{
        //    return 所在ListView控件.DataKeys[e.ItemIndex].Value.ToString().转换为Int32();
        //}

        ///// <summary>
        ///// 获取触发事件对应的对象ID，如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效。
        ///// </summary>
        ///// <param name="所在ListView控件">触发此事件的ListView控件</param>
        //public static Guid 获取GUID类型的操作对象ID(this ListViewUpdateEventArgs e, ListView 所在ListView控件)
        //{
        //    return new Guid(所在ListView控件.DataKeys[e.ItemIndex].Value.ToString());
        //}

        ///// <summary>
        ///// 获取触发事件对应的对象ID，如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效。
        ///// </summary>
        ///// <param name="所在ListView控件">触发此事件的ListView控件</param>
        //public static int 获取INT类型的操作对象ID(this ListViewUpdateEventArgs e, ListView 所在ListView控件)
        //{
        //    return 所在ListView控件.DataKeys[e.ItemIndex].Value.ToString().转换为Int32();
        //}

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static Guid 获取Guid类型的操作对象主键(this ListView o, ListViewUpdateEventArgs 事件参数)
        {
            return new Guid(o.DataKeys[事件参数.ItemIndex].Value.ToString());
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static Guid 获取Guid类型的操作对象主键(this ListView o, ListViewDeleteEventArgs 事件参数)
        {
            return new Guid(o.DataKeys[事件参数.ItemIndex].Value.ToString());
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static Guid 获取Guid类型的操作对象主键(this ListView o, ListViewSelectEventArgs 事件参数)
        {
            return new Guid(o.DataKeys[事件参数.NewSelectedIndex].Value.ToString());
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static Guid 获取Guid类型的操作对象主键(this ListView o, ListViewEditEventArgs 事件参数)
        {
            return new Guid(o.DataKeys[事件参数.NewEditIndex].Value.ToString());
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static Guid 获取Guid类型的操作对象主键(this ListView o, ListViewCommandEventArgs 事件参数)
        {
            return new Guid(o.DataKeys[事件参数.Item.As<ListViewDataItem>().DisplayIndex].Value.ToString());
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static int 获取Int32类型的操作对象主键(this ListView o, ListViewUpdateEventArgs 事件参数)
        {
            return o.DataKeys[事件参数.ItemIndex].Value.ToString().转换为Int32();
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static int 获取Int32类型的操作对象主键(this ListView o, ListViewDeleteEventArgs 事件参数)
        {
            return o.DataKeys[事件参数.ItemIndex].Value.ToString().转换为Int32();
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static int 获取Int32类型的操作对象主键(this ListView o, ListViewSelectEventArgs 事件参数)
        {
            return o.DataKeys[事件参数.NewSelectedIndex].Value.ToString().转换为Int32();
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static int 获取Int32类型的操作对象主键(this ListView o, ListViewEditEventArgs 事件参数)
        {
            return o.DataKeys[事件参数.NewEditIndex].Value.ToString().转换为Int32();
        }

        /// <summary>
        /// 获取触发事件对应的对象主键(ID)。（如果不是通过数据源控件绑定而获得的数据，需要手动指定ListView的DataKeyNames属性后才有效）
        /// </summary>
        public static int 获取Int32类型的操作对象主键(this ListView o, ListViewCommandEventArgs 事件参数)
        {
            return o.DataKeys[事件参数.Item.As<ListViewDataItem>().DisplayIndex].Value.ToString().转换为Int32();
        }

        /// <summary>
        /// 获取触发事件对应的操作项中的子控件。
        /// </summary>
        public static T 获取操作项子控件<T>(this ListView o, ListViewUpdateEventArgs 事件参数, string 控件ID)
            where T : System.Web.UI.Control
        {
            return o.Items[事件参数.ItemIndex].FindControl(控件ID) as T;
        }

        /// <summary>
        /// 获取触发事件对应的操作项中的子控件。
        /// </summary>
        public static Control 获取操作项子控件(this ListView o, ListViewUpdateEventArgs 事件参数, string 控件ID)
        {
            return o.Items[事件参数.ItemIndex].FindControl(控件ID) as Control;
        }

        /// <summary>
        /// 根据事件参数变更相应项的编辑状态。（需要在其后自行书写重新绑定数据源的相关代码以刷新显示）
        /// </summary>
        public static void 变更编辑状态(this ListView o, ListViewEditEventArgs 事件参数)
        {
            o.EditIndex = 事件参数.NewEditIndex;
        }

        /// <summary>
        /// 根据事件参数变更相应项的选取状态。（需要在其后自行书写重新绑定数据源的相关代码以刷新显示）
        /// </summary>
        public static void 变更选取状态(this ListView o, ListViewSelectEventArgs 事件参数)
        {
            o.SelectedIndex = 事件参数.NewSelectedIndex;
        }

        ///// <summary>
        ///// 根据事件参数变更相应项的编辑状态。（需要在其后自行书写重新绑定数据源的相关代码以刷新显示）
        ///// </summary>
        //public static void 变更编辑状态(this ListView o, ListViewCancelEventArgs 事件参数)
        //{
        //    o.EditIndex = -1;
        //}

        /// <summary>
        /// 取消编辑状态。（需要在其后自行书写重新绑定数据源的相关代码以刷新显示）
        /// </summary>
        public static void 取消编辑状态(this ListView o)
        {
            o.EditIndex = -1;
        }

        /// <summary>
        /// 为ListView手动指派数据源
        /// </summary>
        /// <param name="数据源">数据源</param>
        /// <param name="主键属性名">数据项的主键属性名，用作删除、更新等操作，通常指定为ID。设为null则忽略此设置。</param>
        public static void 设置数据源<T>(this ListView o, IEnumerable<T> 数据源, string 主键属性名)
        {
            o.DataKeyNames = null;
            if (!主键属性名.IsNullOrEmpty())
            {
                o.DataKeyNames = new string[] { 主键属性名 };
            }
            o.DataSource = 数据源;
            o.DataBind();
        }

        #endregion

        #region HtmlTextWriter

        /// <summary>
        /// 在网页中写入&nbsp;表示空格
        /// </summary>
        public static void 写入空白占位符(this HtmlTextWriter o)
        {
            o.Write("&nbsp;");
        }

        /// <summary>
        /// 将多个控件一并写入页面
        /// </summary>
        /// <param name="间隔操作">每个控件间隔处执行的操作，如添加空格或其他间隔标记</param>
        /// <param name="控件集合">多个要一并写入的控件</param>
        public static void 写入控件(this HtmlTextWriter o, Action<HtmlTextWriter> 间隔操作, params Control[] 控件集合)
        {
            for (int i = 0; i < 控件集合.Length; i++)
            {
                if (i > 0)
                {
                    间隔操作(o);
                }
                控件集合[i].RenderControl(o);
            }
        }

        /// <summary>
        /// 将多个控件一并写入页面
        /// </summary>
        /// <param name="控件集合">多个要一并写入的控件</param>
        public static void 写入控件(this HtmlTextWriter o, params Control[] 控件集合)
        {
            写入控件(o, q => { }, 控件集合);
        }

        /// <summary>
        /// 将多个控件一并写入页面
        /// </summary>
        /// <param name="间隔字符串">每个控件间隔处写入的字符串</param>
        /// <param name="控件集合">多个要一并写入的控件</param>
        public static void 写入控件(this HtmlTextWriter o, string 间隔字符串, params Control[] 控件集合)
        {
            写入控件(o, q => q.Write(间隔字符串), 控件集合);
        }

        /// <summary>
        /// 将多个控件一并写入页面
        /// </summary>
        /// <param name="间隔标记">每个控件间隔处写入的HTML标记</param>
        /// <param name="控件集合">多个要一并写入的控件</param>
        public static void 写入控件(this HtmlTextWriter o, HtmlTextWriterTag 间隔标记, params Control[] 控件集合)
        {
            写入控件(o, q => { q.RenderBeginTag(间隔标记); q.RenderEndTag(); }, 控件集合);
        }

        #endregion

        #region TreeView

        /// <summary>
        /// 与ValuePath属性不同，此方法获取树状的Text路径
        /// </summary>
        /// <param name="间隔字符串">每个节点间的间隔字符串</param>
        /// <param name="终止节点">指示计算的中止节点，直到遇到此节点或顶层无节点的情况下才中止计算，如果指定为null，则始终计算到无父节点为止</param>
        public static string 获取文字路径(this TreeNode o, TreeNode 终止节点, string 间隔字符串)
        {
            StringBuilder s = new StringBuilder();
            foreach (var f in o.RecursionSelect(q => q.Parent, q => q != null && q != 终止节点, false).Reverse())
            {
                s.AppendAndSeparated(f.Text, 间隔字符串);
            }
            s.AppendAndSeparated(o.Text, 间隔字符串);
            return s.ToString();
        }

        /// <summary>
        /// 与ValuePath属性不同，此方法获取树状的Text路径
        /// </summary>
        /// <param name="间隔字符串">每个节点间的间隔字符串</param>
        public static string 获取文字路径(this TreeNode o, string 间隔字符串)
        {
            return 获取文字路径(o, null, 间隔字符串);
        }

        /// <summary>
        /// 与ValuePath属性不同，此方法获取树状的Text路径，默认采用“/”做分隔
        /// </summary>
        public static string 获取文字路径(this TreeNode o)
        {
            return o.获取文字路径("/");
        }

        #endregion

    }
}
