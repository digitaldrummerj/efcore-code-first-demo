using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CodeFirst.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        int Count { get; }

        TEntity Get(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        IList<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        List<TEntity> GetPage(int page = 0, int count = 10, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);




        (int totalCount, int totalPages) GetTotalPages(int count);

        List<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        int Add(TEntity entity, bool persist = true);

        int AddRange(IEnumerable<TEntity> entities, bool persist = true);

        int Update(TEntity entity, bool persist = true);
        int UpdateRange(IEnumerable<TEntity> entities, bool persist = true);
        int Delete(TEntity entity, bool persist = true);
        int Delete(int id, bool persist = true);
        int DeleteRange(IEnumerable<TEntity> entities, bool persist = true);
        int SaveChanges();

    }
}