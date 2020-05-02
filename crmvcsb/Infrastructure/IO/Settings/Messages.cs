using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace crmvcsb.Infrastructure.IO.Settings
{
    using Microsoft.AspNetCore.Hosting;
    using crmvcsb.Infrastructure.IO.Serialization;

    /// <summary>
    /// Contains class hierarhy for varialbes JSON serialization, deserialization
    /// and property chanining usage
    /// </summary>
    public class _variables
    {
        public _messages Messages = new _messages();
        public class _messages
        {
            public _serviceMessages SrviceMessages = new _serviceMessages();
            public class _serviceMessages
            {
                public string TestMessage;
                public string ServiceIsNull;
            }
        }
    }

    /// <summary>
    /// Class for default variable values initialization and saving loading json file
    /// </summary>
    public class MessagesInitialization
    {
        public static MessagesInitialization _this;
        static JSONio serialization = new JSONio();
        public static _variables Variables = new _variables();
        string path = $"{Directory.GetCurrentDirectory()}\\variables.json";
        public static void Init()
        {
            _this = new MessagesInitialization();
        }
        public MessagesInitialization()
        {
            
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                Variables = serialization.DeSerialize<_variables>(text);
            }
            else 
            {
                init();
                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\variables.json", serialization.Serialize(Variables));
            }            
        }

        void init()
        {
            Variables.Messages.SrviceMessages.TestMessage = @"inited test message";
            Variables.Messages.SrviceMessages.ServiceIsNull = @"service is null";
        }
    }

}
