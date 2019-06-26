using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    class ShowAllEvents : ICommand
    {
        public void ExecuteCommand(Result result,ref IStorage storage, ref TelegramBot bot, ref EventViews viewer)
        {
            DevByParser parser = new DevByParser();
            List<Event> currEvents = parser.GetEvents(2);
            EventViews d = new EventViews();
            foreach (var sub in currEvents)
            {
                bot.SendMessageMeMD(d.ToMdFormat(sub));
            }

        }
    }
}
