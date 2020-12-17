using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Universal.Infrastructure
{

    public interface IValidatorCustom
    {
        void Validate<T>(T instance);
    }
}