
namespace crmvcsb.Infrastructure.EF
{
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    using System;

    using System.Linq;

    using crmvcsb.Domain.Interfaces;
    using System.Reflection;

    public class RepositoryNewOrder : RepositoryEF, IRepository
    {
        public RepositoryNewOrder(DbContext context) : base(context)
        {

        }
    }

}