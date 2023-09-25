using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.ViewModel;
using DotNETCoreMongoDBCRUD.ViewModel.common;

namespace DotNETCoreMongoDBCRUD.Mappers
{
    public class ProductMapper
    {
        public  Dictionary<string, string> GetFilters()
        { 
            Dictionary<string,string> filters = new Dictionary<string,string>();
            return filters;
        }
        public CommandResultViewModel<ProductViewModel> MapListToViewModel(List<Product> products)
        {
            CommandResultViewModel<ProductViewModel> result = new CommandResultViewModel<ProductViewModel>();
            foreach (Product product in products)
            {
                ProductViewModel vm = new   ProductViewModel();
                vm.id= product._id.ToString();
                vm.name = product.name;
                vm.generation = product.generation;
                vm.made= product.made;
                vm.series= product.series;
                vm.graphic=product.graphic;
                vm.ram = product.ram;
                vm.hdwebcam = product.hdwebcam;
                vm.display=product.display;
                result.data.Add(vm);
            }
            result.recordsTotal= products.Count;
            return result;
        }
        public Product MapViewModelToModel(ProductViewModel vm)
        {
            Product model= new Product();
            if (!string.IsNullOrEmpty(vm.id))
            {
                model._id = MongoDB.Bson.ObjectId.Parse(vm.id);
            }
            model.series = vm.series;
            model.made = vm.made;
            model.name=vm.name;
            model.ram = vm.ram; 
            model.generation= vm.generation;
            model.hdwebcam = vm.hdwebcam;
            model.display = vm.display;
            return model;
        }
        public ProductViewModel MapModelToViewModel(Product model)
        {
            ProductViewModel vm = new ProductViewModel();
            vm.id = Convert.ToString(model._id);
            vm.name=model.name;
            vm.generation= model.generation;
            vm.ram=model.ram;
            vm.display = model.display;
            vm.graphic= model.graphic;
            vm.series=model.series;
            vm.made=model.made;
            return vm;
        }
    }
}
