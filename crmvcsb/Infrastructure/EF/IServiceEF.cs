using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Domain.Universal;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    public interface IServiceEF : IService
    {
        string GetConnectionString();
        DatabaseFacade GetDatabase();
        DbContext GetEFContext();

    }
}
