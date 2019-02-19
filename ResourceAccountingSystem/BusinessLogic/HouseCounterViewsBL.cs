using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class HouseCounterViewsBL
    {
        private HouseCounterViewsDAL houseCounterViewsDAL;

        public HouseCounterViewsBL()
        {
            houseCounterViewsDAL = new HouseCounterViewsDAL();
        }

        /// <summary>
        /// Получает список из представления.
        /// </summary>
        /// <returns></returns>
        public List<HouseCounterView> GetHouseCounterView()
        {
            return houseCounterViewsDAL.GetHouseCounterView();
        }
    }
}