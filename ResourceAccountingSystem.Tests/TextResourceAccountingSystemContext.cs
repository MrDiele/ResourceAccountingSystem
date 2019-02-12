using ResourceAccountingSystem.Models;
using System.Data.Entity;

namespace ResourceAccountingSystem.Tests
{
    class TextResourceAccountingSystemContext : IHomeModelContext
    {
        public TextResourceAccountingSystemContext()
        {
            this.Counters = new TestCounterDbSet();
        }

        public DbSet<Counters> Counters { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Counters item) { }
        public void Dispose() { }
    }
}
