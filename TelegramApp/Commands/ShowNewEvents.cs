using DevBy;
using System.Collections.Generic;
using System.Linq;
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
            List<Event> prevrEvents = storage.GetEventsFromStorageForUser(result.message.from.ID.ToString()) ?? new List<Event>();
            List<Event> newEvents = currEvents.Except(prevrEvents).ToList<Event>();

            if (newEvents.Capacity > 0)
            {
                foreach (var sub in newEvents)
                {
                    bot.SendMessageMDCustom(viewer.ToMdFormat(sub), result.message.chat.Id);
                }
                storage.SaveEventsToStorage(result.message.from.ID.ToString(), currEvents);
            }
            else
            {
                bot.SendMessageMDCustom($"*No new events for you, {result.message.from.first_name}!*", result.message.chat.Id);
            }

        }
    }
}
