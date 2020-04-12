

namespace crmvcsb.Domain.Currency.API
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


}

