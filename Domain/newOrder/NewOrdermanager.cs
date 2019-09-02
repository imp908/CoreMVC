
namespace crmvcsb.Domain.newOrder
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

    public class NewOrdermanager : INewOrdermanager
    {
        IRepository _repository;
        IMapper _mapper;

        public NewOrdermanager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(GetCurrencyCommand command)
        {

            List<CurrencyRatesDAL> r = await _repository
                .GetAll<CurrencyRatesDAL>()
                .Where(s => s.CurrencyFrom.IsoCode.ToLower() == command.IsoCode.ToLower() 
                || s.CurrencyTo.IsoCode.ToLower() == command.IsoCode.ToLower())
                .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                .ToListAsync();

            var result = _mapper.Map<IList<CurrencyRatesDAL>, IList<CrossCurrenciesAPI>>(r);

            return result.Cast<ICrossCurrenciesAPI>().ToList();
        }
    }
}