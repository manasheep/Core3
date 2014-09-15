using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Core.Time;

/*
包含RSS相关类
版本:1
-------------------------
源代码使用[项目代码生成器]生成
程序版本:1.0.11.10
核心类库CodeMaker.dll版本:1.7.18.15
-------------------------
创建时间:2007年6月14日 04:21:37
最后修改时间:2008年7月2日 16:47:12
代码生成时间:2008年7月2日 16:47:15
*/

namespace Core.Net
{

    #region 项目代码生成器生成的代码

    /// <summary>
    /// RSS聚合类
    /// 对应数据表:[RSS]
    /// 版本:1
    /// </summary>
    [XmlRoot("rss")]
    public class 聚合
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 聚合()
        {
            version = "2.0";
        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 版本更改委托
        /// </summary>
        public delegate void 版本更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 版本更改事件，在属性即将被更改时触发
        /// </summary>
        public event 版本更改 版本更改事件;
        /// <summary>
        /// 版本已更改委托
        /// </summary>
        public delegate void 版本已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 版本已更改事件，在属性被更改后触发
        /// </summary>
        public event 版本已更改 版本已更改事件;

        /// <summary>
        /// 必要参数
        /// RSS版本，默认为2.0
        /// 对应属性:版本
        /// 数据长度:200
        /// </summary>
        string version;

        /// <summary>
        /// 必要参数
        /// RSS版本，默认为2.0
        /// 对应字段:version
        /// 数据长度:200
        /// </summary>
        [XmlAttribute("version")]
        public string 版本
        {

            get
            {
                return version;
            }

            set
            {
                if (版本更改事件 != null) 版本更改事件(this, version, value);
                version = value;
                if (版本已更改事件 != null) 版本已更改事件(this, version);
            }

        }

        /// <summary>
        /// 频道更改委托
        /// </summary>
        public delegate void 频道更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 频道更改事件，在属性即将被更改时触发
        /// </summary>
        public event 频道更改 频道更改事件;
        /// <summary>
        /// 频道已更改委托
        /// </summary>
        public delegate void 频道已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 频道已更改事件，在属性被更改后触发
        /// </summary>
        public event 频道已更改 频道已更改事件;

        /// <summary>
        /// 必要参数
        /// 频道内容
        /// 对应属性:频道
        /// </summary>
        频道 channel;

        /// <summary>
        /// 必要参数
        /// 频道内容
        /// 对应字段:channel
        /// </summary>
        [XmlElement(ElementName = "channel")]
        public 频道 频道
        {

            get
            {
                return channel;
            }

            set
            {
                if (频道更改事件 != null) 频道更改事件(this, channel, value);
                channel = value;
                if (频道已更改事件 != null) 频道已更改事件(this, channel);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[聚合]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("版本(version:string): " + 版本);
            S.AppendLine("频道(channel:频道): " + 频道);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// RSS频道
    /// 对应数据表:[channel]
    /// 版本:1
    /// </summary>
    [XmlRoot("channel")]
    public class 频道
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 频道()
        {
            language = "zh-cn";
            docs = "http://blogs.law.harvard.edu/tech/rss";
            设置发布时间(DateTime.Now);
            设置最后修改时间(DateTime.Now);
            generator = "Core";
            ttl = 30;
        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 标题更改委托
        /// </summary>
        public delegate void 标题更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 标题更改事件，在属性即将被更改时触发
        /// </summary>
        public event 标题更改 标题更改事件;
        /// <summary>
        /// 标题已更改委托
        /// </summary>
        public delegate void 标题已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 标题已更改事件，在属性被更改后触发
        /// </summary>
        public event 标题已更改 标题已更改事件;

        /// <summary>
        /// 必要参数
        /// 频道名称
        /// 对应属性:标题
        /// 数据长度:200
        /// </summary>
        string title;

        /// <summary>
        /// 必要参数
        /// 频道名称
        /// 对应字段:title
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string 标题
        {

            get
            {
                return title;
            }

            set
            {
                if (标题更改事件 != null) 标题更改事件(this, title, value);
                title = value;
                if (标题已更改事件 != null) 标题已更改事件(this, title);
            }

        }

        /// <summary>
        /// 链接更改委托
        /// </summary>
        public delegate void 链接更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 链接更改事件，在属性即将被更改时触发
        /// </summary>
        public event 链接更改 链接更改事件;
        /// <summary>
        /// 链接已更改委托
        /// </summary>
        public delegate void 链接已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 链接已更改事件，在属性被更改后触发
        /// </summary>
        public event 链接已更改 链接已更改事件;

        /// <summary>
        /// 必要参数
        /// 相关链接地址
        /// 对应属性:链接
        /// 数据长度:500
        /// </summary>
        string link;

        /// <summary>
        /// 必要参数
        /// 相关链接地址
        /// 对应字段:link
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "link")]
        public string 链接
        {

            get
            {
                return link;
            }

            set
            {
                if (链接更改事件 != null) 链接更改事件(this, link, value);
                link = value;
                if (链接已更改事件 != null) 链接已更改事件(this, link);
            }

        }

        /// <summary>
        /// 描述更改委托
        /// </summary>
        public delegate void 描述更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 描述更改事件，在属性即将被更改时触发
        /// </summary>
        public event 描述更改 描述更改事件;
        /// <summary>
        /// 描述已更改委托
        /// </summary>
        public delegate void 描述已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 描述已更改事件，在属性被更改后触发
        /// </summary>
        public event 描述已更改 描述已更改事件;

        /// <summary>
        /// 必要参数
        /// 频道说明
        /// 对应属性:描述
        /// 数据长度:3000
        /// </summary>
        string description;

        /// <summary>
        /// 必要参数
        /// 频道说明
        /// 对应字段:description
        /// 数据长度:3000
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string 描述
        {

            get
            {
                return description;
            }

            set
            {
                if (描述更改事件 != null) 描述更改事件(this, description, value);
                description = value;
                if (描述已更改事件 != null) 描述已更改事件(this, description);
            }

        }

        /// <summary>
        /// 语言更改委托
        /// </summary>
        public delegate void 语言更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 语言更改事件，在属性即将被更改时触发
        /// </summary>
        public event 语言更改 语言更改事件;
        /// <summary>
        /// 语言已更改委托
        /// </summary>
        public delegate void 语言已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 语言已更改事件，在属性被更改后触发
        /// </summary>
        public event 语言已更改 语言已更改事件;

        /// <summary>
        /// 频道使用的语言，默认为zh-cn
        /// 对应属性:语言
        /// 数据长度:100
        /// </summary>
        string language;

        /// <summary>
        /// 频道使用的语言，默认为zh-cn
        /// 对应字段:language
        /// 数据长度:100
        /// </summary>
        [XmlElement(ElementName = "language")]
        public string 语言
        {

            get
            {
                return language;
            }

            set
            {
                if (语言更改事件 != null) 语言更改事件(this, language, value);
                language = value;
                if (语言已更改事件 != null) 语言已更改事件(this, language);
            }

        }

        /// <summary>
        /// 版权声明更改委托
        /// </summary>
        public delegate void 版权声明更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 版权声明更改事件，在属性即将被更改时触发
        /// </summary>
        public event 版权声明更改 版权声明更改事件;
        /// <summary>
        /// 版权声明已更改委托
        /// </summary>
        public delegate void 版权声明已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 版权声明已更改事件，在属性被更改后触发
        /// </summary>
        public event 版权声明已更改 版权声明已更改事件;

        /// <summary>
        /// 版权信息
        /// 对应属性:版权声明
        /// 数据长度:500
        /// </summary>
        string copyright;

        /// <summary>
        /// 版权信息
        /// 对应字段:copyright
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "copyright")]
        public string 版权声明
        {

            get
            {
                return copyright;
            }

            set
            {
                if (版权声明更改事件 != null) 版权声明更改事件(this, copyright, value);
                copyright = value;
                if (版权声明已更改事件 != null) 版权声明已更改事件(this, copyright);
            }

        }

        /// <summary>
        /// 责任编辑更改委托
        /// </summary>
        public delegate void 责任编辑更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 责任编辑更改事件，在属性即将被更改时触发
        /// </summary>
        public event 责任编辑更改 责任编辑更改事件;
        /// <summary>
        /// 责任编辑已更改委托
        /// </summary>
        public delegate void 责任编辑已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 责任编辑已更改事件，在属性被更改后触发
        /// </summary>
        public event 责任编辑已更改 责任编辑已更改事件;

        /// <summary>
        /// 责任编辑电子邮件地址
        /// 对应属性:责任编辑
        /// 数据长度:100
        /// </summary>
        string managingEditor;

        /// <summary>
        /// 责任编辑电子邮件地址
        /// 对应字段:managingEditor
        /// 数据长度:100
        /// </summary>
        [XmlElement(ElementName = "managingEditor")]
        public string 责任编辑
        {

            get
            {
                return managingEditor;
            }

            set
            {
                if (责任编辑更改事件 != null) 责任编辑更改事件(this, managingEditor, value);
                managingEditor = value;
                if (责任编辑已更改事件 != null) 责任编辑已更改事件(this, managingEditor);
            }

        }

        /// <summary>
        /// 管理员更改委托
        /// </summary>
        public delegate void 管理员更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 管理员更改事件，在属性即将被更改时触发
        /// </summary>
        public event 管理员更改 管理员更改事件;
        /// <summary>
        /// 管理员已更改委托
        /// </summary>
        public delegate void 管理员已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 管理员已更改事件，在属性被更改后触发
        /// </summary>
        public event 管理员已更改 管理员已更改事件;

        /// <summary>
        /// 网站管理员电子邮件地址
        /// 对应属性:管理员
        /// 数据长度:100
        /// </summary>
        string webMaster;

        /// <summary>
        /// 网站管理员电子邮件地址
        /// 对应字段:webMaster
        /// 数据长度:100
        /// </summary>
        [XmlElement(ElementName = "webMaster")]
        public string 管理员
        {

            get
            {
                return webMaster;
            }

            set
            {
                if (管理员更改事件 != null) 管理员更改事件(this, webMaster, value);
                webMaster = value;
                if (管理员已更改事件 != null) 管理员已更改事件(this, webMaster);
            }

        }

        /// <summary>
        /// 发布时间更改委托
        /// </summary>
        public delegate void 发布时间更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 发布时间更改事件，在属性即将被更改时触发
        /// </summary>
        public event 发布时间更改 发布时间更改事件;
        /// <summary>
        /// 发布时间已更改委托
        /// </summary>
        public delegate void 发布时间已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 发布时间已更改事件，在属性被更改后触发
        /// </summary>
        public event 发布时间已更改 发布时间已更改事件;

        /// <summary>
        /// 频道内容发布时间
        /// 对应属性:发布时间
        /// 数据长度:200
        /// </summary>
        string pubDate;

        /// <summary>
        /// 频道内容发布时间
        /// 对应字段:pubDate
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "pubDate")]
        public string 发布时间
        {

            get
            {
                return pubDate;
            }

            set
            {
                if (发布时间更改事件 != null) 发布时间更改事件(this, pubDate, value);
                pubDate = value;
                if (发布时间已更改事件 != null) 发布时间已更改事件(this, pubDate);
            }

        }

        /// <summary>
        /// 最后修改时间更改委托
        /// </summary>
        public delegate void 最后修改时间更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 最后修改时间更改事件，在属性即将被更改时触发
        /// </summary>
        public event 最后修改时间更改 最后修改时间更改事件;
        /// <summary>
        /// 最后修改时间已更改委托
        /// </summary>
        public delegate void 最后修改时间已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 最后修改时间已更改事件，在属性被更改后触发
        /// </summary>
        public event 最后修改时间已更改 最后修改时间已更改事件;

        /// <summary>
        /// 频道内容最后修改时间
        /// 对应属性:最后修改时间
        /// 数据长度:200
        /// </summary>
        string lastBuildDate;

        /// <summary>
        /// 频道内容最后修改时间
        /// 对应字段:lastBuildDate
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "lastBuildDate")]
        public string 最后修改时间
        {

            get
            {
                return lastBuildDate;
            }

            set
            {
                if (最后修改时间更改事件 != null) 最后修改时间更改事件(this, lastBuildDate, value);
                lastBuildDate = value;
                if (最后修改时间已更改事件 != null) 最后修改时间已更改事件(this, lastBuildDate);
            }

        }

        /// <summary>
        /// 生成程序更改委托
        /// </summary>
        public delegate void 生成程序更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 生成程序更改事件，在属性即将被更改时触发
        /// </summary>
        public event 生成程序更改 生成程序更改事件;
        /// <summary>
        /// 生成程序已更改委托
        /// </summary>
        public delegate void 生成程序已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 生成程序已更改事件，在属性被更改后触发
        /// </summary>
        public event 生成程序已更改 生成程序已更改事件;

        /// <summary>
        /// 频道生成程序，默认采用程序集名称及版本号
        /// 对应属性:生成程序
        /// 数据长度:200
        /// </summary>
        string generator;

        /// <summary>
        /// 频道生成程序，默认采用程序集名称及版本号
        /// 对应字段:generator
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "generator")]
        public string 生成程序
        {

            get
            {
                return generator;
            }

            set
            {
                if (生成程序更改事件 != null) 生成程序更改事件(this, generator, value);
                generator = value;
                if (生成程序已更改事件 != null) 生成程序已更改事件(this, generator);
            }

        }

        /// <summary>
        /// 说明文档更改委托
        /// </summary>
        public delegate void 说明文档更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 说明文档更改事件，在属性即将被更改时触发
        /// </summary>
        public event 说明文档更改 说明文档更改事件;
        /// <summary>
        /// 说明文档已更改委托
        /// </summary>
        public delegate void 说明文档已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 说明文档已更改事件，在属性被更改后触发
        /// </summary>
        public event 说明文档已更改 说明文档已更改事件;

        /// <summary>
        /// RSS规范说明文档，默认采用http://blogs.law.harvard.edu/tech/rss
        /// 对应属性:说明文档
        /// </summary>
        string docs;

        /// <summary>
        /// RSS规范说明文档，默认采用http://blogs.law.harvard.edu/tech/rss
        /// 对应字段:docs
        /// </summary>
        [XmlElement(ElementName = "docs")]
        public string 说明文档
        {

            get
            {
                return docs;
            }

            set
            {
                if (说明文档更改事件 != null) 说明文档更改事件(this, docs, value);
                docs = value;
                if (说明文档已更改事件 != null) 说明文档已更改事件(this, docs);
            }

        }

        /// <summary>
        /// 忽略时段更改委托
        /// </summary>
        public delegate void 忽略时段更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 忽略时段更改事件，在属性即将被更改时触发
        /// </summary>
        public event 忽略时段更改 忽略时段更改事件;
        /// <summary>
        /// 忽略时段已更改委托
        /// </summary>
        public delegate void 忽略时段已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 忽略时段已更改事件，在属性被更改后触发
        /// </summary>
        public event 忽略时段已更改 忽略时段已更改事件;

        /// <summary>
        /// 指示RSS阅读器跳过的访问时段，有效范围为0-23
        /// 对应属性:忽略时段
        /// </summary>
        List<忽略时段> skipHours;

        /// <summary>
        /// 指示RSS阅读器跳过的访问时段，有效范围为0-23
        /// 对应字段:skipHours
        /// </summary>
        [XmlElement(ElementName = "skipHours")]
        public List<忽略时段> 忽略时段
        {

            get
            {
                return skipHours;
            }

            set
            {
                if (忽略时段更改事件 != null) 忽略时段更改事件(this, skipHours, value);
                skipHours = value;
                if (忽略时段已更改事件 != null) 忽略时段已更改事件(this, skipHours);
            }

        }

        /// <summary>
        /// 忽略日期更改委托
        /// </summary>
        public delegate void 忽略日期更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 忽略日期更改事件，在属性即将被更改时触发
        /// </summary>
        public event 忽略日期更改 忽略日期更改事件;
        /// <summary>
        /// 忽略日期已更改委托
        /// </summary>
        public delegate void 忽略日期已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 忽略日期已更改事件，在属性被更改后触发
        /// </summary>
        public event 忽略日期已更改 忽略日期已更改事件;

        /// <summary>
        /// 指示RSS阅读器跳过的日期，最多可设置7个忽略日期项
        /// 对应属性:忽略日期
        /// </summary>
        List<忽略日期> skipDays;

        /// <summary>
        /// 指示RSS阅读器跳过的日期，最多可设置7个忽略日期项
        /// 对应字段:skipDays
        /// </summary>
        [XmlElement(ElementName = "skipDays")]
        public List<忽略日期> 忽略日期
        {

            get
            {
                return skipDays;
            }

            set
            {
                if (忽略日期更改事件 != null) 忽略日期更改事件(this, skipDays, value);
                skipDays = value;
                if (忽略日期已更改事件 != null) 忽略日期已更改事件(this, skipDays);
            }

        }

        /// <summary>
        /// 生存时间更改委托
        /// </summary>
        public delegate void 生存时间更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 生存时间更改事件，在属性即将被更改时触发
        /// </summary>
        public event 生存时间更改 生存时间更改事件;
        /// <summary>
        /// 生存时间已更改委托
        /// </summary>
        public delegate void 生存时间已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 生存时间已更改事件，在属性被更改后触发
        /// </summary>
        public event 生存时间已更改 生存时间已更改事件;

        /// <summary>
        /// 信息生存周期，指明该频道可被缓存的最长时间，以分钟分单位，默认为30
        /// 对应属性:生存时间
        /// </summary>
        int ttl;

        /// <summary>
        /// 信息生存周期，指明该频道可被缓存的最长时间，以分钟分单位，默认为30
        /// 对应字段:ttl
        /// </summary>
        [XmlElement(ElementName = "ttl")]
        public int 生存时间
        {

            get
            {
                return ttl;
            }

            set
            {
                if (生存时间更改事件 != null) 生存时间更改事件(this, ttl, value);
                ttl = value;
                if (生存时间已更改事件 != null) 生存时间已更改事件(this, ttl);
            }

        }

        /// <summary>
        /// 版块分类更改委托
        /// </summary>
        public delegate void 版块分类更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 版块分类更改事件，在属性即将被更改时触发
        /// </summary>
        public event 版块分类更改 版块分类更改事件;
        /// <summary>
        /// 版块分类已更改委托
        /// </summary>
        public delegate void 版块分类已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 版块分类已更改事件，在属性被更改后触发
        /// </summary>
        public event 版块分类已更改 版块分类已更改事件;

        /// <summary>
        /// 频道版块分类信息集合
        /// 对应属性:版块分类
        /// </summary>
        List<版块分类> category;

        /// <summary>
        /// 频道版块分类信息集合
        /// 对应字段:category
        /// </summary>
        [XmlElement(ElementName = "category")]
        public List<版块分类> 版块分类
        {

            get
            {
                return category;
            }

            set
            {
                if (版块分类更改事件 != null) 版块分类更改事件(this, category, value);
                category = value;
                if (版块分类已更改事件 != null) 版块分类已更改事件(this, category);
            }

        }

        /// <summary>
        /// 图像更改委托
        /// </summary>
        public delegate void 图像更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 图像更改事件，在属性即将被更改时触发
        /// </summary>
        public event 图像更改 图像更改事件;
        /// <summary>
        /// 图像已更改委托
        /// </summary>
        public delegate void 图像已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 图像已更改事件，在属性被更改后触发
        /// </summary>
        public event 图像已更改 图像已更改事件;

        /// <summary>
        /// 频道图像
        /// 对应属性:图像
        /// </summary>
        图像 image;

        /// <summary>
        /// 频道图像
        /// 对应字段:image
        /// </summary>
        [XmlElement(ElementName = "image")]
        public 图像 图像
        {

            get
            {
                return image;
            }

            set
            {
                if (图像更改事件 != null) 图像更改事件(this, image, value);
                image = value;
                if (图像已更改事件 != null) 图像已更改事件(this, image);
            }

        }

        /// <summary>
        /// 项目更改委托
        /// </summary>
        public delegate void 项目更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 项目更改事件，在属性即将被更改时触发
        /// </summary>
        public event 项目更改 项目更改事件;
        /// <summary>
        /// 项目已更改委托
        /// </summary>
        public delegate void 项目已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 项目已更改事件，在属性被更改后触发
        /// </summary>
        public event 项目已更改 项目已更改事件;

        /// <summary>
        /// 频道内容项目集合
        /// 对应属性:项目
        /// </summary>
        List<项目> item;

        /// <summary>
        /// 频道内容项目集合
        /// 对应字段:item
        /// </summary>
        [XmlElement(ElementName = "item")]
        public List<项目> 项目
        {

            get
            {
                return item;
            }

            set
            {
                if (项目更改事件 != null) 项目更改事件(this, item, value);
                item = value;
                if (项目已更改事件 != null) 项目已更改事件(this, item);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[频道]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("标题(title:string): " + 标题);
            S.AppendLine("链接(link:string): " + 链接);
            S.AppendLine("描述(description:string): " + 描述);
            S.AppendLine("语言(language:string): " + 语言);
            S.AppendLine("版权声明(copyright:string): " + 版权声明);
            S.AppendLine("责任编辑(managingEditor:string): " + 责任编辑);
            S.AppendLine("管理员(webMaster:string): " + 管理员);
            S.AppendLine("发布时间(pubDate:string): " + 发布时间);
            S.AppendLine("最后修改时间(lastBuildDate:string): " + 最后修改时间);
            S.AppendLine("生成程序(generator:string): " + 生成程序);
            S.AppendLine("说明文档(docs:string): " + 说明文档);
            S.AppendLine("忽略时段(skipHours:List<忽略时段>): " + 忽略时段);
            S.AppendLine("忽略日期(skipDays:List<忽略日期>): " + 忽略日期);
            S.AppendLine("生存时间(ttl:int): " + 生存时间);
            S.AppendLine("版块分类(category:List<版块分类>): " + 版块分类);
            S.AppendLine("图像(image:图像): " + 图像);
            S.AppendLine("项目(item:List<项目>): " + 项目);
            return S.ToString();

        }

        #endregion

        #region 自定义附加代码

        /// <summary>
        /// 设置频道发布时间
        /// </summary>
        /// <param name="日期时间">日期时间对象</param>
        public void 设置发布时间(DateTime 日期时间)
        {
            pubDate = 日期时间.ToString("R");
        }

        /// <summary>
        /// 获取频道发布时间
        /// </summary>
        /// <returns>日期时间对象</returns>
        public DateTime 获取发布时间()
        {
            return DateTime.Parse(pubDate).ToUniversalTime();
        }

        /// <summary>
        /// 设置频道最后修改时间
        /// </summary>
        /// <param name="日期时间">日期时间对象</param>
        public void 设置最后修改时间(DateTime 日期时间)
        {
            lastBuildDate = 日期时间.ToString("R");
        }

        /// <summary>
        /// 获取频道最后修改时间
        /// </summary>
        /// <returns>日期时间对象</returns>
        public DateTime 获取最后修改时间()
        {
            return DateTime.Parse(lastBuildDate).ToUniversalTime();
        }

        #endregion

    }

    /// <summary>
    /// 版块分类信息
    /// 对应数据表:[category]
    /// 版本:1
    /// </summary>
    [XmlRoot("category")]
    public class 版块分类
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 版块分类()
        {

        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 名称更改委托
        /// </summary>
        public delegate void 名称更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 名称更改事件，在属性即将被更改时触发
        /// </summary>
        public event 名称更改 名称更改事件;
        /// <summary>
        /// 名称已更改委托
        /// </summary>
        public delegate void 名称已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 名称已更改事件，在属性被更改后触发
        /// </summary>
        public event 名称已更改 名称已更改事件;

        /// <summary>
        /// 必要参数
        /// 版块分类名称，可以使用“/”来表示分类层级关系
        /// 对应属性:名称
        /// 数据长度:300
        /// </summary>
        string Name;

        /// <summary>
        /// 必要参数
        /// 版块分类名称，可以使用“/”来表示分类层级关系
        /// 对应字段:Name
        /// 数据长度:300
        /// </summary>
        [XmlTextAttribute]
        public string 名称
        {

            get
            {
                return Name;
            }

            set
            {
                if (名称更改事件 != null) 名称更改事件(this, Name, value);
                Name = value;
                if (名称已更改事件 != null) 名称已更改事件(this, Name);
            }

        }

        /// <summary>
        /// 网址更改委托
        /// </summary>
        public delegate void 网址更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 网址更改事件，在属性即将被更改时触发
        /// </summary>
        public event 网址更改 网址更改事件;
        /// <summary>
        /// 网址已更改委托
        /// </summary>
        public delegate void 网址已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 网址已更改事件，在属性被更改后触发
        /// </summary>
        public event 网址已更改 网址已更改事件;

        /// <summary>
        /// 板块分类网络地址
        /// 对应属性:网址
        /// 数据长度:500
        /// </summary>
        string domain;

        /// <summary>
        /// 板块分类网络地址
        /// 对应字段:domain
        /// 数据长度:500
        /// </summary>
        [XmlAttribute("domain")]
        public string 网址
        {

            get
            {
                return domain;
            }

            set
            {
                if (网址更改事件 != null) 网址更改事件(this, domain, value);
                domain = value;
                if (网址已更改事件 != null) 网址已更改事件(this, domain);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[版块分类]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("名称(Name:string): " + 名称);
            S.AppendLine("网址(domain:string): " + 网址);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// 指示RSS阅读器可忽略的日期
    /// 对应数据表:[skipDays]
    /// 版本:1
    /// </summary>
    [XmlRoot("skipDays")]
    public class 忽略日期
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 忽略日期()
        {

        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 日期更改委托
        /// </summary>
        public delegate void 日期更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 日期更改事件，在属性即将被更改时触发
        /// </summary>
        public event 日期更改 日期更改事件;
        /// <summary>
        /// 日期已更改委托
        /// </summary>
        public delegate void 日期已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 日期已更改事件，在属性被更改后触发
        /// </summary>
        public event 日期已更改 日期已更改事件;

        /// <summary>
        /// 必要参数
        /// 可忽略的日期
        /// 对应属性:日期
        /// </summary>
        星期 days;

        /// <summary>
        /// 必要参数
        /// 可忽略的日期
        /// 对应字段:days
        /// </summary>
        [XmlTextAttribute]
        public 星期 日期
        {

            get
            {
                return days;
            }

            set
            {
                if (日期更改事件 != null) 日期更改事件(this, days, value);
                days = value;
                if (日期已更改事件 != null) 日期已更改事件(this, days);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[忽略日期]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("日期(days:星期): " + 日期);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// 指示RSS阅读器可忽略的时段
    /// 对应数据表:[skipHours]
    /// 版本:1
    /// </summary>
    [XmlRoot("skipHours")]
    public class 忽略时段
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 忽略时段()
        {

        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 时段更改委托
        /// </summary>
        public delegate void 时段更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 时段更改事件，在属性即将被更改时触发
        /// </summary>
        public event 时段更改 时段更改事件;
        /// <summary>
        /// 时段已更改委托
        /// </summary>
        public delegate void 时段已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 时段已更改事件，在属性被更改后触发
        /// </summary>
        public event 时段已更改 时段已更改事件;

        /// <summary>
        /// 必要参数
        /// 可忽略的时段，有效数字为0-23
        /// 对应属性:时段
        /// </summary>
        int time;

        /// <summary>
        /// 必要参数
        /// 可忽略的时段，有效数字为0-23
        /// 对应字段:time
        /// </summary>
        [XmlTextAttribute]
        public int 时段
        {

            get
            {
                return time;
            }

            set
            {
                if (时段更改事件 != null) 时段更改事件(this, time, value);
                time = value;
                if (时段已更改事件 != null) 时段已更改事件(this, time);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[忽略时段]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("时段(time:int): " + 时段);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// RSS图像显示
    /// 对应数据表:[image]
    /// 版本:1
    /// </summary>
    [XmlRoot("image")]
    public class 图像
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 图像()
        {
            width = 88;
            height = 31;
        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 描述更改委托
        /// </summary>
        public delegate void 描述更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 描述更改事件，在属性即将被更改时触发
        /// </summary>
        public event 描述更改 描述更改事件;
        /// <summary>
        /// 描述已更改委托
        /// </summary>
        public delegate void 描述已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 描述已更改事件，在属性被更改后触发
        /// </summary>
        public event 描述已更改 描述已更改事件;

        /// <summary>
        /// 图片描述信息
        /// 对应属性:描述
        /// 数据长度:1000
        /// </summary>
        string description;

        /// <summary>
        /// 图片描述信息
        /// 对应字段:description
        /// 数据长度:1000
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string 描述
        {

            get
            {
                return description;
            }

            set
            {
                if (描述更改事件 != null) 描述更改事件(this, description, value);
                description = value;
                if (描述已更改事件 != null) 描述已更改事件(this, description);
            }

        }

        /// <summary>
        /// 标题更改委托
        /// </summary>
        public delegate void 标题更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 标题更改事件，在属性即将被更改时触发
        /// </summary>
        public event 标题更改 标题更改事件;
        /// <summary>
        /// 标题已更改委托
        /// </summary>
        public delegate void 标题已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 标题已更改事件，在属性被更改后触发
        /// </summary>
        public event 标题已更改 标题已更改事件;

        /// <summary>
        /// 必要参数
        /// 定义当图片不能显示时，页面中所显示的替代文字
        /// 对应属性:标题
        /// 数据长度:300
        /// </summary>
        string title;

        /// <summary>
        /// 必要参数
        /// 定义当图片不能显示时，页面中所显示的替代文字
        /// 对应字段:title
        /// 数据长度:300
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string 标题
        {

            get
            {
                return title;
            }

            set
            {
                if (标题更改事件 != null) 标题更改事件(this, title, value);
                title = value;
                if (标题已更改事件 != null) 标题已更改事件(this, title);
            }

        }

        /// <summary>
        /// 地址更改委托
        /// </summary>
        public delegate void 地址更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 地址更改事件，在属性即将被更改时触发
        /// </summary>
        public event 地址更改 地址更改事件;
        /// <summary>
        /// 地址已更改委托
        /// </summary>
        public delegate void 地址已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 地址已更改事件，在属性被更改后触发
        /// </summary>
        public event 地址已更改 地址已更改事件;

        /// <summary>
        /// 必要参数
        /// 指定图片的URL地址
        /// 对应属性:地址
        /// 数据长度:300
        /// </summary>
        string url;

        /// <summary>
        /// 必要参数
        /// 指定图片的URL地址
        /// 对应字段:url
        /// 数据长度:300
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string 地址
        {

            get
            {
                return url;
            }

            set
            {
                if (地址更改事件 != null) 地址更改事件(this, url, value);
                url = value;
                if (地址已更改事件 != null) 地址已更改事件(this, url);
            }

        }

        /// <summary>
        /// 链接更改委托
        /// </summary>
        public delegate void 链接更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 链接更改事件，在属性即将被更改时触发
        /// </summary>
        public event 链接更改 链接更改事件;
        /// <summary>
        /// 链接已更改委托
        /// </summary>
        public delegate void 链接已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 链接已更改事件，在属性被更改后触发
        /// </summary>
        public event 链接已更改 链接已更改事件;

        /// <summary>
        /// 必要参数
        /// 定义展示频道的站点超链接
        /// 对应属性:链接
        /// 数据长度:500
        /// </summary>
        string link;

        /// <summary>
        /// 必要参数
        /// 定义展示频道的站点超链接
        /// 对应字段:link
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "link")]
        public string 链接
        {

            get
            {
                return link;
            }

            set
            {
                if (链接更改事件 != null) 链接更改事件(this, link, value);
                link = value;
                if (链接已更改事件 != null) 链接已更改事件(this, link);
            }

        }

        /// <summary>
        /// 高度更改委托
        /// </summary>
        public delegate void 高度更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 高度更改事件，在属性即将被更改时触发
        /// </summary>
        public event 高度更改 高度更改事件;
        /// <summary>
        /// 高度已更改委托
        /// </summary>
        public delegate void 高度已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 高度已更改事件，在属性被更改后触发
        /// </summary>
        public event 高度已更改 高度已更改事件;

        /// <summary>
        /// 图像高度，默认值31，最大值400
        /// 对应属性:高度
        /// </summary>
        int height;

        /// <summary>
        /// 图像高度，默认值31，最大值400
        /// 对应字段:height
        /// </summary>
        [XmlElement(ElementName = "height")]
        public int 高度
        {

            get
            {
                return height;
            }

            set
            {
                if (高度更改事件 != null) 高度更改事件(this, height, value);
                height = value;
                if (高度已更改事件 != null) 高度已更改事件(this, height);
            }

        }

        /// <summary>
        /// 宽度更改委托
        /// </summary>
        public delegate void 宽度更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 宽度更改事件，在属性即将被更改时触发
        /// </summary>
        public event 宽度更改 宽度更改事件;
        /// <summary>
        /// 宽度已更改委托
        /// </summary>
        public delegate void 宽度已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 宽度已更改事件，在属性被更改后触发
        /// </summary>
        public event 宽度已更改 宽度已更改事件;

        /// <summary>
        /// 图像宽度，默认值88，最大值144
        /// 对应属性:宽度
        /// </summary>
        int width;

        /// <summary>
        /// 图像宽度，默认值88，最大值144
        /// 对应字段:width
        /// </summary>
        [XmlElement(ElementName = "width")]
        public int 宽度
        {

            get
            {
                return width;
            }

            set
            {
                if (宽度更改事件 != null) 宽度更改事件(this, width, value);
                width = value;
                if (宽度已更改事件 != null) 宽度已更改事件(this, width);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[图像]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("描述(description:string): " + 描述);
            S.AppendLine("标题(title:string): " + 标题);
            S.AppendLine("地址(url:string): " + 地址);
            S.AppendLine("链接(link:string): " + 链接);
            S.AppendLine("高度(height:int): " + 高度);
            S.AppendLine("宽度(width:int): " + 宽度);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// 频道项目内容
    /// 对应数据表:[item]
    /// 版本:1
    /// </summary>
    [XmlRoot("item")]
    public class 项目
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 项目()
        {
            设置发布时间(DateTime.Now);
        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 标题更改委托
        /// </summary>
        public delegate void 标题更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 标题更改事件，在属性即将被更改时触发
        /// </summary>
        public event 标题更改 标题更改事件;
        /// <summary>
        /// 标题已更改委托
        /// </summary>
        public delegate void 标题已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 标题已更改事件，在属性被更改后触发
        /// </summary>
        public event 标题已更改 标题已更改事件;

        /// <summary>
        /// 必要参数
        /// 项目名称
        /// 对应属性:标题
        /// 数据长度:200
        /// </summary>
        string title;

        /// <summary>
        /// 必要参数
        /// 项目名称
        /// 对应字段:title
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string 标题
        {

            get
            {
                return title;
            }

            set
            {
                if (标题更改事件 != null) 标题更改事件(this, title, value);
                title = value;
                if (标题已更改事件 != null) 标题已更改事件(this, title);
            }

        }

        /// <summary>
        /// 链接更改委托
        /// </summary>
        public delegate void 链接更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 链接更改事件，在属性即将被更改时触发
        /// </summary>
        public event 链接更改 链接更改事件;
        /// <summary>
        /// 链接已更改委托
        /// </summary>
        public delegate void 链接已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 链接已更改事件，在属性被更改后触发
        /// </summary>
        public event 链接已更改 链接已更改事件;

        /// <summary>
        /// 必要参数
        /// 项目原文链接地址
        /// 对应属性:链接
        /// 数据长度:500
        /// </summary>
        string link;

        /// <summary>
        /// 必要参数
        /// 项目原文链接地址
        /// 对应字段:link
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "link")]
        public string 链接
        {

            get
            {
                return link;
            }

            set
            {
                if (链接更改事件 != null) 链接更改事件(this, link, value);
                link = value;
                if (链接已更改事件 != null) 链接已更改事件(this, link);
            }

        }

        /// <summary>
        /// 描述更改委托
        /// </summary>
        public delegate void 描述更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 描述更改事件，在属性即将被更改时触发
        /// </summary>
        public event 描述更改 描述更改事件;
        /// <summary>
        /// 描述已更改委托
        /// </summary>
        public delegate void 描述已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 描述已更改事件，在属性被更改后触发
        /// </summary>
        public event 描述已更改 描述已更改事件;

        /// <summary>
        /// 必要参数
        /// 项目摘要
        /// 对应属性:描述
        /// 数据长度:60000
        /// </summary>
        string description;

        /// <summary>
        /// 必要参数
        /// 项目摘要
        /// 对应字段:description
        /// 数据长度:60000
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string 描述
        {

            get
            {
                return description;
            }

            set
            {
                if (描述更改事件 != null) 描述更改事件(this, description, value);
                description = value;
                if (描述已更改事件 != null) 描述已更改事件(this, description);
            }

        }

        /// <summary>
        /// 作者更改委托
        /// </summary>
        public delegate void 作者更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 作者更改事件，在属性即将被更改时触发
        /// </summary>
        public event 作者更改 作者更改事件;
        /// <summary>
        /// 作者已更改委托
        /// </summary>
        public delegate void 作者已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 作者已更改事件，在属性被更改后触发
        /// </summary>
        public event 作者已更改 作者已更改事件;

        /// <summary>
        /// 文档作者电子邮件地址
        /// 对应属性:作者
        /// 数据长度:100
        /// </summary>
        string author;

        /// <summary>
        /// 文档作者电子邮件地址
        /// 对应字段:author
        /// 数据长度:100
        /// </summary>
        [XmlElement(ElementName = "author")]
        public string 作者
        {

            get
            {
                return author;
            }

            set
            {
                if (作者更改事件 != null) 作者更改事件(this, author, value);
                author = value;
                if (作者已更改事件 != null) 作者已更改事件(this, author);
            }

        }

        /// <summary>
        /// 评论更改委托
        /// </summary>
        public delegate void 评论更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 评论更改事件，在属性即将被更改时触发
        /// </summary>
        public event 评论更改 评论更改事件;
        /// <summary>
        /// 评论已更改委托
        /// </summary>
        public delegate void 评论已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 评论已更改事件，在属性被更改后触发
        /// </summary>
        public event 评论已更改 评论已更改事件;

        /// <summary>
        /// 项目相关评论链接地址
        /// 对应属性:评论
        /// 数据长度:500
        /// </summary>
        string comments;

        /// <summary>
        /// 项目相关评论链接地址
        /// 对应字段:comments
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "comments")]
        public string 评论
        {

            get
            {
                return comments;
            }

            set
            {
                if (评论更改事件 != null) 评论更改事件(this, comments, value);
                comments = value;
                if (评论已更改事件 != null) 评论已更改事件(this, comments);
            }

        }

        /// <summary>
        /// 标识符更改委托
        /// </summary>
        public delegate void 标识符更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 标识符更改事件，在属性即将被更改时触发
        /// </summary>
        public event 标识符更改 标识符更改事件;
        /// <summary>
        /// 标识符已更改委托
        /// </summary>
        public delegate void 标识符已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 标识符已更改事件，在属性被更改后触发
        /// </summary>
        public event 标识符已更改 标识符已更改事件;

        /// <summary>
        /// 项目唯一标识符
        /// 对应属性:标识符
        /// 数据长度:500
        /// </summary>
        标识符 guid;

        /// <summary>
        /// 项目唯一标识符
        /// 对应字段:guid
        /// 数据长度:500
        /// </summary>
        [XmlElement(ElementName = "guid")]
        public 标识符 标识符
        {

            get
            {
                return guid;
            }

            set
            {
                if (标识符更改事件 != null) 标识符更改事件(this, guid, value);
                guid = value;
                if (标识符已更改事件 != null) 标识符已更改事件(this, guid);
            }

        }

        /// <summary>
        /// 来源更改委托
        /// </summary>
        public delegate void 来源更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 来源更改事件，在属性即将被更改时触发
        /// </summary>
        public event 来源更改 来源更改事件;
        /// <summary>
        /// 来源已更改委托
        /// </summary>
        public delegate void 来源已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 来源已更改事件，在属性被更改后触发
        /// </summary>
        public event 来源已更改 来源已更改事件;

        /// <summary>
        /// 项目内容来源或RSS信源
        /// 对应属性:来源
        /// </summary>
        来源 source;

        /// <summary>
        /// 项目内容来源或RSS信源
        /// 对应字段:source
        /// </summary>
        [XmlElement(ElementName = "source")]
        public 来源 来源
        {

            get
            {
                return source;
            }

            set
            {
                if (来源更改事件 != null) 来源更改事件(this, source, value);
                source = value;
                if (来源已更改事件 != null) 来源已更改事件(this, source);
            }

        }

        /// <summary>
        /// 媒体更改委托
        /// </summary>
        public delegate void 媒体更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 媒体更改事件，在属性即将被更改时触发
        /// </summary>
        public event 媒体更改 媒体更改事件;
        /// <summary>
        /// 媒体已更改委托
        /// </summary>
        public delegate void 媒体已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 媒体已更改事件，在属性被更改后触发
        /// </summary>
        public event 媒体已更改 媒体已更改事件;

        /// <summary>
        /// 项目附加多媒体信息
        /// 对应属性:媒体
        /// </summary>
        媒体 enclosure;

        /// <summary>
        /// 项目附加多媒体信息
        /// 对应字段:enclosure
        /// </summary>
        [XmlElement(ElementName = "enclosure")]
        public 媒体 媒体
        {

            get
            {
                return enclosure;
            }

            set
            {
                if (媒体更改事件 != null) 媒体更改事件(this, enclosure, value);
                enclosure = value;
                if (媒体已更改事件 != null) 媒体已更改事件(this, enclosure);
            }

        }

        /// <summary>
        /// 版块分类更改委托
        /// </summary>
        public delegate void 版块分类更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 版块分类更改事件，在属性即将被更改时触发
        /// </summary>
        public event 版块分类更改 版块分类更改事件;
        /// <summary>
        /// 版块分类已更改委托
        /// </summary>
        public delegate void 版块分类已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 版块分类已更改事件，在属性被更改后触发
        /// </summary>
        public event 版块分类已更改 版块分类已更改事件;

        /// <summary>
        /// 项目版块分类信息集合
        /// 对应属性:版块分类
        /// </summary>
        List<版块分类> category;

        /// <summary>
        /// 项目版块分类信息集合
        /// 对应字段:category
        /// </summary>
        [XmlElement(ElementName = "category")]
        public List<版块分类> 版块分类
        {

            get
            {
                return category;
            }

            set
            {
                if (版块分类更改事件 != null) 版块分类更改事件(this, category, value);
                category = value;
                if (版块分类已更改事件 != null) 版块分类已更改事件(this, category);
            }

        }

        /// <summary>
        /// 发布时间更改委托
        /// </summary>
        public delegate void 发布时间更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 发布时间更改事件，在属性即将被更改时触发
        /// </summary>
        public event 发布时间更改 发布时间更改事件;
        /// <summary>
        /// 发布时间已更改委托
        /// </summary>
        public delegate void 发布时间已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 发布时间已更改事件，在属性被更改后触发
        /// </summary>
        public event 发布时间已更改 发布时间已更改事件;

        /// <summary>
        /// 项目内容发布时间
        /// 对应属性:发布时间
        /// 数据长度:200
        /// </summary>
        string pubDate;

        /// <summary>
        /// 项目内容发布时间
        /// 对应字段:pubDate
        /// 数据长度:200
        /// </summary>
        [XmlElement(ElementName = "pubDate")]
        public string 发布时间
        {

            get
            {
                return pubDate;
            }

            set
            {
                if (发布时间更改事件 != null) 发布时间更改事件(this, pubDate, value);
                pubDate = value;
                if (发布时间已更改事件 != null) 发布时间已更改事件(this, pubDate);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[项目]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("标题(title:string): " + 标题);
            S.AppendLine("链接(link:string): " + 链接);
            S.AppendLine("描述(description:string): " + 描述);
            S.AppendLine("作者(author:string): " + 作者);
            S.AppendLine("评论(comments:string): " + 评论);
            S.AppendLine("标识符(guid:标识符): " + 标识符);
            S.AppendLine("来源(source:来源): " + 来源);
            S.AppendLine("媒体(enclosure:媒体): " + 媒体);
            S.AppendLine("版块分类(category:List<版块分类>): " + 版块分类);
            S.AppendLine("发布时间(pubDate:string): " + 发布时间);
            return S.ToString();

        }

        #endregion

        #region 自定义附加代码

        /// <summary>
        /// 设置项目发布时间
        /// </summary>
        /// <param name="日期时间">日期时间对象</param>
        public void 设置发布时间(DateTime 日期时间)
        {
            pubDate = 日期时间.ToString("R");
        }

        /// <summary>
        /// 获取项目发布时间
        /// </summary>
        /// <returns>日期时间对象</returns>
        public DateTime 获取发布时间()
        {
            return DateTime.Parse(pubDate).ToUniversalTime();
        }

        #endregion

    }

    /// <summary>
    /// 项目唯一标识符
    /// 对应数据表:[guid]
    /// 版本:1
    /// </summary>
    [XmlRoot("guid")]
    public class 标识符
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 标识符()
        {
            isPermaLink = true;
        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 内容更改委托
        /// </summary>
        public delegate void 内容更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 内容更改事件，在属性即将被更改时触发
        /// </summary>
        public event 内容更改 内容更改事件;
        /// <summary>
        /// 内容已更改委托
        /// </summary>
        public delegate void 内容已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 内容已更改事件，在属性被更改后触发
        /// </summary>
        public event 内容已更改 内容已更改事件;

        /// <summary>
        /// 标识符内容，通常为项目的唯一绝对链接
        /// 对应属性:内容
        /// 数据长度:500
        /// </summary>
        string Text;

        /// <summary>
        /// 标识符内容，通常为项目的唯一绝对链接
        /// 对应字段:Text
        /// 数据长度:500
        /// </summary>
        [XmlTextAttribute]
        public string 内容
        {

            get
            {
                return Text;
            }

            set
            {
                if (内容更改事件 != null) 内容更改事件(this, Text, value);
                Text = value;
                if (内容已更改事件 != null) 内容已更改事件(this, Text);
            }

        }

        /// <summary>
        /// 永久链接更改委托
        /// </summary>
        public delegate void 永久链接更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 永久链接更改事件，在属性即将被更改时触发
        /// </summary>
        public event 永久链接更改 永久链接更改事件;
        /// <summary>
        /// 永久链接已更改委托
        /// </summary>
        public delegate void 永久链接已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 永久链接已更改事件，在属性被更改后触发
        /// </summary>
        public event 永久链接已更改 永久链接已更改事件;

        /// <summary>
        /// 指示该唯一标识符是否是一个永久链接，默认为真
        /// 对应属性:永久链接
        /// </summary>
        bool isPermaLink;

        /// <summary>
        /// 指示该唯一标识符是否是一个永久链接，默认为真
        /// 对应字段:isPermaLink
        /// </summary>
        [XmlAttribute("isPermaLink")]
        public bool 永久链接
        {

            get
            {
                return isPermaLink;
            }

            set
            {
                if (永久链接更改事件 != null) 永久链接更改事件(this, isPermaLink, value);
                isPermaLink = value;
                if (永久链接已更改事件 != null) 永久链接已更改事件(this, isPermaLink);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[标识符]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("内容(Text:string): " + 内容);
            S.AppendLine("永久链接(isPermaLink:bool): " + 永久链接);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// 项目内容来源或RSS信源
    /// 对应数据表:[source]
    /// 版本:1
    /// </summary>
    [XmlRoot("source")]
    public class 来源
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 来源()
        {

        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 名称更改委托
        /// </summary>
        public delegate void 名称更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 名称更改事件，在属性即将被更改时触发
        /// </summary>
        public event 名称更改 名称更改事件;
        /// <summary>
        /// 名称已更改委托
        /// </summary>
        public delegate void 名称已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 名称已更改事件，在属性被更改后触发
        /// </summary>
        public event 名称已更改 名称已更改事件;

        /// <summary>
        /// 必要参数
        /// 项目内容来源或RSS信源名称
        /// 对应属性:名称
        /// 数据长度:300
        /// </summary>
        string Name;

        /// <summary>
        /// 必要参数
        /// 项目内容来源或RSS信源名称
        /// 对应字段:Name
        /// 数据长度:300
        /// </summary>
        [XmlTextAttribute]
        public string 名称
        {

            get
            {
                return Name;
            }

            set
            {
                if (名称更改事件 != null) 名称更改事件(this, Name, value);
                Name = value;
                if (名称已更改事件 != null) 名称已更改事件(this, Name);
            }

        }

        /// <summary>
        /// 网址更改委托
        /// </summary>
        public delegate void 网址更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 网址更改事件，在属性即将被更改时触发
        /// </summary>
        public event 网址更改 网址更改事件;
        /// <summary>
        /// 网址已更改委托
        /// </summary>
        public delegate void 网址已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 网址已更改事件，在属性被更改后触发
        /// </summary>
        public event 网址已更改 网址已更改事件;

        /// <summary>
        /// 必要参数
        /// 项目内容来源或RSS信源的网络地址
        /// 对应属性:网址
        /// 数据长度:500
        /// </summary>
        string url;

        /// <summary>
        /// 必要参数
        /// 项目内容来源或RSS信源的网络地址
        /// 对应字段:url
        /// 数据长度:500
        /// </summary>
        [XmlAttribute("url")]
        public string 网址
        {

            get
            {
                return url;
            }

            set
            {
                if (网址更改事件 != null) 网址更改事件(this, url, value);
                url = value;
                if (网址已更改事件 != null) 网址已更改事件(this, url);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[来源]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("名称(Name:string): " + 名称);
            S.AppendLine("网址(url:string): " + 网址);
            return S.ToString();

        }

        #endregion

    }

    /// <summary>
    /// 项目附加多媒体信息
    /// 对应数据表:[enclosure]
    /// 版本:1
    /// </summary>
    [XmlRoot("enclosure")]
    public class 媒体
    {

        #region 基础代码

        /// <summary>
        /// 构造函数
        /// </summary>
        public 媒体()
        {

        }

        /// <summary>
        /// 编号更改委托
        /// </summary>
        public delegate void 编号更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 编号更改事件，在属性即将被更改时触发
        /// </summary>
        public event 编号更改 编号更改事件;
        /// <summary>
        /// 编号已更改委托
        /// </summary>
        public delegate void 编号已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 编号已更改事件，在属性被更改后触发
        /// </summary>
        public event 编号已更改 编号已更改事件;

        /// <summary>
        /// 对象唯一索引编号
        /// 对应属性:编号
        /// </summary>
        int ID;

        /// <summary>
        /// [只读]
        /// 对象唯一索引编号
        /// 对应字段:ID
        /// </summary>
        public int 编号
        {

            get
            {
                return ID;
            }

        }

        /// <summary>
        /// 长度更改委托
        /// </summary>
        public delegate void 长度更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 长度更改事件，在属性即将被更改时触发
        /// </summary>
        public event 长度更改 长度更改事件;
        /// <summary>
        /// 长度已更改委托
        /// </summary>
        public delegate void 长度已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 长度已更改事件，在属性被更改后触发
        /// </summary>
        public event 长度已更改 长度已更改事件;

        /// <summary>
        /// 必要参数
        /// 媒体文件的长度，单位：字节
        /// 对应属性:长度
        /// </summary>
        int length;

        /// <summary>
        /// 必要参数
        /// 媒体文件的长度，单位：字节
        /// 对应字段:length
        /// </summary>
        [XmlAttribute("length")]
        public int 长度
        {

            get
            {
                return length;
            }

            set
            {
                if (长度更改事件 != null) 长度更改事件(this, length, value);
                length = value;
                if (长度已更改事件 != null) 长度已更改事件(this, length);
            }

        }

        /// <summary>
        /// 类型更改委托
        /// </summary>
        public delegate void 类型更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 类型更改事件，在属性即将被更改时触发
        /// </summary>
        public event 类型更改 类型更改事件;
        /// <summary>
        /// 类型已更改委托
        /// </summary>
        public delegate void 类型已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 类型已更改事件，在属性被更改后触发
        /// </summary>
        public event 类型已更改 类型已更改事件;

        /// <summary>
        /// 必要参数
        /// 媒体文件类型，如“video/wmv”、“audio/mpeg”
        /// 对应属性:类型
        /// 数据长度:100
        /// </summary>
        string type;

        /// <summary>
        /// 必要参数
        /// 媒体文件类型，如“video/wmv”、“audio/mpeg”
        /// 对应字段:type
        /// 数据长度:100
        /// </summary>
        [XmlAttribute("type")]
        public string 类型
        {

            get
            {
                return type;
            }

            set
            {
                if (类型更改事件 != null) 类型更改事件(this, type, value);
                type = value;
                if (类型已更改事件 != null) 类型已更改事件(this, type);
            }

        }

        /// <summary>
        /// 地址更改委托
        /// </summary>
        public delegate void 地址更改(object 触发对象, object 当前值, object 变更值);
        /// <summary>
        /// 地址更改事件，在属性即将被更改时触发
        /// </summary>
        public event 地址更改 地址更改事件;
        /// <summary>
        /// 地址已更改委托
        /// </summary>
        public delegate void 地址已更改(object 触发对象, object 变更后值);
        /// <summary>
        /// 地址已更改事件，在属性被更改后触发
        /// </summary>
        public event 地址已更改 地址已更改事件;

        /// <summary>
        /// 必要参数
        /// 媒体文件的URL地址
        /// 对应属性:地址
        /// 数据长度:500
        /// </summary>
        string url;

        /// <summary>
        /// 必要参数
        /// 媒体文件的URL地址
        /// 对应字段:url
        /// 数据长度:500
        /// </summary>
        [XmlAttribute("url")]
        public string 地址
        {

            get
            {
                return url;
            }

            set
            {
                if (地址更改事件 != null) 地址更改事件(this, url, value);
                url = value;
                if (地址已更改事件 != null) 地址已更改事件(this, url);
            }

        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public override string ToString()
        {
            StringBuilder S = new StringBuilder();
            S.AppendLine("[媒体]对象");
            S.AppendLine("编号(ID:int): " + 编号);
            S.AppendLine("长度(length:int): " + 长度);
            S.AppendLine("类型(type:string): " + 类型);
            S.AppendLine("地址(url:string): " + 地址);
            return S.ToString();

        }

        #endregion

    }

    #endregion

}
