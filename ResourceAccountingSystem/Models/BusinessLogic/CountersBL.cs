using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class CountersBL
    {
        #region Fields
        private CountersDAL countersDAL;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public CountersBL()
        {
            countersDAL = new CountersDAL();
        }

        /// <summary>
        /// Конструктор класса для тестов.
        /// </summary>
        /// <param name="context"></param>
        public CountersBL(IHomeModelContext context)
        {
            countersDAL = new CountersDAL(context);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получает список домов в системе.
        /// </summary>
        /// <returns></returns>
        public List<Counter> GetHouses()
        {
            List<Counter> counter = new List<Counter>();
            var list = countersDAL.GetCounters();
            foreach (Counters counters in list)
            {
                counter.Add(new Counter { IdCounter = counters.IdCounter, SerialNumber = counters.SerialNumber, Indication = counters.Indication});
            }
            return counter;
        }

        /// <summary>
        /// Добавляет новый счётчик.
        /// </summary>
        /// <param name="counters"></param>
        /// <returns></returns>
        public Counter AddNewCounter(Counter counter)
        {
            Counters counters = new Counters { IdCounter = counter.IdCounter,  SerialNumber = counter.SerialNumber, Indication = counter.Indication};
            var _counters = countersDAL.AddNewCounter(counters);
            Counter newCounter = new Counter
            {
                IdCounter = _counters.IdCounter,
                SerialNumber = _counters.SerialNumber,
                Indication = _counters.Indication
            };
            return newCounter;
        }

        /// <summary>
        /// Вносит показания.
        /// </summary>
        /// <param name="counters"></param>
        /// <returns></returns>
        public bool InputIndication(Counter counter)
        {
            //ищем счётчик по серийному номеру
            List<Counters> cts = countersDAL.GetCounter(counter.SerialNumber);
                                                         
            //если нашли счётчик вносим изменения и сохраняем
            if (cts.Count != 0)
            {
                foreach (Counters c in cts)
                {
                    if (c.Indication < counter.Indication)
                    {
                        c.Indication = counter.Indication;
                        countersDAL.EditCounter(c);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Проверяет существование счётчиков.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CountersExists(int id)
        {
            if (countersDAL.FindCount(id) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}