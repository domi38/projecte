using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Services
{
    using Entities;
    using MongoDB.Driver.Builders;

    public class GameService : EntityService<Game>
    {
        public IEnumerable<Game> GetGamesDetails(int limit, int skip)
        {
            var gamesCursor=this.MongoConnectionHandler.MongoCollection.FindAllAs<Game>()
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<Game>.Include(g => g.Id, g => g.Name, g => g.Taula));
            return gamesCursor;
        }
        public override void Update(Game entity)
        {
            
        }
    }
}
