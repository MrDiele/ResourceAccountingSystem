using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.DAL
{
    public class HouseCounterViewsDAL
    {
        private HomeDataEntities db;

        public HouseCounterViewsDAL()
        {
            db = new HomeDataEntities();
        }

        public  List<HouseCounterView> GetHouseCounterView()
        {
            return new List<HouseCounterView>(db.HouseCounterView);
        }

        public  HouseCounterView GetHouseCounter(int IdHouse, string Address)
        {
            return db.HouseCounterView.Find(IdHouse, Address);
        }

        ~ HouseCounterViewsDAL()
        {
            db.Dispose();
        }
    }
}