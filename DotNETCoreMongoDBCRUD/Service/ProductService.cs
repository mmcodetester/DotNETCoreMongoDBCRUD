using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Service.common;

namespace DotNETCoreMongoDBCRUD.Service
{
    public class ProductService : BaseService<Product, IRepository<Product>>
    {
        public ProductService(IRepository<Product> repository, Product model) 
            : base(repository, model)
        {

        }
    }
}
