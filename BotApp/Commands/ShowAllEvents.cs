using DevBy;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    class ShowAllEvents : ICommand
    {
        private readonly IStorage storage;
        private readonly EventViews viewer;
        private readonly TelegramBot bot;
        private readonly DevByParser parser;
        private readonly Result result;

        public ShowAllEvents(Result result,  IStorage storage, TelegramBot bot,EventViews viewer, DevByParser parser)
        {
            this.storage = storage;
            this.viewer = viewer;
            this.bot = bot;
            this.parser = parser;
            this.result = result;
        }
        public void ExecuteCommand()
        {
           
            List<Event> currEvents = parser.GetEvents(2);
            storage.SaveNewEventsToStorage(currEvents);
            storage.SaveUserCheckDate(result.message.from.ID.ToString());
            foreach (var sub in currEvents)
            {
                bot.SendMessageMD(viewer.ToMdFormat(sub),result.message.chat.Id);
            }
        }
    }
}
