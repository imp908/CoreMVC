
namespace crmvcsb.Infrastructure.EF.Blogging
{
    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Domain.Universal.IRepository;
    using crmvcsb.Infrastructure.EF;
    
    using AutoMapper;
    public class BloggingRepository : RepositoryEF, IRepositoryEF, IRepository
    {
        public IRepositoryEF _repository { get; set; }
        public IMapper _mapper { get; set; }
        public BloggingRepository(DbContext context, IRepositoryEF repository, IMapper mapper) : base(context)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
    }

}

