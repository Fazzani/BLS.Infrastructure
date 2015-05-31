using System;

namespace BLS.Infrastructure.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}