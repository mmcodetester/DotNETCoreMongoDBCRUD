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

namespace DotNETCoreMongoDBCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {        
          IRepository<Product> _productRepository;
        ProductMapper mapper;
        public ProductController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            mapper = new ProductMapper();
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
          QueryOption<Product> queryOption = new QueryOption<Product>();
          queryOption = GetQueryOptions<Product>();
          List<Product> products =await _productRepository.GetAllAsync(queryOption);
            //return _productRepository.GetAll();
            return Json(products);
        }
        public JsonResult Save(ProductViewModel vm)
        {
            Product product = mapper.MapViewModelToModel(vm);
            _productRepository.Add(product);
            return Json(new { success=true });
        }
        [HttpGet]
        [Route("GetById")]
        public JsonResult GetById(string id)
        {
            Product product = _productRepository.GetById(id);
            ProductViewModel vm=mapper.MapModelToViewModel(product);
            return Json(vm);
        }
        [HttpDelete]
        public JsonResult Delete(string id)
        {
            _productRepository.Delete(id); 
            return Json(new { success=true });
        }
    }
}
