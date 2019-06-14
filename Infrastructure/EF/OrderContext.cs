namespace order.Infrastructure.EF
{

    using Microsoft.EntityFrameworkCore;
    using order.Domain.Models.Ordering;

    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        public DbSet<AddressDAL> Address {get;set;}
        public DbSet<OrderItemDAL> Order { get; set; }
        public DbSet<DeliveryItemDAL> DeliveryItem{get;set;}
        public DbSet<DimensionalUnitDAL> DimensionalUnit { get; set; }
        public DbSet<UnitsConvertionDAL> UnitsConvertion { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {

            model.Entity<DimensionalUnitDAL>()
            .HasMany(s => s.Convertions)
            .WithOne(c => c.To)
            .OnDelete(DeleteBehavior.Restrict);

            model.Entity<DeliveryItemDAL>()
            .HasMany(c => c.Orders )
            .WithOne(s => s.Delivery)
            .OnDelete(DeleteBehavior.Restrict);     



            model.Entity<OrderItemDAL>()
            .HasMany(s => s.DeliveryItems )
            .WithOne(c => c.Order)
            .OnDelete(DeleteBehavior.Restrict);

            model.Entity<OrderItemDAL>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
            
            model.Entity<OrdersDeliveryItemsDAL>()
            .HasKey(t => new {t.OrderId, t.DeliveryId});

            model.Entity<OrdersDeliveryItemsDAL>()
            .HasOne(s => s.Delivery)
            .WithMany(c => c.Orders)
            .HasForeignKey(k => k.DeliveryId)
            .OnDelete(DeleteBehavior.Restrict);

            model.Entity<OrdersDeliveryItemsDAL>()
            .HasOne(s => s.Order)
            .WithMany(c => c.DeliveryItems)
            .HasForeignKey(k => k.OrderId)
            .OnDelete(DeleteBehavior.Restrict);



            model.Entity<DeliveryItemDimensionUnitDAL>()
            .HasKey(c => new {c.DeliveryItemId, c.DimensionalItemId});
            
            model.Entity<DeliveryItemDimensionUnitDAL>()
            .HasOne(s => s.DelivertyItem)
            .WithMany(c => c.Parameters)
            .HasForeignKey(k => k.DeliveryItemId)
            .OnDelete(DeleteBehavior.Restrict);
            
            model.Entity<DeliveryItemDimensionUnitDAL>()
            .HasOne(s => s.Unit)
            .WithMany(c => c.DeliveryItems)
            .HasForeignKey(k => k.DimensionalItemId)
            .OnDelete(DeleteBehavior.Restrict);
    
            
            model.Entity<OrdersAddressesDAL>()
            .HasKey(s => new {s.AddressFromId, s.AddressToId, s.OrderId});
             
            model.Entity<OrdersAddressesDAL>()
            .HasOne(s => s.AddressFrom)
            .WithMany(c => c.Orders)
            .HasForeignKey(k => k.AddressFromId)
            .OnDelete(DeleteBehavior.Restrict);
     



            model.Entity<DimensionalUnitDAL>().HasData( 
            new DimensionalUnitDAL(){Id = new System.Guid("00000000-0000-0000-0000-000000000001"), Name ="kg", Description ="wight in kg"}
            ,new DimensionalUnitDAL() { Id = new System.Guid("00000000-0000-0000-0000-000000000002"), Name = "lbs", Description = "wight in pounds" }
            ,new DimensionalUnitDAL() {Id = new System.Guid("00000000-0000-0000-0000-000000000003"), Name = "sm" , Description = "lenght in sm"}
            ,new DimensionalUnitDAL() { Id = new System.Guid("00000000-0000-0000-0000-000000000004"), Name = "inch", Description = "lenght in inches" }
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
                    Id = new System.Guid("10000000-0000-0000-0000-000000000001")
                    ,FromId = new System.Guid("00000000-0000-0000-0000-000000000002")
                    ,ToId = new System.Guid("00000000-0000-0000-0000-000000000001")
                    ,ConvertionRate = 0.220462F
                }
            );

            model.Entity<DeliveryItemDAL>().HasData(
                new DeliveryItemDAL(){
                    Id = new System.Guid("20000000-0000-0000-0000-000000000000"), Name = "Item1"                
                }
                , new DeliveryItemDAL()
                {
                    Id = new System.Guid("20000000-0000-0000-0000-000000000001"),
                    Name = "Item2",              
                }
            );

            model.Entity<DeliveryItemDimensionUnitDAL>()
            .HasData(
                new DeliveryItemDimensionUnitDAL(){
                    Id = new System.Guid("40000000-0000-0000-0000-000000000000") ,
                    DeliveryItemId = new System.Guid("20000000-0000-0000-0000-000000000000") 
                    , DimensionalItemId = new System.Guid("00000000-0000-0000-0000-000000000001")
                }
            );


            model.Entity<AddressDAL>().HasData(
                new AddressDAL(){
                    Id = new System.Guid("30000000-0000-0000-0000-000000000000"), Name = "Some address one"
                },
                new AddressDAL(){
                    Id = new System.Guid("30000000-0000-0000-0000-000000000001"), Name = "Some address two"
                }
            );

            model.Entity<OrderItemDAL>().HasData(
                new OrderItemDAL()
                {
                    Id = new System.Guid("50000000-0000-0000-0000-000000000000"),
                    Name = "Order one"
                    
                },
                new OrderItemDAL()
                {
                    Id = new System.Guid("50000000-0000-0000-0000-000000000001"),
                    Name = "Order two"
                    
                }
            );

            model.Entity<OrdersAddressesDAL>().HasData(
                new OrdersAddressesDAL(){
                    Id = new System.Guid("60000000-0000-0000-0000-000000000000") ,
                    OrderId = new System.Guid("50000000-0000-0000-0000-000000000000"),
                    AddressFromId = new System.Guid("30000000-0000-0000-0000-000000000000"),
                    AddressToId = new System.Guid("30000000-0000-0000-0000-000000000001") 
                }
                ,
                new OrdersAddressesDAL()
                {
                    Id = new System.Guid("60000000-0000-0000-0000-000000000001") ,
                    OrderId = new System.Guid("50000000-0000-0000-0000-000000000001"),
                    AddressFromId = new System.Guid("30000000-0000-0000-0000-000000000000"),
                    AddressToId = new System.Guid("30000000-0000-0000-0000-000000000001")
                }
                
            );

        }
    }
}