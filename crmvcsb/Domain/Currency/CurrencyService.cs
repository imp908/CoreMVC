

namespace crmvcsb.Domain.Currency
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Domain.Currency.DAL;
    using crmvcsb.Domain.Currency.API;
    using crmvcsb.Infrastructure.EF;

    public class CurrencyService : ServiceEF, ICurrencyService
    {
        IRepositoryEF _repository;
        IMapper _mapper;

        public CurrencyService(IRepositoryEF repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRates(IGetCurrencyCommand command)
        {

            IList<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();

            if (command != null && command.FromCurrency != null)
            {

                decimal fromRate;
                decimal toRate;

                var pairFrom = await _repository
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairFrom == null)
                {
                    var pairFromReveresd = await _repository
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyTo.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyFrom.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                    if (pairFromReveresd == null) { throw new System.Exception("No from currency pair found"); }

                    fromRate = 1 / pairFromReveresd.Rate;
                }
                else
                {
                    fromRate = pairFrom.Rate;
                }


                var pairTo = await _repository
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairTo == null)
                {

                    var pairToReversed = await _repository
                        .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                        && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                        .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                        .FirstOrDefaultAsync();

                    if (pairToReversed == null) { throw new System.Exception("No to currency pair found"); }

                    toRate = 1 / pairToReversed.Rate;
                }
                else
                {
                    toRate = pairTo.Rate;
                }


                var rate = fromRate / toRate;

                result.Add(new CrossCurrenciesAPI() { From = command.FromCurrency, To = command.ToCurrency, Throught = command.ThroughCurrency, Rate = rate });
            }

            return result.Cast<ICrossCurrenciesAPI>().ToList();
        }
  
        public void ReInitialize(){ }
        public void CleanUp() { }

    }
}
