using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class SignosSintomasClinicos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SSC_DificultadRespiratoriaReposo { get; set; }
        public string SSC_DificultadRespiratoriaActividadFisica { get; set; }
        public string SSC_NeumoniasRepeticion { get; set; }
        public string SSC_OtrasNeumoniasRepeticion { get; set; }
        public string SSC_DolorEspalda { get; set; }
        public string SSC_PalpitacionesTaquicardia { get; set; }
        public string SSC_Otros { get; set; }
        public string[,] SSC_SignosSintomas  { get; set; }

        public string SSC_TipoPectusNombre { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> SSC_TipoPectus { get; set; }

    }
}
