
namespace crmvcsb.Infrastructure.EF.NewOrder
{

    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;

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
    public class RepositoryNewOrderWrite : RepositoryNewOrder, IRepositoryEFWrite
    {
        public RepositoryNewOrderWrite(ContextNewOrderWrite context) : base(context)
        {

        }
    }
}