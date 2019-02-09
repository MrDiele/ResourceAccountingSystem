using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class HousesController : ApiController
    {
        private HomeDataEntities db = new HomeDataEntities();

        // GET: api/Houses
        public IQueryable<Houses> GetHouses()                                                                           //получить список домов
        {
            return db.Houses;
        }

        // GET: api/Houses/5
        [ResponseType(typeof(Houses))]
        public IHttpActionResult GetHouses(int id)                                                          //найти дом по ID
        {
            Houses houses = db.Houses.Find(id);
            if (houses == null)
            {
                return NotFound();
            }
            HouseCounterView houseCounterView = db.HouseCounterView.Find(houses.IdHouse, houses.Address);
            if (houseCounterView == null)
            {
                return NotFound();
            }
            House answer = new House
            {
                IdHouse = houseCounterView.IdHouse,
                SerialNumber = Convert.ToInt32(houseCounterView.SerialNumber),
                Address = houseCounterView.Address,
                Indication = Convert.ToDecimal(houseCounterView.Indication)
            };

            return Ok(answer);
        }

        [ResponseType(typeof(House))]
        [Route("api/Houses/maxVal")]
        public IHttpActionResult GetMaxHouseConsumer()                                                          //найти дом с максимальным потреблением воды
        {
            var id = db.Database.SqlQuery<int>(@"DECLARE @return_value Int 
                                                EXEC @return_value = [dbo].[GetId_MaxConsumerHouse]                        
                                                SELECT @return_value as 'Return Value'").FirstOrDefault();
            Houses houses = db.Houses.Find(id);
            House answer = new House
            {
                Address = houses.Address,
                Indication = houses.Counters.First().Indication
            };
            return Ok(answer);
        }

        [ResponseType(typeof(Houses))]
        [Route("api/Houses/minVal")]
        public IHttpActionResult GetMinHouseConsumer()                                                          //найти дом с максимальным потреблением воды
        {
            var id = db.Database.SqlQuery<int>(@"DECLARE @return_value Int 
                                                EXEC @return_value = [dbo].[GetId_MinConsumerHouse]                     
                                                SELECT @return_value as 'Return Value'").FirstOrDefault();
            Houses houses = db.Houses.Find(id);
            House answer = new House
            {
                Address = houses.Address,
                Indication = houses.Counters.First().Indication
            };
            return Ok(answer);
        }

        // PUT: api/Houses/5
        [ResponseType(typeof(void))]                                                                          //зарегистрировать новый счётчик по ID дома или внести показания по ID дома
        public IHttpActionResult PutHouses(int id, Houses houses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != houses.IdHouse)
            {
                return BadRequest();
            }

            if (houses.Counters.Count != 0)
            {
                foreach (Counters counter in houses.Counters)
                {
                    try
                    {
                        db.AddCounterOrInputIndicationOfIdHouse(id, counter.SerialNumber, counter.Indication);
                    }
                    catch (Exception) { }
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        [Route("api/Houses/inputIndication")]                                                                    //зарегистрировать новые показания по ID дома
        public IHttpActionResult PutHouseIndication(Houses houses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (houses.Counters.Count != 0)
            {
                foreach (Counters counter in houses.Counters)
                {
                    try
                    {
                        db.InputIndicationByIdHouse(houses.IdHouse, counter.Indication);
                    }
                    catch (Exception) { }
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Houses
        [ResponseType(typeof(Houses))]
        public IHttpActionResult PostHouses(Houses houses)                                                  //создать новый дом
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Houses.Add(houses);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = houses.IdHouse }, houses);
        }

        // DELETE: api/Houses/5
        [ResponseType(typeof(Houses))]
        public IHttpActionResult DeleteHouses(int id)                                                       //удалить дом
        {
            Houses houses = db.Houses.Find(id);
            if (houses == null)
            {
                return NotFound();
            }
            try
            {
                db.DeleteHouseWithCounter(id);
            }
            catch (Exception) { }

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

        private bool HousesExists(int id)
        {
            return db.Houses.Count(e => e.IdHouse == id) > 0;
        }
    }
}