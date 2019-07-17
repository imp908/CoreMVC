
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace crmvcsb.Domain.NewOrder
{
    public interface IEntityGuidIdDAL
    {
        Guid Id { get; set; }
    }
    public interface IEntityIntIdDAL
    {
        int Id { get; set; }
    }
    public interface IEntityStringIdDAL
    {
        string Id { get; set; }
    }


    public class EntityGuidIdDAL : IEntityGuidIdDAL
    {
        public Guid Id { get; set; }
    }
    public class EntityIntIdDAL : IEntityIntIdDAL
    {
        public int Id { get; set; }
    }
    public class EntityStringIdDAL : IEntityStringIdDAL
    {
        public string Id { get; set; }
    }


}

namespace crmvcsb.Domain.NewOrder.DAL
{
    
    public class PhysicalUnitConvertions : EntityIntIdDAL
    {
        public PhysicalUnitDAL UnitFrom {get;set;}
        public PhysicalUnitDAL UnitTo { get; set; }
        public double Coefficient {get;set;}
    }

    /*Kg,Pnd,sm,meter etc*/
    public class PhysicalUnitDAL : EntityGuidIdDAL
    {
        public string Name {get;set;}        
    }
    
    /*Length,Height, Weight or density exmpl*/
    public class PhysicalDimensionDAL : EntityGuidIdDAL
    {
        /*Length goes here */
        public string ParameterName {get;set;}

        /*Amount */
        public double Amount { get; set; }

        /*And sm goes here */
        public PhysicalUnitDAL DimensionUnit {get;set;}
    }
    public class GoodsDAL : EntityGuidIdDAL
    {
        public string ProductName { get; set; }

        /*
            Can be a bulk of corn, and it hase only volume and wigth 
            but t can be a pck of corn, with dimensions added to vlume and weight
        */
        List<PhysicalDimensionDAL> Dimensions { get; set; }
    }
    

    public class AddressDAL: EntityGuidIdDAL
    {
        public string Country { get; set; }
        public string City {get;set;}
        public string State { get; set; }
        public string Province { get; set; }
        public string StreetName { get; set; }
        public int Code {get;set;}
    }
    public class RouteVertexDAL : EntityGuidIdDAL
    {
        public int InRouteMoveOrder {get;set;}
        public AddressDAL From {get;set;}
        public AddressDAL To { get; set; }
        public double Distance { get; set; }
        public int PriorityWeigth { get; set; }
    }
    public class RouteDAL : EntityGuidIdDAL
    {
        public string Name {get;set;}
        
        public List<RouteVertexDAL> RouteVertexes {get;set;}
    }


    public class DeliveryItemDAL : EntityGuidIdDAL
    {
        public string DeliveryName { get; set; }
        public int DeliveryNumber { get; set; }

        List<GoodsDAL> GoodsToDeliver {get;set;}
        List<RouteDAL> Routes { get; set; }
    }

    public class ClientDAL : EntityGuidIdDAL
    {
        public string ClientName { get; set; }
        List<OrderDAL> Orders { get; set; }
    }

    public class OrderDAL : EntityGuidIdDAL
    {
        public ClientDAL Client {get;set;}
        List<DeliveryItemDAL> OrderedItems {get;set;}
    }

}

namespace crmvcsb.Domain.NewOrder.API
{


}