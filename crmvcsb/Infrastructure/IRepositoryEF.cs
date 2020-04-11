using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// EF specific repository
/// stays in infrastructure, uses domain Irepository interface
/// </summary>
namespace crmvcsb.Infrastructure.IRepositoryEF
{
    using crmvcsb.Domain.IRepository;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    public interface IRepositoryEF : IRepository
    {
        DatabaseFacade GetDatabase();
    }
}
