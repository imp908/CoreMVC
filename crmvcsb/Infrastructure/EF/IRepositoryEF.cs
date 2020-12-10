
/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    public interface IRepositoryEF : IRepository
    {        
        void SaveIdentity(string command);
        void SaveIdentity<T>() where T : class;
        string GetConnectionString();

        DatabaseFacade GetDatabase();
        DbContext GetEFContext();
    }
}
