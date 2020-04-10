using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace mvccoresb.Infrastructure.IO.Settings
{
    using Microsoft.AspNetCore.Hosting;
    using mvccoresb.Infrastructure.IO.Serialization;

    /// <summary>
    /// Contains class hierarhy for varialbes JSOn serialization, deserialization
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
        }
    }

}
