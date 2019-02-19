using ResourceAccountingSystem.Models;
using ResourceAccountingSystem.Utils;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ResourceAccountingSystem.DAL
{
    public class CountersDAL
    {
        #region Fields
        private IHomeModelContext db;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public CountersDAL()
        {
             db = new HomeDataEntities();
        }

        /// <summary>
        /// Конструктор класса для тестов.
        /// </summary>
        /// <param name="context"></param>
        public CountersDAL(IHomeModelContext context)
        {
            db = context;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получить список счётчиков из базы.
        /// </summary>
        /// <returns></returns>
        public List<Counters> GetCounters()
        {
            return new List<Counters>(db.Counters);
        }

        /// <summary>
        /// Добавить новый счётчик в базу.
        /// </summary>
        /// <param name="counters"></param>
        /// <returns></returns>
        public Counters AddNewCounter(Counters counters)
        {
            //Добавляем новый счётчик
            db.Counters.Add(counters);

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
            return counters;
        }

        /// <summary>
        /// Получить счётчик из базы по серийному номеру.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public List<Counters> GetCounter(int serialNumber)
        {
            return new List<Counters>(db.Counters.Where(c => c.SerialNumber == serialNumber));
        }

        /// <summary>
        /// Редактировать счётчик в базе.
        /// </summary>
        /// <param name="counter"></param>
        public void EditCounter(Counters counter)
        {
            db.MarkAsModified(counter);
            try
            {
                //Сохраняем изменения
                Save(db);
            }
            catch (DbUpdateException) { }
        }

        /// <summary>
        /// Проверить существование счётчика в базе.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int FindCount(int id)
        {
            return db.Counters.Count(e => e.IdCounter == id);
        }
        #endregion

        #region Private methods
        private void Save(IHomeModelContext db)
        {
            db.SaveChanges();
        }
        #endregion

        #region Distructor
        ~CountersDAL()
        {
            db.Dispose();
        }
        #endregion
    }
}