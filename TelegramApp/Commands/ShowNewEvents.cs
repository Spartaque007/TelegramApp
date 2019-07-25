using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
   public class ShowNewEvents : ICommand
    {
        public void ExecuteCommand(Result result, IStorage storage, TelegramBot bot, EventViews viewer)
        {
            DevByParser parser = new DevByParser();
            List<Event> currEvents = parser.GetEvents(2);
            storage.SaveNewEventsToStorage(currEvents);
            List<Event> newEvents = storage.GetNewEventsFromStorageForUser(result.message.from.ID);
            storage.SaveUserCheckDate(result.message.from.ID);
            if (newEvents.Capacity > 0)
            {
                foreach (var sub in newEvents)
                {
                    bot.SendMessageMDCustom(viewer.ToMdFormat(sub), result.message.chat.Id);
                }
            }
            else
            {
                bot.SendMessageMDCustom($"*No new events for you, {result.message.from.first_name}!*", result.message.chat.Id);
            }
        }
    }
}
