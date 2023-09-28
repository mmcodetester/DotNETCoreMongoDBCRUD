using DotNETCoreMongoDBCRUD.Context;
using DotNETCoreMongoDBCRUD.Utli;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Drawing.Printing;

namespace DotNETCoreMongoDBCRUD.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IOptions<DatabaseSetting> options)
        {
            string conn = options.Value.ConnectionString;
            string database = options.Value.DatabaseName;
            string table = typeof(T).Name;
            var mongoClient = new MongoClient(conn);
            var dbList = mongoClient.ListDatabases().ToList();
            _collection = mongoClient.GetDatabase(database).GetCollection<T>(table);
        }
        public void Add(T entity)
        {
             _collection.InsertOne(entity);
        }

        public void Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            _collection.DeleteOne(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id",MongoDB.Bson.ObjectId.Parse(id));
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Update(T entity)
        {
            MongoDB.Bson.ObjectId id = (MongoDB.Bson.ObjectId)entity.GetType().GetProperty("_id").GetValue(entity);
            _collection.ReplaceOne(Builders<T>.Filter.Eq("_id",id), entity);
        }
        public List<T> Get()
        {
            return _collection.Find(_ => true).ToList();
        }

        public async Task<IEnumerable<T>> GetFilteredSortedPaginatedAsync(QueryOption<T> options)
        {
            var filter = !string.IsNullOrWhiteSpace(options.filterJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.filterJson) : new BsonDocument();
            var sort = !string.IsNullOrWhiteSpace(options.sortJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.sortJson) : new BsonDocument();
            var query = _collection.Find(filter);
            if (sort != null && sort.ElementCount > 0)
                query = query.Sort(sort);
            if (options.page != null && options.pageSize != null)
                query = query.Skip((options.page.Value - 1) *options.pageSize.Value).Limit(options.pageSize.Value);
            return await query.ToListAsync();
        }

        public List<T> GetPageResult(QueryOption<T> options)
        {
            var filter = !string.IsNullOrWhiteSpace(options.filterJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.filterJson) : new BsonDocument();
            var sort = !string.IsNullOrWhiteSpace(options.sortJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.sortJson) : new BsonDocument();
            var query = _collection.Find(filter);
            if (sort != null && sort.ElementCount > 0)
                query = query.Sort(sort);
            if (options.page != null && options.pageSize != null)
                query = query.Skip((options.page.Value - 1) * options.pageSize.Value).Limit(options.pageSize.Value);
            return  query.ToList();
        }

        public T Get(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            return _collection.Find(filter).FirstOrDefault();
        }

        public void UpdateAsync(T entity)
        {
            MongoDB.Bson.ObjectId id = (MongoDB.Bson.ObjectId)entity.GetType().GetProperty("_id").GetValue(entity);
            _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity);
        }

        public  void DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", MongoDB.Bson.ObjectId.Parse(id));
            _collection.DeleteOneAsync(filter);
        }
    }
}
