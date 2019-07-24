namespace crmvcsb.Infrastructure.EF.newOrder
{
    using System.Linq;
    using System.Linq.Expressions;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Infrastructure.EF.newOrder;
    using crmvcsb.Domain.NewOrder.DAL;
    using crmvcsb.Domain.NewOrder.API;
    using crmvcsb.Domain.Interfaces;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Collections.Generic;

    public class NewOrdermanager
    {
        IRepository _repository;
        IMapper _mapper;

        public NewOrdermanager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<CrossCurrenciesAPI>> GetCurrencyCrossRates(string isoCode) {
            
            var result = await _repository.QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == isoCode.ToLower())
            .Select(c => new CrossCurrenciesAPI () {From = c.CurrencyFrom.IsoCode, To = c.CurrencyTo.IsoCode, Rate = c.Rate }).ToListAsync();

            if(result?.Any() == true) {
                return result;
            }
            
            return some code
        }
    }
}