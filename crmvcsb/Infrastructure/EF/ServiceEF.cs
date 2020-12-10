
namespace crmvcsb.Infrastructure.EF
{
    using AutoMapper;    
    using crmvcsb.Universal;
    using crmvcsb.Universal.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    public class ServiceEF : IServiceEF
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

        public string GetConnectionString()
        {
            return _repository.GetConnectionString();
        }

        public DatabaseFacade GetDatabase()
        {
            return _repository.GetDatabase();
        }

        public DbContext GetEFContext()
        {
            return _repository.GetEFContext();
        }
    }
}
