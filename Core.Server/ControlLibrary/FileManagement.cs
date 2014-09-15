using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Core.WebSite;
using Core.IO;
using System.Threading;

namespace Core.ControlLibrary
{
    [DefaultProperty("管理目录路径")]	//对于控件类，表示设计时选定控件时属性面板中突出显示的属性，此外验证控件的目标为此控件时，也会将此属性作为验证目标属性
    [ToolboxData("<{0}:FileManagement runat=server></{0}:FileManagement>")]
    [ToolboxBitmap(typeof(FileManagement), "Core.ControlLibrary.FileManagementResources.FolderIcon.gif")]
    public class FileManagement : WebControl, INamingContainer
    {
        TreeView 树状视图;
        Button 删除按钮;
        Button 复制按钮;
        Button 移动按钮;
        Label 重命名标签;
        TextBox 重命名文本框;
        Button 重命名按钮;
        Label 新建目录标签;
        TextBox 新建目录文本框;
        Button 新建目录按钮;
        BulletedList 消息记录列表;

        /// <summary>
        /// 指示此控件的外围标签
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("CheckChangedPostback", Page.ClientScript.GetWebResourceUrl(this.GetType(), "Core.ClientScript.CheckChangedPostback.js"));
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!Page.IsPostBack)
            {
                刷新树状视图();
                刷新显示状态();
            }
        }

        public void 重载()
        {
            刷新树状视图();
            刷新显示状态();
        }

        /// <summary>
        /// 仅当文件已经添加后，使用此方法更新视图状态
        /// </summary>
        /// <param name="文件名">新文件名</param>
        public void 添加文件到当前选定目录(string 文件名, bool 是否为目录)
        {
            var p = 当前选定目录.AsPathString().Combine(文件名);
            if (获取当前选定项目录节点().ChildNodes.Cast<TreeNode>().Any(q => q.Value.ToLower() == p.ToLower())) return;
            var n = 是否为目录 ? 创建目录节点(new DirectoryInfo(p)) : 创建文件节点(new FileInfo(p));
            获取当前选定项目录节点().ChildNodes.Add(n);
        }

        private void 刷新树状视图()
        {
            刷新树状视图(null, null, true);
        }

        private void 刷新树状视图(TreeNode 刷新起始节点, DirectoryInfo 刷新目录, bool 清除起始节点子项)
        {
            if (刷新起始节点 == null)
            {
                if (管理目录路径.IsNullOrEmpty())
                {
                    throw new Exception("未指定属性“管理目录路径”");
                }
                if (清除起始节点子项)
                {
                    树状视图.Nodes.Clear();
                    写入消息("扫描并加载文件系统结构");
                }
                添加文件系统数据(new DirectoryInfo(管理目录路径.AsServerPathString().ToPhysicsAbsolutePath()), 树状视图, null);
            }
            else
            {
                if (清除起始节点子项) 刷新起始节点.ChildNodes.Clear();
                添加文件系统数据(刷新目录, 树状视图, 刷新起始节点);
            }
        }

        void 树状视图_SelectedNodeChanged(object sender, EventArgs e)
        {
            刷新显示状态();
            触发选定节点变更事件();
        }

        void 树状视图_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Checked)
            {
                foreach (var f in e.Node.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()))
                {
                    f.Checked = true;
                }
            }
            else
            {
                foreach (var f in e.Node.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()))
                {
                    f.Checked = false;
                }
                foreach (var f in e.Node.RecursionSelect(q => q.Parent, q => q != null))
                {
                    f.Checked = false;
                }
            }
            刷新显示状态();
            触发勾选节点变更事件();
        }

        protected override void LoadViewState(object savedState)
        {
            刷新树状视图();
            刷新显示状态();
            base.LoadViewState(savedState);
        }

        private void 刷新显示状态()
        {
            var c = 树状视图.CheckedNodes.Count;
            if (c > 0)
            {
                删除按钮.Text = "删除勾选的{0}项".FormatWith(c);
                复制按钮.Enabled = 移动按钮.Enabled = 删除按钮.Enabled = true;
            }
            else
            {
                删除按钮.Text = "删除勾选项";
                复制按钮.Enabled = 移动按钮.Enabled = 删除按钮.Enabled = false;
            }

            if (树状视图.SelectedNode == null)
            {
                树状视图.Nodes[0].Selected = true;
                触发选定节点变更事件();
            }

            重命名标签.Text = "重命名“{0}”为：".FormatWith(树状视图.SelectedNode.Text);
            重命名文本框.Text = 树状视图.SelectedNode.Text;
            重命名文本框.Enabled = 重命名按钮.Enabled = 树状视图.SelectedNode.Parent != null;

            var p = 获取当前选定项目录节点();
            复制按钮.Text = "复制勾选项到：" + p.获取文字路径();
            移动按钮.Text = "移动勾选项到：" + p.获取文字路径();
            新建目录标签.Text = "在“{0}”中创建新目录：".FormatWith(p.获取文字路径());
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            树状视图 = new TreeView();
            树状视图.ID = "树状视图";
            树状视图.ExpandDepth = 1;
            树状视图.EnableViewState = true;
            树状视图.SelectedNodeStyle.ForeColor = Color.OrangeRed;
            树状视图.NodeStyle.ForeColor = Color.DarkSlateGray;
            树状视图.SelectedNodeStyle.Font.Bold = true;
            树状视图.ShowCheckBoxes = TreeNodeTypes.Leaf | TreeNodeTypes.Parent;
            树状视图.TreeNodeCheckChanged -= new TreeNodeEventHandler(树状视图_TreeNodeCheckChanged);
            树状视图.SelectedNodeChanged -= new EventHandler(树状视图_SelectedNodeChanged);
            树状视图.TreeNodeCheckChanged += new TreeNodeEventHandler(树状视图_TreeNodeCheckChanged);
            树状视图.SelectedNodeChanged += new EventHandler(树状视图_SelectedNodeChanged);
            树状视图.Attributes.Add("onclick", "postBackByObject()");
            this.Controls.Add(树状视图);

            删除按钮 = new Button();
            删除按钮.ID = "删除按钮";
            删除按钮.Text = "删除";
            删除按钮.Click -= new EventHandler(删除按钮_Click);
            删除按钮.Click += new EventHandler(删除按钮_Click);
            删除按钮.添加确认对话框("确定要删除所有勾选项吗？");
            this.Controls.Add(删除按钮);

            复制按钮 = new Button();
            复制按钮.ID = "复制按钮";
            复制按钮.Text = "复制";
            复制按钮.Click -= new EventHandler(复制按钮_Click);
            复制按钮.Click += new EventHandler(复制按钮_Click);
            复制按钮.添加确认对话框("确定要复制所有勾选项到选定目录吗？（同路径文件将被覆盖）");
            this.Controls.Add(复制按钮);

            移动按钮 = new Button();
            移动按钮.ID = "移动按钮";
            移动按钮.Text = "移动";
            移动按钮.Click -= new EventHandler(移动按钮_Click);
            移动按钮.Click += new EventHandler(移动按钮_Click);
            移动按钮.添加确认对话框("确定要移动所有勾选项到选定目录吗？（同路径文件将被覆盖）");
            this.Controls.Add(移动按钮);

            重命名标签 = new Label();
            重命名标签.ID = "重命名标签";
            重命名标签.Text = "重命名：";
            this.Controls.Add(重命名标签);

            重命名文本框 = new TextBox();
            重命名文本框.ID = "重命名文本框";
            this.Controls.Add(重命名文本框);

            重命名按钮 = new Button();
            重命名按钮.ID = "重命名按钮";
            重命名按钮.Text = "确定";
            重命名按钮.Click -= new EventHandler(重命名按钮_Click);
            重命名按钮.Click += new EventHandler(重命名按钮_Click);
            this.Controls.Add(重命名按钮);

            新建目录标签 = new Label();
            新建目录标签.ID = "新建目录标签";
            新建目录标签.Text = "新建目录：";
            this.Controls.Add(新建目录标签);

            新建目录文本框 = new TextBox();
            新建目录文本框.ID = "新建目录文本框";
            新建目录文本框.Text = "新建文件夹";
            this.Controls.Add(新建目录文本框);

            新建目录按钮 = new Button();
            新建目录按钮.ID = "新建目录按钮";
            新建目录按钮.Text = "确定";
            新建目录按钮.Click -= new EventHandler(新建目录按钮_Click);
            新建目录按钮.Click += new EventHandler(新建目录按钮_Click);
            this.Controls.Add(新建目录按钮);

            消息记录列表 = new BulletedList();
            消息记录列表.ID = "消息记录列表";
            this.Controls.Add(消息记录列表);
        }

        void 新建目录按钮_Click(object sender, EventArgs e)
        {
            try
            {
                var n = 获取当前选定项目录节点();
                var path = n.Value.AsPathString().Combine(新建目录文本框.Text);
                if (Directory.Exists(path) || File.Exists(path)) throw new Exception("目录或文件已存在，无法创建目录！");
                Directory.CreateDirectory(path);
                n.ChildNodes.Add(创建目录节点(new DirectoryInfo(path)));
                Page.显示对话框("创建目录成功！");
                写入消息("成功创建目录“{0}”".FormatWith(path));
                刷新显示状态();
                触发文件创建事件(path, null, true);
            }
            catch (Exception er)
            {
                er.Message.Trace();
                写入消息(er.Message);
            }
        }

        void 重命名按钮_Click(object sender, EventArgs e)
        {
            try
            {
                var s = 树状视图.SelectedNode;
                var path = s.Value.AsPathString().DirectoryName.AsPathString().Combine(重命名文本框.Text.Trim());
                if (s.ImageToolTip == "目录")
                {
                    Directory.Move(s.Value, path);
                }
                else
                {
                    File.Move(s.Value, path);
                }
                Page.显示对话框("更名成功！");
                写入消息("成功将“{0}”更名为“{1}”".FormatWith(s.Value, 重命名文本框.Text.Trim()));
                var p = s.Parent;
                p.ChildNodes.Remove(s);
                s.Text = 重命名文本框.Text.Trim();
                复制项到节点(p, s);
                var S = s.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()).ToList();
                var t = p.ChildNodes.Cast<TreeNode>().First(q => q.Value == path);
                var T = t.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()).ToList();
                触发文件转移事件(t.Value, s.Value, s.ImageToolTip == "目录");
                for (int i = 0; i < S.Count; i++)
                {
                    触发文件转移事件(T[i].Value, S[i].Value, T[i].ImageToolTip == "目录");
                }
                刷新显示状态();
            }
            catch (Exception er)
            {
                er.Message.Trace();
                写入消息(er.Message);
            }
        }

        void 移动按钮_Click(object sender, EventArgs e)
        {
            复制文件(true);
        }

        void 复制按钮_Click(object sender, EventArgs e)
        {
            复制文件(false);
        }

        private void 复制文件(bool 删除源文件)
        {
            var a = 树状视图.CheckedNodes.Count;
            var c = 0;
            var d = 树状视图.CheckedNodes.Cast<TreeNode>().OrderBy(q => q.Depth);
            var tlist = new List<DirectoryInfo>();
            var cancelPathList = new List<string>();
            foreach (var f in d)
            {
                var pn = f.RecursionSelect(q => q.Parent, q => q != null && q.Checked == true).OrderBy(q => q.Depth);
                var path = pn.Count() > 0 ? 当前选定目录.AsPathString().Combine(f.获取文字路径(pn.First().Parent, "\\")) : 当前选定目录.AsPathString().Combine(f.Text);
                try
                {
                    if (f.Parent.Value.IsIn(cancelPathList))
                    {
                        throw new Exception("受其父项影响，取消对“{0}”的操作".FormatWith(f.Value));
                    }
                    if (删除源文件)
                    {
                        if (f.Value == 当前选定目录 || f.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()).Any(q => q.Value == 当前选定目录))
                        {
                            throw new Exception("移动目标“{0}”是移动源“{1}”的本身或其子项，无法完成移动操作！移动源及其子项取消此次移动操作！".FormatWith(当前选定目录, f.Value));
                        }
                    }
                    if (f.ImageToolTip == "目录")
                    {
                        if (!Directory.Exists(f.Value)) goto OK;

                        if (删除源文件)
                        {
                            Directory.Move(f.Value, path);
                            f.Parent.ChildNodes.Remove(f);
                            复制项到节点(获取当前选定项目录节点(), f);
                        }
                        else
                        {
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            var n = new DirectoryInfo(path);
                            if (pn.Count() == 0)
                            {
                                tlist.Add(n);
                            }
                        }
                    }
                    else
                    {
                        if (!File.Exists(f.Value)) goto OK;

                        if (删除源文件)
                        {
                            File.Move(f.Value, path);
                            f.Parent.ChildNodes.Remove(f);
                            获取当前选定项目录节点().ChildNodes.Add(创建文件节点(new FileInfo(path)));
                        }
                        else
                        {
                            File.Copy(f.Value, path, true);
                            var n = 创建文件节点(new FileInfo(path));
                            if (d.Any(q => q.Value == n.Value))
                            {
                                goto OK;
                            }
                            if (pn.Count() == 0)
                            {
                                获取当前选定项目录节点().ChildNodes.Add(n);
                            }
                        }
                    }
                OK:
                    c++;
                    写入消息("成功{2}“{0}”到“{1}”".FormatWith(f.Value, path, 删除源文件 ? "移动" : "复制"));
                    if (!删除源文件) 触发文件创建事件(path, f.Value, f.ToolTip == "目录");
                    else 触发文件转移事件(path, f.Value, f.ToolTip == "目录");
                }
                catch (Exception er)
                {
                    cancelPathList.Add(f.Value);
                    er.Message.Trace();
                    写入消息(er.Message);
                }
            }
            tlist.ForEach(q => 刷新树状视图(树状视图.SelectedNode, q, false));
            d.ForEach(q => q.Checked = q.Value.IsIn(cancelPathList));
            Page.显示对话框("共计请求{2}{0}个勾选项，成功复制了{1}个勾选项！".FormatWith(a, c, 删除源文件 ? "移动" : "复制"));
            刷新显示状态();
            if (c > 0)
            {
                触发勾选节点变更事件();
                树状视图_TreeNodeCheckChanged(this, new TreeNodeEventArgs(获取当前选定项目录节点()));
            }
        }

        private void 复制项到节点(TreeNode 目标父节点, TreeNode 复制源)
        {
            var f = 复制源;
            var t = 复制源.ImageToolTip == "目录" ? 创建目录节点(new DirectoryInfo(目标父节点.Value + "\\" + f.Text)) : 创建文件节点(new FileInfo(目标父节点.Value + "\\" + f.Text));
            t.Selected = f.Selected;
            t.Checked = f.Checked;
            t.Expanded = f.Expanded;
            目标父节点.ChildNodes.Add(t);
            foreach (TreeNode d in 复制源.ChildNodes.Cast<TreeNode>().ToList())
            {
                复制项到节点(t, d);
            }
        }

        void 删除按钮_Click(object sender, EventArgs e)
        {
            var a = 树状视图.CheckedNodes.Count;
            var c = 0;
            foreach (var f in 树状视图.CheckedNodes.Cast<TreeNode>().OrderByDescending(q => q.Depth))
            {
                try
                {
                    if (f.ImageToolTip == "文件")
                    {
                        File.Delete(f.Value);
                        触发文件删除事件(f.Value, false);
                    }
                    else
                    {
                        Directory.Delete(f.Value);
                        触发文件删除事件(f.Value, true);
                    }
                    f.Parent.ChildNodes.Remove(f);
                    c++;
                    写入消息("成功删除“{0}”".FormatWith(f.Value));
                }
                catch (Exception er)
                {
                    er.Message.Trace();
                    写入消息(er.Message);
                }
            }
            Page.显示对话框("共计请求删除{0}个勾选项，成功删除了{1}个勾选项！".FormatWith(a, c));
            if (c > 0)
            {
                触发勾选节点变更事件();
            }
            刷新显示状态();
        }

        void 写入消息(object o)
        {
            消息记录列表.Items.Insert(0, new ListItem("{0} - {1}".FormatWith(DateTime.Now.ToLongTimeString(), o)));
            触发新消息事件(o.ToString());
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            AddAttributesToRender(output);

            树状视图.RenderControl(output);
            var l = new List<Control>();
            if (允许删除) l.Add(删除按钮);
            if (允许移动) l.Add(移动按钮);
            if (允许复制) l.Add(复制按钮);
            if (l.Count > 0)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                {
                    output.写入控件(q => q.写入空白占位符(), l.ToArray());
                }
                output.RenderEndTag();
            }
            if (允许重命名)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                {
                    output.写入控件(重命名标签, 重命名文本框, 重命名按钮);
                }
                output.RenderEndTag();
            }
            if (允许创建目录)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                {
                    output.写入控件(新建目录标签, 新建目录文本框, 新建目录按钮);
                }
                output.RenderEndTag();
            }
            if (显示消息记录)
            {
                output.RenderBeginTag(HtmlTextWriterTag.P);
                {
                    output.RenderBeginTag(HtmlTextWriterTag.Label);
                    {
                        output.Write("消息记录：");
                    }
                    output.RenderEndTag();
                }
                output.RenderEndTag();
                output.写入控件(消息记录列表);
            }
        }

        private void 添加文件系统数据(DirectoryInfo 目标目录, TreeView 树状视图控件, TreeNode 父节点)
        {
            var n = 创建目录节点(目标目录);
            if (父节点 == null)
            {
                树状视图控件.Nodes.Add(n);
                n.Selected = true;
            }
            else
            {
                var p = 父节点.RecursionSelect(q => q.ChildNodes.Cast<TreeNode>()).FirstOrDefault(q => q.Value == n.Value);
                if (p != null) n = p;
                else 父节点.ChildNodes.Add(n);
            }
            foreach (var f in 目标目录.GetDirectories())
            {
                添加文件系统数据(f, 树状视图控件, n);
            }
            foreach (var f in 目标目录.GetFiles())
            {
                var t = 创建文件节点(f);
                n.ChildNodes.Add(t);
            }
        }

        private TreeNode 创建文件节点(FileInfo 目标文件)
        {
            if (!目标文件.Exists) throw new Exception(目标文件.FullName + "不存在！");
            var t = new TreeNode(目标文件.Name, 目标文件.FullName);
            t.ToolTip = @"大小：{4}
创建时间：{0}
最后修改时间：{1}
最后访问时间：{2}
完整物理路径：{3}".FormatWith(目标文件.CreationTime, 目标文件.LastWriteTime, 目标文件.LastAccessTime, 目标文件.FullName, 目标文件.获取文件尺寸());
            t.ImageToolTip = "文件";
            t.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Core.ControlLibrary.FileManagementResources.FileIcon.gif");
            return t;
        }

        private TreeNode 创建目录节点(DirectoryInfo 目标目录)
        {
            if (!目标目录.Exists) throw new Exception(目标目录.FullName + "不存在！");
            var n = new TreeNode(目标目录.Name, 目标目录.FullName);
            n.ToolTip = @"创建时间：{0}
最后修改时间：{1}
最后访问时间：{2}
完整物理路径：{3}".FormatWith(目标目录.CreationTime, 目标目录.LastWriteTime, 目标目录.LastAccessTime, 目标目录.FullName);
            n.ImageToolTip = "目录";
            n.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Core.ControlLibrary.FileManagementResources.FolderIcon.gif");
            return n;
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-显示消息记录"]，默认值为true
        /// </summary>
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("指示是否显示消息记录相关的控件")]		//此属性在属性面板中的说明
        [DefaultValue(true)]
        public bool 显示消息记录
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-显示消息记录"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-显示消息记录"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-允许重命名"]，默认值为true
        /// </summary>
        [DefaultValue(true)]
        [Description("指示是否显示重命名相关的控件")]		//此属性在属性面板中的说明
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        public bool 允许重命名
        {
            get
            {
                object s = ViewState["Property-允许重命名"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                ViewState["Property-允许重命名"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-允许删除"]，默认值为true
        /// </summary>
        [Description("指示是否显示删除相关的控件")]		//此属性在属性面板中的说明
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [DefaultValue(true)]
        public bool 允许删除
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-允许删除"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-允许删除"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-允许移动"]，默认值为true
        /// </summary>
        [Description("指示是否显示移动相关的控件")]		//此属性在属性面板中的说明
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [DefaultValue(true)]
        public bool 允许移动
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-允许移动"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-允许移动"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-允许复制"]，默认值为true
        /// </summary>
        [DefaultValue(true)]
        [Description("指示是否显示复制相关的控件")]		//此属性在属性面板中的说明
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        public bool 允许复制
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-允许复制"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-允许复制"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-允许创建目录"]，默认值为true
        /// </summary>
        [DefaultValue(true)]
        [Description("指示是否显示创建目录相关的控件")]		//此属性在属性面板中的说明
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        public bool 允许创建目录
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Property-允许创建目录"];
                return ((s == null) ? true : (bool)s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-允许创建目录"] = value;
            }
        }

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Property-管理目录路径"]，默认值为string.Empty
        /// </summary>
        [DefaultValue("")]
        [Browsable(true)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        [Bindable(true)]	//指示数据绑定是否对此属性有意义
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [Description("指示由此控件管理的根目录服务器路径，如“~/Upload/")]		//此属性在属性面板中的说明
        public string 管理目录路径
        {
            get
            {
                EnsureChildControls();
                string s = (string)ViewState["Property-管理目录路径"];
                return ((s == null) ? string.Empty : s);
            }
            set
            {
                EnsureChildControls();
                ViewState["Property-管理目录路径"] = value;
            }
        }

        [Category("外观")]		//指示此属性在属性面板中所属的分类
        [Description("指示选定项的颜色")]		//此属性在属性面板中的说明
        public Color 选定项颜色
        {
            get
            {
                EnsureChildControls();
                return 树状视图.SelectedNodeStyle.ForeColor;
            }
            set
            {
                EnsureChildControls();
                树状视图.SelectedNodeStyle.ForeColor = value;
            }
        }

        [Category("外观")]		//指示此属性在属性面板中所属的分类
        [Description("指示常规项的颜色")]		//此属性在属性面板中的说明
        public Color 常规项颜色
        {
            get
            {
                EnsureChildControls();
                return 树状视图.NodeStyle.ForeColor;
            }
            set
            {
                EnsureChildControls();
                树状视图.NodeStyle.ForeColor = value;
            }
        }

        /// <summary>
        /// 当前所选定的目录或文件完整物理路径
        /// </summary>
        [Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        public string 当前选定路径
        {
            get
            {
                EnsureChildControls();
                return 树状视图.SelectedNode.Value;
            }
        }

        /// <summary>
        /// 当前所选定项的临近目录完整物理路径（如果选定的是目录，则直接返回其路径，如果是文件则返回其所在目录的路径）
        /// </summary>
        [Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        public string 当前选定目录
        {
            get
            {
                EnsureChildControls();
                return 获取当前选定项目录节点().Value;
            }
        }

        private TreeNode 获取当前选定项目录节点()
        {
            return 树状视图.SelectedNode.ImageToolTip == "目录" ? 树状视图.SelectedNode : 树状视图.SelectedNode.Parent;
        }

        [Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        public TreeNode 当前选定节点
        {
            get
            {
                EnsureChildControls();
                return 树状视图.SelectedNode;
            }
        }

        [Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
        public TreeNodeCollection 当前勾选节点集合
        {
            get
            {
                EnsureChildControls();
                return 树状视图.CheckedNodes;
            }
        }

        public delegate void 勾选节点变更(object sender, TreeNodeCollection 当前勾选节点集合);
        public event 勾选节点变更 勾选节点变更事件;
        protected virtual void 触发勾选节点变更事件()
        {
            if (勾选节点变更事件 != null) 勾选节点变更事件(this, 当前勾选节点集合);
        }

        public delegate void 选定节点变更(object sender, TreeNode 当前选定节点);
        public event 选定节点变更 选定节点变更事件;
        protected virtual void 触发选定节点变更事件()
        {
            if (选定节点变更事件 != null) 选定节点变更事件(this, 当前选定节点);
        }

        public delegate void 文件删除(object sender, string 文件路径, bool 是否为目录);
        public event 文件删除 文件删除事件;
        protected virtual void 触发文件删除事件(string 文件路径, bool 是否为目录)
        {
            if (文件删除事件 != null) 文件删除事件(this, 文件路径, 是否为目录);
        }

        public delegate void 文件转移(object sender, string 文件路径, string 移动源路径, bool 是否为目录);
        public event 文件转移 文件转移事件;
        protected virtual void 触发文件转移事件(string 文件路径, string 移动源路径, bool 是否为目录)
        {
            if (文件转移事件 != null) 文件转移事件(this, 文件路径, 移动源路径, 是否为目录);
        }

        public delegate void 文件创建(object sender, string 文件路径, string 复制源路径, bool 是否为目录);
        public event 文件创建 文件创建事件;
        protected virtual void 触发文件创建事件(string 文件路径, string 复制源路径, bool 是否为目录)
        {
            if (文件创建事件 != null) 文件创建事件(this, 文件路径, 复制源路径, 是否为目录);
        }

        public delegate void 新消息(object sender, string 内容);
        public event 新消息 新消息事件;
        protected virtual void 触发新消息事件(string 内容)
        {
            if (新消息事件 != null) 新消息事件(this, 内容);
        }
    }
}
