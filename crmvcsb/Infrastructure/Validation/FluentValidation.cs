using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace crmvcsbs.Infrastructure.Validation
{

    using FluentValidation;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;
    public class CurrenciesValidation
        : AbstractValidator<CurrencyAPI>, IValidator<CurrencyAPI>
    {
        public CurrenciesValidation()
        {
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
        }
    }
}
