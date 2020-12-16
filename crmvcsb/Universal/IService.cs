
namespace crmvcsb.Universal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IService 
    {     

        void ReInitialize();
        void CleanUp();
        string GetConnectionString();
        string GetDatabaseName();

    }

}
