namespace crmvcsb.Infrastructure.EF
{
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    using System;

    using System.Linq;

    using crmvcsb.Domain.Interfaces;


    public class RepositoryEF : IRepository
    {
        DbContext _context;

        public RepositoryEF(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
            where T : class
        {
            return (expression == null)
                ? this._context.Set<T>()
                : this._context.Set<T>().Where(expression);

        }

        public void Add<T>(T item)
            where T : class
        {
            this._context.Set<T>().Add(item);
        }

        public Task<EntityEntry<T>> AddAsync<T>(T item)
           where T : class
        {
            return this._context.Set<T>().AddAsync(item);
        }

        public void AddRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().AddRange(items);
        }

        public Task AddRangeAsync<T>(IList<T> items)
                where T : class
        {
            return this._context.Set<T>().AddRangeAsync(items);
        }

        public void Delete<T>(T item)
           where T : class
        {
            this._context.Set<T>().Remove(item);
        }

        public void DeleteRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().RemoveRange(items);
        }

        public void Update<T>(T item)
            where T : class
        {
            this._context.Set<T>().Update(item);
        }

        public void UpdateRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().UpdateRange(items);
        }

        public IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return this._context.Set<T>().Where(expression);
        }

        public IQueryable<T> SkipTake<T>(int skip = 0, int take = 10)
            where T : class
        {
            return this._context.Set<T>().Skip(skip).Take(take);
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public void SaveIdentity<T>()
        {
            this.SaveIdentity(nameof(T));
        }

        /*Provides identity column manual insert while testing */
        public void SaveIdentity(string tableFullName)
        {
            string cmd = $"SET IDENTITY_INSERT {tableFullName} ON;";
            this._context.Database.OpenConnection();
            this._context.Database.ExecuteSqlCommand(cmd);
            this._context.SaveChanges();
            cmd = $"SET IDENTITY_INSERT {tableFullName} OFF;";
            this._context.Database.ExecuteSqlCommand(cmd);
            this._context.Database.CloseConnection();
        }
    }

}