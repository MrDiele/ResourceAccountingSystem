using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class HouseCounterViewsBL
    {
        /// <summary>
        /// Получает список из представления.
        /// </summary>
        /// <returns></returns>
        public static List<HouseCounterView> GetHouseCounterView()
        {
            return HouseCounterViewsDAL.GetHouseCounterView();
        }
    }
}