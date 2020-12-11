

namespace crmvcsb.Universal.DomainSpecific.Currency.API
{
    using System;

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

    public interface ICurrencyAPI
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsMain { get; set; }
    }

}

