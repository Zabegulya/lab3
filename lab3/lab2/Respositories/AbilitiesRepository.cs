using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab2.Models;

namespace lab2.Respositories
{
    public class AbilitiesRepository : Repository<Ability>, IRepository<Ability>
    {
        public int Add(Ability entity)
        {
            entity.Id = CurrentId++;
            Entities[entity.Id] = entity;
            return entity.Id;
        }

        public void Delete(Ability entity)
        {
            Entities.Remove(entity.Id);
        }

        public Ability Find(int id)
        {
            Ability ability;
            Entities.TryGetValue(id, out ability);
            return ability;
        }

        public IEnumerable<Ability> FindAll(Predicate<Ability> predicate)
        {
            return from e in Entities
                   where predicate(e.Value)
                   select e.Value;
        }

        public void Update(Ability entity)
        {
            Entities[entity.Id] = entity;
        }

        public static class BD
        {
            private static AbilitiesRepository abilitiesRepository = new AbilitiesRepository();

            public static AbilitiesRepository Abilities
            {
                get { return abilitiesRepository; }
            }
        }
    }
}