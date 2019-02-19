using ResourceAccountingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ResourceAccountingSystem.Controllers.DAL
{
    public class CountersDAL
    {
        private IHomeModelContext db;

        public CountersDAL()
        {
             db = new HomeDataEntities();
        }

        public CountersDAL(IHomeModelContext context)
        {
            db = context;
        }

        public List<Counters> GetCounters()
        {
            return new List<Counters>(db.Counters);
        }

        public Counters AddNewCounter(Counters counters)
        {
                //Добавляем новый счётчик
            db.Counters.Add(counters);

            try
            {
                //Сохраняем изменения
                Save(db);
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return counters;
        }

        public List<Counters> GetCounter(int serialNumber)
        {
            return new List<Counters>(db.Counters.Where(c => c.SerialNumber == serialNumber));
        }

        public void EditCounter(Counters counter)
        {
            db.MarkAsModified(counter);
            try
            {
                //Сохраняем изменения
                Save(db);
            }
            catch (DbUpdateException) { }
        }

        public int FindCount(int id)
        {
            return db.Counters.Count(e => e.IdCounter == id);
        }

        private void Save(IHomeModelContext db)
        {
            db.SaveChanges();
        }

        ~ CountersDAL()
        {
            db.Dispose();
        }
    }
}