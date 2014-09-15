using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Core.IO
{
    /// <summary>
    /// 为对象提供XML序列化、二进制序列化及反序列化功能
    /// 实现了ICloneable接口
    /// </summary>
    /// <typeparam name="T">继承类的类型，用以判定XML序列化类型</typeparam>
    [Serializable()]
    public abstract class 可序列化基类<T> : ICloneable, ISerializable where T : 可序列化基类<T>
    {
        public 可序列化基类()
        {

        }

        /// <summary>
        /// 指示对象是否未脱离序列化状态，不可被正常使用，可以此来避免状态读写期间触发不必要的更新事件。
        /// </summary>
        protected bool 处于序列化状态中
        {
            get
            {
                return _处于序列化状态中;
            }
            set
            {
                _处于序列化状态中 = value;
                if (!value && 反序列化完成事件 != null)
                {
                    反序列化完成事件();
                }
            }
        }
        private bool _处于序列化状态中 = false;

        /// <summary>
        /// 反序列化完成事件代理
        /// </summary>
        public delegate void 反序列化完成代理();

        /// <summary>
        /// 反序列化完成事件
        /// </summary>
        public event 反序列化完成代理 反序列化完成事件;

        /// <summary>
        /// 进行二进制序列化
        /// </summary>
        /// <returns>序列化代码</returns>
        protected virtual byte[] 序列化为二进制数据()
        {
            处于序列化状态中 = true;
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            ser.Serialize(mStream, this);
            byte[] buf = mStream.ToArray();
            mStream.Close();
            return buf;
        }

        /// <summary>
        /// 进行二进制序列化
        /// </summary>
        /// <param name="文件路径">保存路径</param>
        protected virtual void 序列化为二进制文件(string 文件路径)
        {
            var s = new BinaryWriter(new FileStream(文件路径, FileMode.OpenOrCreate));
            s.Write(序列化为二进制数据());
            s.Close();
        }

        /// <summary>
        /// 进行XML序列化
        /// </summary>
        /// <returns>XML序列化代码</returns>
        protected virtual string 序列化为XML数据()
        {
            处于序列化状态中 = true;
            XmlSerializer xmlSerializer = new XmlSerializer(GetType());
            MemoryStream stream = new MemoryStream();
            xmlSerializer.Serialize(stream, this);
            byte[] buf = stream.ToArray();
            string xml = Encoding.UTF8.GetString(buf);
            stream.Close();
            return xml;
        }

        /// <summary>
        /// 进行XML序列化
        /// </summary>
        /// <param name="文件路径">保存路径</param>
        protected virtual void 序列化为XML文件(string 文件路径)
        {
            var s = new StreamWriter(文件路径, false, Encoding.UTF8);
            s.Write(序列化为XML数据());
            s.Close();
        }

        /// <summary>
        /// 进行二进制反序列化
        /// </summary>
        /// <param name="二进制数据">二进制序列化代码</param>
        /// <returns>反序列化后的对象，若失败则返回null</returns>
        protected static T 反序列化自二进制数据(byte[] 二进制数据)
        {
            BinaryFormatter ser = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream(二进制数据);
            T o = (T)ser.Deserialize(mStream);
            mStream.Close();
            o.处于序列化状态中 = false;
            return o;
        }

        /// <summary>
        /// 进行二进制反序列化
        /// </summary>
        /// <param name="文件路径">读取路径</param>
        /// <returns>反序列化后的对象，若失败则返回null</returns>
        protected static T 反序列化自二进制文件(string 文件路径)
        {
            var s = new BinaryReader(new FileStream(文件路径, FileMode.Open));
            var o = 反序列化自二进制数据(s.ReadBytes((int)s.BaseStream.Length));
            s.Close();
            return o;
        }

        /// <summary>
        /// 进行XML反序列化
        /// </summary>
        /// <param name="XML数据">XML序列化代码</param>
        /// <returns>反序列化后的对象，若失败则返回null</returns>
        protected static T 反序列化自XML数据(string XML数据)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            byte[] buf = Encoding.UTF8.GetBytes(XML数据);
            MemoryStream stream = new MemoryStream(buf);
            T o = (T)xmlSerializer.Deserialize(stream);
            o.处于序列化状态中 = false;
            return o;
        }

        /// <summary>
        /// 进行XML反序列化
        /// </summary>
        /// <param name="文件路径">读取路径</param>
        /// <returns>反序列化后的对象，若失败则返回null</returns>
        protected static T 反序列化自XML文件(string 文件路径)
        {
            var s = new StreamReader(文件路径, Encoding.UTF8);
            var o = 反序列化自XML数据(s.ReadToEnd());
            s.Close();
            return o;
        }

        /// <summary>
        /// 尝试使用二进制数据进行反序列化
        /// </summary>
        /// <param name="二进制数据">序列化二进制数据</param>
        /// <param name="输出对象">返回对象引用</param>
        /// <returns>反序列化是否成功</returns>
        protected static bool 尝试二进制反序列化(byte[] 二进制数据, ref T 输出对象)
        {
            try
            {
                输出对象 = 反序列化自二进制数据(二进制数据);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 尝试使用XML数据进行反序列化
        /// </summary>
        /// <param name="XML数据">序列化XML数据</param>
        /// <param name="输出对象">返回对象引用</param>
        /// <returns>反序列化是否成功</returns>
        protected static bool 尝试XML反序列化(string XML数据, ref T 输出对象)
        {
            try
            {
                输出对象 = 反序列化自XML数据(XML数据);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 完成对象的浅复制
        /// </summary>
        /// <returns>对象的副本</returns>
        public virtual T 复制()
        {
            return (T)Clone();
        }

        #region ICloneable 成员
        /// <summary>
        /// 完成对象的浅复制
        /// </summary>
        /// <returns>对象的副本</returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion


        #region ISerializable 成员

        public 可序列化基类(SerializationInfo info, StreamingContext context)
        {
            处于序列化状态中 = true;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {

        }

        #endregion
    }
}
