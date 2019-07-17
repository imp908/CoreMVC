namespace crmvcsb.Infrastructure.EF.newOrder
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using crmvcsb.Domain.NewOrder.DAL;
    using System;

    public class NewOrderContext : DbContext
    {
        public NewOrderContext(DbContextOptions<NewOrderContext> options) : base(options)
        {

        }        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //registration in startup.cs
            //optionsBuilder.UseSqlServer(@"Servedotnet buildr=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*Mark key */
            builder.Entity<AddressDAL>().HasKey(s=>s.Id);
            
            /*rename property */
            //builder.Entity<AddressDAL>().Property(s => s.Id).HasColumnName("AddressId");

            /*Rename table */
            builder.Entity<RouteVertexDAL>().ToTable("RouteVertex");

            /* Generate value in db*/
            builder.Entity<AddressDAL>().Property(s => s.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");

            builder.Entity<AddressDAL>().HasData(
                new AddressDAL(){ Id = Guid.NewGuid(), StreetName ="test street 1", Code = 1}
                ,new AddressDAL() { Id = Guid.NewGuid(), StreetName = "test street 2", Code = 2 }
                , new AddressDAL() { Id = Guid.NewGuid(), StreetName = "test street 3", Code = 3 }
            );

        }
        public DbSet<AddressDAL> Adresses {get;set;}
        public DbSet<RouteVertexDAL> RouteVertexes { get; set; }
        public DbSet<RouteDAL> Routes { get; set; }
        
        
        public DbSet<PhysicalUnitDAL> PhysicalUnits { get; set; }
        public DbSet<PhysicalDimensionDAL> PhysicalDimensions { get; set; }
        
        
        public DbSet<GoodsDAL> Goods { get; set; }


        public DbSet<DeliveryItemDAL> DeliveryItems { get; set; }
        public DbSet<ClientDAL> Clients { get; set; }
        public DbSet<OrderDAL> Orders { get; set; }

    }
}