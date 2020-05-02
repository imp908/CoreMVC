

namespace crmvcsb.Infrastructure.EF.Currencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Domain.Universal.IRepository;
    using crmvcsb.Infrastructure.EF;

    public class RepositoryCurrency : RepositoryEF, IRepositoryEF, IRepository
    {
        public RepositoryCurrency(DbContext context) : base(context)
        {

        }
    }
}
