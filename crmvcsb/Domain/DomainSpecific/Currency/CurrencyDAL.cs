
namespace crmvcsb.Domain.DomainSpecific.Currency.DAL
{
    using System;
    using System.Collections.Generic;

    using crmvcsb.Domain.Universal.IEntities;
    using crmvcsb.Domain.Universal.Entities;
    
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

