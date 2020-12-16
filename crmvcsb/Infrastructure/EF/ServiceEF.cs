

namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;
    using crmvcsb.Universal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Threading.Tasks;

    public class ServiceEF : Service, IService, IServiceEF
    {
        IRepository _repositoryRead;
        IRepository _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;

        public ServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite, IMapper mapper, IValidatorCustom validator)
            : base(repositoryRead, repositoryWrite, mapper, validator)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
        public ServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite, IMapper mapper)
            : base(repositoryRead, repositoryWrite, mapper)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
        }
        public ServiceEF(IRepositoryEF repositoryRead, IRepositoryEF repositoryWrite)
             : base(repositoryRead, repositoryWrite)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
        }
        public ServiceEF(IRepositoryEF repositoryWrite)
            : base(repositoryWrite)
        {
            _repositoryWrite = repositoryWrite;
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
