
namespace crmvcsb.Infrastructure.EF.NewOrder
{

    using Microsoft.EntityFrameworkCore;
  
    using crmvcsb.Domain.IRepository;
    using crmvcsb.Infrastructure.IRepositoryEF;

    public class RepositoryNewOrder : RepositoryEF, IRepositoryEF
    {
        public RepositoryNewOrder(DbContext context) : base(context)
        {

        }
    }

}