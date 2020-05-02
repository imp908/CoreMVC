

namespace crmvcsb.Domain.DomainSpecific.Currency
{
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using crmvcsb.Domain.DomainSpecific.Currency.API;
    using crmvcsb.Domain.Universal;
    public interface ICurrencyService : IService
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(IGetCurrencyCommand command);

    }
}
