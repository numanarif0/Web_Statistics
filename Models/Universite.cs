using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YokIstatistikWeb.Models
{
    public class Universite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string universite { get; set; }
        public string tur { get; set; }
        public string sehir { get; set; }

        public List<Birim> birimler { get; set; }

        public string yil { get; set; }
    }
}
