using DotNETCoreMongoDBCRUD.Utli;

namespace DotNETCoreMongoDBCRUD.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetFilteredSortedPaginatedAsync(QueryOption<T> options);
        List<T> GetPageResult(QueryOption<T> options);

        List<T> Get();
        T Get(string id);
        T GetById(string id);
        void Add(T entity);
        void Update(T entity);
        void UpdateAsync(T entity);
        void Delete(string id);
        void DeleteAsync(string id);
    }
}
