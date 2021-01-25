
/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System.Threading.Tasks;

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
