using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Repository;
using DotNETCoreMongoDBCRUD.Utli;
using DotNETCoreMongoDBCRUD.ViewModel;
using DotNETCoreMongoDBCRUD.ViewModel.common;

namespace DotNETCoreMongoDBCRUD.Mappers
{
    public class UserMapper
    {
        public User MapViewModelToModel(UserViewModel vm)
        {
            User user = new User();
            if (!string.IsNullOrEmpty(vm.id))
            {
                user._id = MongoDB.Bson.ObjectId.Parse(vm.id);
            }
            if (!string.IsNullOrEmpty(vm.name))
            {
                user.name = vm.name;
            }
            if (!string.IsNullOrEmpty(vm.password))
            {
                user.password = PasswordHashHelper.HashPassword(vm.password);
            }
            if(!string.IsNullOrEmpty(vm.role_id))
            {
                user.role_id = MongoDB.Bson.ObjectId.Parse(vm.role_id);
            }
            return user;
        }
        public CommandResultViewModel<UserViewModel> MapListToViewModel(List<User> users,IRepository<Role> _roleRepo)
        {
            CommandResultViewModel<UserViewModel> vmList = new CommandResultViewModel<UserViewModel>();
            foreach (User user in users)
            {
                UserViewModel vm = new UserViewModel();
                vm.id = user._id.ToString();
                vm.name = user.name;
                vm.role_id = user.role_id.ToString();
                Role role = _roleRepo.GetById(vm.role_id);
                vm.role_name = role.name;
                vmList.data.Add(vm);
            }
            vmList.recordsTotal = users.Count;
            return vmList;
        }
    }
}
