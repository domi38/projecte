using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projecte.Data.Services
{
    using Entities;

    public interface IEntityService<T> where T : IMongoEntity
    {
        void Create(T entity);
        void Delete(string id);

        T GetById(string id);

        void Update(T entity);
    }
}
