using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class DolorPecho
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string[,] DP_Tipo { get; set; }
        public string[,] DP_Frecuencia { get; set; }
    }
}
