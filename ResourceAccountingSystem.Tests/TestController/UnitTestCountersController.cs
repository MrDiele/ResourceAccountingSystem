using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceAccountingSystem.Models;
using ResourceAccountingSystem.DAL;
using ResourceAccountingSystem.BusinessLogic;

namespace ResourceAccountingSystem.Tests
{
    [TestClass]
    public class UnitTestCountersController
    {
        [TestMethod]
        public void GetCounters()
        {
            var countersBL = new CountersBL(new TextResourceAccountingSystemContext());

            var item = GetDemoCounters();

            var result = countersBL.AddNewCounter(item);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.IdCounter, item.IdCounter);
            Assert.AreEqual(result.SerialNumber, item.SerialNumber);
        }

        [TestMethod]
        public void GetProducts_ShouldReturnAllProducts()
        {
            var context = new TextResourceAccountingSystemContext();
            context.Counters.Add(new Counters() { IdCounter = 3, SerialNumber = 12345, Indication = 1 });
            context.Counters.Add(new Counters() { IdCounter = 4, SerialNumber = 12346, Indication = 2 });
            context.Counters.Add(new Counters() { IdCounter = 5, SerialNumber = 12347, Indication = 3 });

            var controller = new CountersDAL(context);
            var result = controller.GetCounters();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        Counters GetDemoCounters()
        {
            return new Counters() { IdCounter = 3, SerialNumber= 12345, Indication = 5 };
        }
    }
}
