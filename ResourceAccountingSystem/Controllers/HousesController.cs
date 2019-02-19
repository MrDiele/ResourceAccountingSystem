using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ResourceAccountingSystem.BusinessLogic;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class HousesController : ApiController
    {
        HousesBL housesBL = new HousesBL();

        /// <summary>
        /// Получить список домов в системе.
        /// </summary>
        /// <returns>Список домов.</returns>
        // GET: api/Houses
        public List<House> GetHouses()                                                                          
        {
            return housesBL.GetHouses();
        }

        /// <summary>
        /// Найти дом по ID.
        /// </summary>
        /// <param name="id">ID дома.</param>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        // GET: api/Houses/5
        [ResponseType(typeof(House))]
        public IHttpActionResult GetHouses(int id)                                                
        {
            return Ok(housesBL.GetHouse(id));
        }

        /// <summary>
        /// Найти дом с максимальным потреблением воды.
        /// </summary>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        [ResponseType(typeof(House))]
        [Route("api/Houses/maxVal")]
        public IHttpActionResult GetMaxHouseConsumer()                                                     
        {
            return Ok(housesBL.GetMaxHouseConsumer());
        }

        /// <summary>
        /// Найти дом с максимальным потреблением воды.
        /// </summary>
        /// <returns>В случае успешного выполнения возвращает сообщение содержащее обьект House.</returns>
        [ResponseType(typeof(House))]
        [Route("api/Houses/minVal")]
        public IHttpActionResult GetMinHouseConsumer()                                                       
        {
            return Ok(housesBL.GetMinHouseConsumer());
        }

        /// <summary>
        /// Зарегистрировать новый счётчик по ID дома или внести показания по ID дома.
        /// </summary>
        /// <param name="id">ID дома.</param>
        /// <param name="houses">Обьект Houses.</param>
        /// <returns>Статус сообщения Http.</returns>
        // PUT: api/Houses/5
        [ResponseType(typeof(void))]                                                                       
        public IHttpActionResult PutHouses(int id, Counter counter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (housesBL.AddNewCounterInHouse(id, counter))
                return StatusCode(HttpStatusCode.NoContent);
            else
                return StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Зарегистрировать новые показания по ID дома.
        /// </summary>
        /// <param name="houses">Обьект Houses.</param>
        /// <returns>Статус сообщения Http.</returns>
        [ResponseType(typeof(void))]
        [Route("api/Houses/inputIndication")]                                                                   
        public IHttpActionResult PutHouseIndication(House house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (housesBL.InputIndicationByIdHouse(house))
                return StatusCode(HttpStatusCode.NoContent);
            else
                return StatusCode(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Создать новый дом.
        /// </summary>
        /// <param name="houses">Обьект Houses.</param>
        // POST: api/Houses
        [ResponseType(typeof(Houses))]
        public IHttpActionResult PostHouses(House house)                                              
        {
            if (!ModelState.IsValid)                              
            {
                return BadRequest(ModelState);
            }

            var newHouse = housesBL.AddNewHouse(house);
            if (newHouse != null)
                return CreatedAtRoute("DefaultApi", new { id = newHouse.IdHouse }, newHouse);
            else
                return Conflict();
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
            if (housesBL.DelHouse(id))
                return StatusCode(HttpStatusCode.NoContent);
            else
                return StatusCode(HttpStatusCode.InternalServerError);
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