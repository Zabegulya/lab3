using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab2.Models;

namespace lab2.Respositories
{
    public class JediesRepository : Repository<Jedi>, IRepository<Jedi>
    {
        public int Add(Jedi entity)
        {
            entity.Id = CurrentId++;
            Entities[entity.Id] = entity;
            return entity.Id;
        }

        public void Delete(Jedi entity)
        {
            Entities.Remove(entity.Id);
        }

        public Jedi Find(int id)
        {
            Jedi entity;
            Entities.TryGetValue(id, out entity);
            return entity;
        }

        public IEnumerable<Jedi> FindAll(Predicate<Jedi> predicate)
        {
            return from e in Entities
                   where predicate(e.Value)
                   select e.Value;
        }

        public void Update(Jedi entity)
        {
            Entities[entity.Id] = entity;
        }

        public static class BD
        {
            private static JediesRepository jediesRepository = new JediesRepository();

            public static JediesRepository Jedies
            {
                get { return jediesRepository; }
            }
        }
    }
}