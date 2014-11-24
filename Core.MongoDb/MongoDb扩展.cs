using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

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
    /// 统计以Lambda表达式形式执行的Where查询结果集数据总数
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="predicate">查询条件数组</param>
    /// <returns>数据总数</returns>
    public static Int64 Count<TDefaultDocument>(
        this MongoCollection<TDefaultDocument> mc,
        Expression<Func<TDefaultDocument, bool>> predicate
        )
    {
        return mc.Count(Query<TDefaultDocument>.Where(predicate));
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
    /// 使用Javascript进行查询，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>数据结果集游标</returns>
    public static MongoCursor<TDefaultDocument> Find<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, string javascriptcode)
    {
        return mc.Find(Query.Where(javascriptcode));
    }

    /// <summary>
    /// 使用Javascript进行查询，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>数据结果集游标</returns>
    public static MongoCursor Find(this MongoCollection mc, string javascriptcode)
    {
        return mc.FindAs(typeof(object),Query.Where(javascriptcode));
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
    /// 使用Javascript进行查询单个数据，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <typeparam name="TDefaultDocument">默认数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>单个数据</returns>
    public static TDefaultDocument FindOne<TDefaultDocument>(this MongoCollection<TDefaultDocument> mc, string javascriptcode)
    {
        return mc.FindOne(Query.Where(javascriptcode));
    }

    /// <summary>
    /// 使用Javascript进行查询单个数据，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>单个数据</returns>
    public static object FindOne(this MongoCollection mc, string javascriptcode)
    {
        return mc.FindOneAs(typeof(object),Query.Where(javascriptcode));
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
    /// 使用Javascript进行查询，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>数据结果集游标</returns>
    public static MongoCursor<TDocument> FindAs<TDocument>(this MongoCollection mc, string javascriptcode)
    {
        return mc.FindAs<TDocument>(Query.Where(javascriptcode));
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
    /// 使用Javascript进行查询，返回单个数据，详情参考Mongodb的$where查询方法。
    /// 基础查询代码示例："this.性别==false&amp;&amp;this.身高&gt;=1.89&amp;&amp;this.生日.getFullYear()&gt;=1995"
    /// 注意：此方法性能很低。
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="javascriptcode">Javascript编写的$where查询代码</param>
    /// <returns>单个数据</returns>
    public static TDocument FindOneAs<TDocument>(this MongoCollection mc, string javascriptcode)
    {
        return mc.FindOneAs<TDocument>(Query.Where(javascriptcode));
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
    /// 更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="update">更新方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateOneById(this MongoCollection mc, BsonValue id, IMongoUpdate update)
    {
        return mc.Update(Query.EQ("_id", id), update);
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
    /// 移除单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <returns>结果</returns>
    public static WriteConcernResult RemoveOneById(this MongoCollection mc, BsonValue id)
    {
        return mc.Remove(Query.EQ("_id", id), RemoveFlags.Single);
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

    #region 自动生成的方法

    /// <summary>
    /// 以AddToSet方式更新单个数据
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以AddToSet方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSet<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以AddToSet方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSet<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以AddToSet方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSet<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以AddToSet方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSet<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以AddToSet方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSet<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSet<TValue>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以AddToSetEach方式更新单个数据
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEachOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以AddToSetEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以AddToSetEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values), updateOptions);
    }

    /// <summary>
    /// 以AddToSetEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values), flags);
    }

    /// <summary>
    /// 以AddToSetEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values), writeConcern);
    }

    /// <summary>
    /// 以AddToSetEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateAddToSetEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.AddToSetEach<TValue>(memberExpression, values), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAndOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAndOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseAnd方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseAnd<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseAnd(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseOr方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOrOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseOr方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOrOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseOr方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseOr<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseOr(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseXor方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXorOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以BitwiseXor方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXorOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value));
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), flags);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以BitwiseXor方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateBitwiseXor<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.BitwiseXor(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Combine方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="updates">updates参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombineOneById<TDocument>(this MongoCollection mc, BsonValue id, IEnumerable<IMongoUpdate> updates)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates));
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
IEnumerable<IMongoUpdate> updates)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates));
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
IEnumerable<IMongoUpdate> updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), updateOptions);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
IEnumerable<IMongoUpdate> updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), flags);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
IEnumerable<IMongoUpdate> updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), writeConcern);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
IEnumerable<IMongoUpdate> updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), flags, writeConcern);
    }

    /// <summary>
    /// 以Combine方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="updates">updates参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombineOneById<TDocument>(this MongoCollection mc, BsonValue id, params IMongoUpdate[] updates)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates));
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
params IMongoUpdate[] updates)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates));
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
params IMongoUpdate[] updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), updateOptions);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
params IMongoUpdate[] updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), flags);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
params IMongoUpdate[] updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), writeConcern);
    }

    /// <summary>
    /// 以Combine方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="updates">updates参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCombine<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
params IMongoUpdate[] updates
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Combine(updates), flags, writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDateOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, DateTime>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, DateTime>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, DateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, DateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, DateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, DateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags, writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDateOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, MongoDB.Bson.BsonDateTime>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags, writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDateOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression));
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以CurrentDate方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateCurrentDate<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, MongoDB.Bson.BsonTimestamp>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.CurrentDate(memberExpression), flags, writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateIncOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, double>> memberExpression, double value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, double>> memberExpression, double value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateIncOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateIncOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value));
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Inc方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateInc<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Inc(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Max方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMaxOneById<TDocument, TMember>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Max方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMax<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Max方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMax<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Max方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMax<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Max方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMax<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Max方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMax<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Max<TMember>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Min方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMinOneById<TDocument, TMember>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Min方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMin<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Min方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMin<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Min方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMin<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Min方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMin<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Min方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMin<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Min<TMember>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMulOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, double>> memberExpression, double value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, double>> memberExpression, double value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, double>> memberExpression, double value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMulOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, int>> memberExpression, int value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, int>> memberExpression, int value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMulOneById<TDocument>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, long>> memberExpression, long value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value));
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Mul方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateMul<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, long>> memberExpression, long value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Mul(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以PopFirst方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirstOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression));
    }

    /// <summary>
    /// 以PopFirst方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirst<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression));
    }

    /// <summary>
    /// 以PopFirst方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirst<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以PopFirst方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirst<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression), flags);
    }

    /// <summary>
    /// 以PopFirst方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirst<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以PopFirst方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopFirst<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopFirst<TValue>(memberExpression), flags, writeConcern);
    }

    /// <summary>
    /// 以PopLast方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLastOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression));
    }

    /// <summary>
    /// 以PopLast方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLast<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression));
    }

    /// <summary>
    /// 以PopLast方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLast<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以PopLast方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLast<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression), flags);
    }

    /// <summary>
    /// 以PopLast方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLast<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以PopLast方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePopLast<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PopLast<TValue>(memberExpression), flags, writeConcern);
    }

    /// <summary>
    /// 以Pull方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction));
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction));
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction), updateOptions);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction), flags);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction), writeConcern);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="elementQueryBuilderFunction">elementQueryBuilderFunction参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Func<QueryBuilder<TValue>, IMongoQuery> elementQueryBuilderFunction
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, elementQueryBuilderFunction), flags, writeConcern);
    }

    /// <summary>
    /// 以Pull方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Pull方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePull<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Pull<TValue>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以PullAll方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAllOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PullAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PullAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values), updateOptions);
    }

    /// <summary>
    /// 以PullAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values), flags);
    }

    /// <summary>
    /// 以PullAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values), writeConcern);
    }

    /// <summary>
    /// 以PullAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePullAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PullAll<TValue>(memberExpression, values), flags, writeConcern);
    }

    /// <summary>
    /// 以Push方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以Push方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePush<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value));
    }

    /// <summary>
    /// 以Push方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePush<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Push方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePush<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Push方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePush<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Push方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePush<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, TValue value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Push<TValue>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以PushAll方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAllOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PushAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PushAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values), updateOptions);
    }

    /// <summary>
    /// 以PushAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values), flags);
    }

    /// <summary>
    /// 以PushAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values), writeConcern);
    }

    /// <summary>
    /// 以PushAll方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushAll<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushAll<TValue>(memberExpression, values), flags, writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEachOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values), updateOptions);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values), flags);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values), writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, values), flags, writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEachOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), updateOptions);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), flags);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, Action<PushEachOptionsBuilder<TValue>> options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), flags, writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEachOneById<TDocument, TValue>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values));
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), updateOptions);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), flags);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), writeConcern);
    }

    /// <summary>
    /// 以PushEach方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TValue">TValue类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="options">options参数</param>
    /// <param name="values">values参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdatePushEach<TDocument, TValue>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, IEnumerable<TValue>>> memberExpression, PushEachOptions options, IEnumerable<TValue> values
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.PushEach<TValue>(memberExpression, options, values), flags, writeConcern);
    }

    /// <summary>
    /// 以Replace方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <param name="id">ID号</param>
    /// <param name="document">document参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplaceOneById<TDocument>(this MongoCollection mc, BsonValue id, TDocument document)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Replace(document));
    }

    /// <summary>
    /// 以Replace方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="document">document参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplace<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
TDocument document)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Replace(document));
    }

    /// <summary>
    /// 以Replace方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="document">document参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplace<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
TDocument document
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Replace(document), updateOptions);
    }

    /// <summary>
    /// 以Replace方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="document">document参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplace<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
TDocument document
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Replace(document), flags);
    }

    /// <summary>
    /// 以Replace方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="document">document参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplace<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
TDocument document
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Replace(document), writeConcern);
    }

    /// <summary>
    /// 以Replace方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="document">document参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateReplace<TDocument>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
TDocument document
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Replace(document), flags, writeConcern);
    }

    /// <summary>
    /// 以Set方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOneById<TDocument, TMember>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Set方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSet<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以Set方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSet<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以Set方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSet<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以Set方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSet<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以Set方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSet<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Set<TMember>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以SetOnInsert方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsertOneById<TDocument, TMember>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以SetOnInsert方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsert<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, TMember>> memberExpression, TMember value)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value));
    }

    /// <summary>
    /// 以SetOnInsert方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsert<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value), updateOptions);
    }

    /// <summary>
    /// 以SetOnInsert方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsert<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value), flags);
    }

    /// <summary>
    /// 以SetOnInsert方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsert<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value), writeConcern);
    }

    /// <summary>
    /// 以SetOnInsert方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="value">value参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateSetOnInsert<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression, TMember value
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.SetOnInsert<TMember>(memberExpression, value), flags, writeConcern);
    }

    /// <summary>
    /// 以Unset方式更新单个数据
    /// </summary>
    /// <param name="mc">数据集合</param>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="id">ID号</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnsetOneById<TDocument, TMember>(this MongoCollection mc, BsonValue id, Expression<Func<TDocument, TMember>> memberExpression)
    {
        return mc.Update(Query.EQ("_id", id), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression));
    }

    /// <summary>
    /// 以Unset方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnset<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
Expression<Func<TDocument, TMember>> memberExpression)
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression));
    }

    /// <summary>
    /// 以Unset方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="updateOptions">选项</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnset<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        MongoUpdateOptions updateOptions,
Expression<Func<TDocument, TMember>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression), updateOptions);
    }

    /// <summary>
    /// 以Unset方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnset<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
         UpdateFlags flags,
Expression<Func<TDocument, TMember>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression), flags);
    }

    /// <summary>
    /// 以Unset方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnset<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression), writeConcern);
    }

    /// <summary>
    /// 以Unset方式更新
    /// </summary>
    /// <typeparam name="TDocument">数据类型</typeparam>
    /// <typeparam name="TMember">TMember类型参数</typeparam>
    /// <param name="mc">数据集合</param>
    /// <param name="memberExpression">memberExpression参数</param>
    /// <param name="whereExpression">Where表达式</param>
    /// <param name="flags">更新标记</param>
    /// <param name="writeConcern">对于写入时产生的异常关注方式</param>
    /// <returns>结果</returns>
    public static WriteConcernResult UpdateUnset<TDocument, TMember>(
        this MongoCollection<TDocument> mc,
        Expression<Func<TDocument, bool>> whereExpression,
        UpdateFlags flags,
        WriteConcern writeConcern,
Expression<Func<TDocument, TMember>> memberExpression
        )
    {
        return mc.Update(Query<TDocument>.Where(whereExpression), MongoDB.Driver.Builders.Update<TDocument>.Unset<TMember>(memberExpression), flags, writeConcern);
    }



    #endregion
}
