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
        public List<HouseCounter> GetHouseCounterView()
        {
            List<HouseCounter> houseCounter = new List<HouseCounter>();
            var list = houseCounterViewsDAL.GetHouseCounterView();
            foreach (HouseCounterView hcv in list)
            {
                houseCounter.Add(new HouseCounter { IdHouse = hcv.IdHouse, Address= hcv.Address, SerialNumber = hcv.SerialNumber, Indication = hcv.Indication});
            }
            return houseCounter;
        }
        #endregion
    }
}