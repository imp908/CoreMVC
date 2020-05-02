namespace crmvcsb.Domain.DomainSpecific
{

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.Logging;
    using crmvcsb.Domain.DomainSpecific.NewOrder;
    using crmvcsb.Domain.DomainSpecific.Currency;

    using crmvcsb.Domain.Universal;

    public class DomainManager : IDomainManager
    {
        public static IConfigurationRoot configuration { get; set; }

        private static ILogger _logger;

        private static INewOrderService _newOrderService { get; set; }
        private static ICurrencyService _currencyService { get; set; }

        public void BindService(INewOrderService newOrderService, ICurrencyService currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }

        public string GetDbName([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            return _newOrderService.GetDbName();
        }
        public void ReInitialize([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.ReInitialize();
            _currencyService.ReInitialize();
        }
        public void CleanUp([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.CleanUp();
        }
    }
}
