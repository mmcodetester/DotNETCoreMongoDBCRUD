using DotNETCoreMongoDBCRUD.Utli;

namespace DotNETCoreMongoDBCRUD.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetFilteredSortedPaginatedAsync(QueryOption<T> options);
        Task<List<T>> GetAllAsync(QueryOption<T> options);

        List<T> Get();
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
