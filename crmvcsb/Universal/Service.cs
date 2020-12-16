
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
     
        IRepository _repositoryRead;
        IRepository _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;
        
        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper, IValidatorCustom validator)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
        }
        public Service(IRepository repositoryRead, IRepository repositoryWrite)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
        }
        public Service(IRepository repositoryWrite)
        {
            _repositoryWrite = repositoryWrite;
        }


        public virtual void ReInitialize()
        {
            _repositoryWrite.ReInitialize();
        }

        public virtual void CleanUp()
        {
            _repositoryWrite.CleanUp();
        }

        public virtual string GetConnectionString()
        {
            return _repositoryRead.GetConnectionString();
        }
        public string GetDatabaseName()
        {
            return _repositoryRead.GetDatabaseName();
        }
    }
}
