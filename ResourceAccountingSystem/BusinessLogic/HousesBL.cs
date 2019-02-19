using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using ResourceAccountingSystem.Utils;
using System;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class HousesBL
    {
        /// <summary>
        /// Получить список домов.
        /// </summary>
        /// <returns></returns>
        public static List<Houses> GetHouses()
        {
            return HousesDAL.GetHouses();
        }

        /// <summary>
        /// Получить дом по Id.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <returns></returns>
        public static House GetHouse(int id)
        {
            try
            {
                //проверяем существует ли дом в системе
                Houses houses = HousesDAL.GetHouse(id);
                if (houses == null)
                {
                    return null;
                }
                HouseCounterView houseCounterView = HouseCounterViewsDAL.GetHouseCounter(houses.IdHouse, houses.Address);
                if (houseCounterView == null)
                {
                    return null;
                }
                //формируем ответ
                House answer = new House
                {
                    IdHouse = houseCounterView.IdHouse,
                    SerialNumber = Convert.ToInt32(houseCounterView.SerialNumber),
                    Address = houseCounterView.Address,
                    Indication = Convert.ToDecimal(houseCounterView.Indication)
                };
                return answer;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return null;
            }
        }

        /// <summary>
        /// Получить дом с максимальным потреблением.
        /// </summary>
        /// <returns></returns>
        public static House GetMaxHouseConsumer()
        {
            try
            {
                House answer = null;
                //запускаем метод получающий информацию из базы
                var houses = HousesDAL.GetMaxHouseConsumer();
                foreach (var house in houses)
                {
                    //формируем ответ
                    answer = new House
                    {
                        Address = house.Address,
                        Indication = house.Indication
                    };
                }
                return answer;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return null;
            }
        }

        /// <summary>
        /// Получить дом с минимальным потреблением.
        /// </summary>
        /// <returns></returns>
        public static House GetMinHouseConsumer()
        {
            try
            {
                House answer = null;
                //запускаем метод получающий информацию из базы
                var houses = HousesDAL.GetMinHouseConsumer();
                foreach (var house in houses)
                {
                    answer = new House
                    {
                        //формируем ответ
                        Address = house.Address,
                        Indication = house.Indication
                    };
                }
                return answer;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return null;
            }
        }

        /// <summary>
        /// Добавить счётчик в дом.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <param name="houses">Обьект содержащий сведения о счётчике.</param>
        /// <returns></returns>
        public static bool AddNewCounterInHouse(int id, Houses houses)
        {
            try
            {
                if (id != houses.IdHouse)
                {
                    return false;
                }

                if (houses.Counters.Count != 0)
                {
                    //запускаем процедуру сохранения нового счётчика и привязки его к дому
                    foreach (Counters counter in houses.Counters)
                    {
                        HousesDAL.AddCounterAndInputIndicationForHouse(id, counter.SerialNumber, counter.Indication);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return false;
            }
        }

        /// <summary>
        /// Внести показания по Id дома.
        /// </summary>
        /// <param name="houses">Обьект Houses.</param>
        /// <returns></returns>
        public static bool InputIndicationByIdHouse(Houses houses)
        {
            try
            {     
                if (houses.Counters.Count != 0)
                {
                    //запускаем процедуру сохранения показаний по ID дома
                    foreach (Counters counter in houses.Counters)
                    {
                        HousesDAL.InputIndicationByIdHouse(houses.IdHouse, counter.Indication);
                    }
                } 
                return true;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return false;
            }
        }

        /// <summary>
        /// Добавить новый дом.
        /// </summary>
        /// <param name="houses">Обьект Houses с информацией о новом доме.</param>
        /// <returns></returns>
        public static Houses AddNewHouse(Houses houses)
        {
            return HousesDAL.AddHouse(houses);
        }

        /// <summary>
        /// Удалить дом по Id.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <returns></returns>
        public static bool DelHouse(int id)
        {
            //Проверяем существует ли дом
            Houses houses = HousesDAL.GetHouse(id);
            if (houses == null)
            {
                return false;
            }
            try
            {
                //запускаем процедуру удаления
                HousesDAL.DeleteHouse(id);
                return true;
            }
            catch (Exception e)
            {
                Logs.Add(System.Reflection.MethodBase.GetCurrentMethod().Name, e.ToString());
                if (e.InnerException != null)
                    Logs.Add("InnerException" + e.InnerException.Message);
                return false;
            }
        }
    }
}