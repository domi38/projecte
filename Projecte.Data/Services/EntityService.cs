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

    public abstract class EntityService<T> : IEntityService<T> where T :IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHandler;

        public virtual void Create(T entity)
        {
            var result = this.MongoConnectionHandler.MongoCollection.Save(entity,
                new MongoInsertOptions
                {
                    WriteConcern = WriteConcern.Acknowledged
                });
            if (!result.Ok)
            {

            }
        }
        public virtual void Delete(string id)
        {
            var result=this.MongoConnectionHandler.MongoCollection.Remove(
                Query<T>.EQ(e => e.Id,
                new ObjectId(id)),
                RemoveFlags.None,
                WriteConcern.Acknowledged);
            if (!result.Ok)
            {

            }

        }
        protected EntityService()
        {
            MongoConnectionHandler = new MongoConnectionHandler<T>();
        }
        public virtual T GetById(string id)
        {
            var entityQuery = Query<T>.EQ(e => e.Id, new ObjectId(id));
            return this.MongoConnectionHandler.MongoCollection.FindOne(entityQuery);
        }
        public abstract void Update(T entity);
    }
}
