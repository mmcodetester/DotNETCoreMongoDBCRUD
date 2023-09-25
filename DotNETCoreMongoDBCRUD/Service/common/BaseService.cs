using DotNETCoreMongoDBCRUD.Entity.common;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Utli;

namespace DotNETCoreMongoDBCRUD.Service.common
{
    public class BaseService<TEntity,TRepo> where TEntity : class
        where TRepo : IRepository<TEntity>
    {
        TRepo repo;
        TEntity entity;
        public BaseService(TRepo repository, TEntity model) 
        {
            repo = repository;
            entity = model;
        }    
        public CommandResultModel SaveOrUpdate(TEntity entity)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                repo.Add(entity);
                result.success = true;
                MongoDB.Bson.ObjectId id= (MongoDB.Bson.ObjectId)entity.GetType().GetProperty("_id").GetValue(entity);
                result.id = id.ToString();
                result.messages.Add("Save Success!");
            }catch(Exception ex)
            {
                result.success = false;
                result.messages.Add("Error");
            }
            return result;
        }
        public CommandResultModel Delete(TEntity entity)
        {
            CommandResultModel result = new CommandResultModel();
            try
            {
                MongoDB.Bson.ObjectId id = (MongoDB.Bson.ObjectId)entity.GetType().GetProperty("_id").GetValue(entity);
                repo.Delete(id.ToString());
                result.success = true;
                result.messages.Add("Delete Success");
            }catch(Exception ex)
            {
                result.success = false;
                result.messages.Add("Delete Error");
            }
            return result;
        }
    }
}
