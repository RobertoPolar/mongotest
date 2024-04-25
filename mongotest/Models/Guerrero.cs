using MongoDB.Bson.Serialization.Attributes;

namespace mongotest.Models
{
    public class Guerrero
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("nickname")]
        public string Nickname { get; set; }

        [BsonElement("birthday")]
        public string Birthday { get; set; }

        [BsonElement("weapons")]
        public List<Weapon> Weapons { get; set; }

        [BsonElement("attributes")]
        public Attribute Attributes { get; set; }

        [BsonElement("keyAttribute")]
        public string KeyAttribute { get; set; }
    }
}
