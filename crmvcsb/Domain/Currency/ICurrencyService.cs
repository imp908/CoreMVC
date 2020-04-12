

namespace crmvcsb.Domain.Currency
{
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using crmvcsb.Domain.Currency.API;
    public interface ICurrencyService : IService
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(IGetCurrencyCommand command);

    }
}
