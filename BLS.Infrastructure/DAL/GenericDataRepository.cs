using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using BLS.Infrastructure.Domain;

namespace BLS.Infrastructure.DAL
{
  public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
  {
    readonly DbContext _dbContext;
    public GenericDataRepository(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
    {
      IQueryable<T> dbQuery = _dbContext.Set<T>();

      //Apply eager loading
      foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
        dbQuery = dbQuery.Include<T, object>(navigationProperty);

      return dbQuery
          .AsNoTracking()
          .ToList<T>();
    }

    public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
    {
      List<T> list;

      IQueryable<T> dbQuery = _dbContext.Set<T>();

      //Apply eager loading
      foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
        dbQuery = dbQuery.Include<T, object>(navigationProperty);

      list = dbQuery
          .AsNoTracking()
          .Where(where)
          .ToList<T>();

      return list;
    }

    public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
    {
      T item = null;

      IQueryable<T> dbQuery = _dbContext.Set<T>();

      //Apply eager loading
      foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
        dbQuery = dbQuery.Include<T, object>(navigationProperty);

      item = dbQuery
          .AsNoTracking() //Don't track any changes for the selected item
          .FirstOrDefault(where); //Apply where clause

      return item;
    }

    public virtual void Add(params T[] items)
    {
      Update(items);
    }

    public virtual void Update(params T[] items)
    {

      DbSet<T> dbSet = _dbContext.Set<T>();
      foreach (T item in items)
      {
        dbSet.Add(item);
        foreach (DbEntityEntry<IEntity> entry in _dbContext.ChangeTracker.Entries<IEntity>())
        {
          IEntity entity = entry.Entity;
          entry.State = GetEntityState(entity.EntityState);
        }
      }
      _dbContext.SaveChanges();
    }


    public virtual void Remove(params T[] items)
    {
      Update(items);
    }

    protected static System.Data.Entity.EntityState GetEntityState(BLS.Infrastructure.Domain.EntityState entityState)
    {
      switch (entityState)
      {
        case BLS.Infrastructure.Domain.EntityState.Unchanged:
          return System.Data.Entity.EntityState.Unchanged;
        case BLS.Infrastructure.Domain.EntityState.Added:
          return System.Data.Entity.EntityState.Added;
        case BLS.Infrastructure.Domain.EntityState.Modified:
          return System.Data.Entity.EntityState.Modified;
        case BLS.Infrastructure.Domain.EntityState.Deleted:
          return System.Data.Entity.EntityState.Deleted;
        default:
          return System.Data.Entity.EntityState.Detached;
      }
    }
  }
}
