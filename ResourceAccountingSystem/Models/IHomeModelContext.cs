using ResourceAccountingSystem.Models;
using System;
using System.Data.Entity;

namespace ResourceAccountingSystem.Models
{
    public interface IHomeModelContext : IDisposable
    {
        DbSet<Counters> Counters { get; }
        int SaveChanges();
        void MarkAsModified(Counters item);
    }
}