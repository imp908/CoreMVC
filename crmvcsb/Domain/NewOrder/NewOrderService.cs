
namespace crmvcsb.Domain.NewOrder
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Domain.Currency.DAL;
    using crmvcsb.Domain.Currency.API;
    using crmvcsb.Domain.NewOrder.DAL;
    using crmvcsb.Infrastructure.EF;

    public class NewOrderService : ServiceEF, INewOrderService
    {
        IRepositoryEF _repository;
        IMapper _mapper;

        public NewOrderService(IRepositoryEF repository, IMapper mapper) 
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
     

        public void ReInitialize()
        {
            
            _repository.GetDatabase().EnsureDeleted();
            _repository.GetDatabase().EnsureCreated();

            List<AddressDAL> addresses = new List<AddressDAL>();

            for (int i = 0; i < 10; i++)
            {
                addresses.Add(new AddressDAL() { Id = i + 1, StreetName = $"test street {i}", Code = i + 1 });
            };

            _repository.AddRange(addresses);

            try
            {
                _repository.SaveIdentity("Adresses");
            }
            catch (Exception e)
            {

            }

          

            
        }
        public void CleanUp()
        {
            _repository.GetDatabase().EnsureCreated();
            var addressesExist = _repository.QueryByFilter<AddressDAL>(s => s.Id != 0).ToList();
            _repository.DeleteRange(addressesExist);
            try { _repository.Save(); } catch (Exception e) { throw; }

           
        }
    
    }
}
