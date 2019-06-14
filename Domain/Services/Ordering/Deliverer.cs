namespace order.Domain.Services
{
    using order.Domain.Models;
    using order.Domain.Interfaces;
    using AutoMapper;

    public class Deliverer : IDeliverer
    {
        IOrdersManagerWrite _orderManager;
        IBirdAccounter _birdAccounter;
        ITortiseAccounter _tortiseAccounter;
        IMapper _mapper;

        public Deliverer( IMapper mapper, IOrdersManagerWrite orderManager,IBirdAccounter birdAccounter,ITortiseAccounter tortiseAccounter)
        {
            _orderManager = orderManager;
            _birdAccounter = birdAccounter;
            _tortiseAccounter = tortiseAccounter;
            _mapper = mapper;
        }

        public IOrderDeliveryBirdAPI AddOrderBirdService(IOrderCreateAPI order){
            OrderItemDAL orderCreated = this._orderManager.AddOrder(order);
            
            var orderToCount = this._mapper.Map<OrderItemDAL, OrderBLL>(orderCreated);           
            OrderDeliveryBirdBLL accountedDelivery = _birdAccounter.Count(orderToCount) as OrderDeliveryBirdBLL;

            var orderToUpdate = _mapper.Map<OrderDeliveryBirdBLL, OrderUpdateBLL>(accountedDelivery);
            OrderItemDAL updatedOrder = this._orderManager.UpdateOrder(orderToUpdate);

            var result = _mapper.Map<OrderItemDAL,OrderDeliveryBirdAPI>(updatedOrder);            
            
            return result;
        }
        public IOrderDeliveryTortiseAPI AddOrderTortiseService(IOrderCreateAPI order)
        {
            OrderItemDAL orderCreated = this._orderManager.AddOrder(order);

            var orderToCount = this._mapper.Map<OrderItemDAL, OrderBLL>(orderCreated);
            OrderDeliveryTortiseBLL accountedDelivery = _tortiseAccounter.Count(orderToCount) as OrderDeliveryTortiseBLL;

            var orderToUpdate = _mapper.Map<OrderDeliveryTortiseBLL, OrderUpdateBLL>(accountedDelivery);
            OrderItemDAL updatedOrder = this._orderManager.UpdateOrder(orderToUpdate);

            var result = _mapper.Map<OrderDeliveryTortiseBLL, OrderDeliveryTortiseAPI>(accountedDelivery);

            result.DaysToDelivery = System.DateTime.Now.AddDays(accountedDelivery.DaysToDelivery);
            return result;
        }
        
    }
}