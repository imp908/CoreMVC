

namespace crmvcsb.Domain.DomainSpecific.Currency
{
    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using crmvcsb.Domain.Universal;
    using crmvcsb.Domain.DomainSpecific.Currency.API;
   
    public interface ICurrencyService : IService
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
