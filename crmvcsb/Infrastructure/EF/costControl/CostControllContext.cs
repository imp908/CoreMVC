using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using crmvcsb.Domain.Interfaces;
using crmvcsb.Domain.TestModels.Models.CostControl;

namespace crmvcsb.Infrastructure.EF.costControl
{
    public class CostControllContext : DbContext
    {

        public CostControllContext(DbContextOptions<CostControllContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //registration in startup.cs
            //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BusinessColumn>().HasKey(k => k.Id);
            builder.Entity<BusinessColumn>().Property(p => p.Id).HasColumnName("Id_Business_Column");
            builder.Entity<BusinessColumn>().HasMany(p => p.BusinessLines).WithOne(p => p.BusinessColumn).HasForeignKey("BusinessColumnId");

            builder.Entity<BusinessLine>().HasKey(k => k.Id);
            builder.Entity<BusinessLine>().Property(p => p.Id).HasColumnName("Id_Business_Line");
        }

        public DbSet<BusinessColumn> BudgetColumn { get; set; }
        public DbSet<BusinessLine> BusinessLine { get; set; }

    }
}
