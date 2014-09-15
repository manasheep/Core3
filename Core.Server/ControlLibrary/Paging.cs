using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Core.WebSite;
using Core.Text;
using System.Web.UI.Design;
using System.Drawing;

namespace Core.ControlLibrary
{
	[DefaultEvent("执行分页")]	//对于控件类，表示设计时双击控件，进入代码文件，并开始编辑的事件
	[DefaultProperty("每页显示最大项目数")]
	[ToolboxData("<{0}:Paging runat=server></{0}:Paging>")]
	[ToolboxBitmap(typeof(Paging), "Core.ControlLibrary.PagingResources.Paging.bmp")]
	public class Paging : WebControl, IPostBackEventHandler
	{
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Ul;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-当前页"]，默认值为0
		/// </summary>
		[DefaultValue(0)]
		[Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
		public int 当前页
		{
			get
			{
				EnsureChildControls();
				object s = ViewState["Property-当前页"];
				if (s == null)
				{
					var i = 0;
					Int32.TryParse(Page.Request.QueryString[Url分页参数名], out i);
					当前页 = i;
					return i;
				}
				else return (int)s;
			}
			set
			{
				EnsureChildControls();
				ViewState["Property-当前页"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-输出HTML代码"]，默认值为null
		/// </summary>
		[DefaultValue(null)]
		[Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
		protected string 输出HTML代码
		{
			get
			{
				string s = (string)ViewState["Property-输出HTML代码"];
				return ((s == null) ? null : s);
			}
			set
			{
				ViewState["Property-输出HTML代码"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-页面跳转模式"]，默认值为false
		/// </summary>
		[DefaultValue(false)]
		[Category("行为")]		//指示此属性在属性面板中所属的分类
		[Description("指示用户点击页面之后的页面跳转方式，是跳转到新的Url，还是仅在本页更新数据。此属性应当只在设计时设置。")]		//此属性在属性面板中的说明
		public bool 页面跳转模式
		{
			get
			{
				EnsureChildControls();
				object s = ViewState["Property-页面跳转模式"];
				return ((s == null) ? false : (bool)s);
			}
			set
			{
				EnsureChildControls();
				ViewState["Property-页面跳转模式"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-每页显示最大项目数"]，默认值为10
		/// </summary>
		[DefaultValue(10)]
		[Category("行为")]		//指示此属性在属性面板中所属的分类
		[Description("指示分页大小")]		//此属性在属性面板中的说明
		public int 每页显示最大项目数
		{
			get
			{
				EnsureChildControls();
				object s = ViewState["Property-每页显示最大项目数"];
				return ((s == null) ? 10 : (int)s);
			}
			set
			{
				EnsureChildControls();
				ViewState["Property-每页显示最大项目数"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-总项目数"]，默认值为-1
		/// </summary>
		[DefaultValue(-1)]
		[Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
		public int 总项目数
		{
			get
			{
				object s = ViewState["Property-总项目数"];
				return ((s == null) ? -1 : (int)s);
			}
			set
			{
				ViewState["Property-总项目数"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-Url分页参数名"]，默认值为"PageNum"
		/// </summary>
		[DefaultValue("PageNum")]
		[Category("行为")]		//指示此属性在属性面板中所属的分类
		[Description("设定在url参数中标示当前页编号的参数名。此属性应当只在设计时设置。")]		//此属性在属性面板中的说明
		public string Url分页参数名
		{
			get
			{
				EnsureChildControls();
				string s = (string)ViewState["Property-Url分页参数名"];
				return ((s == null) ? "PageNum" : s);
			}
			set
			{
				EnsureChildControls();
				ViewState["Property-Url分页参数名"] = value;
			}
		}

        /// <summary>
        /// 基于ViewState的属性，对应于ViewState["Paging-Property-仅有一页时隐藏控件"]，默认值为false
        /// </summary>
        [Category("行为")]		//指示此属性在属性面板中所属的分类
        [DefaultValue(false)]		//指示此属性的默认值（仅用于在属性面板中提示用户是否已改变此属性值）
        [Description("指示总页数小于或等于1时是否隐藏该分页控件")]		//此属性在属性面板中的说明
        public bool 仅有一页时隐藏控件
        {
            get
            {
                EnsureChildControls();
                object s = ViewState["Paging-Property-仅有一页时隐藏控件"];
                return ((s == null) ? false : (bool)s);
            }
            set
            {
                ViewState["Paging-Property-仅有一页时隐藏控件"] = value;
            }
        }


		protected override void RenderContents(HtmlTextWriter output)
		{
			更新();
			output.Write(输出HTML代码);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsPostBack) 触发执行分页事件(当前页 * 每页显示最大项目数, 每页显示最大项目数);
			this.CssClass = "Paging";
		}

		/// <summary>
		/// 更新控件的显示
		/// </summary>
		public void 更新()
		{
			更新输出HTML代码();
		}

		public delegate void 执行分页(object sender, int 跳过记录数, int 获取记录数);
		/// <summary>
		/// 当需要执行分页时触发的事件，应当在Page_Load中注册此事件并依照参数指示获取指定区段的数据记录，未注册此事件将引发异常
		/// </summary>
		[Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
		public event 执行分页 执行分页事件;
		protected virtual void 触发执行分页事件(int 跳过记录数, int 获取记录数)
		{
			if (执行分页事件 == null) throw new Exception("未注册任何对于“执行分页事件”的处理函数！");
			执行分页事件(this, 跳过记录数, 获取记录数);
		}

		/// <summary>
		/// 指示在读取数据时应跳过的记录数
		/// </summary>
		[Browsable(false)]	//指示此属性是否在属性面板中可见（默认为可见，即使不添加此特性也是可见的）
		public int 数据读取跳过项
		{
			get
			{
				return 当前页 * 每页显示最大项目数;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-当前页前后临近页码显示量"]，默认值为5
		/// </summary>
		[DefaultValue(5)]
		[Category("行为")]		//指示此属性在属性面板中所属的分类
		[Description("指示当前页码前后显示几个临近页码，超过的部分将显示为“…”")]		//此属性在属性面板中的说明
		public int 当前页前后临近页码显示量
		{
			get
			{
				EnsureChildControls();
				object s = ViewState["Property-当前页前后临近页码显示量"];
				return ((s == null) ? 5 : (int)s);
			}
			set
			{
				EnsureChildControls();
				ViewState["Property-当前页前后临近页码显示量"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Property-页码链接提示文字"]，默认值为"转到第{0}页"
		/// </summary>
		[DefaultValue("转到第{0}页")]
		[Category("外观")]		//指示此属性在属性面板中所属的分类
		[Description("指定页码提示文字的格式化文本，{0}代表页码数")]		//此属性在属性面板中的说明
		public string 页码链接提示文字
		{
			get
			{
				string s = (string)ViewState["Property-页码链接提示文字"];
				return ((s == null) ? "转到第{0}页" : s);
			}
			set
			{
				ViewState["Property-页码链接提示文字"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Paging-Property-上一页显示字符串"]，默认值为"上一页"
		/// </summary>
		[DefaultValue("上一页")]
		[Category("外观")]		//指示此属性在属性面板中所属的分类
		[Description("上一页链接所现实的字符")]		//此属性在属性面板中的说明
		public string 上一页显示字符串
		{
			get
			{
				string s = (string)ViewState["Paging-Property-上一页显示字符串"];
				return ((s == null) ? "上一页" : s);
			}
			set
			{
				ViewState["Paging-Property-上一页显示字符串"] = value;
			}
		}

		/// <summary>
		/// 基于ViewState的属性，对应于ViewState["Paging-Property-下一页显示字符串"]，默认值为"下一页"
		/// </summary>
		[DefaultValue("下一页")]
		[Category("外观")]		//指示此属性在属性面板中所属的分类
		[Description("下一页链接所现实的字符")]		//此属性在属性面板中的说明
		public string 下一页显示字符串
		{
			get
			{
				string s = (string)ViewState["Paging-Property-下一页显示字符串"];
				return ((s == null) ? "下一页" : s);
			}
			set
			{
				ViewState["Paging-Property-下一页显示字符串"] = value;
			}
		}

		private void 更新输出HTML代码()
		{
			var tp = 计算总页数(总项目数, 每页显示最大项目数);
            if (tp <= 1 && 仅有一页时隐藏控件)
            {
                输出HTML代码 = string.Empty;
                return;
            }
			var np = 当前页;
			Func<int, string> makenavurl = 页码 =>
				{
					var d = new Dictionary<string, string>();
					d.Add(Url分页参数名, 页码.ToString());
					return WebSite处理函数.获取页面名称() + "?" + WebSite处理函数.生成URL参数字串(d, true);
				};
			Func<int, string, string, string> makehtml = (页码, 显示字符串, Css类) =>
				{
					if (页码 == np)
					{
						return @"<li class=""{1}"">{0}</li>".FormatWith(显示字符串 == null ? (页码 + 1).ToString() : 显示字符串, Css类 == null ? "NowPage" : Css类);
					}
					else
					{
						return @"<li class=""{3}""><a href=""{2}"" title=""{0}"">{1}</a></li>".FormatWith(页码链接提示文字.FormatWith(页码 + 1), 显示字符串 == null ? (页码 + 1).ToString() : 显示字符串, 页面跳转模式 ? makenavurl(页码) : Page.GetPostBackClientHyperlink(this, 页码.ToString()), Css类 == null ? "Page" : Css类);
					}
				};

			StringBuilder s = new StringBuilder();
			if (np > 0) s.AppendLine(makehtml(np - 1, "上一页", "LastPage"));
			for (int i = 0; i < tp; i++)
			{
				if (i == 0 || i == tp - 1 || (i <= np + 当前页前后临近页码显示量 && i >= np - 当前页前后临近页码显示量))
				{
					s.AppendLine(makehtml(i, null, null));
				}
				else if (i == (tp - np - 当前页前后临近页码显示量) / 2 + 当前页前后临近页码显示量 + np || i == (np - 当前页前后临近页码显示量) / 2)
				{
					s.AppendLine(makehtml(i, "…", null));
				}
			}
			if (np < tp - 1) s.AppendLine(makehtml(np + 1, "下一页", "NextPage"));
			输出HTML代码 = s.ToString();
		}

		private int 计算总页数(int 总项目数, int 每页显示最大项目数)
		{
			return 总项目数 / 每页显示最大项目数 + (总项目数 % 每页显示最大项目数 > 0 ? 1 : 0);
		}

		#region IPostBackEventHandler 成员

		public void RaisePostBackEvent(string eventArgument)
		{
			当前页 = eventArgument.转换为Int32();
			触发执行分页事件(当前页 * 每页显示最大项目数, 每页显示最大项目数);
		}

		#endregion
	}

	public class PagingDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			return @"<ul id=""ctl00_ContentPlaceHolder1_Paging1"">
	<li class=""NowPage"">1</li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','1')"" title=""转到第2页"">2</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','2')"" title=""转到第3页"">3</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','3')"" title=""转到第4页"">4</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','4')"" title=""转到第5页"">5</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','5')"" title=""转到第6页"">6</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','16')"" title=""转到第17页"">…</a></li>
<li class=""Page""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','26')"" title=""转到第27页"">27</a></li>
<li class=""NextPage""><a href=""javascript:__doPostBack('ctl00$ContentPlaceHolder1$Paging1','1')"" title=""转到第2页"">下一页</a></li>

</ul>";
		}
	}
}
