using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;

namespace Core.Db4o
{
    /// <summary>
    /// Db4o服务器访问器。注意，对数据进行修改后必须释放此对象才能真正的将更改提交到服务器。建议配合using(var dbsa=new Db4oServerAccessor(...)){...}语句使用
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Db4oServerAccessor : IDisposable
    {
        // ReSharper disable once InconsistentNaming
        public IObjectContainer DBContainer { get; set; }

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="container">Db4o数据容器</param>
        //public Db4oServerAccessor(IObjectContainer container)
        //{
        //    DBContainer = container;
        //}

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="serverManager">Db4o服务器管理器</param>
        //public Db4oServerAccessor(Db4oServerManager serverManager)
        //    :this(serverManager.OpenClient())
        //{

        //}

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="server">Db4o服务器</param>
        //public Db4oServerAccessor(IObjectServer server)
        //    : this(server.OpenClient())
        //{

        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverManager">Db4o服务器管理器</param>
        public Db4oServerAccessor(Db4oServerManager serverManager)
        {
            DBContainer = serverManager.OpenClient();
        }

        public void Store(object o)
        {
            DBContainer.Store(o);
        }

        public IDb4oLinqQuery<T> Query<T>(Predicate<T> p)
        {
            return from T q in DBContainer where p(q) select q;
        }

        public IDb4oLinqQuery<T> QueryAll<T>()
        {
            return from T q in DBContainer select q;
        }

        public int Count<T>(IDb4oLinqQuery<T> collection)
        {
            return collection.Count();
        }

        public int CountAll<T>()
        {
            return QueryAll<T>().Count();
        }

        public int Count<T>(Predicate<T> p)
        {
            return Query(p).Count();
        }

        public int CountAllByExt<T>()
        {
            foreach (var storedClass in DBContainer.Ext().StoredClasses())
            {
                if (storedClass.GetName() == typeof(T).FullName) return storedClass.InstanceCount();
            }
            return 0;
        }

        public void Delete(object o)
        {
            DBContainer.Delete(o);
        }

        public void Delete<T>(Predicate<T> p)
        {
            foreach (var f in Query<T>(p))
            {
                Delete(f);
            }
        }

        /// <summary>
        /// 获取SODA模式查询对象
        /// </summary>
        /// <returns>SODA模式查询对象</returns>
        public IQuery GetQuery()
        {
            return DBContainer.Query();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            DBContainer.Commit();
            DBContainer.Close();
            DBContainer.Dispose();
        }

        #endregion
    }
}
