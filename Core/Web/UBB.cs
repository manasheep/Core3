using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Core.Text;

namespace Core.Web
{
    /// <summary>
    /// UBB代码转换类
    /// </summary>
    [Serializable]
    public class UBB
    {

        //可以考虑在以后版本中加入表情代码及上传文件的转换功能

        /// <summary>
        /// 构造默认配置的UBB对象，在默认配置中将禁用Html标记、Code标记、Quote标记及除图像外的多媒体标记转换功能
        /// </summary>
        public UBB()
        {
            默认配置();
        }

        /// <summary>
        /// 构造UBB对象，在默认配置中将禁用Html标记、Code标记、Quote标记及除图像外的多媒体标记转换功能
        /// </summary>
        /// <param name="待处理字符串">用于转换的原始字符串</param>
        public UBB(string 待处理字符串)
        {
            this.待处理字符串 = 待处理字符串;
            默认配置();
        }

        private void 默认配置()
        {
            this.对Url路径进行编码 = true;
            this.限制多媒体内容尺寸 = false;
            this.在新窗口中打开超链接 = true;
            this.允许Image标记 = this.允许Email标记 = this.允许Url标记 = true;
            this.允许Html标记 = this.允许Code标记 = this.允许Quote标记 = this.允许RealPlayer标记 = this.允许QuickTime标记 = this.允许MediaPlayer标记 = this.允许Flash标记 = this.允许Director标记 = false;
            this.限制多媒体内容最大宽度 = this.限制多媒体内容最大高度 = 9999;
        }

        #region 设置参数

        /// <summary>
        /// 指示是否对图像、视频等内容的尺寸进行限制
        /// </summary>
        public bool 限制多媒体内容尺寸 { get; set; }
        /// <summary>
        /// 如果“限制多媒体内容尺寸”选项被启用，限制图像、视频等内容的最大宽度
        /// </summary>
        public int 限制多媒体内容最大宽度 { get; set; }
        /// <summary>
        /// 如果“限制多媒体内容尺寸”选项被启用，限制图像、视频等内容的最大高度
        /// </summary>
        public int 限制多媒体内容最大高度 { get; set; }
        /// <summary>
        /// 如果“限制多媒体内容尺寸”选项被启用，限制图像、视频等内容的最小宽度
        /// </summary>
        public int 限制多媒体内容最小宽度 { get; set; }
        /// <summary>
        /// 如果“限制多媒体内容尺寸”选项被启用，限制图像、视频等内容的最小高度
        /// </summary>
        public int 限制多媒体内容最小高度 { get; set; }
        /// <summary>
        /// 用于转换的原始字符串
        /// </summary>
        public string 待处理字符串 { get; set; }
        /// <summary>
        /// 指示超链接(a)是否都在新窗口打开
        /// </summary>
        public bool 在新窗口中打开超链接 { get; set; }
        /// <summary>
        /// 允许转换用于直接呈现Html内容的[html]...[/html]标记
        /// </summary>
        public bool 允许Html标记 { get; set; }
        /// <summary>
        /// 允许转换用于显示Flash内容的[flash=x,y]...[/flash]或[swf=x,y]...[/swf]标记
        /// </summary>
        public bool 允许Flash标记 { get; set; }
        /// <summary>
        /// 允许转换用于显示Director内容的[dir=x,y]...[/dir]标记
        /// </summary>
        public bool 允许Director标记 { get; set; }
        /// <summary>
        /// 允许转换用于显示RealPlayer内容的[rm=x,y]...[/rm]标记
        /// </summary>
        public bool 允许RealPlayer标记 { get; set; }
        /// <summary>
        /// 允许转换用于显示MediaPlayer内容的[mp=x,y]...[/mp]标记
        /// </summary>
        public bool 允许MediaPlayer标记 { get; set; }
        /// <summary>
        ///  允许转换用于显示QuickTime内容的[qt=x,y]...[/qt]标记
        /// </summary>
        public bool 允许QuickTime标记 { get; set; }
        /// <summary>
        /// 允许转换用于显示图像内容的[img]...[/img]或[image]...[/image]标记
        /// </summary>
        public bool 允许Image标记 { get; set; }
        /// <summary>
        ///  允许转换用于显示超链接内容的[url]...[/url]或[url=...]...[/url]标记
        /// </summary>
        public bool 允许Url标记 { get; set; }
        /// <summary>
        ///  允许转换用于显示电子邮件链接内容的[email]...[/email]或[email=...]...[/email]标记
        /// </summary>
        public bool 允许Email标记 { get; set; }
        /// <summary>
        ///  允许转换用于显示程序代码内容的[code]...[/code]标记，需要配合设置对应的CSS类
        /// </summary>
        public bool 允许Code标记 { get; set; }
        /// <summary>
        ///  允许转换用于显示引用内容的[quote]...[/quote]标记，需要配合设置对应的CSS类
        /// </summary>
        public bool 允许Quote标记 { get; set; }
        /// <summary>
        /// 允许对Url路径进行编码，这将把Url路径中的双字节字符转换为浏览器可识别的编码
        /// </summary>
        public bool 对Url路径进行编码 { get; set; }
        /// <summary>
        /// 在图像的Html标记属性中，添加的自定义Html代码；可以使用“${url}”代表其url地址引用
        /// </summary>
        public string 附加图像属性代码 { get; set; }
        /// <summary>
        /// 在超链接的Html标记属性中，添加的自定义Html代码；可以使用“${url}”代表其url地址引用
        /// </summary>
        public string 附加超链接属性代码 { get; set; }
        /// <summary>
        /// 在电子邮件链接的Html标记属性中，添加的自定义Html代码；可以使用“${url}”代表其url地址引用
        /// </summary>
        public string 附加电子邮件链接属性代码 { get; set; }
        /// <summary>
        /// 在引用内容块的Html标记属性中，添加的自定义Html代码
        /// </summary>
        public string 附加引用内容块属性代码 { get; set; }
        /// <summary>
        /// 在代码内容块的Html标记属性中，添加的自定义Html代码
        /// </summary>
        public string 附加代码内容块属性代码 { get; set; }

        #endregion

        /// <summary>
        /// 依据设置，将待处理字符串中的UBB内容转换为Html代码
        /// </summary>
        /// <returns>转换后的Html代码字符串</returns>
        public string 转换为Html代码()
        {
            if (!待处理字符串.验证有效性()) return 待处理字符串;

            string 字符串 = 待处理字符串;

            #region 预处理特殊标记
            MatchCollection HTML = null;
            MatchCollection CODE = null;
            MatchCollection QUOTE = null;

            if (允许Code标记)
            {
                CODE = Regex.Matches(字符串, @"\[code\]([\s\S]*?)\[/code\]", RegexOptions.IgnoreCase);
                字符串 = Regex.Replace(字符串, @"\[code\]([\s\S]*?)\[/code\]", "[||CODEBLOCK||]", RegexOptions.IgnoreCase);
            }

            if (允许Quote标记)
            {
                QUOTE = Regex.Matches(字符串, @"\[quote\]([\s\S]*?)\[/quote\]", RegexOptions.IgnoreCase);
                字符串 = Regex.Replace(字符串, @"\[quote\]([\s\S]*?)\[/quote\]", "[||QUOTEBLOCK||]", RegexOptions.IgnoreCase);
            }

            if (允许Html标记)
            {
                HTML = Regex.Matches(字符串, @"\[html\]([\s\S]*?)\[/html\]", RegexOptions.IgnoreCase);
                字符串 = Regex.Replace(字符串, @"\[html\]([\s\S]*?)\[/html\]", "[||HTMLCODE||]", RegexOptions.IgnoreCase);
            }

            #endregion

            字符串 = 字符串.进行HTML转义();

            #region 处理常规标记
            字符串 = Regex.Replace(字符串, @"\[b\]([\s\S]*?)\[/b\]", "<strong>$1</strong>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[u\]([\s\S]*?)\[/u\]", @"<span style=""text-decoration: underline"">$1</span>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[i\]([\s\S]*?)\[/i\]", "<em>$1</em>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[sup\]([\s\S]*?)\[/sup\]", "<sup>$1</sup>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[sub\]([\s\S]*?)\[/sub\]", "<sub>$1</sub>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[center\]([\s\S]*?)\[/center\]", @"<p style=""text-align: center"">$1</p>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[left\]([\s\S]*?)\[/left\]", @"<p style=""text-align: left"">$1</p>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[right\]([\s\S]*?)\[/right\]", @"<p style=""text-align: right"">$1</p>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[size=([1-7])\]([\s\S]*?)\[/size\]", @"<span style=""font-size: $1ex"">$2</span>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[font=(.*?)\]([\s\S]*?)\[/font\]", @"<span style=""font-family: $1"">$2</span>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[color=(.*?)\]([\s\S]*?)\[/color\]", @"<span style=""color: $1"">$2</span>", RegexOptions.IgnoreCase);
            foreach (Match m in Regex.Matches(字符串, @"\[(list|ul|ol)\]([\s\S]*?)\[/\1\]", RegexOptions.IgnoreCase))
            {
                StringBuilder S = new StringBuilder();
                foreach (Match v in Regex.Matches(m.Groups[2].Value, "(.*?)(<br />|$)"))
                {
                    if (v.Groups[1].Value.验证有效性()) S.Append("<li>" + v.Groups[1].Value + "</li>");
                }
                字符串 = Regex.Replace(字符串, String.Format(@"\[(list|ul|ol)\]{0}\[/\1\]", m.Groups[2].Value.进行正则表达式转义()), String.Format(@"[$1]{0}[/$1]", S), RegexOptions.IgnoreCase);
            }
            字符串 = Regex.Replace(字符串, @"\[(list|ul)\]([\s\S]*?)\[/\1\]", @"<ul class=""UBBUList"">$2</ul>", RegexOptions.IgnoreCase);
            字符串 = Regex.Replace(字符串, @"\[(ol)\]([\s\S]*?)\[/\1\]", @"<ol class=""UBBOList"">$2</ol>", RegexOptions.IgnoreCase);
            #endregion

            #region 处理特殊标记
            if (允许Email标记)
            {
                字符串 = Regex.Replace(字符串, @"\[email\](?<url>.*?)\[/email\]", String.Format(@"<a href=""mailto:[||URL||]${{url}}[||/URL||]"" class=""UBBLink""{0} {1}>${{url}}</a>", 在新窗口中打开超链接 ? " target=\"_blank\"" : "", 附加电子邮件链接属性代码), RegexOptions.IgnoreCase);
                字符串 = Regex.Replace(字符串, @"\[email=(?<url>.*?)\](?<content>[\s\S]*?)\[/email\]", String.Format(@"<a href=""mailto:[||URL||]${{url}}[||/URL||]"" class=""UBBLink""{0} {1}>${{content}}</a>", 在新窗口中打开超链接 ? " target=\"_blank\"" : "", 附加电子邮件链接属性代码), RegexOptions.IgnoreCase);
            }
            if (允许Url标记)
            {
                字符串 = Regex.Replace(字符串, @"\[url\](?<url>.*?)\[/url\]", String.Format(@"<a href=""[||URL||]${{url}}[||/URL||]"" class=""UBBLink"" {0} {1}>${{url}}</a>", 在新窗口中打开超链接 ? " target=\"_blank\"" : "", 附加超链接属性代码), RegexOptions.IgnoreCase);
                字符串 = Regex.Replace(字符串, @"\[url=(?<url>.*?)\](?<content>[\s\S]*?)\[/url\]", String.Format(@"<a href=""[||URL||]${{url}}[||/URL||]"" class=""UBBLink""{0} {1}>${{content}}</a>", 在新窗口中打开超链接 ? " target=\"_blank\"" : "", 附加超链接属性代码), RegexOptions.IgnoreCase);
            }
            if (允许Image标记)
            {
                字符串 = Regex.Replace(字符串, @"\[(img|image)\](?<url>.*?)\[/\1\]", String.Format(@"<img src=""[||URL||]${{url}}[||/URL||]"" class=""UBBImage""{0} {1}/>", 限制多媒体内容尺寸 ? String.Format(@" onload=""javascript:if(this.width>{0})this.width={0};if(this.width<{1})this.width={1};if(this.height>{2})this.height={2};if(this.height<{3})this.height={3}""", 限制多媒体内容最大宽度, 限制多媒体内容最小宽度, 限制多媒体内容最大高度, 限制多媒体内容最小高度) : "", 附加图像属性代码), RegexOptions.IgnoreCase);
            }
            if (允许Flash标记)
            {
                foreach (Match m in Regex.Matches(字符串, @"\[(swf|flash)=(?<width>\d+),(?<height>\d+)\](?<url>.*?)\[/\1\]", RegexOptions.IgnoreCase))
                {
                    int w = m.Groups["width"].Value.转换为Int32();
                    int h = m.Groups["height"].Value.转换为Int32();
                    if (限制多媒体内容尺寸)
                    {
                        if (w > 限制多媒体内容最大宽度) w = 限制多媒体内容最大宽度;
                        if (w < 限制多媒体内容最小宽度) w = 限制多媒体内容最小宽度;
                        if (h > 限制多媒体内容最大高度) h = 限制多媒体内容最大高度;
                        if (h < 限制多媒体内容最小高度) h = 限制多媒体内容最小高度;
                    }
                    字符串 = 字符串.Replace(m.Value, String.Format(@"<object class=""UBBFlash"" classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"" width=""{1}"" height=""{2}"">
  <param name=""movie"" value=""{0}"" />
  <param name=""quality"" value=""high"" />
  <embed src=""{0}"" quality=""high"" pluginspage=""http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"" type=""application/x-shockwave-flash"" width=""{1}"" height=""{2}""></embed>
</object>", m.Groups["url"], w, h));
                }
            }
            if (允许Director标记)
            {
                foreach (Match m in Regex.Matches(字符串, @"\[dcr=(?<width>\d+),(?<height>\d+)\](?<url>.*?)\[/dcr\]", RegexOptions.IgnoreCase))
                {
                    int w = m.Groups["width"].Value.转换为Int32();
                    int h = m.Groups["height"].Value.转换为Int32();
                    if (限制多媒体内容尺寸)
                    {
                        if (w > 限制多媒体内容最大宽度) w = 限制多媒体内容最大宽度;
                        if (w < 限制多媒体内容最小宽度) w = 限制多媒体内容最小宽度;
                        if (h > 限制多媒体内容最大高度) h = 限制多媒体内容最大高度;
                        if (h < 限制多媒体内容最小高度) h = 限制多媒体内容最小高度;
                    }
                    字符串 = 字符串.Replace(m.Value, String.Format(@"<object class=""UBBDirector"" classid=""clsid:166B1BCA-3F9C-11CF-8075-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=7,0,2,0"" width=""{1}"" height=""{2}"">
<param name=""src"" value=""{0}"" />
<embed src=""{0}"" pluginspage=""http://www.macromedia.com/shockwave/download/"" width=""{1}"" height=""{2}""></embed>
</object>", m.Groups["url"], w, h));
                }
            }
            if (允许MediaPlayer标记)
            {
                foreach (Match m in Regex.Matches(字符串, @"\[wmp=(?<width>\d+),(?<height>\d+)\](?<url>.*?)\[/wmp\]", RegexOptions.IgnoreCase))
                {
                    int w = m.Groups["width"].Value.转换为Int32();
                    int h = m.Groups["height"].Value.转换为Int32();
                    if (限制多媒体内容尺寸)
                    {
                        if (w > 限制多媒体内容最大宽度) w = 限制多媒体内容最大宽度;
                        if (w < 限制多媒体内容最小宽度) w = 限制多媒体内容最小宽度;
                        if (h > 限制多媒体内容最大高度) h = 限制多媒体内容最大高度;
                        if (h < 限制多媒体内容最小高度) h = 限制多媒体内容最小高度;
                    }
                    字符串 = 字符串.Replace(m.Value, String.Format(@"<object class=""UBBMediaPlayer"" height=""{2}"" width=""{1}"" classid=""CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6"">
<param name=""AutoStart"" value=""0"">
<param name=""enabled"" value=""-1"">
<param name=""url"" value=""{0}"">
</object>", m.Groups["url"], w, h));
                }
            }
            if (允许RealPlayer标记)
            {
                int i = 0;
                foreach (Match m in Regex.Matches(字符串, @"\[rp=(?<width>\d+),(?<height>\d+)\](?<url>.*?)\[/rp\]", RegexOptions.IgnoreCase))
                {
                    i++;
                    int w = m.Groups["width"].Value.转换为Int32();
                    int h = m.Groups["height"].Value.转换为Int32();
                    if (限制多媒体内容尺寸)
                    {
                        if (w > 限制多媒体内容最大宽度) w = 限制多媒体内容最大宽度;
                        if (w < 限制多媒体内容最小宽度) w = 限制多媒体内容最小宽度;
                        if (h > 限制多媒体内容最大高度) h = 限制多媒体内容最大高度;
                        if (h < 限制多媒体内容最小高度) h = 限制多媒体内容最小高度;
                    }
                    字符串 = 字符串.Replace(m.Value, String.Format(@"<object classid=""clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"" class=""UBBRealPlayer"" width=""{1}"" height=""{2}"">
<param name=""SRC"" value=""{0}"">
<param name=""CONSOLE"" value=""{3}"">
<param name=""CONTROLS"" value=""imagewindow"">
<param name=""AUTOSTART"" value=""0"">
</object>
<br>
<object classid=""CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" height=""32"" width=""{1}"">
<param name=""SRC"" value=""{0}"">
<param name=""AUTOSTART"" value=""0"">
<param name=""CONTROLS"" value=""controlpanel"">
<param name=""CONSOLE"" value=""{3}"">
</object>", m.Groups["url"], w, h, "UBBRM" + i));
                }
            }
            if (允许QuickTime标记)
            {
                foreach (Match m in Regex.Matches(字符串, @"\[qt=(?<width>\d+),(?<height>\d+)\](?<url>.*?)\[/qt\]", RegexOptions.IgnoreCase))
                {
                    int w = m.Groups["width"].Value.转换为Int32();
                    int h = m.Groups["height"].Value.转换为Int32();
                    if (限制多媒体内容尺寸)
                    {
                        if (w > 限制多媒体内容最大宽度) w = 限制多媒体内容最大宽度;
                        if (w < 限制多媒体内容最小宽度) w = 限制多媒体内容最小宽度;
                        if (h > 限制多媒体内容最大高度) h = 限制多媒体内容最大高度;
                        if (h < 限制多媒体内容最小高度) h = 限制多媒体内容最小高度;
                    }
                    字符串 = 字符串.Replace(m.Value, String.Format(@"<embed src=""{0}"" width=""{1}"" height=""{2}"" autoplay=""0"" loop=""0"" controller=""1"" playeveryframe=""0"" cache=""0"" scale=""TOFIT"" bgcolor=""#000000"" kioskmode=""0"" targetcache=""0"" pluginspage=""http://www.apple.com/quicktime"" />", m.Groups["url"], w, h));
                }
            }
            if (允许Code标记)
            {
                for (int i = 0; i < CODE.Count; i++)
                {
                    字符串 = 字符串.Insert(字符串.IndexOf("[||CODEBLOCK||]"), "[||NOWCHANGE||]");
                    字符串 = 字符串.Replace("[||NOWCHANGE||][||CODEBLOCK||]", String.Format(@"<div class=""UBBCode"" {1}>{0}</div>", CODE[i].Groups[1].Value.进行HTML转义(), 附加代码内容块属性代码));
                }
            }
            if (允许Quote标记)
            {
                for (int i = 0; i < QUOTE.Count; i++)
                {
                    字符串 = 字符串.Insert(字符串.IndexOf("[||QUOTEBLOCK||]"), "[||NOWCHANGE||]");
                    字符串 = 字符串.Replace("[||NOWCHANGE||][||QUOTEBLOCK||]", String.Format(@"引用:<br /><div class=""UBBQuote"" {1}>{0}</div>", QUOTE[i].Groups[1].Value.清除UBB格式().进行HTML转义(), 附加引用内容块属性代码));
                }
            }
            if (允许Html标记)
            {
                for (int i = 0; i < HTML.Count; i++)
                {
                    字符串 = 字符串.Insert(字符串.IndexOf("[||HTMLCODE||]"), "[||NOWCHANGE||]");
                    字符串 = 字符串.Replace("[||NOWCHANGE||][||HTMLCODE||]", HTML[i].Groups[1].Value);
                }
            }
            foreach (Match m in Regex.Matches(字符串, @"\[\|\|URL\|\|\](.*?)\[\|\|\/URL\|\|\]"))
            {
                字符串 = 字符串.Replace(m.Value, 对Url路径进行编码 ? m.Groups[1].Value.进行URL路径编码() : m.Groups[1].Value);
            }
            #endregion

            return 字符串;
        }

        /// <summary>
        /// 采用默认设置，将待处理字符串中的UBB内容转换为Html代码
        /// </summary>
        /// <param name="待处理字符串">用于转换的原始字符串</param>
        /// <returns>转换后的Html代码字符串</returns>
        public static string 转换为Html代码(string 待处理字符串)
        {
            return new UBB(待处理字符串).转换为Html代码();
        }

        /// <summary>
        /// 除超链接相关的参数外，均采用默认设置，将待处理字符串中的UBB内容转换为Html代码
        /// </summary>
        /// <param name="待处理字符串">用于转换的原始字符串</param>
        /// <param name="在新窗口中打开超链接">指示超链接(a)是否都在新窗口打开</param>
        /// <returns>转换后的Html代码字符串</returns>
        public static string 转换为Html代码(string 待处理字符串, bool 在新窗口中打开超链接)
        {
            return new UBB(待处理字符串) { 在新窗口中打开超链接 = 在新窗口中打开超链接 }.转换为Html代码();
        }

        /// <summary>
        /// 获取HTML格式的UBB代码使用帮助，帮助内容会随着设置的改变产生相应变化，比如设置中禁用的转换标记将不会出现在帮助内容中
        /// </summary>
        public string 帮助信息
        {
            get
            {
                StringBuilder S = new StringBuilder();
                S.Append(String.Format(@"粗体标记：[b]粗体[/b]演示
显示为：{0}

".进行HTML转义(), "[b]粗体[/b]演示".转换UBB内容()));
                S.Append(String.Format(@"斜体标记：[i]斜体[/i]演示
显示为：{0}

".进行HTML转义(), "[i]斜体[/i]演示".转换UBB内容()));
                S.Append(String.Format(@"下划线标记：[u]下划线[/u]演示
显示为：{0}

".进行HTML转义(), "[u]下划线[/u]演示".转换UBB内容()));
                S.Append(String.Format(@"上标标记：[sup]上标[/sup]演示
显示为：{0}

".进行HTML转义(), "[sup]上标[/sup]演示".转换UBB内容()));
                S.Append(String.Format(@"下标标记：[sub]下标[/sub]演示
显示为：{0}

".进行HTML转义(), "[sub]下标[/sub]演示".转换UBB内容()));
                S.Append(String.Format(@"居左标记：[left]居左[/left]演示
显示为：{0}

".进行HTML转义(), "[left]居左[/left]演示".转换UBB内容()));
                S.Append(String.Format(@"居中标记：[center]居中[/center]演示
显示为：{0}

".进行HTML转义(), "[center]居中[/center]演示".转换UBB内容()));
                S.Append(String.Format(@"居右标记：[right]居右[/right]演示
显示为：{0}

".进行HTML转义(), "[right]居右[/right]演示".转换UBB内容()));
                S.Append(String.Format(@"字号标记：[size=5]字号[/size]演示
显示为：{0}

".进行HTML转义(), "[size=5]字号[/size]演示".转换UBB内容()));
                S.Append(String.Format(@"字体标记：[font=黑体]字体[/font]演示
显示为：{0}

".进行HTML转义(), "[font=黑体]字体[/font]演示".转换UBB内容()));
                S.Append(String.Format(@"字色标记：[color=red]字色[/color]演示
显示为：{0}

".进行HTML转义(), "[color=red]字色[/color]演示".转换UBB内容()));
                S.Append(String.Format(@"有序列表标记：有序列表[ol]项目1
项目2
项目3[/ol]演示
显示为：{0}

".进行HTML转义(), @"有序列表[ol]项目1
项目2
项目3[/ol]演示".转换UBB内容()));
                S.Append(String.Format(@"无序列表标记：无序列表[ul]项目1
项目2
项目3[/ul]演示
显示为：{0}

".进行HTML转义(), @"无序列表[ul]项目1
项目2
项目3[/ul]演示".转换UBB内容()));
                if (允许Url标记) S.Append(String.Format(@"超链接标记：超链接[url]http://www.163.com[/url]演示
显示为：{0}

".进行HTML转义(), @"超链接[url]http://www.163.com[/url]演示".转换UBB内容()));
                if (允许Url标记) S.Append(String.Format(@"隐式超链接标记：[url=http://www.163.com]隐式超链接[/url]演示
显示为：{0}

".进行HTML转义(), @"[url=http://www.163.com]隐式超链接[/url]演示".转换UBB内容()));
                if (允许Email标记) S.Append(String.Format(@"电子邮件标记：电子邮件[email]abc@163.com[/email]演示
显示为：{0}

".进行HTML转义(), @"电子邮件[email]abc@163.com[/email]演示".转换UBB内容()));
                if (允许Email标记) S.Append(String.Format(@"隐式电子邮件标记：[email=abc@163.com]隐式电子邮件[/email]演示
显示为：{0}

".进行HTML转义(), @"[email=abc@163.com]隐式电子邮件[/email]演示".转换UBB内容()));
                if (允许Image标记) S.Append(String.Format(@"图像标记：图像[image]http://cimg2.163.com/cnews/163/img6/logo.gif[/image]演示
显示为：{0}

".进行HTML转义(), @"图像[image]http://cimg2.163.com/cnews/163/img6/logo.gif[/image]演示".转换UBB内容()));
                if (允许Code标记) S.Append(String.Format(@"代码标记：代码[code]<div class=""test"">
test text
</div>[/code]演示
显示为：{0}

".进行HTML转义(), new UBB(@"代码[code]<div class=""test"">
test text
</div>[/code]演示") { 允许Code标记 = true }.转换为Html代码()));
                if (允许Quote标记) S.Append(String.Format(@"引用标记：[quote]引用内容abcde12345[/quote]演示
显示为：{0}

".进行HTML转义(), new UBB(@"[quote]引用内容abcde12345[/quote]演示") { 允许Quote标记 = true }.转换为Html代码()));
                if (允许Html标记) S.Append(String.Format(@"HTML源代码标记：HTML源代码[html]<a href=""#"">ABC</a>[/html]演示
显示为：{0}

".进行HTML转义(), new UBB(@"HTML源代码[html]<a href=""#"">ABC</a>[/html]演示") { 允许Html标记 = true }.转换为Html代码()));
                if (允许Flash标记) S.Append(@"Flash标记：Flash[swf=320,240]http://www.xflash.com/abc.swf[/swf]演示

".进行HTML转义());
                if (允许Director标记) S.Append(@"Director标记：Director[dcr=320,240]http://www.dirfans.com/abc.dcr[/dcr]演示

".进行HTML转义());
                if (允许MediaPlayer标记) S.Append(@"MediaPlayer标记：MediaPlayer[wmp=320,240]http://www.windowslives.com/abc.wmv[/wmp]演示

".进行HTML转义());
                if (允许RealPlayer标记) S.Append(@"RealPlayer标记：RealPlayer[rp=320,240]http://www.realnets.com/abc.rm[/rp]演示

".进行HTML转义());
                if (允许QuickTime标记) S.Append(@"QuickTime标记：QuickTime[qt=320,240]http://www.applemov.com/abc.mov[/qt]演示

".进行HTML转义());

                return S.ToString();
            }
        }
    }
}
