using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class Score
    {
        public ObjectId GameId { get; set; }
        public string GameName { get; set; }
        public int ScoreValue { get; set; }
        public DateTime ScoreDateTime { get; set; }
    }

}
