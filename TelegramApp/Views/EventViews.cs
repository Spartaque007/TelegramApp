﻿using DevBy;

namespace TelegramApp.Views
{
    public class EventViews
    {
        public string ToMdFormat(Event @event)
        {
            return $@"*{@event.EverntDate}*
[{@event.EventName}]({@event.EventURL})";
        }
    }
}
