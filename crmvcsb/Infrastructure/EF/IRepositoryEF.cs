
/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Domain.Universal.IRepository;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    public interface IRepositoryEF : IRepository
    {
        DatabaseFacade GetDatabase();
        DbContext GetEFContext();
    }
}
