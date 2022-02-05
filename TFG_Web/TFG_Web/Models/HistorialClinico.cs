using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TFG_Web.Models
{
    public class HistorialClinico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string HC_Nombre { get; set; }
        public string HC_Apellidos { get; set; }
        public DateTime HC_FechaPrimeraConsulta { get; set; }
        public DateTime? HC_FechaUltimaConsulta { get; set; }
        public DateTime HC_Edad { get; set; }
        public string HC_Genero { get; set; }
        public string HC_MotivoConsulta { get; set; }
        public string HC_EdadNotaronDeformidadToracica { get; set; }
        public string HC_EmpeoradoUltimamente { get; set; }
        public string HC_Cuando { get; set; }
        public string HC_HermanosGemelos { get; set; }
        public string[,] HC_AntecendentesFamilia { get; set; } = new string[6,6];
        public string HC_OtrosFamiliares { get; set; }
        public string HC_ConsultadoPectus { get; set; }
        public string HC_ConsultadoPectusCuando { get; set; }
        public string HC_ConsultadoPectusDonde { get; set; }
        public string HC_CirurgiaPrevia { get; set; }
        public string HC_TipoPectus { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> CirugiaPrevia { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Anamnesis { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> EnfermdedadPreexistente { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Deporte { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SignosSintomasClinicos { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ClasificacionPectus { get; set; }
    }
}
