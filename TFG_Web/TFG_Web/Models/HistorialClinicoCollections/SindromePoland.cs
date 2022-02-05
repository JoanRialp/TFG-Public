using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace TFG_Web.Models.HistorialClinicoCollections
{
    public class SindromePoland
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SP_AnomaliaToracica { get; set; }
        public string SP_AnomaliaMamaria { get; set; }
        public string SP_AnomaliaComplejoPezonAreola { get; set; }
        public string SP_T2T3T4 { get; set; }
        public string SP_PCPSIPectusCarinatum { get; set; }
    }
}
