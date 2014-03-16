using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Entities
{
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Game : MongoEntity
    {
        public Game()
        {
            Taula= new List<List<char>>();
        }
        
        public string Name { get; set;}

        public List<List<char>> Taula { get; set; }
    }

    
}
