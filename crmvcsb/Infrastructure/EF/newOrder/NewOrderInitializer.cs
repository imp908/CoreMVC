namespace crmvcsb
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Infrastructure.EF.newOrder;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Domain.NewOrder.DAL;
    using System.Linq;

    using System.IO;

    using Microsoft.Extensions.Configuration;

    public class NewOrderInitializer
    {
        static string connectionString = "Server=AAAPC;Database=newOrderDB;User Id=tl;Password=QwErT123;";
        public static IConfigurationRoot configuration { get; set; }

        static NewOrderInitializer()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            configuration = builder.Build();
            connectionString = configuration.GetConnectionString("CostControlDb");
        }
   

        public static void ReInitialize()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            configuration = builder.Build();
            connectionString = configuration.GetConnectionString("CostControlDb");

            using ( NewOrderContext context = new NewOrderContext(new DbContextOptionsBuilder<NewOrderContext>().UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                RepositoryEF repo = new RepositoryEF(context);
                
                List<AddressDAL> addresses = new List<AddressDAL>();

                for(int i = 0; i < 10; i++){
                    addresses.Add(new AddressDAL(){Id = i+1, StreetName = $"test street {i}", Code = i+1});
                };

                repo.AddRange(addresses);
                
                try {
                    repo.SaveIdentity("Adresses");
                }
                catch(Exception e)
                {
                  
                }

                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 1, Name = "USD", IsoCode = "USD" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 2, Name = "EUR", IsoCode = "EUR" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 3, Name = "GBP", IsoCode = "GBP" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 4, Name = "RUB", IsoCode = "RUB" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 5, Name = "JPY", IsoCode = "JPY" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 6, Name = "AUD", IsoCode = "AUD" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 7, Name = "CAD", IsoCode = "CAD" });
                repo.Add<CurrencyDAL>(new CurrencyDAL() { Id = 8, Name = "CHF", IsoCode = "CHF" });
                try{repo.SaveIdentity<CurrencyDAL>();}catch(Exception e)
                {}

                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) });

                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 7, CurrencyFromId = 2, CurrencyToId = 5, Rate = 85.2M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 8, CurrencyFromId = 3, CurrencyToId = 5, Rate = 95.2M, Date = new DateTime(2019, 07, 23) });

                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 9, CurrencyFromId = 2, CurrencyToId = 6, Rate = 15M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 6, CurrencyToId = 3, Rate = 0.25M, Date = new DateTime(2019, 07, 23) });

                try { repo.SaveIdentity<CurrencyRatesDAL>(); }catch (Exception e)
                {}

            }
        }

        public static void CleanUp()
        {
            using(NewOrderContext context = new NewOrderContext(new DbContextOptionsBuilder<NewOrderContext>().UseSqlServer(connectionString).Options))
            {
                context.Database.EnsureCreated();
                RepositoryEF repo = new RepositoryEF(context);
                var addressesExist = repo.QueryByFilter<AddressDAL>(s => s.Id != null).ToList();
                repo.DeleteRange(addressesExist);
                try { repo.Save(); } catch (Exception e) { throw; }

                repo.DeleteRange(repo.GetAll<CurrencyRatesDAL>().ToList());
                repo.DeleteRange(repo.GetAll<CurrencyDAL>().ToList());
                try { repo.Save(); } catch (Exception e) { throw; }
            }           
        }
    }
}
