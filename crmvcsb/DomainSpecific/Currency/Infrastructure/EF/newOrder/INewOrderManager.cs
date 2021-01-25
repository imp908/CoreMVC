
namespace crmvcsb.Infrastructure.EF.NewOrder
{
    using crmvcsb.Universal;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewOrderManager : IDomainManager
    {
        Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency);
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command);
    }
}
