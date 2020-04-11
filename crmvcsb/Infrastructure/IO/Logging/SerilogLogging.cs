using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Serilog;

namespace crmvcsb.Infrastructure.IO.Logging
{
    public class SerilogLogging : ILogger
    {
        Serilog.Core.Logger log;
        public SerilogLogging()
        {
            this.log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        }

        public void Information(string input)
        {
            this.log.Information(input);
        }
    }
}
