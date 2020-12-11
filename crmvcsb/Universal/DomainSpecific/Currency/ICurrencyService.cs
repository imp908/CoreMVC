

namespace crmvcsb.Universal.DomainSpecific.Currency
{
    
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using crmvcsb.Universal;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
   
    public interface ICurrencyService : IService
    {
        public void CleanUp();
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
