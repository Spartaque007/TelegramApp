using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    public class ShowAllEvents : ICommand
    {
        public void ExecuteCommand(Result result, IStorage storage,  TelegramBot bot,  EventViews viewer)
        {
            DevByParser parser = new DevByParser();
            List<Event> currEvents = parser.GetEvents(2);
            storage.SaveNewEventsToStorage(currEvents);
            storage.SaveUserCheckDate(result.message.from.ID);
            foreach (var sub in currEvents)
            {
                bot.SendMessageMDCustom(viewer.ToMdFormat(sub),result.message.chat.Id);
            }
        }
    }
}
