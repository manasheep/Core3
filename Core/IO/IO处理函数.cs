using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core.Text;
using System.Runtime.InteropServices;
using System.Web;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace Core.IO
{
    public static class IO处理函数
    {
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="文件路径">文件存放路径</param>
        /// <returns>MD5值</returns>
        public static string 计算MD5值(string 文件路径)
        {
            return HashHelper.ComputeMD5(文件路径);
        }

        /// <summary>
        /// 计算文件的CRC32值
        /// </summary>
        /// <param name="文件路径">文件存放路径</param>
        /// <returns>CRC32值</returns>
        public static string 计算CRC32值(string 文件路径)
        {
            return HashHelper.ComputeCRC32(文件路径);
        }

        /// <summary>
        /// 计算文件的SHA1值
        /// </summary>
        /// <param name="文件路径">文件存放路径</param>
        /// <returns>SHA1值</returns>
        public static string 计算SHA1值(string 文件路径)
        {
            return HashHelper.ComputeSHA1(文件路径);
        }

        /// <summary>
        /// 读取文件的二进制数据
        /// </summary>
        public static byte[] 读取二进制数据(string 文件路径)
        {
            return 读取二进制数据(new FileInfo(文件路径));
        }

        /// <summary>
        /// 读取文件的二进制数据
        /// </summary>
        public static byte[] 读取二进制数据(this FileInfo 文件信息)
        {
            var b = new BinaryReader(文件信息.OpenRead());
            var l = new byte[b.BaseStream.Length];
            for (long i = 0; i < b.BaseStream.Length; i++)
            {
                l[i] = b.ReadByte();
            }
            b.Close();
            return l;
        }

        /// <summary>
        /// 将二进制文件数据写入文件
        /// </summary>
        public static void 写入二进制数据(string 文件路径, byte[] 数据)
        {
            写入二进制数据(new FileInfo(文件路径), 数据);
        }

        /// <summary>
        /// 将二进制文件数据写入文件
        /// </summary>
        public static void 写入二进制数据(this FileInfo 文件信息, byte[] 数据)
        {
            var b = new BinaryWriter(文件信息.Open(FileMode.OpenOrCreate));
            b.Write(数据);
            b.Close();
        }

        /// <summary>
        /// 将多个对象的字符串形式写入文件
        /// </summary>
        /// <param name="路径">文件保存路径</param>
        /// <param name="编码">编码格式</param>
        /// <param name="对象">待写入的一个或多个对象</param>
        public static void 写入到文件(string 路径, Encoding 编码, params object[] 对象)
        {
            写入到文件(路径, 编码, false, 对象);
        }

        /// <summary>
        /// 将多个对象的字符串形式写入文件
        /// </summary>
        /// <param name="路径">文件保存路径</param>
        /// <param name="编码">编码格式</param>
        /// <param name="对象">待写入的一个或多个对象</param>
        /// <param name="追加">是否追加到最后</param>
        public static void 写入到文件(string 路径, Encoding 编码, bool 追加, params object[] 对象)
        {
            var S = new StringBuilder();
            foreach (var f in 对象)
            {
                if (S.Length > 0) S.AppendLine("");
                S.Append(f.ToString());
            }
            var s = new StreamWriter(路径, 追加, 编码);
            s.Write(S.ToString());
            s.Close();
        }

        /// <summary>
        /// 从文件中读出文本内容
        /// </summary>
        /// <param name="路径">要打开的文件路径</param>
        /// <param name="编码">编码格式</param>
        /// <returns>内容</returns>
        public static string 读取自文件(string 路径, Encoding 编码)
        {
            var s = new StreamReader(路径, 编码);
            var t = s.ReadToEnd();
            s.Close();
            return t;
        }

        /// <summary>
        ///  验证路径名中是否包含不允许的字符，包含则返回false。
        /// </summary>
        /// <param name="字符串">路径名字符串</param>
        /// <returns>验证结果</returns>
        public static bool 验证是否符合路径名规则(this string 字符串)
        {
            return 字符串.验证是否包含指定字符(false, Path.GetInvalidPathChars()) == false;
        }

        /// <summary>
        ///  验证文件名中是否包含不允许的字符，包含则返回false。
        /// </summary>
        /// <param name="字符串">文件名字符串</param>
        /// <returns>验证结果</returns>
        public static bool 验证是否符合文件名规则(this string 字符串)
        {
            return 字符串.验证是否包含指定字符(false, Path.GetInvalidFileNameChars()) == false;
        }

        /// <summary>
        /// 获取指定类型所在程序集的所有嵌入资源名称
        /// </summary>
        /// <param name="类型">依据此类型判断资源所在程序集</param>
        /// <returns>嵌入资源名称列表</returns>
        public static string[] 获取程序集嵌入资源列表(this Type 类型)
        {
            var a = 类型.Assembly;
            return a.GetManifestResourceNames();
        }

        /// <summary>
        ///  获取指定类型所在程序集内的指定嵌入资源流
        /// </summary>
        /// <param name="类型">依据此类型判断资源所在程序集</param>
        /// <param name="资源名称">嵌入资源名称，格式为：(程序集默认命名空间).(目录名).(文件名)</param>
        /// <returns>嵌入资源文件的流</returns>
        public static Stream 获取程序集嵌入资源流(this Type 类型, string 资源名称)
        {
            var a = 类型.Assembly;
            return a.GetManifestResourceStream(资源名称);
        }

        /// <summary>
        /// 使用异或算法对文件进行加密，同密钥运行第二次即为解密
        /// </summary>
        /// <param name="文件路径">文件路径</param>
        /// <param name="密钥">密钥</param>
        public static void 异或加密(string 文件路径, string 密钥)
        {
            var b = File.ReadAllBytes(文件路径);
            for (var i = 0; i < b.Length; i++)
            {
                b[i] = (byte)(b[i] ^ 密钥[i % 密钥.Length]);
                i++;
            }
            File.WriteAllBytes(文件路径, b);
        }

        /// <summary>
        /// 在指定的文件列表中删除多余指定数量的旧文件
        /// </summary>
        /// <param name="文件列表">要处理的文件列表</param>
        /// <param name="保留数量">要保留的新文件数量</param>
        public static void 删除额外的旧文件(string[] 文件列表, int 保留数量)
        {
            for (var i = 保留数量; i < 文件列表.Length; i++)
            {
                var MinDateFile = new FileInfo(文件列表[0]);
                foreach (var f in 文件列表)
                {
                    var F = new FileInfo(f);
                    if (F.LastWriteTime < MinDateFile.LastWriteTime) MinDateFile = F;
                }
                MinDateFile.Delete();
            }
        }

        /// <summary>
        /// 在指定的文件列表中删除多余指定数量的旧文件
        /// </summary>
        /// <param name="文件列表">要处理的文件列表</param>
        /// <param name="保留数量">要保留的新文件数量</param>
        public static void 删除额外的旧文件(FileInfo[] 文件列表, int 保留数量)
        {
            for (var i = 保留数量; i < 文件列表.Length; i++)
            {
                var MinDateFile = 文件列表[0];
                foreach (var f in 文件列表)
                {
                    var F = f;
                    if (F.LastWriteTime < MinDateFile.LastWriteTime) MinDateFile = F;
                }
                MinDateFile.Delete();
            }
        }

        #region  删除文件到回收站代码

        private const int FO_DELETE = 0x3;
        private const ushort FOF_NOCONFIRMATION = 0x10;
        private const ushort FOF_ALLOWUNDO = 0x40;

        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int SHFileOperation([In, Out] _SHFILEOPSTRUCT str);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class _SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public UInt32 wFunc;
            public string pFrom;
            public string pTo;
            public UInt16 fFlags;
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        /// <summary>
        /// 将文件删除到回收站中
        /// </summary>
        /// <param name="path">文件路径</param>
        public static int 删除文件到回收站(string path)
        {
            var pm = new _SHFILEOPSTRUCT();
            pm.wFunc = FO_DELETE;
            pm.pFrom = path + '\0';
            pm.pTo = null;
            pm.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            return SHFileOperation(pm);
        }

        #endregion

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="路径">文件路径</param>
        /// <returns>是否存在</returns>
        public static bool 判断文件是否存在(string 路径)
        {
            return File.Exists(路径);
        }

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="路径">目录路径</param>
        /// <returns>是否存在</returns>
        public static bool 判断目录是否存在(string 路径)
        {
            return Directory.Exists(路径);
        }

        /// <summary>
        /// 根据指定路径创建目录,如果该目录已存在则不进行任何操作
        /// </summary>
        /// <param name="路径">目录路径</param>
        public static void 创建目录(string 路径)
        {
            if (!判断文件是否存在(路径)) Directory.CreateDirectory(路径);
        }

        /// <summary>
        /// 获取指定路径的文件扩展名的小写形式
        /// </summary>
        /// <param name="文件路径">文件路径</param>
        public static string 获取文件扩展名(string 文件路径)
        {
            return Path.GetExtension(文件路径).ToLower();
        }

        /// <summary>
        /// 获取指定路径的文件名的小写形式
        /// </summary>
        /// <param name="文件路径">文件路径</param>
        public static string 获取文件名(string 文件路径)
        {
            return Path.GetFileName(文件路径).ToLower();
        }

        /// <summary>
        /// 将指定目录下的所有文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="操作目录">要操作的目录</param>
        /// <param name="目标目录">目标目录</param>
        public static void 复制目录文件(string 操作目录, string 目标目录)
        {
            复制目录文件(new DirectoryInfo(操作目录), new DirectoryInfo(目标目录));
        }

        /// <summary>
        /// 检测此目录及其子目录是否为空目录，并删除所有的空目录
        /// </summary>
        public static void 删除空目录(string 操作目录)
        {
            删除空目录(new DirectoryInfo(操作目录));
        }

        /// <summary>
        /// 检测此目录及其子目录是否为空目录，并删除所有的空目录
        /// </summary>
        public static void 删除空目录(DirectoryInfo 操作目录)
        {
            foreach (var f in 操作目录.GetDirectories())
            {
                删除空目录(f);
            }
            if (操作目录.GetFiles().Length == 0 && 操作目录.GetDirectories().Length == 0)
                操作目录.Delete();
        }

        /// <summary>
        /// 获得指定相对路径的绝对路径
        /// </summary>
        /// <param name="相对路径">指定的相对路径</param>
        /// <returns>绝对路径</returns>
        public static string 获取绝对路径(string 相对路径)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(相对路径);
            }
            else //非web程序引用
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 相对路径);
            }
        }

        /// <summary>
        /// 将指定目录下的所有文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="操作目录">要操作的目录</param>
        /// <param name="目标目录">目标目录</param>
        public static void 复制目录文件(this DirectoryInfo 操作目录, DirectoryInfo 目标目录)
        {
            if (!目标目录.Exists) 目标目录.Create();
            foreach (var f in 操作目录.GetFiles())
            {
                f.CopyTo(目标目录.FullName.AsPathString().Combine(f.Name), true);
            }
            foreach (var d in 操作目录.GetDirectories())
            {
                复制目录文件(d, new DirectoryInfo(目标目录.FullName.AsPathString().Combine(d.Name)));
            }
        }

        /// <summary>
        /// 将指定目录下的所有符合条件的文件及子目录复制到目标目录中
        /// </summary>
        /// <param name="操作目录">要操作的目录</param>
        /// <param name="目标目录">目标目录</param>
        /// <param name="目录筛选">判断目录是否应当被复制的方法</param>
        /// <param name="文件筛选">判断文件是否应当被复制的方法</param>
        public static void 复制目录文件(this DirectoryInfo 操作目录, DirectoryInfo 目标目录, Predicate<FileInfo> 文件筛选, Predicate<DirectoryInfo> 目录筛选)
        {
            if (!目标目录.Exists) 目标目录.Create();
            foreach (var f in 操作目录.GetFiles())
            {
                if (文件筛选(f))
                {
                    f.CopyTo(目标目录.FullName.AsPathString().Combine(f.Name), true);
                }
            }
            foreach (var d in 操作目录.GetDirectories())
            {
                if (目录筛选(d))
                {
                    复制目录文件(d, new DirectoryInfo(目标目录.FullName.AsPathString().Combine(d.Name)), 文件筛选, 目录筛选);
                }
            }
        }

        /// <summary>
        /// 删除目录中的所有文件
        /// </summary>
        /// <param name="操作目录">要操作的目录</param>
        public static void 删除目录文件(this DirectoryInfo 操作目录)
        {
            foreach (var f in 操作目录.GetFiles())
            {
                f.Delete();
            }
            foreach (var f in 操作目录.GetDirectories())
            {
                删除目录文件(f);
                f.Delete();
            }
        }

        /// <summary>
        /// 删除目录中的所有文件
        /// </summary>
        /// <param name="操作目录">要操作的目录</param>
        public static void 删除目录文件(string 操作目录)
        {
            删除目录文件(new DirectoryInfo(操作目录));
        }

        /// <summary>
        /// 判断是否为目录
        /// </summary>
        /// <param name="文件路径">文件绝对路径</param>
        public static bool 判断是否为目录(string 文件路径)
        {
            var f = new FileInfo(文件路径);
            return f.Attributes.ToString().IndexOf("Directory") >= 0;
        }

        /// <summary>
        /// [已过时，应使用：IO.GZip]
        /// 以Zip格式压缩指定文件
        /// </summary>
        /// <param name="源文件">要压缩的文件路径</param>
        /// <param name="目标文件">压缩后保存的文件路径</param>
        public static void 压缩文件(string 源文件, string 目标文件)
        {
            if (!File.Exists(源文件)) throw new FileNotFoundException();
            using (var sourceStream = new FileStream(源文件, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[sourceStream.Length];
                var checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();
                using (var destinationStream = new FileStream(目标文件, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true))
                    {
                        compressedStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩指定的Zip文件
        /// </summary>
        /// <param name="源文件">要解压缩的文件路径</param>
        /// <param name="目标文件">解压缩后保存的文件路径</param>
        public static void 解压缩文件(string 源文件, string 目标文件)
        {
            if (!File.Exists(源文件)) throw new FileNotFoundException();
            using (var sourceStream = new FileStream(源文件, FileMode.Open))
            {
                var quartetBuffer = new byte[4];
                var position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                var checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                var buffer = new byte[checkLength + 100];
                using (var decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
                {
                    var total = 0;
                    for (var offset = 0; ; )
                    {
                        var bytesRead = decompressedStream.Read(buffer, offset, 100);
                        if (bytesRead == 0) break;
                        offset += bytesRead;
                        total += bytesRead;
                    }
                    using (var destinationStream = new FileStream(目标文件, FileMode.Create))
                    {
                        destinationStream.Write(buffer, 0, total);
                        destinationStream.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 比对文件A的内容是否与文件B的内容相同，如果文件不存在或处理过程中发生异常，则返回null
        /// </summary>
        /// <param name="A">文件A</param>
        /// <param name="B">文件B</param>
        /// <returns>比对结果</returns>
        public static bool? 比对文件是否相同(this FileInfo A, FileInfo B)
        {
            if (!A.Exists || !B.Exists) return null;
            if (A.Length != B.Length) return false;
            if (A.FullName == B.FullName) return true;
            var f1 = A.OpenRead();
            var f2 = B.OpenRead();
            try
            {
                var b1 = 0;
                var b2 = 0;
                do
                {
                    b1 = f1.ReadByte();
                    b2 = f2.ReadByte();
                    if (b1 != b2) return false;
                }
                while (b1 != -1);
                return true;
            }
            catch (Exception e) { e.Message.Trace(); return null; }
            finally { f1.Close(); f2.Close(); }
        }

        /// <summary>
        /// 将对象序列化并转为字符串
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>包含序列化内容的字符串</returns>
        public static string 序列化对象为Json字符串(this Object obj)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            var serializer = new DataContractJsonSerializer(obj.GetType());
            //实例化一个内存流，用于存放序列化后的数据
            var stream = new MemoryStream();
            //使用WriteObject序列化对象
            serializer.WriteObject(stream, obj);
            //写入内存流中
            var dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            //通过UTF8格式转换为字符串
            return Encoding.UTF8.GetString(dataBytes);
        }

        /// <summary>
        /// 泛型方法，从序列化Json字符串中读取指定类型的对象
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="jsonString">Json字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T 反序列化对象自Json字符串<T>(this string jsonString)
        {
            //实例化DataContractJsonSerializer对象，需要待序列化的对象类型
            var serializer = new DataContractJsonSerializer(typeof(T));
            //把Json传入内存流中保存
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象
            return (T)(serializer.ReadObject(stream));
        }

        /// <summary>
        /// 通过XML序列化和反序列化实现对象的克隆
        /// </summary>
        /// <returns>克隆后的对象</returns>
        public static T 序列化克隆<T>(this T 对象)
        {
            return 反序列化对象自XML字符串<T>(序列化对象为XML字符串(对象));
        }

        /// <summary>
        /// 将对象序列化并保存到文件
        /// </summary>
        /// <param name="对象">要序列化的对象</param>
        /// <param name="路径">文件保存的位置</param>
        public static void 序列化对象为XML文件(this object 对象, string 路径)
        {
            var XS = new XmlSerializer(对象.GetType());
            Stream stream = new FileStream(路径, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            var wt = XmlWriter.Create(stream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
            XS.Serialize(wt, 对象);
            stream.Close();
        }

        /// <summary>
        /// 将对象序列化到内存流
        /// </summary>
        /// <param name="对象">要序列化的对象</param>
        /// <returns>内存流</returns>
        public static MemoryStream 序列化对象为XML内存流(this object 对象)
        {
            var XS = new XmlSerializer(对象.GetType());
            var stream = new MemoryStream();
            var wt = XmlWriter.Create(stream, new XmlWriterSettings() { Encoding = Encoding.UTF8 });
            XS.Serialize(wt, 对象);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// 将对象序列化并转为字符串
        /// </summary>
        /// <param name="对象">要序列化的对象</param>
        /// <returns>包含序列化内容的字符串</returns>
        public static string 序列化对象为XML字符串(this object 对象)
        {
            var XS = new XmlSerializer(对象.GetType());
            var S = new StringBuilder();
            TextWriter TW = new StringWriter(S);
            XS.Serialize(TW, 对象);
            TW.Close();
            return S.ToString();
        }

        /// <summary>
        /// 泛型方法，从序列化XML文件中读取指定类型的对象
        /// </summary>
        /// <typeparam name="类型">反序列化类型</typeparam>
        /// <param name="路径">文件保存的位置</param>
        /// <returns>反序列化后的对象</returns>
        public static 类型 反序列化对象自XML文件<类型>(string 路径)
        {
            var XS = new XmlSerializer(typeof(类型));
            Stream stream = new FileStream(路径, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var reader = new XmlTextReader(stream);
            reader.Normalization = false;
            var OBJ = (类型)XS.Deserialize(reader);
            stream.Close();
            return OBJ;
        }

        /// <summary>
        /// 泛型方法，从序列化XML文件流中读取指定类型的对象
        /// </summary>
        /// <typeparam name="类型">反序列化类型</typeparam>
        /// <param name="数据流">数据输入流</param>
        /// <returns>反序列化后的对象</returns>
        public static 类型 反序列化对象自XML数据流<类型>(this Stream 数据流)
        {
            var XS = new XmlSerializer(typeof(类型));
            数据流.Position = 0;
            var reader = new XmlTextReader(数据流);
            reader.Normalization = false;
            var OBJ = (类型)XS.Deserialize(reader);
            return OBJ;
        }

        /// <summary>
        /// 泛型方法，从字符串中读取指定类型的对象
        /// </summary>
        /// <typeparam name="类型">反序列化类型</typeparam>
        /// <param name="序列化字串">包含序列化内容的字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static 类型 反序列化对象自XML字符串<类型>(string 序列化字串)
        {
            var XS = new XmlSerializer(typeof(类型));
            TextReader TR = new StringReader(序列化字串);
            var reader = new XmlTextReader(TR);
            reader.Normalization = false;
            var OBJ = (类型)XS.Deserialize(reader);
            TR.Close();
            return OBJ;
        }

        /// <summary>
        /// 将指定的对象序列化为二进制文件
        /// </summary>
        /// <param name="对象">要进行序列化的对象</param>
        /// <param name="路径">保存的文件路径</param>
        public static void 序列化对象为二进制文件(this object 对象, string 路径)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(路径, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, 对象);
            stream.Close();
        }

        /// <summary>
        ///  将指定的对象序列化为内存流
        /// </summary>
        /// <param name="对象">要进行序列化的对象</param>
        /// <returns>包含序列化信息的内存流</returns>
        public static MemoryStream 序列化对象为二进制内存流(this object 对象)
        {
            var MS = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(MS, 对象);
            MS.Seek(0, SeekOrigin.Begin);
            return MS;
        }

        /// <summary>
        /// 通过序列化的二进制文件反序列化对象
        /// </summary>
        /// <param name="路径">此前保存序列化文件的路径</param>
        /// <returns>反序列化后的对象</returns>
        public static object 反序列化对象自二进制文件(string 路径)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(路径, FileMode.Open, FileAccess.Read, FileShare.Read);
            var OBJ = formatter.Deserialize(stream);
            stream.Close();
            return OBJ;
        }

        /// <summary>
        /// 通过包含序列化的内存流反序列化对象.
        /// </summary>
        /// <param name="数据流">输入数据流</param>
        /// <returns>反序列化后的对象</returns>
        public static object 反序列化对象自二进制数据流(this Stream 数据流)
        {
            var bf = new BinaryFormatter();
            数据流.Position = 0;
            return bf.Deserialize(数据流);
        }

        /// <summary>
        /// 获取exe或dll等文件的版本号
        /// </summary>
        /// <param name="文件路径">文件所在路径</param>
        /// <returns>版本号</returns>
        public static string 获取文件版本(string 文件路径)
        {
            return FileVersionInfo.GetVersionInfo(文件路径).FileVersion;
        }

        /// <summary>
        /// 获取文件尺寸数值
        /// </summary>
        /// <param name="单位">尺寸单位</param>
        public static double 获取文件尺寸(this FileInfo f, 存储单位 单位)
        {
            return f.Length / (int)单位;
        }

        /// <summary>
        /// 获取文件尺寸，会自动调整显示单位，如：32B 或 21.7KB 或 1.2GB
        /// </summary
        public static string 获取文件尺寸(this FileInfo f)
        {
            if (f.Length < (int)存储单位.KB)
            {
                return f.Length + "B";
            }
            if (f.Length < (int)存储单位.MB)
            {
                return "{0:0.0}KB".FormatWith(f.Length / (double)存储单位.KB);
            }
            if (f.Length < (int)存储单位.GB)
            {
                return "{0:0.0}MB".FormatWith(f.Length / (double)存储单位.MB);
            }
            return "{0:0.0}GB".FormatWith(f.Length / (double)存储单位.GB);
        }

    }

}
