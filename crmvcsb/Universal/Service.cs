
namespace crmvcsb.Universal
{
    using AutoMapper;
    using crmvcsb.Universal.Infrastructure;

    public class Service : IService
    {
     
        IRepository _repositoryRead;
        IRepository _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;
        
        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
        public Service( IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null)
        {
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
        }
       


        public IRepository GetRepositoryRead()
        {
            return this._repositoryRead;
        }
        public IRepository GetRepositoryWrite()
        {
            return this._repositoryWrite;
        }
    }
}
