namespace crmvcsb.Domain.NewOrder
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using crmvcsb.Infrastructure.EF.NewOrder;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Domain.NewOrder.DAL;
    using System.Linq;
    using System.IO;

    using Microsoft.Extensions.Configuration;

    using Microsoft.Extensions.Logging;
    using crmvcsb.Domain.Currency;

    public class NewOrderManager
    {
        public static IConfigurationRoot configuration { get; set; }

        private static ILogger _logger;

        private static INewOrderService _newOrderService  { get;set;}

        public void BindService(INewOrderService newOrderService)
        {
            _newOrderService = newOrderService;
        }

        public static void ReInitialize()
        {
            _newOrderService.ReInitialize();
        }

        public static void CleanUp()
        {
            _newOrderService.CleanUp();
        }
    }
}
