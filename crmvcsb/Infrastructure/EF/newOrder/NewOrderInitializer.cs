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
                    throw;
                }                
                
            }
        }

        public static void CleanUp()
        {
            using(NewOrderContext context = new NewOrderContext(new DbContextOptionsBuilder<NewOrderContext>().UseSqlServer(connectionString).Options))
            {
                RepositoryEF repo = new RepositoryEF(context);
                var addressesExist = repo.QueryByFilter<AddressDAL>(s => s.Id != null).ToList();
                repo.DeleteRange(addressesExist);

                try
                {
                    repo.Save();
                }
                catch (Exception e)
                {
                    throw;
                }
              
            }
           
        }
    }
}