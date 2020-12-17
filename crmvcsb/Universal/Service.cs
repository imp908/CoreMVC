
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
        ILoggerCustom _logger;

        public string actualStatus { get; set; }

        public Service(IRepository repositoryRead, IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }
        public Service( IRepository repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
        {
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
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
