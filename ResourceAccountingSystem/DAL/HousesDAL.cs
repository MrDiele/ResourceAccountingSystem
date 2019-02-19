using ResourceAccountingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace ResourceAccountingSystem.DAL
{
    public class HousesDAL
    {
        private HomeDataEntities db;

        public HousesDAL()
        {
            db = new HomeDataEntities();
        }

        public List<Houses> GetHouses()
        {
            return new List<Houses>(db.Houses);
        }

        public Houses AddHouse(Houses houses)
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

        public Houses GetHouse(int id)
        {
            return db.Houses.Find(id);
        }

        public List<House> GetMaxHouseConsumer()
        {
            return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MaxConsumerHouse]()"));
        }

        public List<House> GetMinHouseConsumer()
        {
            return new List<House>(db.Database.SqlQuery<House>(@"SELECT * FROM [dbo].[GetId_MinConsumerHouse]()"));
        }

        public void AddCounterAndInputIndicationForHouse(int id, int serialNumber, decimal indication)
        {
            db.AddCounterOrInputIndicationOfIdHouse(id, serialNumber, indication);
        }

        public void InputIndicationByIdHouse(int id, decimal indication)
        {
            db.InputIndicationByIdHouse(id, indication);
        }

        public void DeleteHouse(int id)
        {
            db.DeleteHouseWithCounter(id);
        }

        private void Save(HomeDataEntities db)
        {
            db.SaveChanges();
        }

        ~ HousesDAL()
        {
            db.Dispose();
        }
    }
}