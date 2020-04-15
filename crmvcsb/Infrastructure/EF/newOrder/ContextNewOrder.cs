namespace crmvcsb.Infrastructure.EF.NewOrder
{
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Domain.NewOrder.DAL;
    

    public class ContextNewOrder : DbContext
    {
        public ContextNewOrder(DbContextOptions<ContextNewOrder> options) : base(options)
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
            builder.Entity<AddressDAL>().Property(s => s.Id).ValueGeneratedOnAdd()
            //.HasDefaultValueSql("IDENTITY(1,1)")
            ;

          

        }
     

        public DbSet<AddressDAL> Adresses { get; set; }
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