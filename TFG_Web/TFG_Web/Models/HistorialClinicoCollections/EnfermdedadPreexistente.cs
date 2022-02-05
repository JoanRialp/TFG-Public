using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class EnfermdedadPreexistente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string A_Tipo { get; set; }
        public string A_Edad { get; set; }
        public string A_Otros { get; set; }
        public string A_Especificar { get; set; }
    }
}
