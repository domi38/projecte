using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Entities
{
    using MongoDB.Bson;

    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}
