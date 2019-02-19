using ResourceAccountingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace ResourceAccountingSystem.DAL
{
    public class HousesDAL
    {
        public static List<Houses> GetHouses()
        {
            using (var db = new HomeDataEntities())
            {
                return new List<Houses>(db.Houses);
            }
        }

        public static Houses AddHouse(Houses houses)
        {
            using (var db = new HomeDataEntities())
            {
                //Добавляем новый дом
                db.Houses.Add(houses);

                try
                {
                    //Сохраняем изменения
                    Save(db);
                }
                catch (DbUpdateException)
                {
                    return null;
                }
                return houses;
            }
        }

        public static Houses GetHouse(int id)
        {
            using (var db = new HomeDataEntities())
            {
                return db.Houses.Find(id);
            }
        }

        public static List<House> GetMaxHouseConsumer()
        {
            using (var db = new HomeDataEntities())
            {
                return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MaxConsumerHouse]()"));
            }
        }

        public static List<House> GetMinHouseConsumer()
        {
            using (var db = new HomeDataEntities())
            {
                return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MinConsumerHouse]()"));
            }
        }

        public static void AddCounterAndInputIndicationForHouse(int id, int serialNumber, decimal indication)
        {
            using (var db = new HomeDataEntities())
            {
                db.AddCounterOrInputIndicationOfIdHouse(id, serialNumber, indication);
            }
        }

        public static void InputIndicationByIdHouse(int id, decimal indication)
        {
            using (var db = new HomeDataEntities())
            {
                db.InputIndicationByIdHouse(id, indication);
            }
        }

        public static void DeleteHouse(int id)
        {
            using (var db = new HomeDataEntities())
            {
                db.DeleteHouseWithCounter(id);
            }
        }
        private static void Save(HomeDataEntities db)
        {
            db.SaveChanges();
        }
    }
}