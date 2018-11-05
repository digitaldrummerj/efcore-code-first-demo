using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeFirst.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : ModelBase, new()
    {
        protected readonly DbSet<T> Table;
        public CodeFirstContext Context { get; private set; }

        protected RepositoryBase(CodeFirstContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public int Count => Table.Count();

        public virtual T Get(int id, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;
            query = query.Where(t => t.Id == id);

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefault();
        }

        public virtual IList<T> GetAll(Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;
            if (includes != null)
            {
                query = includes(query);
            }

            return query.ToList();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;

            if (includes != null)
            {
                query = includes(query);
            }

            return query.FirstOrDefault(predicate);
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefault(predicate);
        }

        public (int totalCount, int totalPages) GetTotalPages(int count)
        {
            var totalPages = (int)Math.Ceiling(Count / (double)count);
            return (Count, totalPages);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includes != null)
            {
                query = includes(query);
            }

            return query.ToList();
        }

        public virtual List<T> GetPage(int page = 1, int count = 10, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = Table;

            if (includes != null)
            {
                query = includes(query);
            }

            List<T> results =  query.Skip(count * (page - 1)).Take(count).ToList();
            if (results.Count == 0)
            {
                results = null;
            }

            return results;
        }

        public int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public int Delete(int id, bool persist = true)
        {
            T result = Get(id);
            Table.Remove(result);
            return persist ? SaveChanges() : 0;
        }

        public int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //A general exception occurred and should be handled intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        private bool _disposedValue;

        private void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                Context.Dispose();
            }
            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}