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

        public DbSet<AdressDAL> Address {get;set;}
        public DbSet<OrderItemDAL> Order { get; set; }
        public DbSet<DeliveryItemDAL> DeliveryItem{get;set;}
        public DbSet<DeliveryItemParameterDAL> DeliveryItemParameters { get; set; }
        public DbSet<MaterialUnitDAL> MaterialUnit { get; set; }
        public DbSet<UnitsConvertionDAL> UnitsConvertion { get; set; }


    protected override void OnModelCreating(ModelBuilder model)
    {

    }
}
}