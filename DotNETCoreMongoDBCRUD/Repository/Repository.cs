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
            throw new NotImplementedException();
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

        public async Task<List<T>> GetAllAsync(QueryOption<T> options)
        {
            var filter = !string.IsNullOrWhiteSpace(options.filterJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.filterJson) : new BsonDocument();
            var sort = !string.IsNullOrWhiteSpace(options.sortJson) ? MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(options.sortJson) : new BsonDocument();
            var query = _collection.Find(filter);
            if (sort != null && sort.ElementCount > 0)
                query = query.Sort(sort);
            if (options.page != null && options.pageSize != null)
                query = query.Skip((options.page.Value - 1) * options.pageSize.Value).Limit(options.pageSize.Value);
            return await query.ToListAsync();
        }
    }
}
