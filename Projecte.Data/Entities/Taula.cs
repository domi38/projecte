using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecte.Data.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Taula
    {
        public string GameName { get; set; }
        public int ScoreValue { get; set; }
        public DateTime ScoreDateTime { get; set; }
    }
}
