
namespace crmvcsb.Domain.Universal
{
    public interface IServiceEF
    {
        string GetDbName();
    
    }

    public interface IService : IServiceEF
    {

        void ReInitialize();

        void CleanUp();
    }

}
