namespace crmvcsb.Universal
{

    public interface IValidatorCustom
    {
        void Validate<T>(T instance);
    }
}