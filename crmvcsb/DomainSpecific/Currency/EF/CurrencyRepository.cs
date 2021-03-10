

namespace crmvcsb.DomainSpecific.Infrastructure.EF
{

    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;

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
