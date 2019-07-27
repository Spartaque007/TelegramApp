using System.Collections.Generic;

namespace Telegram
{
    public class TelegramResponse
    {
        public bool Ok { get; set; }
        public IList<Result> result = new List<Result>();
    }
}
