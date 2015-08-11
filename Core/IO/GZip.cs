using System;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Core.IO
{
    /// <summary>
    /// Gzip 的摘要说明
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// 对目标文件夹进行压缩，将压缩结果保存为指定文件
        /// </summary>
        /// <param name="目标路径">要压缩的文件或文件夹路径，可指定多个</param>
        /// <param name="保存路径">压缩文件保存路径</param>
        public static void 压缩(string 保存路径, params string[] 目标路径)
        {
            ArrayList list = new ArrayList();
            foreach (TempFileName f in 转化文件列表(目标路径))
            {
                byte[] destBuffer = File.ReadAllBytes(f.FullName);
                SerializeFileInfo sfi = new SerializeFileInfo(f.Name, destBuffer);
                list.Add(sfi);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream s = new MemoryStream())
            {
                formatter.Serialize(s, list);
                s.Position = 0;
                CreateCompressFile(s, 保存路径);
            }
        }

        /// <summary>
        /// 将输入的数据流进行压缩，然后输出到一个内存流
        /// </summary>
        /// <param name="数据流">输入的数据流</param>
        /// <returns>输出的内存流</returns>
        public static MemoryStream 压缩(Stream 数据流)
        {
            var ms = new MemoryStream();
            using (GZipStream output = new GZipStream(ms, CompressionMode.Compress, true))
            {
                byte[] bytes = new byte[4096];
                int n;
                while ((n = 数据流.Read(bytes, 0, bytes.Length)) != 0)
                {
                    output.Write(bytes, 0, n);
                }
            }
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        private static List<TempFileName> 转化文件列表(string[] 文件列表)
        {
            var list = new List<TempFileName>();
            foreach (string file in 文件列表)
            {
                var f = Path.GetFullPath(file);
                string s = Path.GetDirectoryName(f.EndsWith(@"\") ? f.Substring(0, f.Length - 1) : f);
                if (Directory.Exists(f))
                {
                    foreach (string sf in Directory.GetFiles(f, "*", SearchOption.AllDirectories))
                    {
                        list.Add(new TempFileName(sf.Replace(s, ""), sf));
                    }
                }
                else list.Add(new TempFileName(f.Replace(s, ""), f));
            }
            return list;
        }

        /**/
        /// <summary>
        /// 对目标压缩文件解压缩，将内容解压缩到指定文件夹
        /// </summary>
        /// <param name="文件路径">压缩文件路径</param>
        /// <param name="解压缩目录路径">解压缩目录</param>
        public static void 解压缩(string 文件路径, string 解压缩目录路径)
        {
            解压缩目录路径 = Path.GetFullPath(解压缩目录路径 + @"\");
            using (Stream source = File.OpenRead(文件路径))
            {
                using (Stream destination = new MemoryStream())
                {
                    using (GZipStream input = new GZipStream(source, CompressionMode.Decompress, true))
                    {
                        if (!Directory.Exists(解压缩目录路径)) Directory.CreateDirectory(解压缩目录路径);
                        byte[] bytes = new byte[4096];
                        int n;
                        while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            destination.Write(bytes, 0, n);
                        }
                    }
                    destination.Flush();
                    destination.Position = 0;
                    DeSerializeFiles(destination, 解压缩目录路径);
                }
            }
        }

        /// <summary>
        /// 将输入的压缩数据进行解压缩，然后输出到一个内存流
        /// </summary>
        /// <param name="压缩数据流">输入的压缩数据流</param>
        /// <returns>输出的内存流</returns>
        public static MemoryStream 解压缩(Stream 压缩数据流)
        {
            var ms = new MemoryStream();

            using (var input = new GZipStream(压缩数据流, CompressionMode.Decompress, true))
            {
                byte[] bytes = new byte[4096];
                int n;
                while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                {
                    ms.Write(bytes, 0, n);
                }
            }

            //using (var input=new DeflateStream(压缩数据流, CompressionMode.Decompress, true))
            //{
            //    byte[] bytes = new byte[4096];
            //    int n;
            //    while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
            //    {
            //        ms.Write(bytes, 0, n);
            //    }
            //}
            ms.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 将输入的压缩数据进行解压缩，输出解压缩后的数据
        /// </summary>
        /// <param name="压缩数据">输入的压缩数据流</param>
        /// <returns>解压缩后的数据</returns>
        public static byte[] 解压缩(byte[] 压缩数据)
        {
            var input = new MemoryStream(压缩数据, false);
            input.Position = 0;
            var ms = 解压缩(input);
            return ms.ToBytes();
        }

        private static void DeSerializeFiles(Stream s, string dirPath)
        {
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(s);

            foreach (SerializeFileInfo f in list)
            {
                string newName = dirPath + f.FileName;
                if (!Directory.Exists(Path.GetDirectoryName(newName))) Directory.CreateDirectory(Path.GetDirectoryName(newName));
                using (FileStream fs = new FileStream(newName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(f.FileBuffer, 0, f.FileBuffer.Length);
                    fs.Close();
                }
            }
        }

        private static void CreateCompressFile(Stream source, string destinationName)
        {
            using (Stream destination = new FileStream(destinationName, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream output = new GZipStream(destination, CompressionMode.Compress))
                {
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = source.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        output.Write(bytes, 0, n);
                    }
                }
            }
        }

        class TempFileName
        {
            public TempFileName(string Name, string FullName)
            {
                this.Name = Name;
                this.FullName = FullName;
            }

            public string Name { get; set; }
            public string FullName { get; set; }
        }

        [Serializable]
        class SerializeFileInfo
        {
            public SerializeFileInfo(string name, byte[] buffer)
            {
                fileName = name;
                fileBuffer = buffer;
            }

            string fileName;
            public string FileName
            {
                get
                {
                    return fileName;
                }
            }

            byte[] fileBuffer;
            public byte[] FileBuffer
            {
                get
                {
                    return fileBuffer;
                }
            }
        }

    }
}
