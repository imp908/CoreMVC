namespace crmvcsb.Domain
{

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.Logging;
    using crmvcsb.Domain.NewOrder;
    using crmvcsb.Domain.Currency;

    public class DomainManager
    {
        public static IConfigurationRoot configuration { get; set; }

        private static ILogger _logger;

        private static INewOrderService _newOrderService  { get;set;}
        private static ICurrencyService _currencyService { get; set; }

        public void BindService(INewOrderService newOrderService, ICurrencyService currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }

        public static string GetDbName([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            return _newOrderService.GetDbName();
        }
        public static void ReInitialize([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {            
            if (_newOrderService == null) 
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.ReInitialize();
            _currencyService.ReInitialize();
        }
        public static void CleanUp([System.Runtime.CompilerServices.CallerMemberName] string CallerMemberName = "")
        {
            if (_newOrderService == null)
            {
                _logger.LogError(CallerMemberName + ": service is null");
            }
            _newOrderService.CleanUp();
        }
    }
}
