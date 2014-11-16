using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

// ReSharper disable once CheckNamespace
public static class MongoDb扩展
{
    /// <summary>
    /// 获取与默认数据类型同名的数据集合
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="db">数据库</param>
    /// <returns>与默认数据类型同名的数据集合</returns>
    public static MongoCollection<TDefaultDocument> GetCollection<TDefaultDocument>(this MongoDatabase db)
    {
        return db.GetCollection<TDefaultDocument>(typeof (TDefaultDocument).Name);
    }

    /// <summary>
    /// 多条件查询，条件关系为“And”，返回数据结果集游标
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="queries">查询条件数组</param>
    /// <returns>数据结果集游标</returns>
    public static MongoCursor<TDefaultDocument> Find<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, params IMongoQuery[] queries)
    {
        return mc.Find(Query.And(queries));
    }

    /// <summary>
    /// 多条件查询，条件关系为“And”，返回单个数据
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="queries">查询条件数组</param>
    /// <returns>单个数据</returns>
    public static TDefaultDocument FindOne<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, params IMongoQuery[] queries)
    {
        return mc.FindOne(Query.And(queries));
    }
}
