using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Core.Net
{
    public enum 电子邮件处理状态
    {
        等待中, 发送中, 发送成功, 发送失败, 取消发送
    }

    public class 电子邮件
    {
        #region 私有变量

        SmtpClient Client;

        MailMessage Mail;

        NetworkCredential Info;

        /// <summary>
        /// 邮件开始发送的时间取样
        /// </summary>
        DateTime BeginSend;

        /// <summary>
        /// 邮件发送结束的时间取样
        /// </summary>
        DateTime FinishiSend;

        #endregion

        #region 属性

        /// <summary>
        /// 当邮件处于“发送中”状态以外时，使用此方法重置其状态
        /// </summary>
        public void 重置发送状态()
        {
            if (状态 == 电子邮件处理状态.发送中) throw new Exception("发送时不能重置邮件状态！");
            _状态 = 电子邮件处理状态.等待中;
            BeginSend = FinishiSend = DateTime.MinValue;
        }

        /// <summary>
        /// 在邮件发送完毕后，显示发送邮件所消耗的时间，单位为秒
        /// </summary>
        public int 发送耗时
        {
            get
            {
                if (状态 == 电子邮件处理状态.等待中 || 状态 == 电子邮件处理状态.发送中) return -1;
                else return (int)FinishiSend.Subtract(BeginSend).TotalSeconds;
            }
        }

        /// <summary>
        /// [只读] 当前邮件所处的状态枚举
        /// </summary>
        public 电子邮件处理状态 状态
        {
            get
            {
                return _状态;
            }
        }
        private 电子邮件处理状态 _状态;

        /// <summary>
        /// 邮件主体
        /// </summary>
        public MailMessage 邮件主体
        {
            get { return Mail; }
            set { Mail = value; }
        }

        public MailAddressCollection 密送
        {
            get { return Mail.Bcc; }
        }

        public MailAddressCollection 抄送
        {
            get { return Mail.CC; }
        }

        public MailAddressCollection 收件人
        {
            get { return Mail.To; }
        }

        public MailAddress 发件人
        {
            get { return Mail.From; }
            set { Mail.From = value; }
        }

        public string 标题
        {
            get { return Mail.Subject; }
            set { Mail.Subject = value; }
        }

        public string 正文
        {
            get { return Mail.Body; }
            set { Mail.Body = value; }
        }

        public Encoding 标题编码
        {
            get { return Mail.SubjectEncoding; }
            set { Mail.SubjectEncoding = value; }
        }

        public Encoding 正文编码
        {
            get { return Mail.BodyEncoding; }
            set { Mail.BodyEncoding = value; }
        }

        public bool 正文是否为HTML格式
        {
            get { return Mail.IsBodyHtml; }
            set { Mail.IsBodyHtml = value; }
        }

        public AttachmentCollection 附件
        {
            get { return Mail.Attachments; }
        }

        public MailPriority 优先级
        {
            get { return Mail.Priority; }
            set { Mail.Priority = value; }
        }

        public DeliveryNotificationOptions 通知
        {
            get { return Mail.DeliveryNotificationOptions; }
            set { Mail.DeliveryNotificationOptions = value; }
        }

        /// <summary>
        /// SMTP客户端
        /// </summary>
        public SmtpClient 邮件客户端
        {
            get { return Client; }
            set { Client = value; }
        }

        /// <summary>
        /// 同步发送邮件时的超时时间，单位为毫秒，默认为60000毫秒，即一分钟
        /// </summary>
        public int 超时时间
        {
            get { return Client.Timeout; }
            set { Client.Timeout = value; }
        }

        /// <summary>
        /// SMTP服务器地址，如“smtp.126.com”
        /// </summary>
        public string 邮件发送服务器地址
        {
            get { return Client.Host; }
            set { Client.Host = value; }
        }

        /// <summary>
        /// 默认为25
        /// </summary>
        public int 邮件发送服务器端口
        {
            get { return Client.Port; }
            set { Client.Port = value; }
        }

        public string 邮件发送服务器登录名
        {
            get { return Info.UserName; }
            set { Info.UserName = value; }
        }

        public string 邮件发送服务器登录密码
        {
            get { return Info.Password; }
            set { Info.Password = value; }
        }

        public bool 启用SSL加密连接
        {
            get { return Client.EnableSsl; }
            set { Client.EnableSsl = value; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public 电子邮件()
        {
            Info = new NetworkCredential();
            邮件客户端 = new SmtpClient();
            邮件客户端.UseDefaultCredentials = false;
            邮件客户端.Credentials = Info;
            //启用SSL加密连接 = true;
            邮件主体 = new MailMessage();
            超时时间 = 60000;
            邮件发送服务器端口 = 25;
            正文是否为HTML格式 = true;
            _状态 = 电子邮件处理状态.等待中;
            正文编码 = 标题编码 = Encoding.Default;
            邮件客户端.SendCompleted += new SendCompletedEventHandler(邮件客户端_SendCompleted);
        }

        void 邮件客户端_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            FinishiSend = DateTime.Now;
            if (e.Cancelled) _状态 = 电子邮件处理状态.取消发送;
            else _状态 = e.Error == null ? 电子邮件处理状态.发送成功 : 电子邮件处理状态.发送失败;
        }

        /// <summary>
        /// 简易配置以建立一个纯文本邮件主体
        /// </summary>
        /// <param name="发件人地址">发件人地址</param>
        /// <param name="收件人地址">收件人地址</param>
        /// <param name="标题">邮件标题</param>
        /// <param name="正文">邮件正文</param>
        public 电子邮件(string 发件人地址, string 收件人地址, string 标题, string 正文)
            : this()
        {
            this.发件人 = new MailAddress(发件人地址);
            this.收件人.Add(new MailAddress(收件人地址));
            this.标题 = 标题;
            this.正文 = 正文;
            this.正文是否为HTML格式 = false;
        }

        #endregion

        public void 发送邮件()
        {
            BeginSend = DateTime.Now;
            _状态 = 电子邮件处理状态.发送中;
            邮件客户端.Send(邮件主体);
            _状态 = 电子邮件处理状态.发送成功;
            FinishiSend = DateTime.Now;
        }

        public void 异步发送邮件()
        {
            异步发送邮件(this);
        }

        /// <summary>
        /// 在后台发送邮件
        /// </summary>
        /// <param name="传递对象">异步发送完毕后传递给事件处理程序的自定义对象，在处理事件时可以通过e.UserState来访问该对象</param>
        public void 异步发送邮件(object 传递对象)
        {
            BeginSend = DateTime.Now;
            _状态 = 电子邮件处理状态.发送中;
            邮件客户端.SendAsync(邮件主体, 传递对象);
        }

        public void 取消异步发送()
        {
            邮件客户端.SendAsyncCancel();
        }

    }

}
