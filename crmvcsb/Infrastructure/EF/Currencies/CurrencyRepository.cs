

namespace crmvcsb.Infrastructure.EF.Currencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Universal;
    using crmvcsb.Infrastructure.EF;

    public class RepositoryCurrencyRead : RepositoryEF, IRepositoryEFRead, IRepository
    {
        public RepositoryCurrencyRead(DbContext context) : base(context)
        {

        }
    }
    public class RepositoryCurrencyWrite : RepositoryEF, IRepositoryEFWrite, IRepository
    {
        public RepositoryCurrencyWrite(DbContext context) : base(context)
        {

        }
      
    }

}
