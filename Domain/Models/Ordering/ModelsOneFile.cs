namespace order.Domain.Models.Ordering
{
    using System;
    using System.Collections.Generic;
    using order.Domain.Interfaces;

    /*Api layer */
    public class OrderCreateAPI : IOrderCreateAPI
    {
        public string AdressFrom { get; set; }
        public string AdressTo { get; set; }    
        public string DelivertyItemName { get; set; }

        public List<DimensionalUnitAPI> Dimensions { get; set; }
    }
    public class DimensionalUnitAPI : IDimensionalUnitAPI
    {
        public string Name {get;set;}
        public string Description { get; set; }
    }


    public class OrderItemAPI : IOrderItemAPI
    {
        public float DeliveryPrice { get; set; }
        public float DaysToDelivery { get; set; }
    }

    public class OrderDeliveryBirdAPI : IOrderDeliveryBirdAPI
    {
        public float DeliveryPrice { get; set; }
        public float DaysToDelivery { get; set; }
    }

    public class OrderDeliveryTortiseAPI : IOrderDeliveryTortiseAPI 
    {
        public float DeliveryPriceKoefficient { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class AdressAPI : IAdressAPI
    {
        public string Name { get; set; }     
    }
    public class OrderAPI : IOrderAPI
    {  
        public string Name { get; set; }


        public int ItemsOrderedAmount { get; set; }
        public float DeliveryPrice { get; set; }
        public float DaysToDelivery { get; set; }
    }




    /*EF level */
    public interface IGuidEntity
    {
         Guid Id {get;set;}
    }
    public interface INamedEntity
    {
        string Name { get; set; }
    }
    public class BaseEntity : IGuidEntity
    {
        public Guid Id { get; set; }
    }
    public class NameEntity : INamedEntity
    {
        public string Name { get; set; }
    }



    
    public class AdressDAL 
    : BaseEntity, IGuidEntity
        , INamedEntity
    {
        public string Name { get; set; }

        public List<OrdersAdresses> Orders {get;set;}
    }

    public class OrderItemUpdateDAL{
        public string Name { get; set; }


        public int ItemsOrderedAmount { get; set; }
        public float DeliveryPrice { get; set; }
        public float DaysToDelivery { get; set; }

        public List<OrdersDeliveryItemsDAL> DeliveryItems { get; set; }
        public List<OrdersAdresses> Directions { get; set; }
    }
    public class OrderItemDAL
        : BaseEntity, IGuidEntity
        , INamedEntity
    {
        public string Name { get; set; }              

        public float? DaysToDelivery {get;set;}
        
        public float? DeliveryPrice { get; set; }


        public List<OrdersDeliveryItemsDAL> DeliveryItems { get; set; }
        public List<OrdersAdresses> Directions {get;set;}
    }

    public class OrdersAdresses : BaseEntity, IGuidEntity
    {
        public AdressDAL AddressFrom { get; set; }
        public Guid AddressFromId { get; set; }
        public AdressDAL AddressTo { get; set; }
        public Guid AddressToId { get; set; }
        public OrderItemDAL Order {get;set;}
        public Guid OrderId {get;set;}
        
    }

    public class DeliveryItemDAL : BaseEntity, IGuidEntity
    {
        public string Name { get; set; }
        
        
        public List<DeliveryItemDimensionUnitDAL> Parameters {get;set;}
        
        public List<OrdersDeliveryItemsDAL> Orders {get;set;}

    }

    public class OrdersDeliveryItemsDAL
        : BaseEntity, IGuidEntity
    {
        public OrderItemDAL Order { get; set; }
        public Guid OrderId {get;set;}
        public DeliveryItemDAL Delivery { get; set; }
        public Guid DeliveryId {get;set;}
    }

    public class DimensionalUnitDAL
    : BaseEntity, IGuidEntity
    , INamedEntity
    {
        public string Name {get;set;}
        public string Description { get; set; }

        
        public List<DeliveryItemDimensionUnitDAL> DeliveryItems {get;set;}

        public List<UnitsConvertionDAL> Convertions {get;set;}
    }
    
    public class DeliveryItemDimensionUnitDAL
        : BaseEntity, IGuidEntity
    {
        public DeliveryItemDAL DelivertyItem {get;set;}
        public Guid DeliveryItemId { get; set; }
        public DimensionalUnitDAL Unit {get;set;}
        public Guid DimensionalItemId {get;set;}

        public float UnitAmount {get;set;}
    }


    public class UnitsConvertionDAL : BaseEntity, IGuidEntity
    {
        public DimensionalUnitDAL From {get;set;}
        public Guid FromId {get;set;}
        public DimensionalUnitDAL To { get; set; }
        public Guid ToId { get; set; }
        
        public float ConvertionRate {get;set;}
    }
    

}
