
namespace crmvcsb.Infrastructure.EF.NewOrder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal;

    public interface INewOrderManager : IDomainManager
    {
        Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
