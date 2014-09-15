using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.IO;
using Core.Environment;

namespace Core.类库.桌面程序
{
    /// <summary>
    /// 用于表示程序的配置文件
    /// </summary>
    [Serializable]
    public abstract class 配置基类<T> : 可序列化基类<T>, INotifyPropertyChanged where T : 配置基类<T>
    {
        public 配置基类()
        {

        }

        public void 保存()
        {
            保存(默认存储路径);
        }

        public void 保存(string 文件路径)
        {
            _上次保存时间 = DateTime.Now;
            _版本++;
            this.序列化为二进制文件(文件路径);
        }

        public static T 读取()
        {
            return 读取(默认存储路径);
        }

        public static T 读取(string 文件路径)
        {
            if (File.Exists(默认存储路径))
            {
                return 反序列化自二进制文件(文件路径);
            }
            else return null;
        }

        public static string 默认存储路径
        {
            get
            {
                return Path.Combine(ClientEnvironment变量.程序所在目录路径, ClientEnvironment变量.程序名称 + ".cfg");
            }
        }

        public DateTime 创建时间
        {
            get
            {
                return _创建时间;
            }
        }
        private DateTime _创建时间 = DateTime.Now;

        /// <summary>
        /// 记录配置被保存的次数
        /// </summary>
        public ulong 版本
        {
            get
            {
                return _版本;
            }
        }
        private ulong _版本;

        /// <summary>
        /// 记录配置初始化完毕后的所有属性总变更次数（需要属性在变更时触发OnPropertyChanged方法）
        /// </summary>
        public ulong 变更次数
        {
            get
            {
                return _变更次数;
            }
        }
        private ulong _变更次数;

        /// <summary>
        /// 记录上次保存文件的时间
        /// </summary>
        public DateTime 上次保存时间
        {
            get
            {
                return _上次保存时间;
            }
        }
        private DateTime _上次保存时间;

        public 配置基类(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _上次保存时间 = info.GetDateTime("上次保存时间");
            _创建时间 = info.GetDateTime("创建时间");
            _版本 = info.GetUInt64("版本");
            _变更次数 = info.GetUInt64("变更次数");
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("上次保存时间", 上次保存时间);
            info.AddValue("变更次数", 变更次数);
            info.AddValue("版本", 版本);
            info.AddValue("创建时间", 创建时间);
        }

        #region 实现INotifyPropertyChanged

        protected void OnPropertyChanged(string name)
        {
            if (!处于序列化状态中) _变更次数++;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
