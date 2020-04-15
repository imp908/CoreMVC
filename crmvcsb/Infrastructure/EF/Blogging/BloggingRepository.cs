
namespace crmvcsb.Infrastructure.EF.Blogging
{
    using AutoMapper;

    public class BloggingRepository
    {
        internal IRepositoryEF _repository;
        internal IMapper _mapper;

        public BloggingRepository(IRepositoryEF repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

    }
}
