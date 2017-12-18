using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab2.Models;
namespace lab2.Respositories
{
    public class JediReferencesRepository : Repository<JediReference>, IRepository<JediReference>
    {
        public int Add(JediReference entity)
        {
            entity.Id = CurrentId++;
            Entities[entity.Id] = entity;
            return entity.Id;
        }

        public void Delete(JediReference entity)
        {
            Entities.Remove(entity.Id);
        }

        public JediReference Find(int id)
        {
            return Entities[id];
        }

        public IEnumerable<JediReference> FindAll(Predicate<JediReference> predicate)
        {
            return from e in Entities
                   where predicate(e.Value)
                   select e.Value;
        }

        public void Update(JediReference entity)
        {
            Entities[entity.Id] = entity;
        }

        public static class BD
        {
            private static JediReferencesRepository jediReferencesRepository = new JediReferencesRepository();

            public static JediReferencesRepository JediReferences
            {
                get { return jediReferencesRepository; }
            }
        }
    }
}