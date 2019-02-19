using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.DAL
{
    public class HouseCounterViewsDAL
    {
        #region Fields
        private HomeDataEntities db;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public HouseCounterViewsDAL()
        {
            db = new HomeDataEntities();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получить список HouseCounterView из представления в базе.
        /// </summary>
        /// <returns></returns>
        public List<HouseCounterView> GetHouseCounterView()
        {
            return new List<HouseCounterView>(db.HouseCounterView);
        }

        /// <summary>
        /// Найти обьект HouseCounterView в базе по параметрам.
        /// </summary>
        /// <param name="IdHouse">Id дома.</param>
        /// <param name="Address">Адрес.</param>
        /// <returns></returns>
        public HouseCounterView GetHouseCounter(int IdHouse, string Address)
        {
            return db.HouseCounterView.Find(IdHouse, Address);
        }
        #endregion

        #region Distructor
        ~HouseCounterViewsDAL()
        {
            db.Dispose();
        }
        #endregion
    }
}