using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mvccoresb.Domain.Interfaces
{
    
    using mvccoresb.Domain.TestModels;
    
    public interface IEntityIntId
    {
        int Id{get;set;}
    }
    public interface IEntityGuidId
    {
        Guid Id { get; set; }
    }

    public interface IRepository
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
            where T : class;
   
        void Add<T>(T item) where T : class;
        void AddRange<T>(IList<T> items) where T : class;
        void Delete<T>(T item) where T : class;
        void DeleteRange<T>(IList<T> items) where T : class;
        void Update<T>(T item) where T : class;
        void UpdateRange<T>(IList<T> items) where T : class;
        IQueryable<T> SkipTake<T>(int skip,int take)
            where T : class;
        IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class;
        void Save();

        void SaveIdentity(string command);
    }

    public interface ICQRScrud
    {
        T Add<T>(T item) where T : class;
        void AddRange<T>(IList<T> items) where T : class;
        void Delete<T>(T item) where T : class;
        void DeleteRange<T>(IList<T> items) where T : class;
        T Update<T>(T item) where T : class;
        void UpdateRange<T>(IList<T> items) where T : class;
        IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression)
            where T : class;
    }

    public interface ICQRSBloggingWrite
    {
        PostAPI PersonAdsPostToBlog(PersonAdsPostCommand command);
        bool PersonDeletesPostFromBlog(PersonDeletesPost command);
        PostAPI PersonUpdatesPost(PersonUpdatesBlog command);
    }
    public interface ICQRSBloggingRead
    {
        IList<PostAPI> Get(GetPostsByPerson command);
        IList<PostAPI> Get(GetPostsByBlog command);
        IList<BlogAPI> Get(GetBlogsByPerson command);
    }
    
}


namespace order.Domain.Interfaces
{


    public class IOrderCreateAPI
    {
        public string AdressFrom { get; set; }
        public string AdressTo { get; set; }
        public string DelivertyItemName { get; set; }

        public IList<IDimensionalUnitAPI> Dimensions { get; set; }
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
    
    /*Results of order delivery accounting */
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



    /*SQRS wrapper above EF dbContext */
    public interface IOrdersManagerWrite
    {
        IOrderItemAPI AddOrder(IOrderCreateAPI query);
    }

    /* Accounter of price and delivery */    
    public interface IBirdAccounter
    {
        IOrderDeliveryBirdAPI Count(IAdressAPI addressFrom, IAdressAPI addressto, IAdressAPI Order);
    }
    public interface ITortiseAccounter
    {
        IOrderDeliveryTortiseAPI Count(IAdressAPI addressFrom, IAdressAPI addressto, IAdressAPI Order);
    }
  
}