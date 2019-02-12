using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResourceAccountingSystem.Controllers;
using ResourceAccountingSystem.Models;
using System.Web.Http.Results;
using System.Net;

namespace ResourceAccountingSystem.Tests
{
    [TestClass]
    public class UnitTestCountersController
    {
        [TestMethod]
        public void GetCounters()
        {
            var controller = new CountersController(new TextResourceAccountingSystemContext());

            var item = GetDemoCounters();

            var result = controller.PostCounters(item) as CreatedAtRouteNegotiatedContentResult<Counters>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.IdCounter);
            Assert.AreEqual(result.Content.SerialNumber, item.SerialNumber);
        }

        [TestMethod]
        public void GetProducts_ShouldReturnAllProducts()
        {
            var context = new TextResourceAccountingSystemContext();
            context.Counters.Add(new Counters() { IdCounter = 3, SerialNumber = 12345, Indication = 1 });
            context.Counters.Add(new Counters() { IdCounter = 4, SerialNumber = 12346, Indication = 2 });
            context.Counters.Add(new Counters() { IdCounter = 5, SerialNumber = 12347, Indication = 3 });

            var controller = new CountersController(context);
            var result = controller.GetCounters() as TestCounterDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Local.Count);
        }

        [TestMethod]
        public void PutCounters_ShouldReturnStatusCode()
        {
            var controller = new CountersController(new TextResourceAccountingSystemContext());

            var item = GetDemoCounters();
            controller.PostCounters(item);
            Counters newItem = new Counters()
            {
                IdCounter = item.IdCounter,
                SerialNumber = item.SerialNumber,
                Indication = 6
            };
            var result = controller.PutCounters(newItem) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        Counters GetDemoCounters()
        {
            return new Counters() { IdCounter = 3, SerialNumber= 12345, Indication = 5 };
        }
    }
}
