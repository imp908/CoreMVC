

namespace crmvcsb.Domain.TestModels
{
 
    using AutoMapper;
   
    using crmvcsb.Domain.IRepository;
    public class TestManager : ITestManager
    {
        IRepository _repository;
        IMapper _mapper;

        public TestManager(IRepository repository, IMapper mapper)
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
