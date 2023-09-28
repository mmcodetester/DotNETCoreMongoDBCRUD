using DotNETCoreMongoDBCRUD.Controllers.Common;
using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Entity.common;
using DotNETCoreMongoDBCRUD.Mappers;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Service;
using DotNETCoreMongoDBCRUD.Utli;
using DotNETCoreMongoDBCRUD.ViewModel;
using DotNETCoreMongoDBCRUD.ViewModel.common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNETCoreMongoDBCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IRepository<User> _userRepo;
        IRepository<Role> _roleRepo;
        UserService service;
        User userModel;
        UserMapper mapper;
        public UserController(IRepository<User> repository,IRepository<Role> roleRepository) 
        {
             _userRepo = repository;
            _roleRepo = roleRepository;
            userModel = new User();
            service = new UserService(_userRepo, userModel);
            mapper = new UserMapper();
        }
        [HttpGet]
        public JsonResult Get()
        {
            CommandResultViewModel<UserViewModel> result = new CommandResultViewModel<UserViewModel>();
            try
            {
                QueryOption<User> queryOption = new QueryOption<User>();
                queryOption = GetQueryOptions<User>();
                List<User> users = _userRepo.GetPageResult(queryOption);
                result = mapper.MapListToViewModel(users,_roleRepo);
            }
            catch (Exception ex)
            {

            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult Save(UserViewModel vm)
        {
            CommandResultModel result = new CommandResultModel();
            User user = mapper.MapViewModelToModel(vm);
            result = service.SaveOrUpdate(user);
            return Json(result);
        }
    }
}
