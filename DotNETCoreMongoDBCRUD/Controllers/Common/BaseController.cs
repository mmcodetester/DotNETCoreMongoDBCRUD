using DotNETCoreMongoDBCRUD.Utli;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Xml;

namespace DotNETCoreMongoDBCRUD.Controllers.Common
{
    public class BaseController : Controller
    {
        public BaseController() { }
        public TType? GetRequestParameters<TType>(string key)
        {
            TType? val = default(TType?);
            try
            {
                if (!string.IsNullOrEmpty(Request.Query[key].ToString()))
                {
                    string? tmp = Request.Query[key].FirstOrDefault();
                    val = (TType?)Convert.ChangeType(tmp, typeof(TType));
                }
            }
            catch (Exception ex) 
            {
                val = default(TType);
            }
            return val;
        }

        protected QueryOption<TViewModel> GetQueryOptions<TViewModel>() where TViewModel : class
        {
            QueryOption<TViewModel> op = new QueryOption<TViewModel>();
            Dictionary<string, string> filters=new Dictionary<string, string>();
            Dictionary<string, int> sorts = new Dictionary<string, int>();
            int pageSize = 0;
            int pageNumber =0;
            foreach ( var key in Request.Query)
            {
                if (key.Key == "sort")
                {
                    if (key.Value == "ASC")
                    {
                        sorts.Add("_id",1);
                    }
                    else
                    {
                        sorts.Add("_id", -1);
                    }
                }
                else if(key.Key == "pageSize")
                {
                    pageSize =Convert.ToInt32(key.Value);
                }
                else if(key.Key == "page")
                {
                    pageNumber=Convert.ToInt32(key.Value);
                }
                else
                {
                    filters.Add(key.Key, key.Value);
                }
            }
            op.filterJson = FormatRequestQueryToJson(filters);
            op.sortJson = FormatRequestQueryToJsonInt(sorts);
            
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            //Find Order Column
            var sortColumnDir = GetRequestParameters<string>("order[0][dir]");
            ////e.g sc=a&so=asc&ps=10&pn=1
            int skip = 0;
            try
            {
                if (pageNumber > 0)
                {
                    skip = (pageNumber - 1) * pageSize;
                }
            }
            catch (Exception ex)
            {

            }
            op.page = pageNumber;
            op.recordPerPage = pageSize;
            op.pageSize = pageSize;
            return op;        
        }
        public static string FormatRequestQueryToJson(Dictionary<string, string> requestQuery)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(requestQuery, (Newtonsoft.Json.Formatting)Formatting.Indented);
        }
        public static string FormatRequestQueryToJsonInt(Dictionary<string, int> requestQuery)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(requestQuery, (Newtonsoft.Json.Formatting)Formatting.Indented);
        }
        public static FilterDefinition<BsonDocument> ConvertToMongoFilter(Dictionary<string, string> queryParams)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filters = new List<FilterDefinition<BsonDocument>>();

            foreach (var kvp in queryParams)
            {
                // Here we assume that the value is always a string for simplicity.
                // You may need to handle other data types accordingly.
                var filter = builder.Eq(kvp.Key, kvp.Value);
                filters.Add(filter);
            }

            return builder.And(filters);
        }
    }
}
