
namespace crmvcsb.Universal
{
    using AutoMapper;    
    using crmvcsb.Universal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Service : IService
    {

        IRepository _repository;
        IMapper _mapper;
        
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
        

      
    }
}
