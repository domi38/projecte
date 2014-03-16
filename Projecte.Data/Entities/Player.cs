using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Player : MongoEntity
    {
        public Player()
        {
            Scores = new List<Score>();
        }
        
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }

        

        public List<Score> Scores { get; set; }
    }


    public class PlayerVM
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }

        

        public List<Score> Scores { get; set; }
    }
}
