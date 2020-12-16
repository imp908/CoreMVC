
/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepositoryEF : IRepository
    {

        void SaveIdentity(string command);
        void SaveIdentity<T>() where T : class;

        Task<EntityEntry<T>> AddAsync<T>(T item) where T : class;
  
        DatabaseFacade GetDatabase();
        DbContext GetEFContext();
    }

    public interface IRepositoryEFRead : IRepositoryEF { }
    public interface IRepositoryEFWrite : IRepositoryEF { }
}
