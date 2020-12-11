namespace crmvcsb.Infrastructure.Mapping
{

    using AutoMapper;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;

    /*Build in logging*/
    public class CurrenciesMapping
    {
        public static MapperConfiguration config()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurrencyRatesDAL, CrossCurrenciesAPI>()
                    .ForMember(d => d.From, m => m.MapFrom(src => src.CurrencyFrom.Name))
                    .ForMember(d => d.To, m => m.MapFrom(src => src.CurrencyTo.Name))
                    .ReverseMap().ForAllMembers(o => o.Ignore());

                cfg.CreateMap<CurrencyAPI, CurrencyDAL>()
                    .ForMember(d => d.CurRatesFrom, m => m.MapFrom(src => src.IsoCode))
                    .ForMember(d => d.CurRatesTo, m => m.MapFrom(src => src.IsoCode))
                    .ReverseMap().ForAllMembers(o => o.Ignore());
            });
        }
    }
}
