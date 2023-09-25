namespace DotNETCoreMongoDBCRUD.ViewModel.common
{
    public class CommandResultViewModel<TEntity>
    {
        public List<TEntity> data { get; set; }
        public int recordsTotal { get; set; }
        public string? recordsFilter { get; set; }   
        public CommandResultViewModel()
        {
            data = new List<TEntity>();
        }
    }
}
