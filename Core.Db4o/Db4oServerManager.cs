using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;

namespace Core.Db4o
{
    /// <summary>
    /// Db4o服务器管理器
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Db4oServerManager : IDisposable
    {
        private IObjectServer _db4OServer;
        private readonly string _dbFilePath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbFilePath">数据库文件路径，通常使用Server.MapPath("/xxxx/xx.xx")函数获取到。</param>
        public Db4oServerManager(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
            OpenServer();
        }

        private void OpenServer()
        {
            IServerConfiguration serverConfig = Db4oClientServer.NewServerConfiguration();
            //serverConfig.Common.ObjectClass(typeof(人员)).MinimumActivationDepth(5);
            //serverConfig.Common.ObjectClass(typeof(人员)).CascadeOnUpdate(true);
            //serverConfig.Common.ObjectClass(typeof(人员)).CascadeOnDelete(true);
            serverConfig.Common.Queries.EvaluationMode(QueryEvaluationMode.Lazy);
            _db4OServer = Db4oClientServer.OpenServer(serverConfig, _dbFilePath, 0);
        }

        /// <summary>
        /// 开启一个客户端实例
        /// </summary>
        /// <returns>客户端实例</returns>
        public IObjectContainer OpenClient()
        {
        Begin:
            try
            {
                return _db4OServer.OpenClient();
            }
            catch
            {
                OpenServer();
                goto Begin;
            }
        }

        /// <summary>
        /// 创建一个服务器访问器对象。注意，对数据进行修改后必须释放此对象才能真正的将更改提交到服务器。
        /// </summary>
        /// <returns>一个服务器访问器对象</returns>
        public Db4oServerAccessor CreatAccessor()
        {
            return new Db4oServerAccessor(this);
        }

        /// <summary>
        /// 创建并访问一个服务器访问器对象。
        /// </summary>
        /// <param name="action">对服务器访问器对象的操作行为</param>
        public void Access(Action<Db4oServerAccessor> action)
        {
            using (var dba = CreatAccessor())
            {
                action(dba);
            }
        }

        /// <summary>
        /// 创建并访问一个服务器访问器对象，继而获得返回值。
        /// </summary>
        /// <param name="action">对服务器访问器对象的操作行为</param>
        /// <typeparam name="T">返回值类型</typeparam>
        public T AccessAndReturn<T>(Func<Db4oServerAccessor,T> action)
        {
            T v = default(T);
            using (var dba = CreatAccessor())
            {
                v= action(dba);
                //System.Diagnostics.Debug.WriteLine(v.ToString());
            }
            return v;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _db4OServer.Dispose();
        }

        #endregion
    }
}
