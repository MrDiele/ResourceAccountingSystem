using ResourceAccountingSystem.Models;
using System.Linq;

namespace ResourceAccountingSystem.Tests
{
    class TestCounterDbSet : TestDbSet<Counters>
    {
        public override Counters Find(params object[] keyValues)
        {
            return this.SingleOrDefault(counters => counters.IdCounter == (int)keyValues.Single());
        }
    }
}
