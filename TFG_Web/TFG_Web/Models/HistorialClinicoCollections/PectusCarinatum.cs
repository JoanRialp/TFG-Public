using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class PectusCarinatum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PC_Tipo { get; set; }
        public string PC_Simetria { get; set; }
        public string PC_RotacionEsternal { get; set; }
        public string PC_PresionCorreccion { get; set; }
    }
}
