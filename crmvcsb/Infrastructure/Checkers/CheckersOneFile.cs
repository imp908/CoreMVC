
namespace InfrastructureCheckers
{
    using System;
    using System.Linq;
    
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;


    using AutoMapper;

    using crmvcsb.Infrastructure.EF.Currencies;
    using crmvcsb.Domain.DomainSpecific.Currency.DAL;

    using crmvcsb.Domain.DomainSpecific.Currency;

    public static class RepoAndUOWCheck
    {

        //static string connectionStringSQL = "Server=HP-HP000114\\SQLEXPRESS02;Database=EFdb;Trusted_Connection=True;";
        static string connectionStringSQL = "Server=AAAPC;Database=testdb;User Id=tl;Password=QwErT123;";

        public static void GO()
        {
            DbWithRepoReinitCheck();
        }

        public static void DbWithRepoReinitCheck()
        {

            using (CurrencyContext context = new CurrencyContext(
                new DbContextOptionsBuilder<CurrencyContext>()
                    .UseSqlServer(connectionStringSQL).Options))
            {
                crmvcsb.Infrastructure.EF.RepositoryEF repo = new crmvcsb.Infrastructure.EF.RepositoryEF(context);

                List<CurrencyDAL> currencies = repo.QueryByFilter<CurrencyDAL>(s => s.Id != 0).ToList();
                repo.DeleteRange(currencies);
                repo.Save();

                CurrencyService currencyService = new CurrencyService(repo);
                currencyService.ReInitialize();

                repo.Add(new CurrencyDAL() { Id = 1, Name = "KRW", IsoCode = "KRW" });
  
                try
                {
                    repo.SaveIdentity("Blogs");
                }
                catch (Exception)
                {
                    throw;
                }

                repo.AddRange(new List<CurrencyDAL>() {
                    new CurrencyDAL() { Id = 1, Name = "NOK", IsoCode = "NOK" }
                    ,new CurrencyDAL() { Id = 1, Name = "TRY", IsoCode = "TRY" }
                });

                try
                {
                    repo.SaveIdentity("Posts");
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }


    }
}

