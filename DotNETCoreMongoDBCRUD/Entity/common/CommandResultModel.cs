namespace DotNETCoreMongoDBCRUD.Entity.common
{
    public class CommandResultModel
    {
        public bool success { get; set; }
        public string? id { get; set; }
        public List<string> messages { get; set; }
        public CommandResultModel() 
        { 
            messages=new List<string>();
        }
    }
}
