
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Threading;
using System.Threading.Tasks;

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

    public interface IDateEntityDAL
    {
        DateTime Date {get;set;}
    }
    public interface IDateRangeEntityDAL
    {
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
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


    public class EntityDateDAL : IDateEntityDAL
    {
        public DateTime Date {get;set;}
    }
    public class EntityDateRangeDAL : IDateRangeEntityDAL
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public interface ICrossCurrenciesAPI
    {
        string From {get;set;}
        string To { get; set; }
        decimal Rate { get; set; }
    }


    public interface INewOrderManager
    {
        Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(GetCurrencyCommand command);
    }

    public class GetCurrencyCommand
    {
        public string IsoCode { get; set; }
        public DateTime Date { get; set; }
    }

}

namespace crmvcsb.Domain.NewOrder.DAL
{

    public class CurrencyDAL : EntityIntIdDAL
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsMain { get; set; }

        public List<CurrencyRatesDAL> CurRatesFrom { get; set; }
        public List<CurrencyRatesDAL> CurRatesTo { get; set; }
    }

    public class CurrencyRatesDAL : EntityIntIdDAL, IEntityIntIdDAL, IDateEntityDAL
    {
        public CurrencyDAL CurrencyFrom { get; set; }
        public CurrencyDAL CurrencyTo { get; set; }
        
        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }
        
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }





    public class PhysicalUnitConvertions : EntityIntIdDAL
    {
        public PhysicalUnitDAL UnitFrom { get; set; }
        public PhysicalUnitDAL UnitTo { get; set; }
        public double Coefficient { get; set; }
    }

    /*Kg,Pnd,sm,meter etc*/
    public class PhysicalUnitDAL : EntityIntIdDAL
    {
        public string Name { get; set; }
    }

    /*Length,Height, Weight or density exmpl*/
    public class PhysicalDimensionDAL : EntityIntIdDAL
    {
        /*Length goes here */
        public string ParameterName { get; set; }

        /*Amount */
        public double Amount { get; set; }

        /*And sm goes here */
        public PhysicalUnitDAL DimensionUnit { get; set; }
    }
    public class GoodsDAL : EntityIntIdDAL
    {
        public string ProductName { get; set; }

        /*
            Can be a bulk of corn, and it hase only volume and wigth 
            but t can be a pck of corn, with dimensions added to vlume and weight
        */
        List<PhysicalDimensionDAL> Dimensions { get; set; }
    }


    public class AddressDAL : EntityIntIdDAL
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string StreetName { get; set; }
        public int Code { get; set; }
    }
    public class RouteVertexDAL : EntityIntIdDAL
    {
        public int InRouteMoveOrder { get; set; }
        public AddressDAL From { get; set; }
        public AddressDAL To { get; set; }
        public double Distance { get; set; }
        public int PriorityWeigth { get; set; }
    }
    public class RouteDAL : EntityIntIdDAL
    {
        public string Name { get; set; }

        public List<RouteVertexDAL> RouteVertexes { get; set; }
    }


    public class DeliveryItemDAL : EntityGuidIdDAL
    {
        public string DeliveryName { get; set; }
        public int DeliveryNumber { get; set; }

        List<GoodsDAL> GoodsToDeliver { get; set; }
        List<RouteDAL> Routes { get; set; }
    }

    public class ClientDAL : EntityGuidIdDAL
    {
        public string ClientName { get; set; }
        List<OrderDAL> Orders { get; set; }
    }

    public class OrderDAL : EntityGuidIdDAL
    {
        public ClientDAL Client { get; set; }
        List<DeliveryItemDAL> OrderedItems { get; set; }
    }

}

namespace crmvcsb.Domain.NewOrder.API
{

    public class CrossCurrenciesAPI : ICrossCurrenciesAPI
    {
        public string From {get;set;}
        public string To { get; set; }
        public decimal Rate { get; set; }
    }

 }