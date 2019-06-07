using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{
    
    public class TelegramResponse
    {
        public bool Ok { get; set; }
        public IList<Result> result = new List<Result>();
        
    }
}
