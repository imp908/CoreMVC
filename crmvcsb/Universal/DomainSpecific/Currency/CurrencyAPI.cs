﻿using System;

namespace crmvcsb.Universal.DomainSpecific.Currency.API
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
        public string From { get; set; }
        public string To { get; set; }
        public string Throught { get; set; }
        public decimal Rate { get; set; }
    }

    public class CurrencyAPI : ICurrencyAPI
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsMain { get; set; }  
    }
}