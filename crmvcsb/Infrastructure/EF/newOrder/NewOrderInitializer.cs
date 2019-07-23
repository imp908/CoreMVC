namespace crmvcsb
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Infrastructure.EF.newOrder;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Domain.NewOrder.DAL;
    using System.Linq;

    public class NewOrderInitializer
    {
        static string  connectionString = "Server=AAAPC;Database=newOrderDB;User Id=tl;Password=QwErT123;";
     
        public static void Initialize()
        {
            using( NewOrderContext context = new NewOrderContext(new DbContextOptionsBuilder<NewOrderContext>().UseSqlServer(connectionString).Options))
            {
                RepositoryEF repo = new RepositoryEF(context);

                List<AddressDAL> addresses = new List<AddressDAL>();

                for(int i = 0; i < 10; i++){
                    addresses.Add(new AddressDAL(){Id = i+1, StreetName = $"test street {i}", Code = i+1});
                };

                repo.AddRange(addresses);
                
                try {
                    repo.SaveIdentity("Adresses");
                } catch(Exception e)
                {
                  
                }

                repo.Add<Currency>(new Currency() { Id = 1, Name = "USD", IsoCode = "USD" });
                repo.Add<Currency>(new Currency() { Id = 2, Name = "EUR", IsoCode = "EUR" });
                repo.Add<Currency>(new Currency() { Id = 3, Name = "GBP", IsoCode = "GBP" });
                repo.Add<Currency>(new Currency() { Id = 4, Name = "RUB", IsoCode = "RUB" });
                repo.Add<Currency>(new Currency() { Id = 5, Name = "JPY", IsoCode = "JPY" });
                repo.Add<Currency>(new Currency() { Id = 6, Name = "AUD", IsoCode = "AUD" });
                repo.Add<Currency>(new Currency() { Id = 7, Name = "CAD", IsoCode = "CAD" });
                repo.Add<Currency>(new Currency() { Id = 8, Name = "CHF", IsoCode = "CHF" });
                try{repo.SaveIdentity<Currency>();}catch(Exception e)
                {}

                repo.Add<CurrencyRates>(new CurrencyRates() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRates>(new CurrencyRates() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) });
                repo.Add<CurrencyRates>(new CurrencyRates() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) });
                try { repo.SaveIdentity<CurrencyRates>(); }catch (Exception e)
                { }

            }
        }

        public static void CleanUp()
        {
            using(NewOrderContext context = new NewOrderContext(new DbContextOptionsBuilder<NewOrderContext>().UseSqlServer(connectionString).Options))
            {

                RepositoryEF repo = new RepositoryEF(context);
                var addressesExist = repo.QueryByFilter<AddressDAL>(s => s.Id != null).ToList();
                repo.DeleteRange(addressesExist);
                try { repo.Save(); } catch (Exception e) { throw; }

                repo.DeleteRange(repo.GetAll<CurrencyRates>().ToList());
                repo.DeleteRange(repo.GetAll<Currency>().ToList());
                try { repo.Save(); } catch (Exception e) { throw; }

            }
           
        }
    }
}