using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace TFG_Web.Models
{
    public class ControlSistemaCompresor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "La Fecha es requerida ")]
        public DateTime CSC_Fecha { get; set; }
        public int CSC_HorasUsoDia { get; set; }
        public int CSC_HorasUsoNoche { get; set; }
        public string CSC_PC { get; set; }
        public int CSC_Grupo { get; set; }
        public string CSC_PTPreAjustes { get; set; }
        public string CSC_PTPostAjustes { get; set; }
        public string CSC_AjustesSistemaCompresorDinamico { get; set; }
        public string CSC_F1 { get; set; }
        public string CSC_F2 { get; set; }
        public string CSC_F3 { get; set; }
        public string CSC_F4 { get; set; }
        public string CSC_F5 { get; set; }
        public string CSC_Doblador { get; set; }
        public string CSC_RecambioPieza { get; set; }
        public string CSC_PAD { get; set; }
        public string CSC_Compilaciones { get; set; }
        public string CSC_Observaciones { get; set; }

    }
}