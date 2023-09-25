using DotNETCoreMongoDBCRUD.Utli;
using Microsoft.AspNetCore.Mvc;

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
            string? sortOrder = GetRequestParameters<string>("so");
            int pageSize = GetRequestParameters<int>("ps");
            int pageNumber = GetRequestParameters<int>("pn");
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
            op.fromPage = pageNumber;
            op.fromRecord = skip;
            op.recordPerPage = pageSize;
            return op;
        }
    }
}
