using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class PectusExcavatum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PE_Simetria { get; set; }
        public string PE_HundimientoToracicoParado { get; set; }
        public string PE_HundimientoToracicoAcostado { get; set; }
        public string PE_RotacionEsternal { get; set; }
    }
}
