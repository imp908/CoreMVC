using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System;

using System.Threading.Tasks;

using System.Linq;

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.ChangeTracking;


using AutoMapper;


namespace order.Infrastructure.EF
{

    using order.Domain.Models;
    using order.Domain.Interfaces;

    public class OrdersManager
    {
        internal IRepository _repository;
        internal IMapper _mapper;

        public OrdersManager()
        {

        }

        public OrdersManager(IRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

    }

    public class OrdersManagerWrite : OrdersManager, IOrdersManagerWrite
    {
        public OrdersManagerWrite() : base() { }
        public OrdersManagerWrite(IRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }

        public OrderItemDAL AddOrder(IOrderCreateAPI queryIn)
        {
            OrderItemDAL order = new OrderItemDAL();
            OrderCreateAPI query = new OrderCreateAPI();

            if (queryIn is OrderCreateAPI)
            {
                query = queryIn as OrderCreateAPI;
            }

            if (
                query == null
                || string.IsNullOrEmpty(query.AdressFrom)
                || string.IsNullOrEmpty(query.AdressTo)
                || string.IsNullOrEmpty(query.DelivertyItemName)
                || (query.Dimensions?.Any() != true)
            ) { throw new NullReferenceException(); }

            try
            {

                var itemToAdd = new DeliveryItemDAL();
                itemToAdd = this._repository.GetAll<DeliveryItemDAL>(s => s.Name == query.DelivertyItemName).FirstOrDefault();

                if (itemToAdd == null)
                {
                    itemToAdd = new DeliveryItemDAL() { Name = query.DelivertyItemName };
                    this._repository.Add<DeliveryItemDAL>(itemToAdd);
                    this._repository.Save();
                }

                if (itemToAdd == null) { throw new NullReferenceException(); }

                foreach (DimensionalUnitAPI d in query.Dimensions)
                {
                    DimensionalUnitDAL exist = this._repository.GetAll<DimensionalUnitDAL>(s => s.Name == d.Name).FirstOrDefault();
                    if (exist == null)
                    {
                        exist = new DimensionalUnitDAL() { Name = d.Name, Description = d.Description };
                        this._repository.Add<DimensionalUnitDAL>(exist);
                        this._repository.Save();

                        var dimUnit = new DeliveryItemDimensionUnitDAL() { DeliveryItemId = itemToAdd.Id, DimensionalItemId = exist.Id };
                        this._repository.Add<DeliveryItemDimensionUnitDAL>(dimUnit);
                        this._repository.Save();
                        if (dimUnit == null) { throw new NullReferenceException(); }
                    }
                    if (exist == null) { throw new NullReferenceException(); }

                }

                order = new OrderItemDAL() { Name = "New order" };
                this._repository.Add<OrderItemDAL>(order);
                this._repository.Save();
                if (order == null) { throw new NullReferenceException(); }

                var orderDelivery = new OrdersDeliveryItemsDAL() { OrderId = order.Id, DeliveryId = itemToAdd.Id };
                this._repository.Add<OrdersDeliveryItemsDAL>(orderDelivery);
                this._repository.Save();
                if (orderDelivery == null) { throw new NullReferenceException(); }

                var adressFrom = this._repository.GetAll<AddressDAL>(s => s.Name == query.AdressFrom)
                    .FirstOrDefault();
                var adressTo = this._repository.GetAll<AddressDAL>(s => s.Name == query.AdressTo)
                    .FirstOrDefault();

                var orderAdress = new OrdersAddressesDAL()
                {
                    AddressFromId = adressFrom.Id,
                    AddressToId = adressTo.Id,
                    OrderId = order.Id
                };
                this._repository.Add<OrdersAddressesDAL>(orderAdress);
                this._repository.Save();
                if (orderAdress == null) { throw new NullReferenceException(); }

                order = this._repository.GetAll<OrderItemDAL>(s => s.Id == order.Id).FirstOrDefault();
                var orderToUpdate = this._mapper.Map<OrderItemDAL, OrderItemUpdateDAL>(order);
                this._repository.Update<OrderItemDAL>(order);
                this._repository.Save();


            }
            catch (Exception e)
            {

            }
            return order;
        }

        public OrderItemDAL UpdateOrder(IOrderUpdateBLL order)
        {
            OrderItemDAL result = new OrderItemDAL();

            OrderItemDAL orderToUpdate = this._repository.GetAll<OrderItemDAL>(s => s.Id == order.OrderId)
                .FirstOrDefault();
            orderToUpdate = this._mapper.Map<OrderUpdateBLL, OrderItemDAL>((OrderUpdateBLL)order, orderToUpdate);
            this._repository.Save();
            if (orderToUpdate == null) { throw new NullReferenceException(); }

            return orderToUpdate;

        }

    }

    public class OrdersManagerRead : OrdersManager
    {
        public OrdersManagerRead(IRepository repository, IMapper mapper)
            : base(repository, mapper)
        {

        }

    }

}
