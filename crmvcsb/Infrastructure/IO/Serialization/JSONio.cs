using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using crmvcsb.Infrastructure.IO.Serialization;
using Newtonsoft.Json;

namespace crmvcsb.Infrastructure.IO.Serialization
{
    public class JSONio : ISerialization
    {
        public object DeSerialize(string input)
        {
            return JsonConvert.DeserializeObject(input);
        }

        public string Serialize(object input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public T DeSerialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }
    }
}
