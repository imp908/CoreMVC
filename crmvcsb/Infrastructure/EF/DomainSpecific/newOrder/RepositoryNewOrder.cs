
namespace crmvcsb.Infrastructure.EF.NewOrder
{

    using Microsoft.EntityFrameworkCore;
  
    using crmvcsb.Universal;
    using crmvcsb.Infrastructure.EF;

    public class RepositoryNewOrder : RepositoryEF, IRepositoryEF, IRepository
    {
        public RepositoryNewOrder(DbContext context) : base(context)
        {

        }
    }

    public class RepositoryNewOrderRead : RepositoryNewOrder, IRepositoryEFRead
    {
        public RepositoryNewOrderRead(ContextNewOrderRead context) : base(context)
        {

        }
    }
    public class RepositoryNewOrderWrite: RepositoryNewOrder, IRepositoryEFWrite
    {
        public RepositoryNewOrderWrite(ContextNewOrderWrite context) : base(context)
        {

        }
    }
}