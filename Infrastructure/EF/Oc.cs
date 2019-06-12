

namespace mvccoresb.Infrastructure.EF
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    using mvccoresb.Domain.TestModels;

    using mvccoresb.Domain.Interfaces;

    using System.Threading.Tasks;

    public class Oc : DbContext
    {
        
        public Oc(DbContextOptions<Oc> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //registration in startup.cs
            //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
        }

        public DbSet<BlogEF> Blogs { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           

        }

    }
}