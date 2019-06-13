namespace order.Infrastructure.EF
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using order.Domain.Models.Ordering;

    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        public DbSet<AdressDAL> Address {get;set;}
        public DbSet<OrderItemDAL> Order { get; set; }
        public DbSet<DeliveryItemDAL> DeliveryItem{get;set;}
        public DbSet<DeliveryItemParameterDAL> DeliveryItemParameters { get; set; }
        public DbSet<DimensionalUnitDAL> DimensionalUnit { get; set; }
        public DbSet<UnitsConvertionDAL> UnitsConvertion { get; set; }


    protected override void OnModelCreating(ModelBuilder model)
    {

        model.Entity<DimensionalUnitDAL>()
        .HasMany(s => s.Convertions)
        .WithOne(c => c.To)
        .OnDelete(DeleteBehavior.Restrict);

        model.Entity<DeliveryItemParameterDAL>()
        .HasOne(s => s.WeightDimension)
        .WithMany(c => c.DeliveryItems)
        .OnDelete(DeleteBehavior.Restrict);


        model.Entity<DimensionalUnitDAL>().HasData( 
        new DimensionalUnitDAL(){Id = new System.Guid("00000000-0000-0000-0000-000000000001"), Name ="kg", Description ="wight in kg"}
        ,new DimensionalUnitDAL() { Id = new System.Guid("00000000-0000-0000-0000-000000000002"), Name = "lbs", Description = "wight in pounds" }
        ,new DimensionalUnitDAL() {Id = new System.Guid("00000000-0000-0000-0000-000000000003"), Name = "sm" , Description = "lenght in sm"}
        , new DimensionalUnitDAL() { Id = new System.Guid("00000000-0000-0000-0000-000000000004"), Name = "inch", Description = "lenght in inches" }
        );

        model.Entity<UnitsConvertionDAL>().HasData(
            new UnitsConvertionDAL(){
                Id = new System.Guid("10000000-0000-0000-0000-000000000000")
                , FromId = new System.Guid("00000000-0000-0000-0000-000000000001")
                , ToId = new System.Guid("00000000-0000-0000-0000-000000000002")
                , ConvertionRate = 2.20462F
            }
            , new UnitsConvertionDAL()
            {
                Id = new System.Guid("20000000-0000-0000-0000-000000000000")
                ,FromId = new System.Guid("00000000-0000-0000-0000-000000000002")
                ,ToId = new System.Guid("00000000-0000-0000-0000-000000000001")
                ,ConvertionRate = 0.220462F
            }
        );

        model.Entity<DeliveryItemParameterDAL>().HasData(
            new DeliveryItemParameterDAL(){
                Id = new System.Guid("30000000-0000-0000-0000-000000000000"),
                Weight = 10,
                WeightDimensionId = new System.Guid("00000000-0000-0000-0000-000000000001"),
                Lenght = 5,
                LenghtDimensionId = new System.Guid("00000000-0000-0000-0000-000000000003")
            }
        );

    }
}
}