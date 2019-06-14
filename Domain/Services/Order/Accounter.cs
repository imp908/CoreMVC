namespace crmvcsb.Domain.Services.Order
{

    using System;

    using order.Domain.Models.Ordering;
    using order.Domain.Interfaces;


    /* Hardcoded delivery accounting services */
    public class BirdAccounter : IBirdAccounter
    {
        public IOrderDeliveryBirdAPI Count(IAdressAPI addressFrom, IAdressAPI addressto, IAdressAPI Order)
        {
            OrderDeliveryBirdAPI result = new OrderDeliveryBirdAPI(){DeliveryPrice = 10F, DaysToDelivery = 5F};

            return result;
        }
    }

    public class TortiseAccounter : ITortiseAccounter
    {
        public IOrderDeliveryTortiseAPI Count(IAdressAPI addressFrom, IAdressAPI addressto, IAdressAPI Order)
        {
            IOrderDeliveryTortiseAPI result = new OrderDeliveryTortiseAPI(){DeliveryPriceKoefficient=15F, DeliveryDate = DateTime.Now.AddDays(5)};

            return result;
        }
    }

}