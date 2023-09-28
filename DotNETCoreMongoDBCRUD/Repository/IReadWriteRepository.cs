namespace DotNETCoreMongoDBCRUD.Repository
{
    public interface IReadWriteRepository<TEntity>
    {
        TEntity FindByName(string name);
    }
}
