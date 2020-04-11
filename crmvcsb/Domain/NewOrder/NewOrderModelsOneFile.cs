
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;




namespace crmvcsb.Domain.Currencies
{

    public interface ICrossCurrenciesAPI
    {
        string From { get; set; }
        string To { get; set; }
        decimal Rate { get; set; }
    }

    public interface IGetCurrencyCommand
    {
        string FromCurrency { get; set; }
        string ToCurrency { get; set; }
        string ThroughCurrency { get; set; }
        DateTime Date { get; set; }
    }

    public interface IService
    {
        string GetDbName();

        void ReInitialize();

        void CleanUp();
    }

    public interface INewOrderService : IService
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(IGetCurrencyCommand command);

    }
}


namespace crmvcsb.Domain.Currencies.DAL
{
    using crmvcsb.Domain.IEntities;
    using crmvcsb.Domain.Entities;

    public class CurrencyDAL : EntityIntIdDAL
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsMain { get; set; }

        public List<CurrencyRatesDAL> CurRatesFrom { get; set; }
        public List<CurrencyRatesDAL> CurRatesTo { get; set; }
    }

    public class CurrencyRatesDAL : EntityIntIdDAL, IEntityIntIdDAL, IDateEntityDAL
    {
        public CurrencyDAL CurrencyFrom { get; set; }
        public CurrencyDAL CurrencyTo { get; set; }

        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }

        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }


}


namespace crmvcsb.Domain.Currencies.API
{

    public class GetCurrencyCommand : IGetCurrencyCommand
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public string ThroughCurrency { get; set; }
        public DateTime Date { get; set; }
    }

    public class CrossCurrenciesAPI : ICrossCurrenciesAPI
    {
        public string From {get;set;}
        public string To { get; set; }
        public string Throught { get; set; }
        public decimal Rate { get; set; }
    }

 }