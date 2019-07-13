namespace crmvcsb.Infrastructure.EF.newOrder
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class NewOrderContext  : DbContext
    {

        public NewOrderContext(DbContextOptions<NewOrderContext> options) : base(options)
        {

        }
    }
}