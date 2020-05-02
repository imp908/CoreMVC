
namespace crmvcsb.Domain.DomainSpecific.NewOrder
{

    using crmvcsb.Domain.DomainSpecific.Currency;
    public class NewOrderManager : DomainManager
    {

        private static INewOrderService _newOrderService { get; set; }
        private static ICurrencyService _currencyService { get; set; }

        public void BindService(INewOrderService newOrderService, ICurrencyService currencyService)
        {
            _newOrderService = newOrderService;
            _currencyService = currencyService;
        }

    }
}
