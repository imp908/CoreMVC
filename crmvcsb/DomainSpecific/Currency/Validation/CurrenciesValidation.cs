

namespace crmvcsb.DomainSpecific.Validation
{

    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.Infrastructure;
    using FluentValidation;

    public class CurrenciesValidation
        : AbstractValidator<ICurrencyAPI>
    {
        public CurrenciesValidation()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
            RuleFor(c => c.IsMain).NotNull().NotEmpty();
        }
    }

    public class CurrencyUpdatevalidation
        : AbstractValidator<ICurrencyUpdateAPI>
    {
        public CurrencyUpdatevalidation()
        {
            RuleFor(c => c.IsoCode).NotNull().NotEmpty();
        }
    }

    public class ValidatorCustom : IValidatorCustom
    {
        CurrenciesValidation cv = new CurrenciesValidation();
        CurrencyUpdatevalidation cvUpdate = new CurrencyUpdatevalidation();
        public void Validate<T>(T item)
        {
            switch (item)
            {
                case CurrencyUpdateAPI b:
                    cvUpdate.Validate(b);
                    break;
                case CurrencyAPI a:
                    cv.Validate(a);
                    break;
            }

        }
    }
}
