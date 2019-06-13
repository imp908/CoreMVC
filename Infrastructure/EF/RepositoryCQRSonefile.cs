using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using System;

using System.Threading.Tasks;

using System.Linq;

using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.ChangeTracking;

using Newtonsoft;

using AutoMapper;


namespace mvccoresb.Infrastructure.EF
{


    using mvccoresb.Domain.TestModels;    

    using mvccoresb.Domain.Interfaces;    



    public class RepositoryEF : IRepository
    {
        DbContext _context;

        public RepositoryEF(DbContext context){
            _context=context;
        }
        
        public IQueryable<T> GetAll<T>(Expression<Func<T,bool>> expression=null) 
            where T : class
        {
            return (expression == null)
                ? this._context.Set<T>()
                : this._context.Set<T>().Where(expression);

        }     

        public void Add<T> (T item)
            where T : class
        {
            this._context.Set<T>().Add(item);            
        }

        public Task<EntityEntry<T>> AddAsync<T>(T item)
           where T : class
        {
            return this._context.Set<T>().AddAsync(item);
        }

        public void AddRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().AddRange(items);
        }

        public Task AddRangeAsync<T>(IList<T> items)
                where T : class
        {
            return this._context.Set<T>().AddRangeAsync(items);
        }

        public void Delete<T>(T item)
           where T : class
        {
            this._context.Set<T>().Remove(item);
        }

        public void DeleteRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().RemoveRange(items);
        }

        public void Update<T>(T item)
            where T : class
        {
            this._context.Set<T>().Update(item);
        }

        public void UpdateRange<T>(IList<T> items)
            where T : class
        {
            this._context.Set<T>().UpdateRange(items);
        }
    
        public IQueryable<T> QueryByFilter<T>(Expression<Func<T,bool>> expression) 
            where T : class
        {            
            return this._context.Set<T>().Where(expression);
        }

        public IQueryable<T> SkipTake<T>(int skip=0,int take=10)
            where T : class
        {
            return this._context.Set<T>().Skip(skip).Take(take);
        }

        public void Save(){
            this._context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return this._context.SaveChangesAsync();
        }
        
        /*Provides identity column manual insert while testing */
        public void SaveIdentity(string tableFullName)
        {
            string cmd = $"SET IDENTITY_INSERT {tableFullName} ON;";
            this._context.Database.OpenConnection();
            this._context.Database.ExecuteSqlCommand(cmd);
            this._context.SaveChanges();
            cmd = $"SET IDENTITY_INSERT {tableFullName} OFF;";
            this._context.Database.ExecuteSqlCommand(cmd);
            this._context.Database.CloseConnection();
        }
    }

    public class CQRSEFBlogging 
    {
        internal IRepository _repository;
        internal IMapper _mapper;
        
        public CQRSEFBlogging(IRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

    }

    public class CQRSBloggingWrite : CQRSEFBlogging, ICQRSBloggingWrite
    {

        public CQRSBloggingWrite(IRepository repository, IMapper mapper) 
            : base(repository,mapper){}

        /*Adding object drom command, mapping and command->EF returning EF -> API*/
        public PostAPI PersonAdsPostToBlog(PersonAdsPostCommand command)
        {
            PostAPI postReturn = new PostAPI();

            if (this._mapper == null || command == null)
            {
                return null;
            }

            try
            {
                var postToAdd = this._mapper.Map(command, command.GetType(), typeof(PostEF)) as PostEF;
                this._repository.Add<PostEF>(postToAdd);
                this._repository.Save();

                var postAdded = this._repository.QueryByFilter<PostEF>(s => s.PostId == postToAdd.PostId)
                .Include(x => x.Blog).Include(x => x.Author)
                .FirstOrDefault();

                postReturn = this._mapper.Map(postAdded, postAdded.GetType(), typeof(PostAPI)) as PostAPI;
                return postReturn;
            }
            catch (Exception e)
            {

            }

            return postReturn;
        }

        public PostAPI PersonUpdatesPost(PersonUpdatesBlog command)
        {
          
            PostAPI updatedItem = new PostAPI();
            if (command == null || command?.Post == null || this._mapper == null) { return updatedItem; }
            try
            {
                PostEF itemToUpdate = this._repository.GetAll<PostEF>(s => s.PostId == command.Post.PostId).FirstOrDefault();
                //itemToUpdate = this._mapper.Map<PostAPI,PostEF>(command.Post);
                
                itemToUpdate.Title = command.Post.Title;
                itemToUpdate.Content = command.Post.Content;

                this._repository.Update<PostEF>(itemToUpdate);
                this._repository.Save();

                var itemExists = this._repository.GetAll<PostEF>(s => s.PostId == command.Post.PostId)
                .Include(s => s.Blog)
                .Include(s => s.Author)
                .FirstOrDefault();

                if (itemExists !=null)
                {
                    updatedItem = this._mapper.Map<PostEF,PostAPI>(itemExists);
                }

                return updatedItem;
            }
            catch (Exception e)
            {

                return null;
            }

            return updatedItem;
        }

        public bool PersonDeletesPostFromBlog(PersonDeletesPost command)
        {
            if (command == null)
            {
                return false;
            }
            try
            {
                var itemToDelete = this._repository.GetAll<PostEF>(s => s.PostId == command.PostId).FirstOrDefault();

                if (itemToDelete != null)
                {
                    this._repository.Delete<PostEF>(itemToDelete);
                    this._repository.Save();
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }

    }

    public class CQRSBloggingRead : CQRSEFBlogging, ICQRSBloggingRead
    {

        public CQRSBloggingRead(IRepository repository, IMapper mapper)
            : base(repository, mapper) { }


        public IList<PostAPI> Get(GetPostsByPerson command)
        {
            IList<PostAPI> itemsReturn = new List<PostAPI>();
            try
            {
                var itemsExist = this._repository.GetAll<PostEF>()
                .Where(s => s.AuthorId == command.PersonId)
                .ToList();

                if (itemsExist.Any())
                {
                    itemsReturn = this._mapper.Map<IList<PostEF>, IList<PostAPI>>(itemsExist);
                }
            }
            catch (Exception e)
            {

            }

            return itemsReturn;

        }
        
        public IList<PostAPI> Get(GetPostsByBlog command)
        {
            IList<PostAPI> postsReturn = new List<PostAPI>();
            try
            {
                var postsExist = this._repository.GetAll<PostEF>()
                .Include(s => s.Blog)
                .Where(s => s.BlogId == command.BlogId)
                .ToList();

                if(postsExist.Any())
                {
                    postsReturn = this._mapper.Map<IList<PostEF>,IList<PostAPI>>(postsExist);
                }
            }
            catch (Exception e)
            {

            }

            return postsReturn;
        }
        public IList<BlogAPI> Get(GetBlogsByPerson command)
        {
            IList<BlogAPI> itemsReturn = new List<BlogAPI>();
            try
            {
                var itemsExist = this._repository.GetAll<PostEF>()
                .Include(s => s.Author)
                .Include(s => s.Blog)
                .Where(s => s.Author.Id == command.PersonId)
                .Select(s => s.Blog)
                .ToList();

                if (itemsExist.Any())
                {
                    itemsReturn = this._mapper.Map<IList<BlogEF>, IList<BlogAPI>>(itemsExist);
                }
            }
            catch (Exception e)
            {

            }

            return itemsReturn;
        }
    
    }

   
}

namespace order.Infrastructure.EF
{

    using order.Domain.Models.Ordering;


    using mvccoresb.Domain.TestModels;

    using mvccoresb.Domain.Interfaces;
    using order.Domain.Interfaces;
    

public class OrdersManager
{
    internal IRepository _repository;
    internal IMapper _mapper;

    public OrdersManager(){

    }

    public OrdersManager(IRepository repository, IMapper mapper)
    {
        this._repository = repository;
        this._mapper = mapper;
    }

}

public class OrdersManagerWrite : OrdersManager, IOrdersManagerWrite
{
    public OrdersManagerWrite():base(){}
    public OrdersManagerWrite(IRepository repository, IMapper mapper)
        : base(repository, mapper)
    {

    }

    public IOrderItemAPI AddOrder(IOrderCreateAPI queryIn)
    {
        IOrderItemAPI result = new OrderItemAPI();
        OrderCreateAPI query = new OrderCreateAPI();

        if(queryIn is OrderCreateAPI){
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

            if( itemToAdd == null){
                itemToAdd= new DeliveryItemDAL(){Name = query.DelivertyItemName };
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
                }
                if (exist == null) { throw new NullReferenceException(); }

                var dimUnit = new DeliveryItemDimensionUnitDAL() { DeliveryItemId = itemToAdd.Id, DimensionalItemId = exist.Id };
                this._repository.Add<DeliveryItemDimensionUnitDAL>(dimUnit);
                this._repository.Save();
                if (dimUnit == null) { throw new NullReferenceException(); }
            }

            var order = new OrderItemDAL() { Name = "New order" };
            this._repository.Add<OrderItemDAL>(order);
            this._repository.Save();
            if (order == null) { throw new NullReferenceException(); }

            var orderDelivery = new OrdersDeliveryItemsDAL() { OrderId = order.Id, DeliveryId = itemToAdd.Id };
            this._repository.Add<OrdersDeliveryItemsDAL>(orderDelivery);
            this._repository.Save();
            if (orderDelivery == null) { throw new NullReferenceException(); }

            var adressFrom = this._repository.GetAll<AdressDAL>(s => s.Name == query.AdressFrom)
                .FirstOrDefault();
            var adressTo = this._repository.GetAll<AdressDAL>(s => s.Name == query.AdressTo)
                .FirstOrDefault();

            var orderAdress = new OrdersAdresses()
            {
                AddressFromId = adressFrom.Id,
                AddressToId = adressTo.Id,
                OrderId = order.Id
            };
            this._repository.Add<OrdersAdresses>(orderAdress);
            this._repository.Save();
            if (orderAdress == null) { throw new NullReferenceException(); }


            order.DaysToDelivery = 10F;
            order.DeliveryPrice = 10F;
            this._repository.Add<OrderItemDAL>(order);
            this._repository.Save();

            result = this._mapper.Map<OrderItemDAL, OrderItemAPI>(order);
            if (result == null) { throw new NullReferenceException(); }


        }
        catch (Exception e)
        {

        }
        return result;
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
