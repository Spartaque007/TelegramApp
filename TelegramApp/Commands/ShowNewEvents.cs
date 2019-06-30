using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    class ShowNewEvents : ICommand
    {
        public void ExecuteCommand(Result result, ref IStorage storage, ref TelegramBot bot, ref EventViews viewer)
        {
            DevByParser parser = new DevByParser();
            List<Event> currEvents = parser.GetEvents(2);
            storage.SaveNewEventsToStorage(currEvents);
            List<Event> newEvents = storage.GetNewEventsFromStorageForUser(result.message.from.ID.ToString());
            storage.SaveUserCheckDate(result.message.from.ID.ToString());
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
