namespace DotNETCoreMongoDBCRUD.Utli
{
    public class QueryOption<TEntity>
    {
        public QueryOption()
        {
        }
        //GET /api/yourcontroller
        //Headers:
        //- filter: {"age": {"$gt": 30}}
        //- sort: {"name": 1}
        //- page: 1
        //- pageSize: 10

        public int? page { get; set; }
        public int? pageSize { get; set; }
        public string? filterJson { get; set; }
        public int? recordPerPage { get; set; } = 10;
        public string sortJson { get; set; }      
    }
}
