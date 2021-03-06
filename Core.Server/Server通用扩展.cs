﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;
using Core.WebSite;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.WebPages;
using Core.Web;

public static class Server通用扩展
{
    /// <summary>
    /// 获取客户端IP地址，如果是通过代理访问的，显示的是最后一级代理的IP地址，并在括号中显示逐级代理IP路径
    /// </summary>
    /// <param name="o">控制器</param>
    /// <returns>IP地址</returns>
    public static string GetClientIP(this Controller o)
    {
        return GetClientIP(o.HttpContext);
    }

    /// <summary>
    /// 获取客户端IP地址，如果是通过代理访问的，显示的是最后一级代理的IP地址，并在括号中显示逐级代理IP路径
    /// </summary>
    /// <returns>IP地址</returns>
    public static string GetClientIP(this HttpContextBase o)
    {
        if (o.Request.ServerVariables["HTTP_VIA"] != null)
            return o.Request.ServerVariables["REMOTE_ADDR"] + "(" + o.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ")";
        else
            return o.Request.ServerVariables["REMOTE_ADDR"];
    }

    /// <summary>
    /// 获取客户端IP地址，如果是通过代理访问的，显示的是最后一级代理的IP地址，并在括号中显示逐级代理IP路径
    /// </summary>
    /// <returns>IP地址</returns>
    public static string GetClientIP(this HttpContext o)
    {
        if (o.Request.ServerVariables["HTTP_VIA"] != null)
            return o.Request.ServerVariables["REMOTE_ADDR"] + "(" + o.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ")";
        else
            return o.Request.ServerVariables["REMOTE_ADDR"];
    }

    /// <summary>
    /// 设置Cookies中的值
    /// </summary>
    /// <param name="o">控制器</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <param name="expires">有效期，即从当前时刻起持续有效的时间</param>
    public static void SetCookie(this Controller o, string key, string value, TimeSpan expires)
    {
        var c = o.Response.Cookies[key];
        c.Value = value;
        c.Expires = DateTime.Now.Add(expires);
    }

    /// <summary>
    /// 获取Cookies中的值
    /// </summary>
    /// <param name="o">控制器</param>
    /// <param name="key">键</param>
    /// <returns>值</returns>
    public static string GetCookie(this Controller o, string key)
    {
        var c = o.Request.Cookies[key];
        if (c != null)
        {
            return c.Value;
        }
        return null;
    }

    // copied from HtmlHelper.FindPartialView because it's original is internal
    internal static IView FindPartialView(ViewContext viewContext, string partialViewName, ViewEngineCollection viewEngineCollection)
    {
        ViewEngineResult result = viewEngineCollection.FindPartialView(viewContext, partialViewName);
        if (result.View != null)
        {
            return result.View;
        }
        StringBuilder builder = new StringBuilder();
        foreach (string str in result.SearchedLocations)
        {
            builder.AppendLine();
            builder.Append(str);
        }
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
          "The partial view '{0}' was not found or no view engine supports the searched locations. The following locations were searched:{1}",
          new object[] { partialViewName, builder }));
    }

    // copied from HtmlHelper.RenderPartialInternal (thanks Red Gate!)
    // modified the logics to OrNull functionality
    internal static void RenderPartialInternalOrNull(HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData, object model, TextWriter writer, ViewEngineCollection viewEngineCollection)
    {
        if (string.IsNullOrEmpty(partialViewName))
        {
            throw new ArgumentNullException("partialViewName");
        }
        ViewDataDictionary dictionary = new ViewDataDictionary(viewData ?? htmlHelper.ViewData);

        // we explicitly set the model here so we don't get the current Model as fallback.
        dictionary.Model = model;

        ViewContext viewContext = new ViewContext(htmlHelper.ViewContext, htmlHelper.ViewContext.View, dictionary, htmlHelper.ViewContext.TempData, writer);
        FindPartialView(viewContext, partialViewName, viewEngineCollection).Render(viewContext, writer);
    }

    /// <summary>
    /// 输出分部视图，即使传入模型为null也可以正确输出，不会像默认的方法一样报“传入字典的模型项的类型错误”。
    /// 参考自：http://stackoverflow.com/questions/650393/renderpartial-with-null-model-gets-passed-the-wrong-type 。
    /// Renders the specified partial view as an HTML-encoded string (even if the model is null).
    /// This is a safer method than the normal HtmlHelper.Partial() because that one will use the current Model as a fallback when the given model is null.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="partialViewName">The name of the partial view to render.</param>
    /// <param name="model">The model for the partial view, may be null</param>
    /// <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    /// <returns></returns>
    public static MvcHtmlString PartialOrNull(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData = null)
    {
        using (StringWriter writer = new StringWriter(CultureInfo.CurrentCulture))
        {
            RenderPartialInternalOrNull(htmlHelper, partialViewName, viewData, model, writer, ViewEngines.Engines);
            return MvcHtmlString.Create(writer.ToString());
        }
    }

    /// <summary>
    /// 输出分部视图，如果传入模型为null则不做输出，不会像默认的方法一样报“传入字典的模型项的类型错误”。
    /// 参考自：http://stackoverflow.com/questions/650393/renderpartial-with-null-model-gets-passed-the-wrong-type 。
    /// Renders the specified partial view as an HTML-encoded string unless the model is null.
    /// Note that the partial is not exectued when the model is null.
    /// </summary>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="partialViewName">The name of the partial view to render.</param>
    /// <param name="model">The model for the partial view, may be null</param>
    /// <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    /// <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null.</returns>
    public static MvcHtmlString PartialOrDiscard(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData = null)
    {
        if (model == null)
            return null;
        return htmlHelper.Partial(partialViewName, model, viewData);
    }

    /// <summary>
    /// 输出分部视图，如果传入模型为null则不做输出，不会像默认的方法一样报“传入字典的模型项的类型错误”。
    /// 参考自：http://stackoverflow.com/questions/650393/renderpartial-with-null-model-gets-passed-the-wrong-type 。
    /// Renders the specified partial view as an HTML-encoded string unless the model is null.
    /// The given wrapper is used to wrap around the output of the partial result, it uses the Razor @item to place the partial output within the wrapper's template.
    /// Note the partial and the wrapper are not exectued when the model is null.
    /// </summary>
    /// <remarks>
    /// Note that the partial is rendered before wrapper is executed (should there be side-effects in either of them).
    /// </remarks>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="partialViewName">The name of the partial view to render.</param>
    /// <param name="model">The model for the partial view, may be null</param>
    /// <param name="wrapper">This wrapper is excuted when the model is not null, use @item to render the output of the partial</param>
    /// <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    /// <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null.</returns>
    public static HelperResult PartialOrDiscard(this HtmlHelper htmlHelper, string partialViewName, object model, Func<MvcHtmlString, HelperResult> wrapper, ViewDataDictionary viewData = null)
    {
        if (model == null)
            return null;
        return new HelperResult(writer =>
        {
            var partialResult = htmlHelper.Partial(partialViewName, model, viewData);
            wrapper(partialResult).WriteTo(writer);
        });
    }

    /// <summary>
    /// Renders the specified partial view as an HTML-encoded string unless the model is null or empty.
    /// Note that the partial is not exectued when the model is null or empty.
    /// </summary>
    /// <remarks>
    /// The enumerable passed into the model is checked with Any() to see whether it is empty.
    /// In case of a LINQ query this may cause the enumerable to be called twice (once for the Any() check and once propably inside the partial).
    /// </remarks>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="partialViewName">The name of the partial view to render.</param>
    /// <param name="model">The model for the partial view, may be null or an empty enumerable</param>
    /// <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    /// <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null or empty.</returns>
    public static MvcHtmlString PartialOrDiscardIfEmpty(this HtmlHelper htmlHelper, string partialViewName, IEnumerable<object> model, ViewDataDictionary viewData = null)
    {
        if (model == null || !model.Any())
            return null;
        return htmlHelper.Partial(partialViewName, model, viewData);
    }

    /// <summary>
    /// 输出分部视图，如果传入的集合模型为空则不做输出”。
    /// 参考自：http://stackoverflow.com/questions/650393/renderpartial-with-null-model-gets-passed-the-wrong-type
    /// Renders the specified partial view as an HTML-encoded string unless the model is null or empty.<br/>
    /// The given wrapper is used to wrap around the output of the partial result, it uses the Razor @item to place the partial output within the wrapper's template.
    /// Note the partial and the wrapper are not exectued when the model is null or empty.
    /// </summary>
    /// <remarks>
    /// The enumerable passed into the model is checked with Any() to see whether it is empty.
    /// In case of a LINQ query this may cause the enumerable to be called twice (once for the Any() check and once propably inside the partial).
    /// Note that the partial is rendered before wrapper is executed (should there be side-effects in either of them).
    /// </remarks>
    /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
    /// <param name="partialViewName">The name of the partial view to render.</param>
    /// <param name="model">The model for the partial view, may be null or an empty enumerable</param>
    /// <param name="wrapper">This wrapper is excuted when the model is not null or empty, use @item to render the output of the partial</param>
    /// <param name="viewData">A new dictionary or null (in which case the current view data is used as a fallback)</param>
    /// <returns>The partial view that is rendered as an HTML-encoded string or null if the model is null or empty.</returns>
    public static HelperResult PartialOrDiscardIfEmpty(this HtmlHelper htmlHelper, string partialViewName, IEnumerable<object> model, Func<MvcHtmlString, HelperResult> wrapper, ViewDataDictionary viewData = null)
    {
        if (model == null || !model.Any())
            return null;
        return new HelperResult(writer =>
        {
            var partialResult = htmlHelper.Partial(partialViewName, model, viewData);
            wrapper(partialResult).WriteTo(writer);
        });
    }

    public static ModelMetadata GetModelPropertyMetadata<TModel, TValue>(this HtmlHelper<TModel> o, Expression<Func<TModel, TValue>> 属性选取表达式)
    {
        return ModelMetadata.FromLambdaExpression<TModel, TValue>(属性选取表达式, o.ViewData);
    }

    /// <summary>
    ///  将适用于Bootstarp的form元素开始标记写入响应。在用户提交窗体时，将由某个操作方法处理该请求。
    /// </summary>
    /// <param name="o"></param>
    /// <param name="行为名称">Action名</param>
    /// <param name="控制器名称">Controller名</param>
    /// <param name="路由参数对象">构建提交参数的对象</param>
    /// <param name="提交方式">get或post方式，默认为post</param>
    /// <param name="Html属性字典操作">可以在此为form元素增加属性</param>
    /// <returns>form元素开始标记</returns>
    public static MvcForm BeginFormForBootstarp(this HtmlHelper o, string 行为名称, string 控制器名称, object 路由参数对象 = null, FormMethod 提交方式 = FormMethod.Post, Action<Dictionary<string, object>> Html属性字典操作 = null)
    {
        var dic = new Dictionary<string, object>();
        dic.Add("class", "form-horizontal");
        dic.Add("role", "form");
        if (Html属性字典操作 != null) Html属性字典操作(dic);
        return o.BeginForm(行为名称, 控制器名称, new RouteValueDictionary(路由参数对象), 提交方式, dic);
    }

    /// <summary>
    ///  将适用于Bootstarp的form元素开始标记写入响应。在用户提交窗体时，将由某个操作方法处理该请求。
    /// </summary>
    /// <param name="o"></param>
    /// <param name="行为名称">Action名</param>
    /// <param name="控制器名称">Controller名</param>
    /// <param name="路由参数对象">构建提交参数的对象</param>
    /// <param name="选项">Ajax选项，包含提交方式、替代元素Id等</param>
    /// <param name="Html属性字典操作">可以在此为form元素增加属性</param>
    /// <returns>form元素开始标记</returns>
    public static MvcForm BeginAjaxFormForBootstarp(this AjaxHelper o, string 行为名称, string 控制器名称, object 路由参数对象 = null, AjaxOptions 选项=null, Action<Dictionary<string, object>> Html属性字典操作 = null)
    {
        var dic = new Dictionary<string, object>();
        dic.Add("class", "form-horizontal");
        dic.Add("role", "form");
        if (Html属性字典操作 != null) Html属性字典操作(dic);
        return o.BeginForm(行为名称, 控制器名称, new RouteValueDictionary(路由参数对象), 选项, dic);
    }

    /// <summary>
    /// 同原生的Add方法，区别在于添加项后会返回自身，使得允许链式编程以连续添加项。
    /// </summary>
    /// <param name="d"></param>
    /// <param name="键">键</param>
    /// <param name="值">值</param>
    /// <returns></returns>
    public static ViewDataDictionary AddByLink(this ViewDataDictionary d, string 键, object 值)
    {
        d.Add(键, 值);
        return d;
    }

    /// <summary>
    /// 转换为JavaScript对象（Json）的MvcHtml字符串，可以在页面的JavaScript代码中直接引用，比如JavaScript代码：var myarray=@Model.MyArray.ToJavaScriptObject();
    /// </summary>
    /// <param name="o">要转换的对象，需支持Json序列化</param>
    /// <returns>JavaScript对象的MvcHtml字符串</returns>
    public static MvcHtmlString ToJavaScriptObject(this object o)
    {
        if (o.GetType().FullName.Contains("<>f__AnonymousType"))
        {
            //匿名类型
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(o).ToMvcHtmlString();
        }
        return Core.IO.IO处理函数.序列化对象为Json字符串(o).ToMvcHtmlString();
    }

    /// <summary>
    /// 转换为MvcHtml字符串
    /// </summary>
    /// <param name="html">当前字符串</param>
    /// <returns>MvcHtml字符串</returns>
    public static MvcHtmlString ToMvcHtmlString(this string html)
    {
        return MvcHtmlString.Create(html);
    }

    /// <summary>
    /// 转换为MvcHtml字符串
    /// </summary>
    /// <param name="html">当前字符串</param>
    /// <returns>MvcHtml字符串</returns>
    public static MvcHtmlString ToMvcHtmlString(this 通用扩展.HtmlString html)
    {
        return MvcHtmlString.Create(html.Value);
    }

    /// <summary>
    /// 组合Html字符串
    /// </summary>
    /// <param name="html">当前Html字符串</param>
    /// <param name="待组合的Html字符串">待组合的Html字符串</param>
    /// <returns>组合后的Html字符串</returns>
    public static MvcHtmlString Combin(this MvcHtmlString html, MvcHtmlString 待组合的Html字符串)
    {
        return Combin(html, 待组合的Html字符串.ToHtmlString());
    }

    /// <summary>
    /// 组合Html字符串
    /// </summary>
    /// <param name="html">当前Html字符串</param>
    /// <param name="待组合的Html字符串">待组合的Html字符串</param>
    /// <returns>组合后的Html字符串</returns>
    public static MvcHtmlString Combin(this MvcHtmlString html, string 待组合的Html字符串)
    {
        return MvcHtmlString.Create(html.ToHtmlString() + 待组合的Html字符串);
    }

    /// <summary>
    /// 创建超链接代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="显示文本">显示文本</param>
    /// <param name="链接路径">链接路径</param>
    /// <param name="属性字典操作表达式">属性字典操作表达式，可以在此添加标签属性。注：默认已包含href属性</param>
    /// <param name="构造器操作表达式">构造器操作表达式，可在此执行添加class等操作</param>
    /// <returns></returns>
    public static MvcHtmlString Link(this HtmlHelper o, string 显示文本, string 链接路径, Action<IDictionary<string, string>> 属性字典操作表达式 = null, Action<TagBuilder> 构造器操作表达式 = null)
    {
        var builder = new TagBuilder("a");
        if (构造器操作表达式 != null) 构造器操作表达式(builder);
        builder.SetInnerText(显示文本);
        builder.Attributes.Add("href", 链接路径);
        if (属性字典操作表达式 != null) 属性字典操作表达式(builder.Attributes);
        return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
    }

    /// <summary>
    /// 创建JqueryMobile的超链接按钮代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="显示文本">显示文本</param>
    /// <param name="链接路径">链接路径</param>
    /// <param name="使用Ajax">使用Ajax</param>
    /// <param name="采用内联样式">采用内联样式</param>
    /// <param name="属性字典操作表达式">属性字典操作表达式，可以在此添加标签属性。注：默认已包含href属性</param>
    /// <param name="构造器操作表达式">构造器操作表达式，可在此执行添加CssClass等操作</param>
    /// <param name="主题">主题样式，通常为a、b、c、d等</param>
    /// <returns></returns>
    public static MvcHtmlString LinkButtonForJqueryMobile(this HtmlHelper o, string 显示文本, string 链接路径, bool 使用Ajax, bool 采用内联样式, Action<IDictionary<string, string>> 属性字典操作表达式 = null, Action<TagBuilder> 构造器操作表达式 = null, string 主题 = null)
    {
        return Link(o, 显示文本, 链接路径, q =>
        {
            q.AddByLink("data-ajax", 使用Ajax.ToString().ToLower());
            if (属性字典操作表达式 != null) 属性字典操作表达式(q);
        }, q =>
        {
            q.AddCssClass("ui-btn");
            if (!主题.IsNullOrEmpty()) q.AddCssClass("ui-btn-" + 主题);
            q.AddCssClass("ui-corner-all");
            if (采用内联样式) q.AddCssClass("ui-btn-inline");
            if (构造器操作表达式 != null) 构造器操作表达式(q);
        });
    }

    /// <summary>
    /// 创建JqueryMobile的文本输入框代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="值">控件值</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <param name="功能提示">显示在输入框内的功能提示</param>
    /// <param name="控件类型">HTML控件的输入类型，如text、email、url、month、date、week、datetime、time、range、password、number等</param>
    /// <param name="首注">文本框首端显示的标注，通常用来显示货币类型或短语组成部分。注：需要在CSS文件中加入.controlgroup-textinput样式“padding-top:.22em;padding-bottom:.22em;”，否则会有错位现象</param>
    /// <param name="尾注">文本框末端显示的标注，通常用来显示单位或短语组成部分。注：需要在CSS文件中加入.controlgroup-textinput样式“padding-top:.22em;padding-bottom:.22em;”，否则会有错位现象</param>
    /// <returns></returns>
    public static MvcHtmlString TextInputForJqueryMobile(this HtmlHelper o, string 名称, string 值, string 功能提示, string 标签显示内容 = null, string 控件类型 = "text", string 首注 = null, string 尾注 = null, Action<TagBuilder> 构造器操作表达式 = null)
    {
        var id = "id" + Guid.NewGuid();

        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;

        var label = new TagBuilder("label");
        label.MergeAttribute("for", id);
        label.SetInnerText(标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":"));

        var input = new TagBuilder("input");
        input.MergeAttribute("type", 控件类型);
        input.MergeAttribute("name", 名称);
        input.GenerateId(id);
        if (!功能提示.IsNullOrEmpty()) input.MergeAttribute("placeholder", 功能提示);
        if (!值.IsNullOrEmpty()) input.MergeAttribute("value", 值);
        if (构造器操作表达式 != null) 构造器操作表达式(input);

        var rounddiv = new TagBuilder("div");
        rounddiv.Attributes.Add("data-role", "fieldcontain");
        if (首注.IsNullOrEmpty() && 尾注.IsNullOrEmpty())
        {
            rounddiv.InnerHtml = label.ToString(TagRenderMode.Normal) + input.ToString(TagRenderMode.SelfClosing);
        }
        else
        {
            var roundgroup = new TagBuilder("div");
            roundgroup.Attributes.AddByLink("data-role", "controlgroup").AddByLink("data-type", "horizontal");
            if (!首注.IsNullOrEmpty())
            {
                roundgroup.InnerHtml += "<button disabled=''>{0}</button>".FormatWith(首注);
            }
            input.Attributes.Add("data-wrapper-class", "controlgroup-textinput ui-btn");
            roundgroup.InnerHtml += input.ToString(TagRenderMode.SelfClosing);
            if (!尾注.IsNullOrEmpty())
            {
                roundgroup.InnerHtml += "<button disabled=''>{0}</button>".FormatWith(尾注);
            }
            rounddiv.InnerHtml = label.ToString(TagRenderMode.Normal) + roundgroup.ToString(TagRenderMode.Normal);
        }
        return MvcHtmlString.Create(rounddiv.ToString(TagRenderMode.Normal));
    }

    /// <summary>
    /// 创建JqueryMobile的文本输入区域代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="值">控件值</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <param name="功能提示">显示在输入框内的功能提示</param>
    /// <param name="显示行数">显示行数，这将决定容器的显示高度</param>
    /// <param name="id">ID，为null则自动生成</param>
    /// <returns></returns>
    public static MvcHtmlString TextAreaForJqueryMobile(this HtmlHelper o, string 名称, string 值, string 功能提示, string 标签显示内容 = null, int 显示行数 = 5, string id = null, Action<TagBuilder> 构造器操作表达式 = null)
    {
        if (id == null) id = "id" + Guid.NewGuid();

        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;

        var label = new TagBuilder("label");
        label.MergeAttribute("for", id);
        label.SetInnerText(标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":"));

        var input = new TagBuilder("textarea");
        input.MergeAttribute("name", 名称);
        input.MergeAttribute("rows", 显示行数.ToString());
        input.GenerateId(id);
        if (!功能提示.IsNullOrEmpty()) input.MergeAttribute("placeholder", 功能提示);
        if (!值.IsNullOrEmpty()) input.SetInnerText(值);
        if (构造器操作表达式 != null) 构造器操作表达式(input);

        var rounddiv = new TagBuilder("div");
        rounddiv.Attributes.Add("data-role", "fieldcontain");
        rounddiv.InnerHtml = label.ToString(TagRenderMode.Normal) + input.ToString(TagRenderMode.Normal);
        return MvcHtmlString.Create(rounddiv.ToString(TagRenderMode.Normal));
    }

    /// <summary>
    /// 创建JqueryMobile的开关代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="值">当前值</param>
    /// <param name="开状态标签">开状态标签</param>
    /// <param name="关状态标签">关状态标签</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <param name="附加属性">多个属性以空格分隔</param>
    /// <returns></returns>
    public static MvcHtmlString SwitchForJqueryMobile(this HtmlHelper o, string 名称, bool 值, string 开状态标签, string 关状态标签, string 标签显示内容 = null, bool 使用普通下拉列表样式 = false, string 附加属性 = null)
    {
        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;
        return MvcHtmlString.Create(@"<div data-role=""fieldcontain"">
        <label for=""{1}"">{6}</label>
        <select {8} name=""{0}"" id=""{1}"" {7}>
            <option value=""false"" {3}>{5}
                </option>
            <option value=""true"" {2}>{4}
                </option>
        </select>
    </div>".FormatWith(
            名称,
            "id" + Guid.NewGuid(),
            值 ? @"selected=""true""" : String.Empty,
            (!值) ? @"selected=""true""" : String.Empty,
            开状态标签,
            关状态标签,
            标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":"),
            使用普通下拉列表样式 ? @"data-native-menu=""false""" : @"data-role=""slider""",
            附加属性
            ));
    }

    /// <summary>
    /// 创建JqueryMobile的单选项代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="值">当前值</param>
    /// <param name="开状态标签">开状态标签</param>
    /// <param name="关状态标签">关状态标签</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <returns></returns>
    public static MvcHtmlString RadioButtonForJqueryMobile(this HtmlHelper o, string 名称, bool 值, string 开状态标签, string 关状态标签, string 标签显示内容 = null)
    {
        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;
        return MvcHtmlString.Create(@"
<fieldset data-role=""controlgroup"" data-type=""horizontal"">
        <legend>{6}</legend>
        <input type=""radio"" name=""{0}"" id=""{1}a"" value=""true"" {2}>
        <label for=""{1}a"">{4}</label>
        <input type=""radio"" name=""{0}"" id=""{1}b"" value=""false"" {3}>
        <label for=""{1}b"">{5}</label>
    </fieldset>".FormatWith(
                名称,
                "id" + Guid.NewGuid(),
                值 ? @"checked=""checked""" : String.Empty,
                (!值) ? @"checked=""checked""" : String.Empty,
                开状态标签,
                关状态标签,
                标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":")
                ));
    }

    /// <summary>
    /// 将传入对象转化为经过HTML转义的文本
    /// </summary>
    /// <param name="o"></param>
    /// <param name="obj">待输出的对象</param>
    /// <returns></returns>
    public static MvcHtmlString DisplayToText(this HtmlHelper o, object obj)
    {
        return obj == null ? null : MvcHtmlString.Create(Web处理函数.进行HTML转义(obj.ToString()));
    }

    /// <summary>
    /// 创建JqueryMobile的选择列表菜单代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="当前选择的值">当前选择的值</param>
    /// <param name="列表项名称">列表项名称数组</param>
    /// <param name="列表项值">列表项值数组</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <returns></returns>
    public static MvcHtmlString SelectMenuForJqueryMobile(this HtmlHelper o, string 名称, string 当前选择的值, IList<string> 列表项名称, IList<object> 列表项值 = null, string 标签显示内容 = null, bool 是否支持多选 = false)
    {
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < 列表项名称.Count; i++)
        {
            var v = 列表项值 == null ? 列表项名称[i] : 列表项值[i].ToString();
            s.AppendLine(@"<option {2}value=""{1}"">{0}</option>".FormatWith(列表项名称[i], v, v == 当前选择的值 || 尝试进行位运算以检测枚举多选值(v, 当前选择的值) ? @"selected=""true"" " : null));
        }
        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;
        return MvcHtmlString.Create(@"<div data-role=""fieldcontain"">
        <label for=""{1}"">{2}</label>
        <select id=""{1}"" data-native-menu=""false"" {4} name=""{0}"">
            {3}
        </select>
    </div>".FormatWith(名称, "id" + Guid.NewGuid(), 标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":"), s, 是否支持多选 ? @"multiple=""multiple""" : String.Empty));
    }

    private static bool 尝试进行位运算以检测枚举多选值(string 列表项值, string 当前传入的值)
    {
        if ((列表项值 + 当前传入的值).RegexIsMatch(@"^\d+$"))
        {
            return (Int32.Parse(列表项值) & Int32.Parse(当前传入的值)) > 0;
        }
        return false;
    }

    /// <summary>
    /// 创建JqueryMobile的选择列表菜单代码
    /// </summary>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="当前选择的值">当前选择的值</param>
    /// <param name="获取值操作">获取列表项值的操作</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <param name="列表项">列表项</param>
    /// <param name="获取名称操作">获取列表项显示名称的操作</param>
    /// <returns></returns>
    public static MvcHtmlString SelectMenuForJqueryMobile<T>(this HtmlHelper o, string 名称, string 当前选择的值, IList<T> 列表项, Func<T, string> 获取名称操作, Func<T, string> 获取值操作, string 标签显示内容 = null, bool 是否支持多选 = false)
    {
        var s = new StringBuilder();
        foreach (var f in 列表项)
        {
            var v = 获取值操作(f);
            s.AppendLine(@"<option {2}value=""{1}"">{0}</option>".FormatWith(获取名称操作(f), v, !当前选择的值.IsNullOrEmpty() && 当前选择的值.RegexSplit(",").Any(q => q == v) ? @"selected=""true"" " : null));
        }
        if (标签显示内容.IsNullOrEmpty()) 标签显示内容 = 名称;
        return MvcHtmlString.Create(@"<div data-role=""fieldcontain"">
        <label for=""{1}"">{2}</label>
        <select id=""{1}"" data-native-menu=""false"" {4} name=""{0}"">
            {3}
        </select>
    </div>".FormatWith(名称, "id" + Guid.NewGuid(), 标签显示内容 != null && 标签显示内容.EndsWith(":") ? 标签显示内容 : (标签显示内容 + ":"), s, 是否支持多选 ? @"multiple=""multiple""" : String.Empty));
    }

    /// <summary>
    /// 基于指定枚举类型，创建JqueryMobile的选择列表菜单代码
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <param name="o"></param>
    /// <param name="名称">控件name属性值</param>
    /// <param name="当前选择的值">当前选择的值</param>
    /// <param name="是否使用枚举值作为列表选项值">为是则使用枚举对应的整数值作为列表选项值，否则使用显示名作为选项值</param>
    /// <param name="标签显示内容">标签内容，如果为空则使用名称作为标签。将在结尾处自动添加半角冒号。</param>
    /// <returns></returns>
    public static MvcHtmlString SelectMenuForJqueryMobile<E>(this HtmlHelper o, string 名称, E 当前选择的值, bool 是否使用枚举值作为列表选项值, string 标签显示内容 = null, bool 是否支持多选 = false)
    {
        var ea = Enum.GetNames(typeof(E)).Cast<string>().ToList();
        List<object> va = null;
        if (是否使用枚举值作为列表选项值)
        {
            va = new List<object>();
            foreach (var f in ea)
            {
                va.Add(Convert.ToInt32(Enum.Parse(typeof(E), f)));
            }
        }
        return o.SelectMenuForJqueryMobile(名称, 是否使用枚举值作为列表选项值 ? 当前选择的值.GetHashCode().ToString() : 当前选择的值.ToString(), ea, 列表项值: va, 标签显示内容: 标签显示内容, 是否支持多选: 是否支持多选);
    }

    /// <summary>
    /// 将指向当前Web站点下的绝对路径转换为相对于当前页的路径，如“../admin/abc.aspx”
    /// </summary>
    public static string ToRelativePathForPage(this 通用扩展.PathString o)
    {
        var s = o.Value;

        // 根目录虚拟路径
        string virtualPath = WebSite变量.Current.Request.ApplicationPath;
        // 根目录绝对路径
        string pathRooted = HostingEnvironment.MapPath(virtualPath);
        // 页面虚拟路径
        string pageVirtualPath = WebSite变量.Current.Request.Path;

        if (!Path.IsPathRooted(s) || s.IndexOf(pathRooted) == -1)
        {
            throw new Exception("路径“{0}”不是绝对路径或不位于当前虚拟Web服务器目录内".FormatWith(s));
        }

        // 转换成相对路径 
        //(测试发现，pathRooted 在 VS2005 自带的服务器跟在IIS下根目录或者虚拟目录运行似乎不一样,
        // 有此地方后面会加"\", 有些则不会, 为保险起见判断一下)
        if (pathRooted.Substring(pathRooted.Length - 1, 1) == "\\")
        {
            s = s.Replace(pathRooted, "/");
        }
        else
        {
            s = s.Replace(pathRooted, "");
        }

        string relativePath = s.Replace("\\", "/");

        string[] pageNodes = pageVirtualPath.Split('/');

        // 减去最后一个页面和前面一个 "" 值
        int pageNodesCount = pageNodes.Length - 2;

        for (int i = 0; i < pageNodesCount; i++)
        {
            relativePath = "/.." + relativePath;
        }

        if (pageNodesCount > 0)
        {
            // 如果存在 ".." , 则把最前面的 "/" 去掉
            relativePath = relativePath.Substring(1, relativePath.Length - 1);
        }

        return relativePath;
    }

    /// <summary>
    /// 将指向当前Web站点下的绝对路径转换为相对于当前页的路径，如“../admin/abc.aspx”
    /// </summary>
    public static string ToRelativePathForPage(this string o)
    {
        return o.AsPathString().ToRelativePathForPage();
    }

    /// <summary>
    /// 将指向当前Web站点下的绝对路径转换为虚拟服务器路径，如：“~/admin/abc.aspx”
    /// </summary>
    public static ServerPathString ToServerPath(this 通用扩展.PathString o)
    {
        var s = o.Value;
        string pathRooted = HostingEnvironment.MapPath("~/");

        if (!Path.IsPathRooted(s) || s.IndexOf(pathRooted) == -1)
        {
            throw new Exception("路径“{0}”不是绝对路径或不位于当前虚拟Web服务器目录内".FormatWith(s));
        }

        if (pathRooted.Substring(pathRooted.Length - 1, 1) == "\\")
        {
            s = s.Replace(pathRooted, "~/");
        }
        else
        {
            s = s.Replace(pathRooted, "~");
        }

        string relativePath = s.Replace("\\", "/");
        return relativePath.AsServerPathString();
    }

    /// <summary>
    /// 将指向当前Web站点下的绝对路径转换为虚拟服务器路径，如：“~/admin/abc.aspx”
    /// </summary>
    public static ServerPathString ToServerPath(this string o)
    {
        return o.AsPathString().ToServerPath();
    }

    /// <summary>
    /// 将当前相对于服务器根目录的相对Url（如“/Upload/File”）转换为绝对Url网址，如：“http://localhost:6068/Upload/File”
    /// </summary>
    public static string ToAbsoluteUrl(this 通用扩展.PathString o)
    {
        if (o.Value.ToLower().StartsWith("http://")) return o.Value;
        return "http://" + WebSite变量.Current.Request.ServerVariables["SERVER_NAME"] + ":" + WebSite变量.Current.Request.ServerVariables["Server_Port"] + o;
    }

    /// <summary>
    /// 将当前相对于服务器根目录的相对Url（如“/Upload/File”）转换为绝对Url网址，如：“http://localhost:6068/Upload/File”
    /// </summary>
    public static string ToAbsoluteUrl(this string o)
    {
        return o.AsPathString().ToAbsoluteUrl();
    }

    /// <summary>
    /// 定义为服务器虚拟路径字符串，如：~/Admin/Default.aspx
    /// </summary>
    public class ServerPathString : 通用扩展.SpecialString
    {
        /// <summary>
        /// 转换为绝对物理路径，如：C:/dir/abc.def
        /// </summary>
        /// <returns></returns>
        public string ToPhysicsAbsolutePath()
        {
            return HttpContext.Current.Server.MapPath(Value);
        }

        /// <summary>
        /// 转换为绝对服务器路径，如：/images/123.jpg
        /// </summary>
        /// <returns></returns>
        public string ToServerAbsolutePath()
        {
            return VirtualPathUtility.ToAbsolute(Value);
        }
    }

    /// <summary>
    /// 定义为服务器虚拟路径字符串，如：~/Admin/Default.aspx
    /// </summary>
    public static ServerPathString AsServerPathString(this string s)
    {
        return s.As<ServerPathString>();
    }
}
