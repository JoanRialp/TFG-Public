using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class ClasificacionPectus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CP_FormaCostillas { get; set; }
        public string CP_Alerones { get; set; }
        public string CP_Hombros { get; set; }
        public string CP_AntepulsionHombros { get; set; }
        public string CP_AsimetriaPosterior { get; set; }
        public string CP_PosicionCifotica { get; set; }
        public string CP_Estrias { get; set; }
        public string CP_Donde { get; set; }
        public string CP_Tratamiento { get; set; }
        public string CP_Anotaciones { get; set; }
    }
}
