
namespace crmvcsb.Universal
{
    using AutoMapper;    
    using crmvcsb.Universal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class Service : IService
    {

        IRepository _repository;
        IMapper _mapper;
        IValidatorCustom _validator;

        public Service(IRepository repository, IMapper mapper, IValidatorCustom validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public Service(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Service(IRepository repository)
        {
            _repository = repository;
        }
       
        public virtual void ReInitialize()
        {
            _repository.ReInitialize();
        }

        public virtual IQueryable<T> GetAll<T>(Expression<Func<T, bool>> expression = null) where T : class
        {
            return _repository.GetAll<T>(expression);
        }

        public virtual void Add<T>(T item) where T : class
        {
            _repository.Add<T>(item);
        }

        public virtual Task AddRangeAsync<T>(IList<T> items) where T : class
        {
            return _repository.AddRangeAsync<T>(items);
        }

        public virtual Task<int> SaveAsync()
        {
            return _repository.SaveAsync();
        }

        public virtual void AddRange<T>(IList<T> items) where T : class
        {
            _repository.AddRange<T>(items);
        }

        public virtual void Delete<T>(T item) where T : class
        {
            _repository.Delete<T>(item);
        }

        public virtual void DeleteRange<T>(IList<T> items) where T : class
        {
            _repository.DeleteRange<T>(items);
        }

        public virtual void Update<T>(T item) where T : class
        {
            _repository.Update<T>(item);
        }

        public virtual void UpdateRange<T>(IList<T> items) where T : class
        {
            _repository.UpdateRange<T>(items);
        }

        public virtual IQueryable<T> SkipTake<T>(int skip, int take) where T : class
        {
            return _repository.SkipTake<T>(skip,take);
        }

        public virtual IQueryable<T> QueryByFilter<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _repository.QueryByFilter<T>(expression);
        }

        public virtual void Save()
        {
            _repository.Save();
        }

        public virtual void CleanUp()
        {
            _repository.CleanUp();
        }

        public virtual string GetConnectionString()
        {
            return _repository.GetConnectionString();
        }
        public string GetDatabaseName()
        {
            return _repository.GetDatabaseName();
        }
    }
}
