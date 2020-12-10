using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmvcsb.Domain.Infrastructure
{
    public interface ISerialization
    {
        string Serialize(Object input);
        Object DeSerialize(string input);

        string Serialize<T>(T item);
        T DeSerialize<T>(string input);
    }
}
