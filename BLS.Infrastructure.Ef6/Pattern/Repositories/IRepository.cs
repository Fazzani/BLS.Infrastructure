using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLS.Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Add(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> list);
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class;
    }
}