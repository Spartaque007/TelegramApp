using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    class ShowNewEvents : ICommand
    {
        private readonly Result result;
        private readonly IStorage storage;
        private readonly TelegramBot bot;
        private readonly EventViews viewer;
        private readonly DevByParser parser;
        public ShowNewEvents(Result result, IStorage storage, TelegramBot bot, EventViews viewer, DevByParser parser)
        {
            this.result = result;
            this.parser= parser;
            this.storage = storage;
            this.viewer = viewer;
            this.bot = bot;
        }
        public void ExecuteCommand()
        {
          
            List<Event> currEvents = parser.GetEvents(2);
            storage.SaveNewEventsToStorage(currEvents);
            List<Event> newEvents = storage.GetNewEventsFromStorageForUser(result.message.from.ID.ToString());
            storage.SaveUserCheckDate(result.message.from.ID.ToString());
            if (newEvents.Capacity > 0)
            {
                foreach (var sub in newEvents)
                {
                    bot.SendMessageMD(viewer.ToMdFormat(sub), result.message.chat.Id);
                }
            }
            else
            {
                bot.SendMessageMD($"*No new events for you, {result.message.from.first_name}!*", result.message.chat.Id);
            }
        }
    }
}
