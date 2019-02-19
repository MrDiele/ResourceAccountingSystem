using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.Models;
using ResourceAccountingSystem.Utils;
using System;
using System.Collections.Generic;

namespace ResourceAccountingSystem.BusinessLogic
{
    public class HousesBL
    {
        #region Fields
        private HousesDAL housesDAL;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public HousesBL()
        {
            housesDAL = new HousesDAL();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Получить список домов.
        /// </summary>
        /// <returns></returns>
        public List<House> GetHouses()
        {
            List<House> house = new List<House>();
            var list = housesDAL.GetHouses();
            foreach (Houses houses in list)
            {
                house.Add(new House { IdHouse = houses.IdHouse, Address = houses.Address });
            }
            return house;
        }

        /// <summary>
        /// Получить дом по Id.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <returns></returns>
        public House GetHouse(int id)
        {
            try
            {
                //проверяем существует ли дом в системе
                Houses houses = housesDAL.GetHouse(id);
                if (houses == null)
                {
                    return null;
                }
                //получаем информацию о доме
                HouseCounterViewsDAL houseCounterViewsDAL = new HouseCounterViewsDAL();
                HouseCounterView houseCounterView = houseCounterViewsDAL.GetHouseCounter(houses.IdHouse, houses.Address);
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
        public House GetMaxHouseConsumer()
        {
            try
            {
                House answer = null;
                //запускаем метод получающий информацию из базы
                var houses = housesDAL.GetMaxHouseConsumer();
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
        public House GetMinHouseConsumer()
        {
            try
            {
                House answer = null;
                //запускаем метод получающий информацию из базы
                var houses = housesDAL.GetMinHouseConsumer();
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
        public bool AddNewCounterInHouse(int id, Counter counter)                   
        {
            try
            {
                housesDAL.AddCounterAndInputIndicationForHouse(id, counter.SerialNumber, counter.Indication);
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
        public bool InputIndicationByIdHouse(House house)
        {
            try
            {                 
                housesDAL.InputIndicationByIdHouse(house.IdHouse, house.Indication);
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
        public House AddNewHouse(House house)
        {
            Houses houses = new Houses { IdHouse = house.IdHouse, Address = house.Address, Counters = null };
            var _houses = housesDAL.AddHouse(houses);
            House newHouse = new House
            {
                IdHouse = _houses.IdHouse,
                Address = _houses.Address
            };
            return newHouse;
        }

        /// <summary>
        /// Удалить дом по Id.
        /// </summary>
        /// <param name="id">Id дома.</param>
        /// <returns></returns>
        public bool DelHouse(int id)
        {
            //проверяем существует ли дом
            Houses houses = housesDAL.GetHouse(id);
            if (houses == null)
            {
                return false;
            }
            try
            {
                //запускаем процедуру удаления
                housesDAL.DeleteHouse(id);
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
        #endregion
    }
}