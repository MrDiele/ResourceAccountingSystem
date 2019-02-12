using System.Linq;
using System.Web.Http;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class HouseCounterViewsController : ApiController
    {
        private HomeDataEntities db = new HomeDataEntities();

        /// <summary>
        /// Получить список обьектов представления HouseCounterView.
        /// </summary>
        /// <returns>Список обьектов.</returns>
        // GET: api/HouseCounterViews
        public IQueryable<HouseCounterView> GetHouseCounterView()
        {
            return db.HouseCounterView;
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