using System.Collections.Generic;
using System.Web.Http;
using ResourceAccountingSystem.BusinessLogic;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class HouseCounterViewsController : ApiController
    {
        HouseCounterViewsBL houseCounterViewsBL = new HouseCounterViewsBL();

        /// <summary>
        /// Получить список из представления HouseCounterView.
        /// </summary>
        /// <returns>Список обьектов.</returns>
        // GET: api/HouseCounterViews
        public List<HouseCounterView> GetHouseCounterView()
        {
            return houseCounterViewsBL.GetHouseCounterView();
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