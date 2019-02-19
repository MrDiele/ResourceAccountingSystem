using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class HouseCounterViewsBL
    {
        #region Fields
        private HouseCounterViewsDAL houseCounterViewsDAL;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public HouseCounterViewsBL()
        {
            houseCounterViewsDAL = new HouseCounterViewsDAL();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получает список из представления.
        /// </summary>
        /// <returns></returns>
        public List<HouseCounterView> GetHouseCounterView()
        {
            return houseCounterViewsDAL.GetHouseCounterView();
        }
        #endregion
    }
}