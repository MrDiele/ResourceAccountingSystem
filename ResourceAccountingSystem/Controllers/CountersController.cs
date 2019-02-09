using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class CountersController : ApiController
    {
        private HomeDataEntities db = new HomeDataEntities();

        // GET: api/Counters
        public IQueryable<Counters> GetCounters()
        {
            return db.Counters;
        }

        // PUT: api/Counters
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounters(Counters counters)       //внести показания по серийному номеру
        {
            bool needSave = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Counters> cts = db.Counters.Where(c => c.SerialNumber == counters.SerialNumber);           
            if (cts.ToList().Count() != 0)                     
            {
                foreach (Counters counter in cts)
                {
                    if (counter.Indication < counters.Indication)
                    {
                        counter.Indication = counters.Indication;
                        db.Entry(counter).State = EntityState.Modified;
                        needSave = true;
                    }
                }
            }
            else  
            {
                return BadRequest();
            }

            if (needSave)
            {
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException) { }
            }      

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountersExists(int id)
        {
            return db.Counters.Count(e => e.IdCounter == id) > 0;
        }
    }
}