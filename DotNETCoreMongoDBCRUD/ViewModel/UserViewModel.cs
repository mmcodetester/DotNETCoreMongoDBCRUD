using MongoDB.Bson;

namespace DotNETCoreMongoDBCRUD.ViewModel
{
    public class UserViewModel
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? role_id { get; set; }
        public string? role_name { get; set; }
    }
}
