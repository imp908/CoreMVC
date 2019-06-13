namespace order.Domain.Models.Ordering
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IWeightUnit
    {
        string Name {get;}
    }
    public interface IDeliveryItem{
        float Weight { get; set; }
        IWeightUnit WeightUnit { get; set; }
    }


    public class DeliveryItem : IDeliveryItem
    {
        public float Weight {get;set;}
        public IWeightUnit WeightUnit { get; set; }
    }

    public class Kg : IWeightUnit
    {
        public static string name => nameof(Kg);
        public string Name => Kg.name;
    }
    public class Pound : IWeightUnit
    {
        public static string name => nameof(Pound);
        public string Name => Pound.name;
    }
  



    /*EF level */
    public interface IGuidEntity{
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

    }
    public class OrderItemDAL
        : BaseEntity, IGuidEntity
        , INamedEntity
    {
        public string Name { get; set; }

        public DeliveryItemDAL Itme {get;set;}

        public AdressDAL From { get; set; }
        public AdressDAL To { get; set; }

        public int Amount {get;set;}
        public float DeliveryPrice { get; set; }
    }

    public class DeliveryItemDAL 
    : BaseEntity, IGuidEntity
    , INamedEntity
    {
        public string Name {get;set;}

        public DeliveryItemParameterDAL Parameters {get;set;}
    }

    public class DeliveryItemParameterDAL : BaseEntity, IGuidEntity
    {
        public float Weight {get;set;}
        public float Lenght { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }


        public DimensionalUnitDAL WeightDimension {get;set;}
        public Guid WeightDimensionId {get;set;}

        public DimensionalUnitDAL LenghtDimension { get; set; }
        public Guid LenghtDimensionId { get; set; }

        public DimensionalUnitDAL HeightDimension { get; set; }
        public Guid HeightDimensionId { get; set; }

        public DimensionalUnitDAL DepthDimension { get; set; }
        public Guid DepthDimensionId { get; set; }

    }

    public class DimensionalUnitDAL
    : BaseEntity, IGuidEntity
    , INamedEntity
    {
        public string Name {get;set;}
        public string Description { get; set; }
        
        public List<DeliveryItemParameterDAL> DeliveryItems {get;set;}
        public List<UnitsConvertionDAL> Convertions {get;set;}
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
