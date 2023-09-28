using DotNETCoreMongoDBCRUD.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DotNETCoreMongoDBCRUD.Entity
{
    [BsonIgnoreExtraElements]
    [BsonCollection("User")]
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; } = ObjectId.Empty;
        [DataMember]
        [BsonElement("name")]
        public string? name { get; set; }
        [DataMember]
        [BsonElement("password")]
        public string? password { get; set; }
        [DataMember]
        [BsonElement("role")]
        public ObjectId role_id { get; set; } = ObjectId.Empty;
    }
}
