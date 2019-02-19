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
        public List<Counters> GetHouses()
        {             
            return countersDAL.GetCounters();
        }

        /// <summary>
        /// Добавляет новый счётчик.
        /// </summary>
        /// <param name="counters"></param>
        /// <returns></returns>
        public Counters AddNewCounter(Counters counters)
        {
            return countersDAL.AddNewCounter(counters);
        }

        /// <summary>
        /// Вносит показания.
        /// </summary>
        /// <param name="counters"></param>
        /// <returns></returns>
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