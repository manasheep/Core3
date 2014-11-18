using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
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
        return db.GetCollection<TDefaultDocument>(typeof(TDefaultDocument).Name);
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

    /// <summary>
    /// 以Lambda表达式形式执行的Where查询单个数据
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="predicate">查询条件数组</param>
    /// <returns>第一个匹配的数据</returns>
    public static MongoCursor<TDefaultDocument> Find<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, Expression<Func<TDefaultDocument, bool>> predicate)
    {
        return mc.Find(Query<TDefaultDocument>.Where(predicate));
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where查询
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="predicate">查询条件数组</param>
    /// <returns>数据结果集游标</returns>
    public static TDefaultDocument FindOne<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, Expression<Func<TDefaultDocument, bool>> predicate)
    {
        return mc.FindOne(Query<TDefaultDocument>.Where(predicate));
    }

    /// <summary>
    /// 多条件查询，条件关系为“And”，返回数据结果集游标
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="queries">查询条件数组</param>
    /// <returns>数据结果集游标</returns>
    public static MongoCursor<TDocument> FindAs<TDocument>(this MongoCollection mc, params IMongoQuery[] queries)
    {
        return mc.FindAs<TDocument>(Query.And(queries));
    }

    /// <summary>
    /// 多条件查询，条件关系为“And”，返回单个数据
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="queries">查询条件数组</param>
    /// <returns>单个数据</returns>
    public static TDocument FindOneAs<TDocument>(this MongoCollection mc, params IMongoQuery[] queries)
    {
        return mc.FindOneAs<TDocument>(Query.And(queries));
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where查询单个数据
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="predicate">查询条件数组</param>
    /// <returns>第一个匹配的数据</returns>
    public static MongoCursor<TDocument> FindAs<TDocument>(this MongoCollection mc, Expression<Func<TDocument, bool>> predicate)
    {
        return mc.FindAs<TDocument>(Query<TDocument>.Where(predicate));
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where查询
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="predicate">查询条件数组</param>
    /// <returns>数据结果集游标</returns>
    public static TDocument FindOneAs<TDocument>(this MongoCollection mc, Expression<Func<TDocument, bool>> predicate)
    {
        return mc.FindOneAs<TDocument>(Query<TDocument>.Where(predicate));
    }

    /// <summary>
    /// 从数据库的GridFS中获取文件
    /// </summary>
    /// <param name="id">文件编号</param>
    /// <param name="db">数据库</param>
    /// <returns>文件信息</returns>
    public static MongoGridFSFileInfo FindFileById(this MongoDatabase db, BsonValue id)
    {
        return db.GridFS.FindOneById(id);
    }

    /// <summary>
    /// 上传文件到数据库的GridFS中
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    /// <param name="db">数据库</param>
    /// <returns>文件信息</returns>
    public static MongoGridFSFileInfo UploadFile(this MongoDatabase db, string path)
    {
        return db.GridFS.Upload(path);
    }

    /// <summary>
    /// 从数据库的GridFS中删除文件
    /// </summary>
    /// <param name="db">数据库</param>
    /// <param name="id">文件编号</param>
    public static void DeleteFileById(this MongoDatabase db, BsonValue id)
    {
        db.GridFS.DeleteById(id);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="update">更新方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Update<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        IMongoUpdate update
        )
    {
        return mc.Update(Query<TDocument>.Where(expression), update);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="update">更新方式</param>
    /// <param name="options">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Update<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        IMongoUpdate update,
        MongoUpdateOptions options
        )
    {
        return mc.Update(Query<TDocument>.Where(expression), update, options);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="update">更新方式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Update<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        IMongoUpdate update,
         UpdateFlags flags
        )
    {
        return mc.Update(Query<TDocument>.Where(expression), update, flags);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="update">更新方式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Update<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        IMongoUpdate update,
        WriteConcern writeConcern
        )
    {
        return mc.Update(Query<TDocument>.Where(expression), update, writeConcern);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="update">更新方式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Update<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        IMongoUpdate update,
        UpdateFlags flags,
        WriteConcern writeConcern
        )
    {
        return mc.Update(Query<TDocument>.Where(expression), update, flags, writeConcern);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where移除
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Remove<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression
        )
    {
        return mc.Remove(Query<TDocument>.Where(expression));
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where移除
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="flags">移除标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Remove<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
         RemoveFlags flags
        )
    {
        return mc.Remove(Query<TDocument>.Where(expression), flags);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where移除
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Remove<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        WriteConcern writeConcern
        )
    {
        return mc.Remove(Query<TDocument>.Where(expression), writeConcern);
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where移除
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="expression">Where表达式</param>
    /// <param name="flags">移除标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult Remove<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> expression,
        RemoveFlags flags,
        WriteConcern writeConcern
        )
    {
        return mc.Remove(Query<TDocument>.Where(expression), flags, writeConcern);
    }

    /// <summary>
    /// 获取目标属性的所有不同的值集合
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="selectPropertyExpression">属性选取表达式</param>
    /// <returns>值集合</returns>
    public static IEnumerable<TValue> Distinct<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, TValue>> selectPropertyExpression
        )
    {
        return mc.Distinct<TValue>(selectPropertyExpression.GetPropertyName());
    }

    /// <summary>
    /// 以Lambda表达式形式执行的Where过滤后，获取目标属性的所有不同的值集合
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="whereExpression">过滤表达式</param>
    /// <param name="selectPropertyExpression">属性选取表达式</param>
    /// <returns>值集合</returns>
    public static IEnumerable<TValue> Distinct<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        Expression<Func<TDocument, TValue>> selectPropertyExpression
        )
    {
        return mc.Distinct<TValue>(selectPropertyExpression.GetPropertyName(), Query<TDocument>.Where(whereExpression));
    }
}
