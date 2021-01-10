

namespace crmvcsb.Infrastructure.EF.Currencies
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal;
    using crmvcsb.Universal.Infrastructure;

    using crmvcsb.Infrastructure.IO.Settings;

    public class CurrencyServiceEF : Service, ICurrencyServiceEF
    {
        IRepositoryEFRead _repositoryRead;
        IRepositoryEFWrite _repositoryWrite;
        IMapper _mapper;
        IValidatorCustom _validator;
        ILoggerCustom _logger;

        public CurrencyServiceEF(IRepositoryEFRead repositoryRead, IRepositoryEFWrite repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
           : base(repositoryRead, repositoryWrite, mapper, validator, logger)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }
        public CurrencyServiceEF(IRepositoryEFWrite repositoryWrite, IMapper mapper = null, IValidatorCustom validator = null, ILoggerCustom logger = null)
           : base(repositoryWrite, mapper, validator, logger)
        {
            _repositoryWrite = repositoryWrite;

            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }


        public async Task<ICurrencyAPI> AddCurrency(ICurrencyAPI currency)
        {
            _validator.Validate(currency);            

            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if (currencyExists != null) {
                this.actualStatus = MessagesComposite.EntityAllreadyExists(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                _logger.Information(this.actualStatus);
                return null;
            }

            var entityToAdd = _mapper.Map<ICurrencyAPI, CurrencyDAL>(currency);
            await _repositoryWrite.AddAsync<CurrencyDAL>(entityToAdd);
            await _repositoryWrite.SaveAsync();
            if (entityToAdd != null && entityToAdd.Id > 0) {
                this.actualStatus = MessagesComposite.EntitySuccessfullyCreated(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                _logger.Information(this.actualStatus);
            }

            var entityAdded = _mapper.Map<CurrencyDAL, ICurrencyAPI>(entityToAdd);            
            return entityAdded;
        }
        public string UpdateCurrency(ICurrencyAPI currency)
        {
            _validator.Validate(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if (currencyExists == null)
            {
                this.actualStatus = MessagesComposite.EntityNotFoundOnUpdate(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                _logger.Information(this.actualStatus);
                return this.actualStatus;
            }

            var currencyUpdate = (CurrencyUpdateAPI)currency;
            currencyExists = _mapper.Map<CurrencyDAL>(currencyUpdate);
            _repositoryWrite.Update<CurrencyDAL>(currencyExists);
            this.actualStatus = MessagesComposite.EntityModified(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return this.actualStatus;
        }
        public string DeleteCurrency(ICurrencyAPI currency)
        {
            _validator.Validate(currency);
            var currencyExists = _repositoryWrite.QueryByFilter<CurrencyDAL>(s => s.IsoCode == currency.IsoCode).FirstOrDefault();
            if(currencyExists == null)
            {
                this.actualStatus = MessagesComposite.EntityNotFoundOnDelete(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
                _logger.Information(this.actualStatus);
                return this.actualStatus;
            }

            _repositoryWrite.Delete<CurrencyDAL>(currencyExists);
            this.actualStatus = MessagesComposite.EntityDeleted(currency.GetType().Name, this._repositoryWrite.GetDatabaseName());
            return this.actualStatus;
        }

        public async Task<IList<ICrossCurrenciesAPI>> GetCurrencyCrossRatesAsync(IGetCurrencyCommand command)
        {

            IList<CrossCurrenciesAPI> result = new List<CrossCurrenciesAPI>();

            if (command != null && command.FromCurrency != null)
            {

                decimal fromRate;
                decimal toRate;

                var pairFrom = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.FromCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom)
                    .Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairFrom == null)
                {
                    var pairFromReveresd = await _repositoryWrite
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


                var pairTo = await _repositoryWrite
                    .QueryByFilter<CurrencyRatesDAL>(c => c.CurrencyFrom.IsoCode.ToLower() == command.ToCurrency.ToLower()
                    && c.CurrencyTo.IsoCode.ToLower() == command.ThroughCurrency.ToLower())
                    .Include(s => s.CurrencyFrom).Include(s => s.CurrencyTo)
                    .FirstOrDefaultAsync();

                if (pairTo == null)
                {

                    var pairToReversed = await _repositoryWrite
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

            _repositoryRead.ReInitialize();
            _repositoryWrite.ReInitialize();

            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 1, Name = "USD", IsoCode = "USD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 2, Name = "EUR", IsoCode = "EUR" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 3, Name = "GBP", IsoCode = "GBP" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 4, Name = "RUB", IsoCode = "RUB" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 5, Name = "JPY", IsoCode = "JPY" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 6, Name = "AUD", IsoCode = "AUD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 7, Name = "CAD", IsoCode = "CAD" });
            _repositoryWrite.Add<CurrencyDAL>(new CurrencyDAL() { Id = 8, Name = "CHF", IsoCode = "CHF" });
            try { _repositoryWrite.SaveIdentity< CurrencyDAL>(); }
            catch (Exception e)
            {
                throw;
            }

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 4, CurrencyFromId = 1, CurrencyToId = 4, Rate = 63.18M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 5, CurrencyFromId = 2, CurrencyToId = 4, Rate = 70.64M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 6, CurrencyFromId = 3, CurrencyToId = 4, Rate = 78.67M, Date = new DateTime(2019, 07, 23) });

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 7, CurrencyFromId = 2, CurrencyToId = 5, Rate = 85.2M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 8, CurrencyFromId = 3, CurrencyToId = 5, Rate = 95.2M, Date = new DateTime(2019, 07, 23) });

            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 9, CurrencyFromId = 2, CurrencyToId = 6, Rate = 15M, Date = new DateTime(2019, 07, 23) });
            _repositoryWrite.Add<CurrencyRatesDAL>(new CurrencyRatesDAL() { Id = 10, CurrencyFromId = 6, CurrencyToId = 3, Rate = 0.25M, Date = new DateTime(2019, 07, 23) });

            try { _repositoryWrite.SaveIdentity< CurrencyRatesDAL>(); }
            catch (Exception e)
            {
                throw;
            }
        }
        public void CleanUp() 
        {
            _repositoryWrite.ReInitialize();
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyRatesDAL>().ToList());
            _repositoryWrite.DeleteRange(_repositoryRead.GetAll<CurrencyDAL>().ToList());
            try { _repositoryWrite.Save(); } catch (Exception e) { throw; }
        }

    }
}
