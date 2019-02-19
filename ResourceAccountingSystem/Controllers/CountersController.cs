using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ResourceAccountingSystem.BusinessLogic;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class CountersController : ApiController
    {
        CountersBL countersBL = new CountersBL();

        /// <summary>
        /// Получить список счётчиков в системе.
        /// </summary>
        /// <returns>Список счётчиков.</returns>
        // GET: api/Counters
        public List<Counter> GetCounters()
        {
            return countersBL.GetHouses();
        }

        /// <summary>
        /// Создать новый счётчик.
        /// </summary>
        /// <param name="counters">Обьект Counters.</param>
        // POST: api/Houses
        [ResponseType(typeof(Houses))]
        public IHttpActionResult PostCounters(Counter counter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newHouse = countersBL.AddNewCounter(counter);
            if (newHouse != null)
                return CreatedAtRoute("DefaultApi", new { id = newHouse.IdCounter }, newHouse);
            else
                return Conflict();
        }

        /// <summary>
        /// Внести показания по серийному номеру.
        /// </summary>
        /// <param name="counters">Обьект счётчик с новыми показаниями</param>
        /// <returns>Статус сообщения Http.</returns>
        // PUT: api/Counters
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCounters(Counter counter)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            if (countersBL.InputIndication(counter))
                return StatusCode(HttpStatusCode.NoContent);
            else
                return StatusCode(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Диструктор класса.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}