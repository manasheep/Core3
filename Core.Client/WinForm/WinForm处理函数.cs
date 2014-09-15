using System;
using System.Collections.Generic;
using System.Linq;
using Core.IO;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using Core.Caches;
using Core.Web;
using System.Text.RegularExpressions;
using System.IO;

namespace Core.WinForm
{
    public static class WinForm处理函数
    {
        /// <summary>
        /// 序列化控件及其所有子控件至文件
        /// </summary>
        public static void 序列化控件为文件(Control 控件, string 路径)
        {
            DateTime dt = DateTime.Now;
            ArrayList array = new ArrayList();
            int _sum = 0;
            遍历子控件(控件, ref array, 0, ref _sum);
            //SoapFormatter formatter = new SoapFormatter();
            //FileStream stream = new FileStream(路径, FileMode.Create, FileAccess.Write, FileShare.None);
            //formatter.Serialize(stream, array);
            //stream.Close();
            IO处理函数.序列化对象为二进制文件(array, 路径);
        }

        /// <summary>
        /// 从文件反序列化控件及其所有子控件
        /// </summary>
        public static void 反序列化控件自文件(Control 控件, string 路径)
        {
            DateTime dt = DateTime.Now;
            //SoapFormatter formatter = new SoapFormatter();
            //FileStream stream = new FileStream(路径, FileMode.Open);
            //ArrayList array = (ArrayList)formatter.Deserialize(stream);
            //stream.Close();
            ArrayList array = (ArrayList)IO处理函数.反序列化对象自二进制文件(路径);
            int _sum = 0;
            遍历子控件(控件, ref array, 1, ref _sum);
        }

        /// <summary>
        /// 递归所有有控件集合的对象
        /// </summary>
        /// <param name="对象">要操作的对象</param>
        /// <param name="数组">保存数据的数组</param>
        /// <param name="操作">0).表示序列化操作 1).表示反序列化操作</param>
        /// <param name="位置">反序列化操作时的当前位置</param>
        static void 遍历子控件(Control 对象, ref ArrayList 数组, int 操作, ref int 位置)
        {
            if (操作 == 0)
            {
                //循环窗体上的所有控件对象
                foreach (Control o in 对象.Controls)
                {
                    数组.Add(序列化对象(o));
                    if (o.Controls.Count > 0)
                        遍历子控件(o, ref 数组, 0, ref 位置);
                }
            }
            else
            {
                //循环窗体上的所有控件对象
                foreach (Control o in 对象.Controls)
                {

                    反序列化对象(o, (Object[])数组[位置++]);
                    if (o.Controls.Count > 0)
                        遍历子控件(o, ref 数组, 1, ref 位置);
                }
            }
        }

        /// <summary>
        /// 反序列化函数
        /// </summary>
        /// <param name="对象">原对象 将反序列化对象的数据改写原对象</param>
        /// <param name="值">要反序列化的对象</param>
        static void 反序列化对象(Object 对象, Object[] 值)
        {
            //取得控件类型
            Type type = 对象.GetType();

            //根据类型获取属性
            PropertyInfo[] proInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);// | BindingFlags.NonPublic
            int _sum = 0;
            int _unList = 0;//不能序列化数量
            int _readOnly = 0;//只读数量
            int _noAttrib = 0;//无效属性数量

            //此循环中 处理对象类型中的所有属性 但排除三种可能性
            //1).值为null 则序列化为null值
            //2).不能序列化的属性 则跳过不保存
            //3).特殊类型 不能序列化或反序列化 跳过不保存
            foreach (PropertyInfo qq in proInfo)
            {
                //Console.WriteLine("{0}{1}", "     " + _sum.ToString() + "  ", qq + "   " + qq.GetValue(obj, null));
                //判断属性是否无效
                if (TypeDescriptor.GetProperties(对象)[qq.Name] == null ||
                    qq.Name == "Cursor" ||
                    qq.Name == "ColumnCount" ||
                    qq.Name == "RowCount" ||
                    qq.Name == "FirstDisplayedScrollingRowIndex" ||
                    qq.Name == "FirstDisplayedScrollingColumnIndex" ||
                    qq.Name == "AutoScroll" ||
                    qq.Name == "SelectedIndex" ||
                    qq.Name == "SelectedItem" ||
                    qq.Name == "SelectedItems" ||
                    qq.Name == "SelectedValue" ||
                    qq.Name == "SelectedIndices")
                {
                    _noAttrib++;
                    continue;
                }
                //首先判断是否是只读属性
                AttributeCollection attributes = TypeDescriptor.GetProperties(对象)[qq.Name].Attributes;
                ReadOnlyAttribute myAttribute = (ReadOnlyAttribute)attributes[typeof(ReadOnlyAttribute)];
                if (!myAttribute.IsReadOnly)
                {
                    //1).判断为null值
                    if (qq.GetValue(对象, null) == null)
                    {
                        qq.SetValue(对象, 值[_sum++], null);

                        //判断此属性结构是否可序列化
                    }
                    else if (qq.GetValue(对象, null).GetType().IsSerializable)
                    {
                        //3).特殊类型属性处理 不能解析属性
                        //if (qq.GetValue(obj, null) is Cursor)
                        //{
                        //_value.SetValue("None", sum++);

                        //正常处理
                        //}
                        //else                        
                        qq.SetValue(对象, 值[_sum++], null);
                    }
                    //2).不能序列化属性
                    else
                    {
                        //_value.SetValue("未序列化属性", sum++);
                        _unList++;
                    }


                }
                else
                    _readOnly++;

            }
        }

        /// <summary>
        /// 处理单个对象的所有属性 并写入一个Object类型
        /// 
        /// 其它说明
        /// 1).值为null 则序列化为null值
        /// 2).不能序列化的属性 则跳过不保存
        /// 3).属性无效和不能反序列化 不保存(Cursor公用控件属性 ColumnCount,RowCount,FirstDisplayedScrollingRowIndexFirstDisplayedScrollingColumnIndex, DataGridView类型控件 AutoScroll 工具条控件)
        /// 4).属性只读 不保存
        /// </summary>
        /// <param name="对象"></param>
        /// <returns></returns>
        static Object 序列化对象(Object 对象)
        {
            Type type = 对象.GetType();//取得控件类型

            //根据类型获取属性
            PropertyInfo[] proInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);// | BindingFlags.NonPublic

            Object[] _value = new Object[proInfo.GetLength(0)];

            int _sum = 0;//当前属性位置
            int _unList = 0;//不能序列化数量
            int _readOnly = 0;//只读数量
            int _noAttrib = 0;//无效属性与不能反序列化属性数量

            //_value.SetValue(obj.ToString(), _sum++);//此处可以增加保存控件的标识


            //此循环中 处理对象类型中的所有属性 但排除三种可能性
            //1).值为null 则序列化为null值
            //2).不能序列化的属性 则跳过不保存
            //3).特殊类型 不能序列化或反序列化 跳过不保存
            foreach (PropertyInfo qq in proInfo)
            {
                //Console.WriteLine("{0}{1}", "     " + sum.ToString() + "  ", qq + "   " + qq.GetValue(obj, null));                
                //4).判断属性是否无效 则不保存//(qq.Name == "Item")
                if (TypeDescriptor.GetProperties(对象)[qq.Name] == null ||
                    qq.Name == "Cursor" ||
                    qq.Name == "ColumnCount" ||
                    qq.Name == "RowCount" ||
                    qq.Name == "FirstDisplayedScrollingRowIndex" ||
                    qq.Name == "FirstDisplayedScrollingColumnIndex" ||
                    qq.Name == "AutoScroll" ||
                    qq.Name == "SelectedIndex" ||
                    qq.Name == "SelectedItem" ||
                    qq.Name == "SelectedItems" ||
                    qq.Name == "SelectedValue" ||
                    qq.Name == "SelectedIndices")
                {
                    _noAttrib++;
                    continue;
                }
                //5).判断是否是只读属性 则不保存   
                AttributeCollection attributes = TypeDescriptor.GetProperties(对象)[qq.Name].Attributes;
                ReadOnlyAttribute myAttribute = (ReadOnlyAttribute)attributes[typeof(ReadOnlyAttribute)];

                if (!myAttribute.IsReadOnly)
                {

                    //1).判断为null值
                    if (qq.GetValue(对象, null) == null)
                    {
                        _value.SetValue(null, _sum++);
                    }
                    else if (qq.GetValue(对象, null).GetType().IsSerializable)//判断此属性结构是否可序列化
                    {
                        //3).特殊类型属性处理 不能解析属性
                        //if (qq.GetValue(obj, null) is Cursor)
                        //{
                        //_value.SetValue("不能解析属性", sum++);  
                        //}
                        //else
                        _value.SetValue(qq.GetValue(对象, null), _sum++);//正常处理
                    }
                    //2).不能序列化属性
                    else
                    {
                        //_value.SetValue("未序列化属性", sum++);
                        _unList++;
                    }

                }
                else
                    _readOnly++;


            }
            return _value;//返回已序列数组
            /*
                //如何能够判断出所有关联的属性值
                IFormatter formatter = new SoapFormatter();
                FileStream stream = new FileStream("myFileName.xml", FileMode.Create, FileAccess.Write, FileShare.None);//FileMode.Append, FileAccess.Write, FileShare.None
                formatter.Serialize(stream, _value);
                stream.Close();
            */
        }

        /// <summary>
        /// 抓取当前视图为图像
        /// </summary>
        /// <param name="以控件尺寸为准">指示抓取的图像尺寸是否以控件的当前尺寸为准，否则将以页面中Body元素的尺寸为准，并且将自动调整控件尺寸以适应内容</param>
        /// <returns>图像</returns>
        public static Bitmap 抓图(this WebBrowser wb, bool 以控件尺寸为准)
        {
            if (以控件尺寸为准)
            {
                Bitmap b = new Bitmap(wb.Width, wb.Height);
                wb.DrawToBitmap(b, new Rectangle { Height = wb.Height, Width = wb.Width });
                return b;
            }
            else
            {
                var o = wb.Document.Body.OffsetRectangle;
                wb.Height = o.Height + o.Y;
                wb.Width = o.Width + o.X;
                Bitmap b = new Bitmap(o.Width, o.Height);
                wb.DrawToBitmap(b, o);
                return b;
            }
        }

        /// <summary>
        /// 加载指定文件，并抓取其显示视图
        /// </summary>
        /// <param name="文件路径">要加载的文件路径，可以是本地路径，也可以是Url</param>
        /// <param name="延迟">加载完毕后延迟抓图时间，单位为毫秒</param>
        /// <param name="以控件尺寸为准">指示抓取的图像尺寸是否以控件的当前尺寸为准，否则将以页面中Body元素的尺寸为准，并且将自动调整控件尺寸以适应内容</param>
        /// <returns>图像</returns>
        public static Bitmap 抓图(this WebBrowser wb, string 文件路径, int 延迟, bool 以控件尺寸为准)
        {
            wb.Navigate(文件路径);

            //等待加载完毕
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            //延迟
            var time = DateTime.Now.AddMilliseconds(延迟);
            while (DateTime.Now < time)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            return wb.抓图(以控件尺寸为准);
        }

        public static void 抓图(this WebBrowser wb, string 文件路径, int 延迟, bool 以控件尺寸为准, string 保存路径)
        {
            wb.抓图(文件路径, 延迟, 以控件尺寸为准).Save(保存路径);
        }

        /// <summary>
        /// 跳转网页，等待网页加载完毕后执行相应指令，如果超时则重新启动跳转行为并重新计时。
        /// 如果跳转网址设为空，则是在等待页面重加载后执行相应操作，通常用于点击按钮后等待页面切换后执行命令时使用。
        /// </summary>
        /// <param name="跳转网址">要跳转的网址</param>
        /// <param name="超时秒数">超时时间设定</param>
        /// <param name="超时代理">超时后执行的特别操作，比如再次点击按钮以重新发起页面跳转操作，可设为空</param>
        /// <param name="执行代理">跳转后执行的代码</param>
        public static void 跳转后执行(this WebBrowser wb,string 跳转网址,int 超时秒数,WebBrowser跳转超时执行代理 超时代理,WebBrowser跳转后执行代理 执行代理)
        {
            定时器 d = new 定时器(超时秒数*1000, 500);
            d.执行完毕事件 += new 定时器.执行完毕代理((s, o, b) =>
            {
                if (b) return;
                if (超时代理 != null) 超时代理(wb);
                wb.Navigate("about:blank");
                wb.延迟执行(5000, t => wb.跳转后执行(跳转网址, 超时秒数, 超时代理, 执行代理));
            });
            wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            d.执行(null);
            wb.存入缓存("定时器", d);
            wb.存入缓存("执行代理", 执行代理);
            if (!跳转网址.IsNullOrEmpty()) wb.Navigate(跳转网址);
        }

        static void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var wb = sender as WebBrowser;
            if (wb.ReadyState < WebBrowserReadyState.Complete) return;
            wb.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
            var d = wb.取出缓存("定时器") as 定时器;
            d.中止(true);
            var run = wb.取出缓存("执行代理") as WebBrowser跳转后执行代理;
            run(wb);
        }

        public delegate void WebBrowser跳转后执行代理(WebBrowser wb);
        public delegate void WebBrowser跳转超时执行代理(WebBrowser wb);

        /// <summary>
        /// 为HTML元素增加边框颜色样式
        /// </summary>
        /// <param name="边框宽度">宽度</param>
        /// <param name="颜色">颜色</param>
        public static void 设置边框样式(this HtmlElement o,int 边框宽度,Color 颜色){
            o.Style += "border: {0}px solid #{1};".FormatWith(边框宽度,颜色.转换为网页用16进制字串());
        }

        public static IEnumerable<HtmlElement> 通过类名获取元素(this HtmlElementCollection o, string 类名)
        {
            //return 通过属性获取元素(o, "class", 类名);
            return 通过HTML代码获取元素(o, @"^\s*<\s*\w+[^>]*\s+class\s*=\s*[""']?\s*" + 类名 + @"\s*[""']\s*[^<]*>");
        }

        /// <summary>
        /// 通过对比属性值以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="属性名">属性名</param>
        /// <param name="属性值">期待属性值</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过属性获取元素(this HtmlElementCollection o, string 属性名, string 属性值)
        {
            return 通过属性获取元素(o, 属性名, q => q == 属性值);
        }

        /// <summary>
        /// 通过表达式针对属性值进行判断以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="属性名">属性名</param>
        /// <param name="判断表达式">返回真即表示获取</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过属性获取元素(this HtmlElementCollection o, string 属性名, Predicate<string> 判断表达式)
        {
            return 通过表达式获取元素(o, q => 判断表达式(q.GetAttribute(属性名)));
        }

        /// <summary>
        /// 通过正则表达式验证OuterHtml以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="正则表达式">正则表达式</param>
        /// <param name="表达式选项">表达式选项</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过HTML代码获取元素(this HtmlElementCollection o, string 正则表达式, RegexOptions 表达式选项)
        {
            return 通过HTML代码获取元素(o, q =>q!=null&& Regex.IsMatch(q, 正则表达式, 表达式选项));
        }

        /// <summary>
        /// 通过正则表达式验证OuterHtml以获取元素，默认采用不区分大小写的正则表达式选项
        /// </summary>
        /// <param name="o"></param>
        /// <param name="正则表达式">正则表达式</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过HTML代码获取元素(this HtmlElementCollection o, string 正则表达式)
        {
            return 通过HTML代码获取元素(o, 正则表达式, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 通过表达式针对OuterHtml进行判断以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="判断表达式">返回真即表示获取</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过HTML代码获取元素(this HtmlElementCollection o, Predicate<string> 判断表达式)
        {
            return 通过表达式获取元素(o, q => 判断表达式(q.OuterHtml));
        }

        /// <summary>
        /// 通过正则表达式验证InnerText以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="正则表达式">正则表达式</param>
        /// <param name="表达式选项">表达式选项</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过文本内容获取元素(this HtmlElementCollection o, string 正则表达式, RegexOptions 表达式选项)
        {
            return 通过文本内容获取元素(o, q => q != null && Regex.IsMatch(q, 正则表达式, 表达式选项));
        }

        /// <summary>
        /// 通过正则表达式验证InnerText以获取元素，默认采用不区分大小写的正则表达式选项
        /// </summary>
        /// <param name="o"></param>
        /// <param name="正则表达式">正则表达式</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过文本内容获取元素(this HtmlElementCollection o, string 正则表达式)
        {
            return 通过文本内容获取元素(o, 正则表达式, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 通过表达式针对InnerText进行判断以获取元素
        /// </summary>
        /// <param name="o"></param>
        /// <param name="判断表达式">返回真即表示获取</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过文本内容获取元素(this HtmlElementCollection o, Predicate<string> 判断表达式)
        {
            return 通过表达式获取元素(o,q => 判断表达式(q.InnerText));
        }

        /// <summary>
        /// 通过指定的表达式获取元素
        /// </summary>
        /// <param name="判断表达式">返回真即表示获取</param>
        /// <returns>元素集合</returns>
        public static IEnumerable<HtmlElement> 通过表达式获取元素(this HtmlElementCollection o, Predicate<HtmlElement> 判断表达式)
        {
            return o.Cast<HtmlElement>().Where(q => 判断表达式(q));
        }

        /// <summary>
        /// 全部选定所有项
        /// </summary>
        public static void 全部选定(this CheckedListBox c)
        {
            for (int i = 0; i < c.Items.Count; i++)
            {
                c.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// 全部取消选定所有项
        /// </summary>
        public static void 全部取消选定(this CheckedListBox c)
        {
            for (int i = 0; i < c.Items.Count; i++)
            {
                c.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// 反向选定所有项
        /// </summary>
        public static void 反向选定(this CheckedListBox c)
        {
            for (int i = 0; i < c.Items.Count; i++)
            {
                c.SetItemChecked(i, !c.GetItemChecked(i));
            }
        }

        /// <summary>
        /// 根据选定状态列表中的值，逐一设定各列表项的选定状态
        /// </summary>
        /// <param name="选定状态列表">包含所有列表项对应的选定状态的列表</param>
        public static void 自设选定(this CheckedListBox c, IEnumerable<bool> 选定状态列表)
        {
            int x = 0;
            foreach (bool f in 选定状态列表)
            {
                c.SetItemChecked(x++, f);
            }
        }

        /// <summary>
        /// 根据选定项索引列表的值，设定指定索引处列表项的选定状态为已选定，其它处均设为未选定
        /// </summary>
        /// <param name="选定项索引列表">包含选定列表项的索引位置的列表</param>
        public static void 自设选定(this CheckedListBox c, IEnumerable<int> 选定项索引列表)
        {
            c.全部取消选定();
            foreach (int f in 选定项索引列表)
            {
                c.SetItemChecked(f, true);
            }
        }

        /// <summary>
        /// 将一个字典作为数据源加载到CheckedListBox，字典的键即为列表项的值，字典的值用以指示列表项是否被选定
        /// </summary>
        /// <typeparam name="类型">自定义类型</typeparam>
        /// <param name="数据源">数据源</param>
        public static void 数据源设定<类型>(this CheckedListBox c, Dictionary<类型, bool> 数据源)
        {
            var l = 数据源.Values.ToArray();
            c.数据源设定(数据源.Keys.ToList());
            c.自设选定(数据源.Values);
        }

        /// <summary>
        /// 将CheckedListBox的列表项及其选定状态作为字典返回，字典的键即为列表项的值，字典的值用以指示列表项是否被选定
        /// </summary>
        /// <typeparam name="类型">自定义类型</typeparam>
        /// <returns>字典</returns>
        public static Dictionary<类型, bool> 数据源获取<类型>(this CheckedListBox c)
        {
            var l = new Dictionary<类型, bool>();
            for (int i = 0; i < c.Items.Count; i++)
            {
                l.Add((类型)c.Items[i], c.GetItemChecked(i));
            }
            return l;
        }

        /// <summary>
        /// 对控件进行数据绑定.
        /// </summary>
        /// <param name="控件">要绑定的控件</param>
        /// <param name="控件属性">要绑定的控件属性</param>
        /// <param name="对象">绑定的目标对象</param>
        /// <param name="对象属性">要绑定的对象属性</param>
        public static void 数据绑定(this Control 控件, string 控件属性, object 对象, string 对象属性)
        {
            控件.DataBindings.Clear();
            控件.DataBindings.Add(控件属性, 对象, 对象属性);
        }

        /// <summary>
        /// 对控件进行数据绑定.
        /// </summary>
        /// <param name="控件">要绑定的控件</param>
        /// <param name="控件属性">要绑定的控件属性</param>
        /// <param name="对象">绑定的目标对象</param>
        /// <param name="对象属性">要绑定的对象属性</param>
        /// <param name="格式化规则">格式化规则,如格式化日期的规则"yyyy年M月d日 HH:mm:ss"</param>
        /// <param name="数据源更新模式">指示数据源何时进行同步更新</param>
        public static void 数据绑定(this Control 控件, string 控件属性, object 对象, string 对象属性, string 格式化规则, DataSourceUpdateMode 数据源更新模式)
        {
            控件.DataBindings.Clear();
            控件.DataBindings.Add(控件属性, 对象, 对象属性, true, 数据源更新模式);
            控件.DataBindings[0].FormatString = 格式化规则;
        }

        /// <summary>
        /// 对控件进行数据绑定.
        /// </summary>
        /// <param name="控件">要绑定的控件</param>
        /// <param name="控件属性">要绑定的控件属性</param>
        /// <param name="对象">绑定的目标对象</param>
        /// <param name="对象属性">要绑定的对象属性</param>
        /// <param name="格式化规则">格式化规则,如格式化日期的规则"yyyy年M月d日 HH:mm:ss"</param>
        public static void 数据绑定(this Control 控件, string 控件属性, object 对象, string 对象属性, string 格式化规则)
        {
            控件.DataBindings.Clear();
            控件.DataBindings.Add(控件属性, 对象, 对象属性, true);
            控件.DataBindings[0].FormatString = 格式化规则;
        }

        /// <summary>
        /// 为列表类控件指定数据源
        /// </summary>
        /// <param name="控件">列表控件</param>
        /// <param name="对象">绑定对象</param>
        /// <param name="显示属性">用于显示的对象属性</param>
        /// <param name="值属性">用于值的对象属性</param>
        public static void 数据源设定(this ListControl 控件, object 对象, string 显示属性, string 值属性)
        {
            控件.DataSource = null;
            控件.DisplayMember = null;
            控件.ValueMember = null;
            控件.DataSource = 对象;
            控件.DisplayMember = 显示属性;
            控件.ValueMember = 值属性;
        }

        /// <summary>
        /// 为列表类控件指定数据源
        /// </summary>
        /// <param name="控件">列表控件</param>
        /// <param name="对象">绑定对象</param>
        public static void 数据源设定(this ListControl 控件, object 对象)
        {
            控件.DataSource = null;
            控件.DataSource = 对象;
        }

        /// <summary>
        /// 将数据源绑定到枚举类型
        /// </summary>
        /// <param name="控件">列表控件</param>
        /// <param name="默认值">枚举的默认值</param>
        public static void 数据源设定(this ListControl 控件, Enum 默认值)
        {
            var t = Enum.GetValues(默认值.GetType());
            数据源设定(控件, t);
            for (int i = 0; i < t.Length; i++)
            {
                if (t.GetValue(i).Equals(默认值))
                {
                    控件.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 在指定时间内执行Application.DoEvent()方法，以制造延时
        /// </summary>
        public static void 响应事件(this Control o, int 延迟毫秒数)
        {
            var d = DateTime.Now.AddMilliseconds(延迟毫秒数);
            while (DateTime.Now < d) Application.DoEvents();
            //DoEvents(o, 0, f => true, 延迟毫秒数);
        }

        /// <summary>
        /// 制造一个介于最小延迟时间和最大延迟时间之间的延迟，期间如果判断表达式不成立，则延迟中止。返回值指示延迟是否因判断表达式不成立而终止。
        /// </summary>
        /// <param name="判断表达式">如果表达式返回false，延迟将中止。</param>
        public static bool 响应事件(this Control o, int 最小延迟毫秒数, Predicate<Control> 判断表达式, int 最大延迟毫秒数)
        {
            var min = DateTime.Now.AddMilliseconds(最小延迟毫秒数);
            var max = DateTime.Now.AddMilliseconds(最大延迟毫秒数);
            //while (DateTime.Now < min || (判断表达式(o) && DateTime.Now < max)) Application.DoEvents();
            while (DateTime.Now < min) Application.DoEvents();
            while (DateTime.Now < max)
            {
                if (判断表达式(o)) Application.DoEvents();
                else return true;
            }
            return false;
        }

        /// <summary>
        /// 在后台制造延迟，不阻塞主线程，延迟后执行指定内容
        /// </summary>
        /// <param name="延迟毫秒数">延迟时间</param>
        /// <param name="执行内容">执行方法体，传递的参数即是调用延迟执行方法的对象</param>
        public static void 延迟执行(this Control o, int 延迟毫秒数, Action<Control> 执行内容)
        {
            定时器 d = new 定时器(延迟毫秒数);
            d.执行完毕事件 += new 定时器.执行完毕代理((s, op, b) => 执行内容(o));
            d.执行(null);
        }

        /// <summary>
        /// 递归计算控件与其所在容器的距离，将值累加至屏幕左上角或顶端控件。
        /// </summary>
        /// <param name="计算至屏幕左上角">指定顶端为屏幕还是顶端控件（通常为Form窗体）</param>
        public static Point 获取距顶端距离(this Control o, bool 计算至屏幕左上角)
        {
            //Func<Control,Point> func=q=>new Point(q.Location.X + (q.Size.Width - q.ClientSize.Width) / 2, q.Location.Y + (q.Size.Height - q.ClientSize.Height) / 2);
            Func<Control, Point> func = q =>
            {
                if (q.Parent == null)
                    return new Point(q.Location.X + (q.Size.Width - q.ClientSize.Width) / 2, q.Location.Y + (q.Size.Height - q.ClientSize.Height - SystemInformation.CaptionHeight) / 2 + SystemInformation.CaptionHeight);
                else return new Point(q.Location.X + (q.Size.Width - q.ClientSize.Width) / 2, q.Location.Y + (q.Size.Height - q.ClientSize.Height) / 2);
            };
            var p = func(o);
            foreach (var f in o.RecursionSelect(q => q.Parent, q => q != null && (计算至屏幕左上角 ? true : q.Parent != null)))
            {
                //var np = f.Parent == null ? new Point(f.Location.X + (f.Size.Width - f.ClientSize.Width) / 2, f.Location.Y + (f.Size.Height - f.ClientSize.Height) / 2) : f.Location;
                var np = func(f);
                p = new Point(p.X + np.X, p.Y + np.Y);
            }
            return p;
        }

        /// <summary>
        /// 判断指定的屏幕位置点是否位于此控件所在区域之中
        /// </summary>
        /// <param name="边界修正">根据此值对判定区域进行修正，适用于判定存在误差的情况。判定时会将判定区域左上角坐标与修正值左上方进行减运算，右下角则是进行加运算。仅对左上方进行边界修正时，右下点坐标不会改变，反之亦然。</param>
        public static bool 验证坐标点是否位于控件区域中(this Control o, Point 屏幕位置点, Padding 边界修正)
        {
            var s = o.获取距顶端距离(true);
            s = new Point(s.X - 边界修正.Left, s.Y - 边界修正.Top);
            var e = new Point(s.X + o.Size.Width + 边界修正.Right, s.Y + o.Size.Height + 边界修正.Bottom);
            return 屏幕位置点.X.IsBetween(s.X, e.X) && 屏幕位置点.Y.IsBetween(s.Y, e.Y);
        }

        /// <summary>
        /// 判断指定的屏幕位置点是否位于此控件所在区域之中
        /// </summary>
        public static bool 验证坐标点是否位于控件区域中(this Control o, Point 屏幕位置点)
        {
            return o.验证坐标点是否位于控件区域中(屏幕位置点, new Padding());
        }

        /// <summary>
        /// 判断鼠标所在屏幕位置点是否位于此控件所在区域之中
        /// </summary>
        public static bool 验证鼠标是否位于控件区域中(this Control o)
        {
            return o.验证坐标点是否位于控件区域中(Cursor.Position);
        }

        /// <summary>
        /// 判断鼠标所在屏幕位置点是否位于此控件所在区域之中
        /// </summary>
        /// <param name="边界修正">根据此值对判定区域进行修正，适用于判定存在误差的情况。判定时会将判定区域左上角坐标与修正值左上方进行减运算，右下角则是进行加运算。仅对左上方进行边界修正时，右下点坐标不会改变，反之亦然。</param>
        public static bool 验证鼠标是否位于控件区域中(this Control o, Padding 边界修正)
        {
            return o.验证坐标点是否位于控件区域中(Cursor.Position, 边界修正);
        }

        /// <summary>
        /// 将图像转换为WPF中使用的BitmapImage对象
        /// </summary>
        public static System.Windows.Media.Imaging.BitmapImage 转换为WPF的BitmapImage对象(this Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            System.Windows.Media.Imaging.BitmapImage bImage = new System.Windows.Media.Imaging.BitmapImage();
            bImage.BeginInit();
            bImage.StreamSource = new MemoryStream(ms.ToArray());
            bImage.EndInit();
            ms.Dispose();
            return bImage;
        }
    }
}
