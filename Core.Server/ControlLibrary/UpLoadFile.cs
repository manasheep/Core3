using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.WebSite;
using Core.IO;
using Core.Text;
using System.Drawing;

namespace Core.ControlLibrary
{
    [DefaultProperty("同时最大上传文件数")]
    [ToolboxData("<{0}:UpLoadFile runat=server></{0}:UpLoadFile>")]
    [ToolboxBitmap(typeof(UpLoadFile), "Core.ControlLibrary.UpLoadFileResources.UpLoadFile.bmp")]
    public class UpLoadFile : WebControl, INamingContainer
    {
        FileUpload 文件上传0;
        FileUpload 文件上传1;
        FileUpload 文件上传2;
        FileUpload 文件上传3;
        FileUpload 文件上传4;
        FileUpload 文件上传5;
        FileUpload 文件上传6;
        FileUpload 文件上传7;
        FileUpload 文件上传8;
        FileUpload 文件上传9;
        Button 确定按钮;
        CustomValidator 验证0;
        CustomValidator 验证1;
        CustomValidator 验证2;
        CustomValidator 验证3;
        CustomValidator 验证4;
        CustomValidator 验证5;
        CustomValidator 验证6;
        CustomValidator 验证7;
        CustomValidator 验证8;
        CustomValidator 验证9;


        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            文件上传0 = new FileUpload();
            文件上传0.ID = "文件上传0";
            Controls.Add(文件上传0);

            文件上传1 = new FileUpload();
            文件上传1.ID = "文件上传1";
            Controls.Add(文件上传1);

            文件上传2 = new FileUpload();
            文件上传2.ID = "文件上传2";
            Controls.Add(文件上传2);

            文件上传3 = new FileUpload();
            文件上传3.ID = "文件上传3";
            Controls.Add(文件上传3);

            文件上传4 = new FileUpload();
            文件上传4.ID = "文件上传4";
            Controls.Add(文件上传4);

            文件上传5 = new FileUpload();
            文件上传5.ID = "文件上传5";
            Controls.Add(文件上传5);

            文件上传6 = new FileUpload();
            文件上传6.ID = "文件上传6";
            Controls.Add(文件上传6);

            文件上传7 = new FileUpload();
            文件上传7.ID = "文件上传7";
            Controls.Add(文件上传7);

            文件上传8 = new FileUpload();
            文件上传8.ID = "文件上传8";
            Controls.Add(文件上传8);

            文件上传9 = new FileUpload();
            文件上传9.ID = "文件上传9";
            Controls.Add(文件上传9);

            确定按钮 = new Button();
            确定按钮.ID = "确定按钮";
            确定按钮.Text = "开始上传";
            确定按钮.Click -= new EventHandler(确定按钮_Click);
            确定按钮.Click += new EventHandler(确定按钮_Click);
            Controls.Add(确定按钮);

            验证0 = new CustomValidator();
            验证0.ID = "验证0";
            验证0.Display = ValidatorDisplay.Dynamic;
            验证0.ControlToValidate = "文件上传0";
            验证0.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证0.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证0.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证0);

            验证1 = new CustomValidator();
            验证1.ID = "验证1";
            验证1.Display = ValidatorDisplay.Dynamic;
            验证1.ControlToValidate = "文件上传1";
            验证1.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证1.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证1.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证1);

            验证2 = new CustomValidator();
            验证2.ID = "验证2";
            验证2.Display = ValidatorDisplay.Dynamic;
            验证2.ControlToValidate = "文件上传2";
            验证2.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证2.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证2.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证2);

            验证3 = new CustomValidator();
            验证3.ID = "验证3";
            验证3.Display = ValidatorDisplay.Dynamic;
            验证3.ControlToValidate = "文件上传3";
            验证3.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证3.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证3.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证3);

            验证4 = new CustomValidator();
            验证4.ID = "验证4";
            验证4.Display = ValidatorDisplay.Dynamic;
            验证4.ControlToValidate = "文件上传4";
            验证4.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证4.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证4.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证4);

            验证5 = new CustomValidator();
            验证5.ID = "验证5";
            验证5.Display = ValidatorDisplay.Dynamic;
            验证5.ControlToValidate = "文件上传5";
            验证5.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证5.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证5.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证5);

            验证6 = new CustomValidator();
            验证6.ID = "验证6";
            验证6.Display = ValidatorDisplay.Dynamic;
            验证6.ControlToValidate = "文件上传6";
            验证6.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证6.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证6.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证6);

            验证7 = new CustomValidator();
            验证7.ID = "验证7";
            验证7.Display = ValidatorDisplay.Dynamic;
            验证7.ControlToValidate = "文件上传7";
            验证7.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证7.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证7.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证7);

            验证8 = new CustomValidator();
            验证8.ID = "验证8";
            验证8.Display = ValidatorDisplay.Dynamic;
            验证8.ControlToValidate = "文件上传8";
            验证8.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证8.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证8.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证8);

            验证9 = new CustomValidator();
            验证9.ID = "验证9";
            验证9.Display = ValidatorDisplay.Dynamic;
            验证9.ControlToValidate = "文件上传9";
            验证9.ValidationGroup = "Core.ControlLibrary.UpLoadFile";
            验证9.ServerValidate -= new ServerValidateEventHandler(验证_ServerValidate);
            验证9.ServerValidate += new ServerValidateEventHandler(验证_ServerValidate);
            Controls.Add(验证9);

        }

        void 验证_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var sender = source as CustomValidator;
            sender.ErrorMessage = sender.Text = null;
            var f = 验证控件映射字典[sender];

            if (!上传文件类型白名单.IsNullOrEmpty() && !上传文件类型黑名单.IsNullOrEmpty()) throw new Exception("不能同时设置上传类型白名单和黑名单！");
            if (!上传文件类型白名单.IsNullOrEmpty())
            {
                if (f.PostedFile.ContentType == null)
                {
                    sender.ErrorMessage = "无法获取文件“{0}”的MIME类型".FormatWith(f.PostedFile.FileName);
                    args.IsValid = false;
                    return;
                }
                var l = 上传文件类型白名单.切分字符串(",");
                if (!f.PostedFile.ContentType.ToLower().IsIn(l))
                {
                    sender.ErrorMessage = "文件“{0}”的MIME类型为{1}，不在允许上传的类型范围之内".FormatWith(f.PostedFile.FileName, f.PostedFile.ContentType);
                    args.IsValid = false;
                    return;
                }
            }
            if (!上传文件类型黑名单.IsNullOrEmpty())
            {
                if (f.PostedFile.ContentType != null)
                {
                    var l = 上传文件类型黑名单.切分字符串(",");
                    if (f.PostedFile.ContentType.ToLower().IsIn(l))
                    {
                        sender.ErrorMessage = "文件“{0}”的MIME类型为{1}，属于禁止上传的类型范围之内".FormatWith(f.PostedFile.FileName, f.PostedFile.ContentType);
                        args.IsValid = false;
                        return;
                    }
                }
            }

            if (f.PostedFile.ContentLength > 限制上传文件大小 * (int)存储单位.KB)
            {
                sender.ErrorMessage = "文件“{0}”的尺寸为{1}KB，超过限定的{2}KB".FormatWith(f.PostedFile.FileName, f.PostedFile.ContentLength / (int)存储单位.KB, 限制上传文件大小);
                args.IsValid = false;
                return;
            }

            触发验证事件(f.PostedFile, sender, args);
            if (!args.IsValid)
            {
                if (sender.ErrorMessage.IsNullOrEmpty()) throw new Exception("自定义验证未通过，但未在验证控件中指明具体错误信息！");
            }
        }

        void 确定按钮_Click(object sender, EventArgs e)
        {
            foreach (var f in 上传文件控件数组)
            {
                if (f.PostedFile.FileName.IsNullOrEmpty()) continue;
                var v = 上传控件映射字典[f];
                v.Validate();
                if (v.IsValid)
                {
                    var d = 文件保存路径.AsServerPathString();
                    var n = 触发重命名事件(f.PostedFile, d);
                    var p = d.ToPhysicsAbsolutePath().AsPathString().Combine(n);
                    f.SaveAs(p);
                    触发文件上传完毕事件(f.PostedFile, n, p);
                    v.Text = "文件“{0}”上传成功！存储到：{1}".FormatWith(f.PostedFile.FileName,p);
                    v.IsValid = false;
                }
            }
        }

        public delegate void 验证(object sender, HttpPostedFile 传输文件,CustomValidator 验证控件, ServerValidateEventArgs 验证参数);
        /// <summary>
        /// 当文件上传前且通过了内置的一系列验证之后触发，不注册此事件将不对验证结果产生额外影响。应当在此事件中进行自定义验证，并修改验证参数的IsValid属性，如果验证失败，还应修改验证控件的ErrorMessage属性给用户提示，提示内容例如：“文件“flash.swf”已存在”。
        /// </summary>
        public event 验证 验证事件;
        protected virtual void 触发验证事件(HttpPostedFile 传输文件, CustomValidator 验证控件, ServerValidateEventArgs 验证参数)
        {
            if (验证事件 != null) 验证事件(this, 传输文件, 验证控件, 验证参数);
            else 验证参数.IsValid = true;
        }

        public delegate string 重命名(object sender, HttpPostedFile 传输文件, Server通用扩展.ServerPathString 存储目录路径);
        /// <summary>
        /// 当为上传文件命名时触发，如果不注册此事件并进行更名处理的话，文件将保持原名，这可能会导致覆盖掉原来已存在的文件，并可能产生严重的安全问题
        /// </summary>
        public event 重命名 重命名事件;
        protected virtual string 触发重命名事件(HttpPostedFile 传输文件, Server通用扩展.ServerPathString 存储目录路径)
        {
            if (重命名事件 != null) return 重命名事件(this, 传输文件, 存储目录路径);
            return 传输文件.FileName;
        }

        public delegate void 文件上传完毕(object sender, HttpPostedFile 传输文件, string 存储文件名, string 存储完整路径);
        /// <summary>
        /// 当文件上传完毕时触发此事件
        /// </summary>
        public event 文件上传完毕 文件上传完毕事件;
        protected virtual void 触发文件上传完毕事件(HttpPostedFile 传输文件, string 存储文件名, string 存储完整路径)
        {
            if (文件上传完毕事件 != null) 文件上传完毕事件(this, 传输文件, 存储文件名, 存储完整路径);
        }

        /// <summary>
        /// 获取验证控件与上传控件的映射字典
        /// </summary>
        private Dictionary<CustomValidator, FileUpload> 验证控件映射字典
        {
            get
            {
                EnsureChildControls();
                var d = new Dictionary<CustomValidator, FileUpload>(同时最大上传文件数);
                d.Add(验证0, 文件上传0);
                d.Add(验证1, 文件上传1);
                d.Add(验证2, 文件上传2);
                d.Add(验证3, 文件上传3);
                d.Add(验证4, 文件上传4);
                d.Add(验证5, 文件上传5);
                d.Add(验证6, 文件上传6);
                d.Add(验证7, 文件上传7);
                d.Add(验证8, 文件上传8);
                d.Add(验证9, 文件上传9);
                return d;
            }
        }

        /// <summary>
        /// 获取验证控件与上传控件的映射字典
        /// </summary>
        private Dictionary<FileUpload, CustomValidator> 上传控件映射字典
        {
            get
            {
                EnsureChildControls();
                var d = new Dictionary<FileUpload, CustomValidator>(同时最大上传文件数);
                d.Add(文件上传0, 验证0);
                d.Add(文件上传1, 验证1);
                d.Add(文件上传2, 验证2);
                d.Add(文件上传3, 验证3);
                d.Add(文件上传4, 验证4);
                d.Add(文件上传5, 验证5);
                d.Add(文件上传6, 验证6);
                d.Add(文件上传7, 验证7);
                d.Add(文件上传8, 验证8);
                d.Add(文件上传9, 验证9);
                return d;
            }
        }

        /// <summary>
        /// 获取上传文件控件数组，数组长度取决于“同时最大上传文件数”属性
        /// </summary>
        [Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        public FileUpload[] 上传文件控件数组
        {
            get
            {
                EnsureChildControls();
                FileUpload[] a = new FileUpload[同时最大上传文件数];
                if (a.Length > 0) a[0] = 文件上传0;
                if (a.Length > 1) a[1] = 文件上传1;
                if (a.Length > 2) a[2] = 文件上传2;
                if (a.Length > 3) a[3] = 文件上传3;
                if (a.Length > 4) a[4] = 文件上传4;
                if (a.Length > 5) a[5] = 文件上传5;
                if (a.Length > 6) a[6] = 文件上传6;
                if (a.Length > 7) a[7] = 文件上传7;
                if (a.Length > 8) a[8] = 文件上传8;
                if (a.Length > 9) a[9] = 文件上传9;
                return a;
            }
        }

        /// <summary>
        /// 获取验证控件数组，数组长度取决于“同时最大上传文件数”属性
        /// </summary>
        private CustomValidator[] 验证控件数组
        {
            get
            {
                EnsureChildControls();
                CustomValidator[] a = new CustomValidator[同时最大上传文件数];
                if (a.Length > 0) a[0] = 验证0;
                if (a.Length > 1) a[1] = 验证1;
                if (a.Length > 2) a[2] = 验证2;
                if (a.Length > 3) a[3] = 验证3;
                if (a.Length > 4) a[4] = 验证4;
                if (a.Length > 5) a[5] = 验证5;
                if (a.Length > 6) a[6] = 验证6;
                if (a.Length > 7) a[7] = 验证7;
                if (a.Length > 8) a[8] = 验证8;
                if (a.Length > 9) a[9] = 验证9;
                return a;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            var f = 上传文件控件数组;
            var v = 验证控件数组;
            for (int i = 0; i < f.Length; i++)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                {
                    output.写入控件(f[i], v[i]);
                }
                output.RenderEndTag();
            }
            output.RenderBeginTag(HtmlTextWriterTag.P);
            {
                output.写入控件(确定按钮);
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-上传文件类型白名单"]，默认值为""
        /// </summary>
        [DefaultValue("")]
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("表示仅允许上传的文件MIME类型，不能与黑名单同时设置，多个类型之间以半角逗号分隔，如：image/bmp,image/gif,image/jpeg,image/pjpeg,image/png,image/x-png")]		//此属性在属性面板中的说明
        public string 上传文件类型白名单
        {
            get
            {
                EnsureChildControls();
                string s = (string)ViewState["Property-上传文件类型白名单"];
                return ((s == null) ? "" : s.ToLower());
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-上传文件类型白名单"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-上传文件类型黑名单"]，默认值为""
        /// </summary>
        [DefaultValue("")]
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("表示仅拒绝上传的文件MIME类型，不能与白名单同时设置，多个类型之间以半角逗号分隔，如：image/bmp,image/gif,image/jpeg,image/pjpeg,image/png,image/x-png")]		//此属性在属性面板中的说明
        public string 上传文件类型黑名单
        {
            get
            {
                EnsureChildControls();
                string s = (string)ViewState["Property-上传文件类型黑名单"];
                return ((s == null) ? "" : s.ToLower());
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-上传文件类型黑名单"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-限制上传文件大小"]，默认值为4096
        /// </summary>
        [DefaultValue(4096)]
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("单个上传文件的尺寸上限，单位为KB。Asp.Net默认允许总计4M的上传，超过则出错，可以在Web.Config中结合location节点为特定页面提高限制值")]		//此属性在属性面板中的说明
        public int 限制上传文件大小
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-限制上传文件大小"];
                return ((s == null) ? 4096 : (int)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-限制上传文件大小"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-文件保存路径"]，默认值为""
        /// </summary>
        [DefaultValue("")]
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("指示上传文件的保存路径")]		//此属性在属性面板中的说明
        public string 文件保存路径
        {
            get
            {
                EnsureChildControls();
                string s = (string)ViewState["Property-文件保存路径"];
                return ((s == null) ? "" : s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-文件保存路径"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-同时最大上传文件数"]，默认值为1
        /// </summary>
        [DefaultValue(1)]
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("指示显示多少组上传控件，最大支持10个")]		//此属性在属性面板中的说明
        public int 同时最大上传文件数
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-同时最大上传文件数"];
                return ((s == null) ? 1 : (int)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-同时最大上传文件数"] = value;
            }
        }
    }
}
