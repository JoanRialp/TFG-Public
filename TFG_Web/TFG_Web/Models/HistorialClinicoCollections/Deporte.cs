using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class Deporte
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string[] D_Cual { get; set; }
        public string D_Frecuencia { get; set; }
        public string D_DejoDeporte { get; set; }
        public string D_Porque { get; set; }
    }
}
