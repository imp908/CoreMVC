

namespace crmvcsb.Domain.TestModels
{
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Domain.NewOrder.DAL;
    using crmvcsb.Domain.NewOrder.API;
    using crmvcsb.Domain.Interfaces;
    using crmvcsb.Domain.NewOrder;

    using Autofac.Features.AttributeFilters;

    public class TestManager : ITestManager
    {
        IRepository _repository;
        IMapper _mapper;

        public TestManager([KeyFilter("TestContext")] IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

    }
}
