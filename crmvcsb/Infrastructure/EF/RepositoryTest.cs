
namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Domain.IRepository;
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Infrastructure.IRepositoryEF;

    public class RepositoryTest : RepositoryEF, IRepositoryEF, IRepository
    {
        public RepositoryTest(DbContext context) : base(context)
        {

        }
    }

}