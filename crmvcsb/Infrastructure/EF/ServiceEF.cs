

namespace crmvcsb.Infrastructure.EF
{
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System.Linq.Expressions;
    using AutoMapper;
    using crmvcsb.Universal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class ServiceEF : Service, IService, IServiceEF
    {

        IRepository _repository;
        IMapper _mapper;
        IValidatorCustom _validator;

        public ServiceEF(IRepository repository, IMapper mapper, IValidatorCustom validator)
            : base(repository, mapper, validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public ServiceEF(IRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ServiceEF(IRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

      
        public Task<EntityEntry<T>> AddAsync<T>(T item) where T : class
        {
            throw new NotImplementedException();
        }

      
        public DatabaseFacade GetDatabase()
        {
            throw new NotImplementedException();
        }

        public DbContext GetEFContext()
        {
            throw new NotImplementedException();
        }
       
        public void SaveIdentity(string command)
        {
            throw new NotImplementedException();
        }

        public void SaveIdentity<T>() where T : class
        {
            throw new NotImplementedException();
        }

      
    }
}
