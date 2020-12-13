using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace crmvcsbs.Infrastructure.Validation
{

    using FluentValidation;
    using crmvcsb.Universal;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;

    public class CurrenciesValidation
        : AbstractValidator<ICurrencyAPI>
    {
        public CurrenciesValidation()
        {
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
        }
    }

    public class ValidatorCustom : IValidatorCustom
    {
        CurrenciesValidation cv = new CurrenciesValidation();
        public void Validate<T>(T item)
        {            
            switch (item)
            {
                case CurrencyAPI a:
                    cv.Validate(a);
                break;
            }
            
        }
    }
}
