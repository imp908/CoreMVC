
namespace crmvcsb.Domain.NewOrder
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

    public class NewOrderManager : INewOrderManager
    {
        IRepository _repository;
        IMapper _mapper;

        public NewOrderManager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(GetCurrencyCommand command) 
        {

            IList<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();

            if(command != null && command.FromCurrency != null) {

                CurrencyRatesDAL firstPair = await _repository
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if(firstPair == null) { throw new System.Exception("No from currency pair found");}


                CurrencyRatesDAL secondPair = await _repository
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (secondPair == null) { throw new System.Exception("No to currency pair found");}


                var rate = firstPair.Rate / secondPair.Rate;

                result.Add(new CrossCurrenciesAPI() { From = command.FromCurrency, To = command.ToCurrency, Throught = command.ThroughCurrency, Rate = rate });
            }

            return result.Cast<ICrossCurrenciesAPI>().ToList();
        }
    }
}