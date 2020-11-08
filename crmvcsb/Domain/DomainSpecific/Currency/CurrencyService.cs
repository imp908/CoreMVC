

namespace crmvcsb.Domain.DomainSpecific.Currency
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Domain.DomainSpecific.Currency.DAL;
    using crmvcsb.Domain.DomainSpecific.Currency.API;
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

        public CurrencyService(IRepositoryEF repository)
        : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
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
  
        public void ReInitialize()
        {

            _repository.GetDatabase().EnsureDeleted();
            _repository.GetDatabase().EnsureCreated();

            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 1, Name = "USD", IsoCode = "USD" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 2, Name = "EUR", IsoCode = "EUR" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 3, Name = "GBP", IsoCode = "GBP" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 4, Name = "RUB", IsoCode = "RUB" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 5, Name = "JPY", IsoCode = "JPY" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 6, Name = "AUD", IsoCode = "AUD" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 7, Name = "CAD", IsoCode = "CAD" });
            _repository.Add<CurrencyDAL>(new CurrencyDAL() { Id = 8, Name = "CHF", IsoCode = "CHF" });
            try { _repository.SaveIdentity<CurrencyDAL>(); }
            catch (Exception e)
            {
                throw;
            }

            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) });
            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) });
            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) });

            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 7, CurrencyFromId = 2, CurrencyToId = 5, Rate = 85.2M, Date = new DateTime(2019, 07, 23) });
            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 8, CurrencyFromId = 3, CurrencyToId = 5, Rate = 95.2M, Date = new DateTime(2019, 07, 23) });

            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 9, CurrencyFromId = 2, CurrencyToId = 6, Rate = 15M, Date = new DateTime(2019, 07, 23) });
            _repository.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 6, CurrencyToId = 3, Rate = 0.25M, Date = new DateTime(2019, 07, 23) });

            try { _repository.SaveIdentity<CurrencyRatesDAL>(); }
            catch (Exception e)
            {
                throw;
            }
        }
        public void CleanUp() 
        {
            _repository.GetDatabase().EnsureCreated();
            _repository.DeleteRange(_repository.GetAll<CurrencyRatesDAL>().ToList());
            _repository.DeleteRange(_repository.GetAll<CurrencyDAL>().ToList());
            try { _repository.Save(); } catch (Exception e) { throw; }
        }

    }
}
