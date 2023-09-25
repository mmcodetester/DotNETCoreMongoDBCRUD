using DotNETCoreMongoDBCRUD.Context;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace DotNETCoreMongoDBCRUD.Entity
{
    [BsonIgnoreExtraElements]
    [BsonCollection("Product")]
    public class Product
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [DataMember]
        [BsonElement("name")]
        public string? name { get; set; }
        [DataMember]
        [BsonElement("generation")]
        public string? generation { get; set; }
        [DataMember]
        [BsonElement("made")]
        public string? made { get; set; }
        [DataMember]
        [BsonElement("series")]
        public string? series { get; set; }
        [DataMember]
        [BsonElement("graphic")]
        public string? graphic { get; set; }
        [DataMember]
        [BsonElement("ram")]
        public string? ram { get; set; }
        [DataMember]
        [BsonElement("webcam")]
        public bool? hdwebcam { get; set; }
        [DataMember]
        [BsonElement("display")]
        public string? display { get; set; }
    }
}
