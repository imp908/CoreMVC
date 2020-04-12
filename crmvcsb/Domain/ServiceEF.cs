

namespace crmvcsb.Domain
{
    using AutoMapper;
    using crmvcsb.Infrastructure.IRepositoryEF;
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
