using DotNETCoreMongoDBCRUD.Context;
using DotNETCoreMongoDBCRUD.Entity;
using DotNETCoreMongoDBCRUD.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using ParkBee.MongoDb;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using DotNETCoreMongoDBCRUD.Controllers.Common;
using DotNETCoreMongoDBCRUD.Utli;
using DotNETCoreMongoDBCRUD.Mappers;
using DotNETCoreMongoDBCRUD.ViewModel;
using DotNETCoreMongoDBCRUD.Service;
using DotNETCoreMongoDBCRUD.Entity.common;
using DotNETCoreMongoDBCRUD.ViewModel.common;
using Microsoft.AspNetCore.Authorization;

namespace DotNETCoreMongoDBCRUD.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {        
          IRepository<Product> _productRepository;
        ProductMapper mapper;
        ProductService service;
        Product productModel;
        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            productModel = new Product();
            service = new ProductService(_productRepository, productModel);
            mapper = new ProductMapper();
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            CommandResultViewModel<ProductViewModel> results = new CommandResultViewModel<ProductViewModel>();
            try
            {
                QueryOption<Product> queryOption = new QueryOption<Product>();
                queryOption = GetQueryOptions<Product>();
                List<Product> products = _productRepository.GetPageResult(queryOption);
                results = mapper.MapListToViewModel(products);
            }
            catch(Exception ex)
            {

            }            
            return Json(results);
        }
        [HttpPost]
        public JsonResult Save(ProductViewModel vm)
        {
            CommandResultModel result = new CommandResultModel();
            Product product = mapper.MapViewModelToModel(vm);
            result = service.SaveOrUpdate(product);
            return Json(result);
        }
        [HttpGet]
        [Route("GetById")]
        public JsonResult GetById(string id)
        {
            Product product = _productRepository.GetById(id);
            ProductViewModel vm = mapper.MapModelToViewModel(product);
            return Json(vm);
        }
        [HttpPut]
        public JsonResult Update(ProductViewModel vm)
        {
            CommandResultModel result = new CommandResultModel();
            Product product = mapper.MapViewModelToModel(vm);
            result = service.SaveOrUpdate(product);
            return Json(result);
        }
        [HttpDelete]
        public JsonResult Delete(string id)
        {
            CommandResultModel result = new CommandResultModel();
            Product product = _productRepository.GetById(id);
            result = service.Delete(product);
            return Json(result);
        }
    }
}
