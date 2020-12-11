
namespace crmvcsb.Infrastructure.EF
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    public interface IServiceEF
    {
        void SaveIdentity(string command);
        void SaveIdentity<T>() where T : class;

        Task<EntityEntry<T>> AddAsync<T>(T item) where T : class;

        DatabaseFacade GetDatabase();
        DbContext GetEFContext();
    }
}
