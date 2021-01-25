namespace crmvcsb.Universal.Infrastructure
{

    public interface IValidatorCustom
    {
        void Validate<T>(T instance);
    }
}