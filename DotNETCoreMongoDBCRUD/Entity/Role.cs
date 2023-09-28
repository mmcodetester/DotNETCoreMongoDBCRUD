using DotNETCoreMongoDBCRUD.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace DotNETCoreMongoDBCRUD.Entity
{
    [BsonIgnoreExtraElements]
    [BsonCollection("Role")]
    public class Role
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [DataMember]
        [BsonElement("name")]
        public string? name { get; set; }
    }
}
