namespace order.Domain.Services
{

    using order.Domain.Models.Ordering;
    using order.Domain.Interfaces;


    /* Hardcoded delivery accounting services */
    public class BirdAccounter : IBirdAccounter
    {
        public IOrderDeliveryBirdBLL Count(IOrderBLL Order)
        {
            OrderDeliveryBirdBLL result = new OrderDeliveryBirdBLL(){DeliveryPrice = 10F, DaysToDelivery = 5F};

            return result;
        }
    }

    public class TortiseAccounter : ITortiseAccounter
    {
        public IOrderDeliveryTortiseBLL Count(IOrderBLL Order)
        {
            OrderDeliveryTortiseBLL result = new OrderDeliveryTortiseBLL(){DeliveryPriceKoefficient=15F,DeliveryPrice = 10F, DaysToDelivery = 5F };

            return result;
        }
    }

}