
namespace crmvcsb.Infrastructure.EF.Blogging
{
    using AutoMapper;
  
    using Microsoft.EntityFrameworkCore;

    using crmvcsb.Domain.Universal.IRepository;
    using crmvcsb.Infrastructure.EF;

    public class BloggingRepository : RepositoryEF, IRepositoryEF, IRepository
    {
        internal IMapper _mapper { get; set; }
        internal IRepositoryEF _repository { get; set; }
        public BloggingRepository(DbContext context, IRepositoryEF repository, IMapper mapper) : base(context)
        {

        }
    }
}
