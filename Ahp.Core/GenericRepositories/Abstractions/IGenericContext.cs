using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Ahp.Core.Repositories.Abstractions
{
    public interface IGenericContext : IDisposable, IObjectContextAdapter
    {
        Database Database { get; }

        int SaveChanges();
        bool SaveStatus();
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
