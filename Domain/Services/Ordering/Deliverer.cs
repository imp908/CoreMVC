namespace crmvcsb.Domain.Services.Order
{
    using order.Domain.Models.Ordering;
    using order.Domain.Interfaces;
    using AutoMapper;

    public class Deliverer : IDeliverer
    {
        IOrdersManagerWrite _orderManager;
        IBirdAccounter _birdAccounter;
        ITortiseAccounter _tortiseAccounter;
        IMapper _mapper;

        public Deliverer( IMapper mapper, IOrdersManagerWrite orderManager,IBirdAccounter birdAccounter,ITortiseAccounter tortiseAccounter){
            _orderManager = orderManager;
            _birdAccounter = birdAccounter;
            _tortiseAccounter = tortiseAccounter;
            _mapper = mapper;
        }

        public IOrderDeliveryBirdAPI AddOrderBirdService(IOrderCreateAPI order){
            IOrderBLL orderCreated = this._orderManager.AddOrder(order);

            OrderDeliveryBirdBLL accountedDelivery = _birdAccounter.Count(orderCreated) as OrderDeliveryBirdBLL;
            var result = _mapper.Map<OrderDeliveryBirdBLL,OrderDeliveryBirdAPI>(accountedDelivery);

            return result;
        }
        public IOrderDeliveryTortiseAPI AddOrderTortiseService(IOrderCreateAPI order)
        {
            IOrderBLL orderCreated = this._orderManager.AddOrder(order);

            OrderDeliveryTortiseBLL accountedDelivery = _tortiseAccounter.Count(orderCreated) as OrderDeliveryTortiseBLL;

            var result = _mapper.Map<OrderDeliveryTortiseBLL, OrderDeliveryTortiseAPI>(accountedDelivery);

            return result;
        }
        
    }
}