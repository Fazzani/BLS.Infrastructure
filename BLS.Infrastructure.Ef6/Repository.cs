#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BLS.Infrastructure.DataContext;
using BLS.Infrastructure.Repositories;
using BLS.Infrastructure.UnitOfWork;

#endregion

namespace BLS.Infrastructure.Ef6
{
  public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
  {
    #region Private Fields

    private readonly IDataContextAsync _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly IUnitOfWorkAsync _unitOfWork;

    #endregion Private Fields

    public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
    {
      _context = context;
      _unitOfWork = unitOfWork;

      // Temporarily for FakeDbContext, Unit Test and Fakes
      var dbContext = context as DbContext;

      if (dbContext != null)
      {
        _dbSet = dbContext.Set<TEntity>();
      }
      else
      {
        var fakeContext = context as FakeDbContext;

        if (fakeContext != null)
        {
          _dbSet = fakeContext.Set<TEntity>();
        }
      }
    }

    public virtual TEntity Find(params object[] keyValues)
    {
      return _dbSet.Find(keyValues);
    }

    public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
    {
      return _dbSet.SqlQuery(query, parameters).AsQueryable();
    }

    public virtual void Add(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public virtual void Attach(TEntity entity)
    {
      _dbSet.Attach(entity);
    }

    public virtual void InsertRange(IEnumerable<TEntity> entities)
    {
      _dbSet.AddRange(entities);
    }

    public virtual void Update(TEntity entity)
    {
      (_context as DbContext).Entry(entity).State = EntityState.Modified;
    }

    public virtual void DeleteRange(ICollection<TEntity> list)
    {
      _dbSet.RemoveRange(list);
    }

    public void Delete(object id)
    {
      Delete(_dbSet.Find(id));
    }

    public virtual void Delete(TEntity entity)
    {
      if (entity == null)
        throw new ArgumentNullException(string.Format("Entity {0} is null", entity.GetType().FullName));
      _dbSet.Remove(entity);
    }

    public IQueryable<TEntity> Queryable()
    {
      return _dbSet;
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
      return _unitOfWork.Repository<T>();
    }

    public virtual async Task<TEntity> FindAsync(params object[] keyValues)
    {
      return await _dbSet.FindAsync(keyValues);
    }

    public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
    {
      return await _dbSet.FindAsync(cancellationToken, keyValues);
    }

    public virtual async Task<bool> DeleteAsync(params object[] keyValues)
    {
      return await DeleteAsync(CancellationToken.None, keyValues);
    }

    public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
    {
      var entity = await FindAsync(cancellationToken, keyValues);

      if (entity == null)
      {
        return false;
      }

      (_context as DbContext).Entry(entity).State = EntityState.Deleted;

      return true;
    }


  }
}