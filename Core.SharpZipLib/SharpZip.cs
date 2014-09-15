using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Core.IO
{
    public static class SharpZip
    {
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="输入压缩文件路径">要解压的压缩文件</param>
        /// <param name="解压缩目录">解压目标目录路径</param>
        public static void 解压缩(string 输入压缩文件路径, string 解压缩目录)
        {
            ZipInputStream inputStream = new ZipInputStream(File.OpenRead(输入压缩文件路径));
            ZipEntry theEntry;
            while ((theEntry = inputStream.GetNextEntry()) != null)
            {
                解压缩目录 += "/";
                string fileName = Path.GetFileName(theEntry.Name);
                string path = Path.GetDirectoryName(解压缩目录 + theEntry.Name) + "/";
                Directory.CreateDirectory(path);//生成解压目录
                if (fileName != String.Empty)
                {
                    FileStream streamWriter = File.Create(path + fileName);//解压文件到指定的目录 
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = inputStream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            inputStream.Close();
        }

        /// <summary>
        /// 压缩多个文件或目录
        /// </summary>
        /// <param name="输出压缩文件路径">压缩包文件路径</param>
        /// <param name="压缩级别">压缩程度，范围0-9，数值越大，压缩程序越高</param>
        ///// <param name="密码">密码，如不需要设密码只需传入null或空字符串</param>
        /// <param name="目录或文件">多个文件或目录</param>
        public static void 压缩(string 输出压缩文件路径, int 压缩级别, params string[] 目录或文件)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream outPutStream = new ZipOutputStream(File.Create(输出压缩文件路径));
            outPutStream.SetLevel(压缩级别); // 0 - store only to 9 - means best compression
            //if (!密码.IsNullOrEmpty()) outPutStream.Password = 密码;  //密码部分有问题，设了之后输入对密码也没法解压
            outPutStream.UseZip64 = UseZip64.Off;   //执行此命令可修正Android上解压缩出现的这个错误：08-14 09:08:38.111: D/outpath(17145): Cannot read local header version 45
            递归处理("", 目录或文件, crc, outPutStream);

            //GetAllDirectories(rootPath);
            //while (rootPath.LastIndexOf("\\") + 1 == rootPath.Length)//检查路径是否以"\"结尾
            //{
            //    rootPath = rootPath.Substring(0, rootPath.Length - 1);//如果是则去掉末尾的"\"
            //}
            //string rootMark = rootPath.Substring(0, rootPath.LastIndexOf("\\") + 1);//得到当前路径的位置，以备压缩时将所压缩内容转变成相对路径
            //foreach (string file in files)
            //{
            //    FileStream fileStream = File.OpenRead(file);//打开压缩文件
            //    byte[] buffer = new byte[fileStream.Length];
            //    fileStream.Read(buffer, 0, buffer.Length);
            //    ZipEntry entry = new ZipEntry(file.Replace(rootMark, string.Empty));
            //    entry.DateTime = DateTime.Now;
            //    // set Size and the crc, because the information
            //    // about the size and crc should be stored in the header
            //    // if it is not set it is automatically written in the footer.
            //    // (in this case size == crc == -1 in the header)
            //    // Some ZIP programs have problems with zip files that don't store
            //    // the size and crc in the header.
            //    entry.Size = fileStream.Length;
            //    fileStream.Close();
            //    crc.Reset();
            //    crc.Update(buffer);
            //    entry.Crc = crc.Value;
            //    outPutStream.PutNextEntry(entry);
            //    outPutStream.Write(buffer, 0, buffer.Length);
            //}
            //this.files.Clear();
            //foreach (string emptyPath in paths)
            //{
            //    ZipEntry entry = new ZipEntry(emptyPath.Replace(rootMark, string.Empty) + "/");
            //    outPutStream.PutNextEntry(entry);
            //}
            //this.paths.Clear();
            outPutStream.Finish();
            outPutStream.Close();
        }

        private static void 递归处理(string 当前路径, string[] 目录或文件, Crc32 crc, ZipOutputStream 输出流)
        {
            foreach (var f in 目录或文件)
            {
                //注：替换斜杠为反向是为了兼容Apache的ant.jar中的解压缩类，该类在解压缩时如果遇到“\”形式的路径就会报错
                var p = 当前路径.AsPathString().Combine(f.AsPathString().FileName).Replace(@"\", "/");
                if (Directory.Exists(f))
                {
                    ZipEntry entry = new ZipEntry(p + "/");
                    输出流.PutNextEntry(entry);
                    List<string> sub = new List<string>();
                    sub.AddRange(Directory.GetDirectories(f));
                    sub.AddRange(Directory.GetFiles(f));
                    递归处理(p, sub.ToArray(), crc, 输出流);
                }
                else
                {
                    FileStream fileStream = File.OpenRead(f);//打开压缩文件
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(p);
                    entry.DateTime = DateTime.Now;
                    // set Size and the crc, because the information
                    // about the size and crc should be stored in the header
                    // if it is not set it is automatically written in the footer.
                    // (in this case size == crc == -1 in the header)
                    // Some ZIP programs have problems with zip files that don't store
                    // the size and crc in the header.
                    //entry.Size = fileStream.Length;   //执行此命令会导致android解压缩出现错误：08-14 09:12:29.876: D/outpath(18602): Size mismatch
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    输出流.PutNextEntry(entry);
                    输出流.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
