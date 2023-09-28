using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Service.common;

namespace DotNETCoreMongoDBCRUD.Service
{
    public class UserService : BaseService<User, IRepository<User>>
    {
        public UserService(IRepository<User> repository, User model) : base(repository, model)
        {

        }
    }
}
