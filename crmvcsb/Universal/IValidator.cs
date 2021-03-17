namespace crmvcsb.Universal
{

    public interface IValidatorCustom
    {
        bool isValid<T>(T instance);
    }
}