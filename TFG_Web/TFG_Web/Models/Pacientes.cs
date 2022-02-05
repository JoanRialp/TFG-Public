using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models
{
    public class Pacientes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string usuarioId { get; set; }
        public string P_Nombre { get; set; }
        public DateTime P_PrimeraConsulta { get; set; }
        public string P_Correo { get; set; }
        public string P_Apellidos { get; set; }
        public bool P_Finalizado { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> HistorialClinicoId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ControlSistemaCompresor { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ControlCampanaPectusExcavatum { get; set; }
    }
}
