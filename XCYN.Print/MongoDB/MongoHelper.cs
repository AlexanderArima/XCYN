using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace XCYN.Print.MongoDB
{
    public class MongoHelper
    {
        public static IMongoClient _client = null;
        public static IMongoDatabase _db = null;

        public MongoHelper(string Connection,string DataBaseName)
        {
            if(_client == null)
            {
                _client = new MongoClient(Connection);
            }
            if(_db == null)
            {
                _db = _client.GetDatabase(DataBaseName);
            }

        }

        public MongoHelper(string DataBaseName)
        {
            if (_client == null)
            {
                _client = new MongoClient("mongodb://localhost:27017");
            }
            if (_db == null)
            {
                _db = _client.GetDatabase(DataBaseName);
            }
        }

        /// <summary>
        /// 获取一个集合(表)
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            return _db.GetCollection<TDocument>(name);
        }

        #region 插入

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="CollectionName">表明</param>
        /// <param name="document">插入的对象</param>
        public void InsertOne<T>(string CollectionName,T entity) where T : class
        {
            var collect = _db.GetCollection<T>(CollectionName);
            collect.InsertOne(entity);
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName">表名</param>
        /// <param name="entity">插入的对象</param>
        /// <returns></returns>
        public async Task InsertOneAsync<T>(string ColName,T entity) where T : class
        {
            var collect = _db.GetCollection<T>(ColName);
            await collect.InsertOneAsync(entity);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName">表名</param>
        /// <param name="list">一组插入的对象</param>
        public void InsertMany<T>(string ColName,IEnumerable<T> list) where T : class
        {
            var collect = _db.GetCollection<T>(ColName);
            collect.InsertMany(list);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task InsertManyAsync<T>(string ColName,IEnumerable<T> list) where T : class
        {
            var col = _db.GetCollection<T>(ColName);
            await col.InsertManyAsync(list);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long DeleteOne<T>(string ColName, Expression<Func<T, bool>> filter) where T:class
        {
            var col = _db.GetCollection<T>(ColName);
            return col.DeleteOne<T>(filter).DeletedCount;
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteOneAsync<T>(string ColName, Expression<Func<T, bool>> filter) where T : class
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.DeleteOneAsync(filter);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long DeleteMany<T>(string ColName, Expression<Func<T, bool>> filter) where T:class
        {
            var col = _db.GetCollection<T>(ColName);
            return col.DeleteMany<T>(filter).DeletedCount;
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteManyAsync<T>(string ColName, Expression<Func<T, bool>> filter) where T : class
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.DeleteManyAsync<T>(filter);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateOne"></param>
        private UpdateResult UpdateOne<T>(string ColName,Expression<Func<T, bool>> Filter, UpdateDefinition<T> UpdateOne)
        {
            var col = _db.GetCollection<T>(ColName);
            return col.UpdateOne<T>(Filter, UpdateOne);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateObject">匿名对象</param>
        public UpdateResult UpdateOne<T>(string ColName, Expression<Func<T, bool>> Filter, object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return col.UpdateOne<T>(Filter, update);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateJson">MongoDB命令</param>
        public UpdateResult UpdateOne<T>(string ColName, Expression<Func<T, bool>> Filter, string UpdateJson)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(UpdateJson);
            return col.UpdateOne<T>(Filter, update);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateOne"></param>
        private async Task<UpdateResult> UpdateOneAsync<T>(string ColName, Expression<Func<T, bool>> Filter, UpdateDefinition<T> UpdateOne)
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.UpdateOneAsync<T>(Filter, UpdateOne);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateObject">匿名对象</param>
        public async Task<UpdateResult> UpdateOneAsync<T>(string ColName, Expression<Func<T, bool>> Filter, object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return await col.UpdateOneAsync<T>(Filter, update);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateJson">MongoDB命令</param>
        public async Task<UpdateResult> UpdateOneAsync<T>(string ColName, Expression<Func<T, bool>> Filter, string UpdateJson)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(UpdateJson);
            return await col.UpdateOneAsync<T>(Filter, update);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateOne"></param>
        private UpdateResult UpdateMany<T>(string ColName, Expression<Func<T, bool>> Filter, UpdateDefinition<T> UpdateOne)
        {
            var col = _db.GetCollection<T>(ColName);
            return col.UpdateMany<T>(Filter, UpdateOne);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateObject">匿名对象</param>
        public UpdateResult UpdateMany<T>(string ColName, Expression<Func<T, bool>> Filter, object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return col.UpdateMany<T>(Filter, update);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateJson">MongoDB命令</param>
        public UpdateResult UpdateMany<T>(string ColName, Expression<Func<T, bool>> Filter, string UpdateJson)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(UpdateJson);
            return col.UpdateMany<T>(Filter, update);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateOne"></param>
        private async Task<UpdateResult> UpdateManyAsync<T>(string ColName, Expression<Func<T, bool>> Filter, UpdateDefinition<T> UpdateOne)
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.UpdateManyAsync<T>(Filter, UpdateOne);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateObject">匿名对象</param>
        public async Task<UpdateResult> UpdateManyAsync<T>(string ColName, Expression<Func<T, bool>> Filter, object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return await col.UpdateManyAsync<T>(Filter, update);
        }

        /// <summary>
        /// 修改多条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="Filter"></param>
        /// <param name="UpdateJson">MongoDB命令</param>
        public async Task<UpdateResult> UpdateManyAsync<T>(string ColName, Expression<Func<T, bool>> Filter, string UpdateJson)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(UpdateJson);
            return await col.UpdateManyAsync<T>(Filter, update);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Find<T>(string CollectionName, Expression<Func<T, bool>> filter)
        {
            var collect = _db.GetCollection<T>(CollectionName);
            return collect.Find(filter).ToList();
        }

        /// <summary>
        /// 异步的查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CollectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IAsyncCursor<T>> FindAsync<T>(string CollectionName, Expression<Func<T, bool>> filter)
        {
            var collect = _db.GetCollection<T>(CollectionName);
            return await collect.FindAsync(filter);
        }

        /// <summary>
        /// 返回的结果数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long Count<T>(string ColName, Expression<Func<T, bool>> filter)
        {
            var col = _db.GetCollection<T>(ColName);
            return col.CountDocuments<T>(filter);
        }

        /// <summary>
        /// 返回的结果数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> CountAsync<T>(string ColName, Expression<Func<T, bool>> filter)
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.CountDocumentsAsync<T>(filter);
        }

        /// <summary>
        /// 查找一条数据并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T FindOneAndDelete<T>(string ColName, Expression<Func<T, bool>> filter)
        {
            var col = _db.GetCollection<T>(ColName);
            return col.FindOneAndDelete<T>(filter);
        }

        /// <summary>
        /// 查找一条数据并删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> FindOneAndDeleteAsync<T>(string ColName,Expression<Func<T,bool>> filter)
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.FindOneAndDeleteAsync<T>(filter);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public T FindOneAndUpdate<T>(string ColName, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var col = _db.GetCollection<T>(ColName);
            return col.FindOneAndUpdate(filter, update);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public T FindOneAndUpdate<T>(string ColName,Expression<Func<T,bool>> filter,object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return FindOneAndUpdate(ColName,filter, update);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public T FindOneAndUpdate<T>(string ColName, Expression<Func<T, bool>> filter, string json)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(json);
            return FindOneAndUpdate(ColName, filter, update);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<T> FindOneAndUpdateAsync<T>(string ColName, Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            var col = _db.GetCollection<T>(ColName);
            return await col.FindOneAndUpdateAsync(filter, update);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<T> FindOneAndUpdateAsync<T>(string ColName, Expression<Func<T, bool>> filter, object UpdateObject)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>("{$set:" + UpdateObject.ToJson() + "}");
            return await FindOneAndUpdateAsync(ColName, filter, update);
        }

        /// <summary>
        /// 查找一条数据并修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ColName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<T> FindOneAndUpdateAsync<T>(string ColName, Expression<Func<T, bool>> filter, string json)
        {
            var col = _db.GetCollection<T>(ColName);
            UpdateDefinition<T> update = new JsonUpdateDefinition<T>(json);
            return await FindOneAndUpdateAsync(ColName, filter, update);
        }
        
        #endregion

        #region 文件操作

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="FileName"></param>
        public int FilePut(string FileName)
        {
            GridFSBucket fs = new GridFSBucket(_db);
            using (FileStream stream = new FileStream(FileName, FileMode.Open))
            {
                var obj = fs.UploadFromStream(FileName, stream);
                if (obj != null)
                {
                    return obj.Timestamp;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="objectID"></param>
        public void FileDelete(string objectID)
        {
            GridFSBucket fs = new GridFSBucket(_db);
            ObjectId obj = new ObjectId(objectID);
            fs.Delete(obj);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="stream"></param>
        public void FileGet(string objectID,Stream stream)
        {
            GridFSBucket fs = new GridFSBucket(_db);
            ObjectId obj = new ObjectId(objectID);
            fs.DownloadToStream(obj, stream);
        }

        #endregion
    }
    
}
