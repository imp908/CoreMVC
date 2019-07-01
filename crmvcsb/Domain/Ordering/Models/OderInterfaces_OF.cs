using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace order.Domain.Interfaces
{

    using order.Domain.Models;    

    public interface IOrderCreateAPI
    {
        string AdressFrom { get; set; }
        string AdressTo { get; set; }
        string DelivertyItemName { get; set; }

        IList<DimensionalUnitAPI> Dimensions { get; set; }
    }
    public interface IDimensionalUnitAPI
    {
        string Name { get; set; }
        string Description { get; set; }
    }



    /*Model for accounter module do counting delivery price and time for delivery with addresses from to*/
    public interface IAdressAPI
    {
        string Name { get; set; }
    }
    public interface IOrderAPI
    {
        string Name { get; set; }


        int ItemsOrderedAmount { get; set; }
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }



    /* Results of order creation*/
    public interface IOrderItemAPI
    {
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }

    public interface IOrderAccountingBLL
    {
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }

    public interface IOrderDeliveryBirdAPI
    {
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }
    public interface IOrderDeliveryTortiseAPI
    {
        float DeliveryPriceKoefficient { get; set; }
        DateTime DeliveryDate { get; set; }

    }


    /*Results of order delivery accounting */
    public interface IOrderDeliveryBirdBLL
    {
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }
    public interface IOrderDeliveryTortiseBLL
    {
        float DeliveryBasePrice { get; set; }
        float DeliveryPriceKoefficient { get; set; }
    }



    public interface IOrderBLL
    {
        Guid OrderId { get; set; }
        string Name { get; set; }

        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }

        IAddressBLL AddressFrom { get; set; }
        IAddressBLL AddressTo { get; set; }
    }
    public interface IOrderUpdateBLL
    {
        Guid OrderId { get; set; }
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }

    public interface IAddressBLL
    {
        string Name { get; set; }
    }

    /*SQRS wrapper above EF dbContext */
    public interface IOrdersManagerWrite
    {
        OrderItemDAL AddOrder(IOrderCreateAPI query);
        OrderItemDAL UpdateOrder(IOrderUpdateBLL order);
    }


    public interface IDeliverer
    {
        IOrderDeliveryBirdAPI AddOrderBirdService(IOrderCreateAPI order);
        IOrderDeliveryTortiseAPI AddOrderTortiseService(IOrderCreateAPI order);
    }

    /* Accounter of price and delivery */
    public interface IBirdAccounter
    {
        IOrderDeliveryBirdBLL Count(IOrderBLL Order);
    }
    public interface ITortiseAccounter
    {
        IOrderDeliveryTortiseBLL Count(IOrderBLL Order);
    }

}