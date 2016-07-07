using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompromissoRest.Controllers
{
    public class CompromissoController : ApiController
    {
        public IEnumerable<Models.Compromisso> Get()
        {
            Models.CompromissoDataContext dc = new Models.CompromissoDataContext();
            var r1 = from r in dc.Compromissos orderby r.data select r ;
            return r1.ToList();
        }

        public void Delete(int id)
        {
            Models.CompromissoDataContext dc = new Models.CompromissoDataContext();
            Models.Compromisso r = (from f in dc.Compromissos where f.id == id select f).Single();
            dc.Compromissos.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }

        //O bom e velho update
        public void Put(int id, [FromBody] string value)
        {
            Models.Compromisso x = JsonConvert.DeserializeObject<Models.Compromisso>(value);
            Models.CompromissoDataContext dc = new Models.CompromissoDataContext();
            Models.Compromisso rest = (from f in dc.Compromissos where f.id == id select f).Single();
            rest.descricao = x.descricao;
            rest.local = x.local;
            rest.data = x.data;
            rest.realizado = x.realizado;
            dc.SubmitChanges();
        }

        public void Post([FromBody] string value)
        {
            List<Models.Compromisso> r = JsonConvert.DeserializeObject<List<Models.Compromisso>>(value);
            Models.CompromissoDataContext dc = new Models.CompromissoDataContext();
            dc.Compromissos.InsertAllOnSubmit(r);
            dc.SubmitChanges();
        }
    }
}
