using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using lab2.Respositories;
using lab2.Models;
using System.Web.Http;

namespace lab2.Controllers
{
    [RoutePrefix("api/jedies")]
    public class JediesController : ApiController
    {
        private JediesRepository jedies = JediesRepository.BD.Jedies;
        private JediReferencesRepository jediReferences = JediReferencesRepository.BD.JediReferences;
        private AbilitiesRepository abilities = AbilitiesRepository.BD.Abilities;

        [Route("")]
        public IEnumerable<Jedi> Get()
        {
            return jedies.FindAll((jedi) => true);
        }

        [Route("{jediId:int}")]
        public Jedi Get(int jediId)
        {
            var jedi = jedies.Find(jediId);
            if (jedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return jedi;
        }

        [Route("")]
        public int Post([FromBody]Jedi jedi)
        {
            return jedies.Add(jedi);
        }

        [Route("")]
        public void Put([FromBody]Jedi jedi)
        {
            Jedi foundJedi = jedies.Find(jedi.Id);
            if (foundJedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            jedies.Update(jedi);
        }

        [Route("")]
        public void Delete([FromBody]Jedi jedi)
        {
            var foundJedi = jedies.Find(jedi.Id);
            if (foundJedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var foundReferences = jediReferences.FindAll(e => e.Master == jedi.Id || e.Padavan == jedi.Id);
            foreach (var reference in foundReferences) {
                jediReferences.Delete(reference);
            }

            jedies.Delete(jedi);
        }

        [Route("{jediId:int}/padavans")]
        public IEnumerable<Jedi> GetPadavans(int jediId)
        {
            var master = jedies.Find(jediId);
            if (master == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var references = jediReferences.FindAll((e) => e.Master == jediId);
            return from r in references
                   select jedies.Find(r.Padavan);
        }

        [Route("{jediId:int}/padavans")]
        public void PostPadavans(int jediId, [FromBody] Jedi padavan)
        {
            //масетер существует?
            var master = jedies.Find(jediId);
            if (master == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //падаван существует?
            var foundPadavan = jedies.Find(padavan.Id);
            if (foundPadavan == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            //связи еще нет?
            var foundRef = jediReferences.FindAll(e => e.Master == jediId && e.Padavan == padavan.Id);
            if (foundRef.Count() != 0)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            jediReferences.Add(new JediReference() { Master = jediId, Padavan = padavan.Id });
        }

        [Route("{jediId:int}/padavans")]
        public void DeletePadavan(int jediId, [FromBody] Jedi padavan)
        {
            var master = jedies.Find(jediId);
            if (master == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var foundPadavan = jedies.Find(padavan.Id);
            if (foundPadavan == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var references = jediReferences.FindAll(e => e.Master == jediId && e.Padavan == padavan.Id);
            if (references.Count() == 0)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            jediReferences.Delete(references.First());
        }

        [Route("{jediId:int}/abilities")]
        public IEnumerable<Ability> GetAbilities(int jediId)
        {
            var jedi = jedies.Find(jediId);
            if (jedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return abilities.FindAll(e => e.Jedi == jediId);
        }

        [Route("{jediId:int}/abilities")]
        public void PostAbilities(int jediId, [FromBody] Ability ability)
        {
            var jedi = jedies.Find(jediId);
            if (jedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ability.Jedi = jediId;
            abilities.Add(ability);
        }

        [Route("{jediId:int}/abilities")]
        public void DeleteAbilities(int jediId, [FromBody] Ability ability)
        {
            var jedi = jedies.Find(jediId);
            if (jedi == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var foundAbility = abilities.Find(ability.Id);
            if (foundAbility == null)
            {
                var responce = new HttpResponseMessage();
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            abilities.Delete(ability);
        }
    }
}
