using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{ 
    [JsonObject("from")]
    public class Sender
    {
        public int ID { get; set; }
        public bool Is_bot { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public override string ToString() => $"Sender ID: {ID}" + $"User name {username}" + (Is_bot ? " is Bot" : " is human");
     
    }
}
