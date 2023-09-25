namespace DotNETCoreMongoDBCRUD.Utli
{
    public class DatatableQueryOptions<TEntity> where TEntity : class
    {
        public DatatableQueryOptions()
        {
        }
        public int? page { get; set; }
        public int? pageSize { get; set; }
        public string filterJson { get; set; }
        public int? recordPerPage { get; set; } = 10;
        public string sortJson { get; set; }
    }
}
