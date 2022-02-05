using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class Anamnesis
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string A_EnfermedadPreexistente { get; set; }
        public string A_Alergia { get; set; }
        public string A_AlergiaCual { get; set; }
        public string A_Observaciones { get; set; }
        public string A_OtrasAlergias { get; set; }
        public string A_Deporte { get; set; }
        public string A_Fuma { get; set; }
        public string A_Medicacion { get; set; }
        public string A_Cual { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> DolorPecho { get; set; }
    }
}
