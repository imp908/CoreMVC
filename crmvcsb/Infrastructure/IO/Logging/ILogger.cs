using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvccoresb.Infrastructure.IO.Logging
{
    public interface ILogger
    {
        void Information(string input);
    }
}
