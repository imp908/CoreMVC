

namespace crmvcsb.Universal.DomainSpecific.Currency
{
    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using crmvcsb.Universal;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
   
    public interface ICurrencyServiceEF : IService, IServiceEF
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
