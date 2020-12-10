
namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;    
    using crmvcsb.Domain.Universal;
    public class ServiceEF : IService
    {

        IRepositoryEF _repository;
        IMapper _mapper;

        public ServiceEF(IRepositoryEF repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ServiceEF(IRepositoryEF repository)
        {
            _repository = repository;
        }

        public string GetDbName()
        {
            return this._repository.GetConnectionString();
        }

        public virtual void ReInitialize()
        {
            _repository.GetDatabase().EnsureDeleted();
            _repository.GetDatabase().EnsureCreated();
        }

        public virtual void CleanUp()
        {
            _repository.GetDatabase().EnsureDeleted();
            _repository.GetDatabase().EnsureCreated();
        }
    }
}
