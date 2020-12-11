
namespace crmvcsb.Infrastructure.EF.NewOrder
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Universal.DomainSpecific.Currency.DAL;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.NewOrder;
    using crmvcsb.Universal.DomainSpecific.NewOrder.DAL;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal;

    public class NewOrderServiceEF : Service, INewOrderService
    {
        IRepositoryEF _repository;
        IMapper _mapper;

        public NewOrderServiceEF(IRepositoryEF repository, IMapper mapper) 
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
     

        public override void ReInitialize()
        {

            _repository.ReInitialize();

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
        public override void CleanUp()
        {
            _repository.CleanUp();
            var addressesExist = _repository.QueryByFilter<AddressDAL>(s => s.Id != 0).ToList();
            _repository.DeleteRange(addressesExist);
            try { _repository.Save(); } catch (Exception e) { throw; }
           
        }
    
    }
}
