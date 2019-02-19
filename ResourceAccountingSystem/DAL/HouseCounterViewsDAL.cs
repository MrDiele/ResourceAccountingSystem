using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.DAL
{
    public class HouseCounterViewsDAL
    {
        public static List<HouseCounterView> GetHouseCounterView()
        {
            using (var db = new HomeDataEntities())
            {
                return new List<HouseCounterView>(db.HouseCounterView);
            }
        }

        public static HouseCounterView GetHouseCounter(int IdHouse, string Address)
        {
            using (var db = new HomeDataEntities())
            {
                return db.HouseCounterView.Find(IdHouse, Address);
            }
        }
    }
}