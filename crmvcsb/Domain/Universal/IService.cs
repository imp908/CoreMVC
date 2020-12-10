
namespace crmvcsb.Domain.Universal
{
  
    public interface IService 
    {

        string GetDbName();

        void ReInitialize();

        void CleanUp();        

    }

}
