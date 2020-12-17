
namespace crmvcsb.Universal.DomainSpecific.NewOrder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.Logging;

    using crmvcsb.Universal;
    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Infrastructure.EF;

    public class NewOrderManager : IDomainManager, INewOrderManager
    {

        private static ILogger _logger;

        private static INewOrderServiceEF _newOrderService { get; set; }
        private static ICurrencyServiceEF _currencyService { get; set; }

        public NewOrderManager(INewOrderServiceEF newOrderService, ICurrencyServiceEF currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }
        public void BindService(INewOrderServiceEF newOrderService, ICurrencyServiceEF currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }
        
        public async Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency)
        {
            return await _currencyService.AddCurrency(currency);
        }
        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
        {
            return await _currencyService.GetCurrencyCrossRatesAsync(command);
        }

        public string GetDbName([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            return _newOrderService.GetRepositoryRead().GetDatabaseName();
        }
        public void ReInitialize([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.GetRepositoryWrite().ReInitialize();
            _currencyService.GetRepositoryWrite().ReInitialize();
        }
        public void CleanUp([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.GetRepositoryWrite().CleanUp();
        }

    }
}
