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

        /// <summary>
        /// Получить список домов в системе.
        /// </summary>
        /// <returns>Список домов.</returns>
        // GET: api/Houses
        public IQueryable<Houses> GetHouses()                                                                          
        {
            return db.Houses;
        }

        /// <summary>
        /// Найти дом по ID.
        /// </summary>
        /// <param name="id">ID дома.</param>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        // GET: api/Houses/5
        [ResponseType(typeof(Houses))]
        public IHttpActionResult GetHouses(int id)                                                
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
            //формируем ответ
            House answer = new House
            {
                IdHouse = houseCounterView.IdHouse,
                SerialNumber = Convert.ToInt32(houseCounterView.SerialNumber),
                Address = houseCounterView.Address,
                Indication = Convert.ToDecimal(houseCounterView.Indication)
            };

            return Ok(answer);
        }

        /// <summary>
        /// Найти дом с максимальным потреблением воды.
        /// </summary>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        [ResponseType(typeof(House))]
        [Route("api/Houses/maxVal")]
        public IHttpActionResult GetMaxHouseConsumer()                                                     
        {
            House answer = null;
            //запускаем функцию 
            var houses = db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MaxConsumerHouse]()");
            
            //формируем ответ
            foreach (var house in houses)
            {
                answer = new House
                {
                    Address = house.Address,
                    Indication = house.Indication
                };
            }
            return Ok(answer);
        }

        /// <summary>
        /// Найти дом с максимальным потреблением воды.
        /// </summary>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        [ResponseType(typeof(House))]
        [Route("api/Houses/minVal")]
        public IHttpActionResult GetMinHouseConsumer()                                                       
        {
            House answer = null;
            //запускаем функцию 
            var houses = db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MinConsumerHouse]()");

            //формируем ответ
            foreach (var house in houses)
            {
                answer = new House
                {
                    Address = house.Address,
                    Indication = house.Indication
                };
            }
            return Ok(answer);
        }

        /// <summary>
        /// Зарегистрировать новый счётчик по ID дома или внести показания по ID дома.
        /// </summary>
        /// <param name="id">ID дома.</param>
        /// <param name="houses">Обьект Houses.</param>
        /// <returns>Статус сообщения Http.</returns>
        // PUT: api/Houses/5
        [ResponseType(typeof(void))]                                                                       
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
                //запускаем процедуру сохранения нового счётчика и привязки его к дому
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

        /// <summary>
        /// Зарегистрировать новые показания по ID дома.
        /// </summary>
        /// <param name="houses">Обьект Houses.</param>
        /// <returns>Статус сообщения Http.</returns>
        [ResponseType(typeof(void))]
        [Route("api/Houses/inputIndication")]                                                                   
        public IHttpActionResult PutHouseIndication(Houses houses)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (houses.Counters.Count != 0)
            {
                //запускаем процедуру сохранения показаний по ID дома
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

        /// <summary>
        /// Создать новый дом.
        /// </summary>
        /// <param name="houses">Обьект Houses.</param>
        // POST: api/Houses
        [ResponseType(typeof(Houses))]
        public IHttpActionResult PostHouses(Houses houses)                                              
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Добавляем новый дом
            db.Houses.Add(houses);

            try
            {
                //Сохраняем изменения
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = houses.IdHouse }, houses);
        }

        /// <summary>
        /// Удалить дом по ID.
        /// </summary>
        /// <param name="id">ID удаляемого дома.</param>
        /// <returns>Статус сообщения Http.</returns>
        // DELETE: api/Houses/5
        [ResponseType(typeof(Houses))]
        public IHttpActionResult DeleteHouses(int id)                                                     
        {
            //Проверяем существует ли дом
            Houses houses = db.Houses.Find(id);
            if (houses == null)
            {
                return NotFound();
            }
            try
            {
                //запускаем процедуру удаления
                db.DeleteHouseWithCounter(id);
            }
            catch (Exception) { }

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
    }
}