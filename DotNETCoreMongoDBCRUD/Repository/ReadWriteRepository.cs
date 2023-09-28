using DotNETCoreMongoDBCRUD.Context;
using DotNETCoreMongoDBCRUD.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNETCoreMongoDBCRUD.Repository
{
    public class ReadWriteRepository<TEntity> :  IReadWriteRepository<TEntity> where TEntity: class
    {
        private readonly IMongoCollection<TEntity> _collection;
        public ReadWriteRepository(IOptions<DatabaseSetting> options)
        {
            string conn = options.Value.ConnectionString;
            string database = options.Value.DatabaseName;
            string table = typeof(TEntity).Name;
            var mongoClient = new MongoClient(conn);
            var dbList = mongoClient.ListDatabases().ToList();
            _collection = mongoClient.GetDatabase(database).GetCollection<TEntity>(table);
        }

        public TEntity FindByName(string name)
        {
            var filter = Builders<TEntity>.Filter.Eq("name", name);
            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
