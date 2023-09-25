namespace DotNETCoreMongoDBCRUD.Utli
{
    public class DataTablePageResult<TEntity>
    {
        public List<TEntity> data { get; set; }
        public string draw { get; set; }
        public int recordsFiltered { get; set; } = 0;
        public int recordsTotal { get; set; }

        public DataTablePageResult()
        {
            data = new List<TEntity>();
        }
    }
}
