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
    public interface IOrderItemAPI
    {
        float DeliveryPrice { get; set; }
        float DaysToDelivery { get; set; }
    }
    public interface IOrdersManagerWrite
    {
        IOrderItemAPI AddOrder(IOrderCreateAPI query);
    }
}