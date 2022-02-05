using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class PectusMixto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PM_UbicacionDefecto { get; set; }
        public string PM_PresionCorreccionPectusCarinatum { get; set; }
        public string PM_RotacionEsternal { get; set; }
    }
}
