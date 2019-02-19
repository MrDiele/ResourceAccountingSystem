using ResourceAccountingSystem.Models;
using ResourceAccountingSystem.Utils;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace ResourceAccountingSystem.DAL
{
    public class HousesDAL
    {
        #region Fields
        private HomeDataEntities db;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public HousesDAL()
        {
            db = new HomeDataEntities();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получить список домов из базы.
        /// </summary>
        /// <returns></returns>
        public List<Houses> GetHouses()
        {
            return new List<Houses>(db.Houses);
        }

        /// <summary>
        /// Добавить новый дом в базу.
        /// </summary>
        /// <param name="houses"></param>
        /// <returns></returns>
        public Houses AddHouse(Houses houses)
        {
            //Добавляем новый дом
            db.Houses.Add(houses);

            try
            {
                //Сохраняем изменения
                Save(db);
            }
            catch (DbUpdateException e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.Message);
                return null;    
            }
            return houses;
        }

        /// <summary>
        /// Получить дом по ID из базы.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <returns></returns>
        public Houses GetHouse(int id)
        {
            return db.Houses.Find(id);
        }

        /// <summary>
        /// Получить результат скалярной функции находящий дом с максимальным потреблением.
        /// </summary>
        /// <returns></returns>
        public List<House> GetMaxHouseConsumer()
        {
            return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MaxConsumerHouse]()"));
        }

        /// <summary>
        /// Получить результат скалярной функции находящей дом с минимальных потреблением.
        /// </summary>
        /// <returns></returns>
        public List<House> GetMinHouseConsumer()
        {
            return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MinConsumerHouse]()"));
        }

        /// <summary>
        /// Процедура создания нового счётчика и привязка его к существующему дому.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <param name="serialNumber">Серийноый номер счётчика.</param>
        /// <param name="indication">Начальное показание счётчика.</param>
        public void AddCounterAndInputIndicationForHouse(int id, int serialNumber, decimal indication)
        {
            db.AddCounterOrInputIndicationOfIdHouse(id, serialNumber, indication);
        }

        /// <summary>
        /// Внести показания в базу по ID дома.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <param name="indication">Новое показание.</param>
        public void InputIndicationByIdHouse(int id, decimal indication)
        {
            db.InputIndicationByIdHouse(id, indication);
        }

        /// <summary>
        /// Удалить дом из системы по ID.
        /// </summary>
        /// <param name="id">Id дома.</param>
        public void DeleteHouse(int id)
        {
            db.DeleteHouseWithCounter(id);
        }
        #endregion

        #region Private methods
        private void Save(HomeDataEntities db)
        {
            db.SaveChanges();
        }
        #endregion

        #region Distructor
        ~HousesDAL()
        {
            db.Dispose();
        }
        #endregion
    }
}