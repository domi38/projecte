using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Services
{
    using Entities;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public class PlayerService :EntityService<Player>
    {
        public void AddScore(string playerId, Score score)
        {
            var playerObjectId = new ObjectId(playerId);
            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
                Query<Player>.EQ(p => p.Id, playerObjectId),
                Update<Player>.Push(p => p.Scores, score),
                new MongoUpdateOptions
                {
                    WriteConcern = WriteConcern.Acknowledged
                });
            if (updateResult.DocumentsAffected == 0)
            {
 
            }
        }
        public IEnumerable<Player> GetPlayersDetails(int limit, int skip)
        {
            var playerCursor = this.MongoConnectionHandler.MongoCollection.FindAllAs<Player>()
                .SetSortOrder(SortBy<Player>.Ascending(p => p.Name))
                .SetLimit(limit)
                .SetSkip(skip)
                .SetFields(Fields<Player>.Include(p => p.Id, p => p.Name));
            return playerCursor;
        }
        public override void Update(Player entity)
        {
            var updateResult = this.MongoConnectionHandler.MongoCollection.Update(
                Query<Player>.EQ(p => p.Id, entity.Id),
                Update<Player>.Set(p => p.Name, entity.Name)
                    .Set(p => p.Gender, entity.Gender),
                new MongoUpdateOptions
                {
                    WriteConcern = WriteConcern.Acknowledged
                });
            if (updateResult.DocumentsAffected == 0)
            {

            }

        }
    }
}
