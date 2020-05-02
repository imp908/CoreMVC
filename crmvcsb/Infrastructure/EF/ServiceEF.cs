
namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;    
    using crmvcsb.Domain.Universal;
    public class ServiceEF : IServiceEF
    {

        IRepositoryEF _repository;
        IMapper _mapper;

        public ServiceEF(IRepositoryEF repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public string GetDbName()
        {
            return this._repository.GetConnectionString();
        }
       
    }
}
