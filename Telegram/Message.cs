using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{
    public class Message
    {
        public int message_id { get; set; }
        
        public Sender from = new Sender();
        public Chat chat = new Chat();
        public int date { get; set; }
        public string text { get; set; }


        
        

    }
}
