using System.Linq;
using System.Web.Http;
using ResourceAccountingSystem.Models;

namespace ResourceAccountingSystem.Controllers
{
    public class HouseCounterViewsController : ApiController
    {
        private HomeDataEntities db = new HomeDataEntities();

        // GET: api/HouseCounterViews
        public IQueryable<HouseCounterView> GetHouseCounterView()
        {
            return db.HouseCounterView;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HouseCounterViewExists(int id)
        {
            return db.HouseCounterView.Count(e => e.IdHouse == id) > 0;
        }
    }
}