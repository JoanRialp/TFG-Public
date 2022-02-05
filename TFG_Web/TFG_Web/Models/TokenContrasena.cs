using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TFG_Web.Models
{
    public class TokenContrasena
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TC_IdUsuario { get; set; }
        public string TC_Token { get; set; }
    }
}