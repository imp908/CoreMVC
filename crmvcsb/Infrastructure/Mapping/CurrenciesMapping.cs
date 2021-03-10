namespace crmvcsb.Infrastructure.Mapping
{

    using AutoMapper;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;
    using crmvcsb.Universal;
    using Microsoft.AspNetCore.Mvc;

    public class CurrenciesMapping
    {
        public static MapperConfiguration config()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CurrencyRatesDAL, CrossCurrenciesAPI>()                    
                    .ForMember(d => d.From, m => m.MapFrom(src => src.CurrencyFrom.Name))
                    .ForMember(d => d.To, m => m.MapFrom(src => src.CurrencyTo.Name))
                    .ReverseMap().ForAllOtherMembers(o => o.Ignore());   

                cfg.CreateMap<CurrencyAPI, CurrencyDAL>()
                    .ForMember(d => d.IsoCode, m => m.MapFrom(src => src.IsoCode))
                    .ForMember(d => d.Name, m => m.MapFrom(src => src.Name))
                    .ReverseMap().ForAllOtherMembers(o => o.Ignore());

                cfg.CreateMap<CurrencyUpdateAPI, CurrencyDAL>()
                   .ForMember(d => d.IsMain, m => m.MapFrom(src => src.IsMain))
                   .ForMember(d => d.Name, m => m.MapFrom(src => src.Name))
                   .ReverseMap().ForAllOtherMembers(o => o.Ignore());
                                
            });
        }
    }
}
