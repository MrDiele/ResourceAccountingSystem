using System;
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
        private IHomeModelContext db = new HomeDataEntities();

        public CountersController() { }

        public CountersController(IHomeModelContext context)
        {
            db = context;
        }

        /// <summary>
        /// Получить список счётчиков в системе.
        /// </summary>
        /// <returns>Список счётчиков.</returns>
        // GET: api/Counters
        public IQueryable<Counters> GetCounters()
        {
            return db.Counters;
        }

        /// <summary>
        /// Создать новый счётчик.
        /// </summary>
        /// <param name="counters">Обьект Counters.</param>
        // POST: api/Houses
        [ResponseType(typeof(Houses))]
        public IHttpActionResult PostCounters(Counters counters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Добавляем новый счётчик
            db.Counters.Add(counters);

            try
            {
                //Сохраняем изменения
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = counters.IdCounter }, counters);
        }

        /// <summary>
        /// Внести показания по серийному номеру.
        /// </summary>
        /// <param name="counters">Обьект счётчик с новыми показаниями</param>
        /// <returns>Статус сообщения Http.</returns>
        // PUT: api/Counters
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounters(Counters counters)
        {
            bool needSave = false;
            if (!ModelState.IsValid)
            {
                return StatusCode(HttpStatusCode.BadRequest);//BadRequest(ModelState);
            }

            //if (!CountersExists(counters.IdCounter))
            //{
            //    return NotFound();
            //}

            //ищем счётчик по серийному номеру
            IQueryable<Counters> cts = db.Counters.Where(c => c.SerialNumber == counters.SerialNumber); 
            

            //если нашли счётчик вносим изменения и выставляем признак необходимости сохранения
            if (cts.ToList().Count() != 0)                     
            {
                foreach (Counters counter in cts)
                {
                    if (counter.Indication < counters.Indication)
                    {
                        counter.Indication = counters.Indication;
                        db.MarkAsModified(counter);
                        //db.Entry(counter).State = EntityState.Modified;
                        needSave = true;
                    }
                }
            }
            else  
            {
                return StatusCode(HttpStatusCode.NotFound);//BadRequest();
            }

            //проверяем необходимо ли сохранение, если да то сохранияем
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

        /// <summary>
        /// Диструктор класса.
        /// </summary>
        /// <param name="disposing"></param>
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