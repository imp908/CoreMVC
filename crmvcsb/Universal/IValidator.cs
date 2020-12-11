using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Universal
{

    public interface IValidator<T>
    {
        dynamic Validate(T instance);
    }
}