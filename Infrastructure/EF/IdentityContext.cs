

namespace mvccoresb.Infrastructure.EF
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;

    using mvccoresb.Domain.Models;
    using mvccoresb.Domain.Interfaces;
    using chat.Domain.Models;

    using System.Threading.Tasks;

    public class IdentityContext : IdentityDbContext<IdentityUser>
    {
        
        //DbSet<UserAuth> users {get;set;}

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserAuth>(entity =>
            {
                entity.ToTable(name:"AspNetUser",schema: "dbo");
                entity.Property(e => e.Id).HasColumnName("AspNetUserId");        
            });
        }

    }
}