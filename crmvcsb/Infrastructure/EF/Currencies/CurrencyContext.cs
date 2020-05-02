
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.Infrastructure.EF.Currencies
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Domain.DomainSpecific.Currency.DAL;
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //registration in startup.cs
            //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CurrencyRatesDAL>().HasOne(p => p.CurrencyFrom).WithMany(p => p.CurRatesFrom).HasForeignKey(p => p.CurrencyFromId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<CurrencyRatesDAL>().HasOne(p => p.CurrencyTo).WithMany(p => p.CurRatesTo).HasForeignKey(p => p.CurrencyToId).OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<Currency>().HasMany(p => p.CurRatesFrom).WithOne(p => p.CurrencyFrom).HasForeignKey(k => k.CurrencyFromId).OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Currency>().HasMany(p => p.CurRatesTo).WithOne(p => p.CurrencyTo).HasForeignKey(k => k.CurrencyToId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CurrencyDAL>().HasKey(k => k.Id);
            builder.Entity<CurrencyDAL>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<CurrencyRatesDAL>().HasKey(k => k.Id);
            builder.Entity<CurrencyRatesDAL>().Property(k => k.Id).ValueGeneratedOnAdd();
        }

        public DbSet<CurrencyDAL> Currency { get; set; }
        public DbSet<CurrencyRatesDAL> CurrencyRates { get; set; }

    }
}
