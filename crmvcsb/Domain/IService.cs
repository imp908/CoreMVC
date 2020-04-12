using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Domain
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
