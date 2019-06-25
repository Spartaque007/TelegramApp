using DevBy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramApp.Views
{
    public class EventViews
    {
        public string ToMdFormat(Event @event)
        {
            return $@"[{@event.ToString()}] ({@event.EventURL})";
        }
    }
}
