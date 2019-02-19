using ResourceAccountingSystem.Controllers.DAL;
using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.Controllers.BusinessLogic
{
    public class CountersBL
    {
        private CountersDAL countersDAL;

        public CountersBL()
        {
            countersDAL = new CountersDAL();
        }

        public CountersBL(IHomeModelContext context)
        {
            countersDAL = new CountersDAL(context);
        }

        public List<Counters> GetHouses()
        {             
            return countersDAL.GetCounters();
        }

        public Counters AddNewCounter(Counters counters)
        {
            return countersDAL.AddNewCounter(counters);
        }

        public bool InputIndication(Counters counters)
        {
            //ищем счётчик по серийному номеру
            List<Counters> cts = countersDAL.GetCounter(counters.SerialNumber);
                                                         
            //если нашли счётчик вносим изменения и сохраняем
            if (cts.Count != 0)
            {
                foreach (Counters counter in cts)
                {
                    if (counter.Indication < counters.Indication)
                    {
                        counter.Indication = counters.Indication;
                        countersDAL.EditCounter(counter);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CountersExists(int id)
        {
            if (countersDAL.FindCount(id) > 0)
                return true;
            else
                return false;
        }
    }
}