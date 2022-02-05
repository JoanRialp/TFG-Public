using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace TFG_Web.Models
{
    public class ControlCampanaPectusExcavatum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "La Fecha es requerida ")]
        public DateTime CCPE_Fecha { get; set; }
        public int CCPE_HorasUsoDia { get; set; }
        public int CCPE_HorasUsoNoche { get; set; }
        public string CCPE_NumeroPumpsUso { get; set; }
        public string CCPE_PresionTratamiento { get; set; }
        public int CCPE_Uso { get; set; }
        public string CCPE_Indicada { get; set; }
        public string CCPE_NumeroPumpsIndicados { get; set; }
        public string CCPE_Complicaciones { get; set; }
        public string CCPE_Fotos { get; set; }
        public string CCPE_Observaciones { get; set; }
    }
}