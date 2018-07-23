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

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="CollectionName"></param>
        /// <param name="document"></param>
        public void InsertOne<TDocument>(string CollectionName,TDocument document)
        {
            var collect = _db.GetCollection<TDocument>(CollectionName);
            collect.InsertOne(document);
        }

        public IList<TDocument> Find<TDocument>(string CollectionName, Expression<Func<TDocument, bool>> filter)
        {
            var collect = _db.GetCollection<TDocument>(CollectionName);
            return collect.Find(filter).ToList();
        }

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
    }

    #region 生成一个ObjectID

    //public class ObjectId
    //{
    //    private string _string;

    //    public ObjectId()
    //    {
    //    }

    //    public ObjectId(string value)
    //      : this(DecodeHex(value))
    //    {
    //    }

    //    internal ObjectId(byte[] value)
    //    {
    //        Value = value;
    //    }

    //    public static ObjectId Empty
    //    {
    //        get { return new ObjectId("000000000000000000000000"); }
    //    }

    //    public byte[] Value { get; private set; }

    //    public static ObjectId NewObjectId()
    //    {
    //        return new ObjectId { Value = ObjectIdGenerator.Generate() };
    //    }

    //    public static bool TryParse(string value, out ObjectId objectId)
    //    {
    //        objectId = Empty;
    //        if (value == null || value.Length != 24)
    //        {
    //            return false;
    //        }

    //        try
    //        {
    //            objectId = new ObjectId(value);
    //            return true;
    //        }
    //        catch (FormatException)
    //        {
    //            return false;
    //        }
    //    }

    //    protected static byte[] DecodeHex(string value)
    //    {
    //        if (string.IsNullOrEmpty(value))
    //            throw new ArgumentNullException("value");

    //        var chars = value.ToCharArray();
    //        var numberChars = chars.Length;
    //        var bytes = new byte[numberChars / 2];

    //        for (var i = 0; i < numberChars; i += 2)
    //        {
    //            bytes[i / 2] = Convert.ToByte(new string(chars, i, 2), 16);
    //        }

    //        return bytes;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return Value != null ? ToString().GetHashCode() : 0;
    //    }

    //    public override string ToString()
    //    {
    //        if (_string == null && Value != null)
    //        {
    //            _string = BitConverter.ToString(Value)
    //              .Replace("-", string.Empty)
    //              .ToLowerInvariant();
    //        }

    //        return _string;
    //    }

    //    public override bool Equals(object obj)
    //    {
    //        var other = obj as ObjectId;
    //        return Equals(other);
    //    }

    //    public bool Equals(ObjectId other)
    //    {
    //        return other != null && ToString() == other.ToString();
    //    }

    //    public static implicit operator string(ObjectId objectId)
    //    {
    //        return objectId == null ? null : objectId.ToString();
    //    }

    //    public static implicit operator ObjectId(string value)
    //    {
    //        return new ObjectId(value);
    //    }

    //    public static bool operator ==(ObjectId left, ObjectId right)
    //    {
    //        if (ReferenceEquals(left, right))
    //        {
    //            return true;
    //        }

    //        if (((object)left == null) || ((object)right == null))
    //        {
    //            return false;
    //        }

    //        return left.Equals(right);
    //    }

    //    public static bool operator !=(ObjectId left, ObjectId right)
    //    {
    //        return !(left == right);
    //    }
    //}

    //internal static class ObjectIdGenerator
    //{
    //    private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    //    private static readonly object _innerLock = new object();
    //    private static int _counter;
    //    private static readonly byte[] _machineHash = GenerateHostHash();
    //    private static readonly byte[] _processId = BitConverter.GetBytes(GenerateProcessId());

    //    public static byte[] Generate()
    //    {
    //        var oid = new byte[12];
    //        var copyidx = 0;

    //        Array.Copy(BitConverter.GetBytes(GenerateTime()), 0, oid, copyidx, 4);
    //        copyidx += 4;

    //        Array.Copy(_machineHash, 0, oid, copyidx, 3);
    //        copyidx += 3;

    //        Array.Copy(_processId, 0, oid, copyidx, 2);
    //        copyidx += 2;

    //        Array.Copy(BitConverter.GetBytes(GenerateCounter()), 0, oid, copyidx, 3);

    //        return oid;
    //    }

    //    private static int GenerateTime()
    //    {
    //        var now = DateTime.UtcNow;
    //        var nowtime = new DateTime(Epoch.Year, Epoch.Month, Epoch.Day,
    //          now.Hour, now.Minute, now.Second, now.Millisecond);
    //        var diff = nowtime - Epoch;
    //        return Convert.ToInt32(Math.Floor(diff.TotalMilliseconds));
    //    }

    //    private static byte[] GenerateHostHash()
    //    {
    //        using (var md5 = MD5.Create())
    //        {
    //            var host = Dns.GetHostName();
    //            return md5.ComputeHash(Encoding.Default.GetBytes(host));
    //        }
    //    }

    //    private static int GenerateProcessId()
    //    {
    //        var process = Process.GetCurrentProcess();
    //        return process.Id;
    //    }

    //    private static int GenerateCounter()
    //    {
    //        lock (_innerLock)
    //        {
    //            return _counter++;
    //        }
    //    }
    //}

    #endregion
}
