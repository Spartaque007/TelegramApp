using DevBy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Dependency;

namespace TelegramApp.Commands
{
    class ShowAllEvents : ICommand
    {
        public void ExecuteCommand(Result result)
        {
            TelegramBot bot = new TelegramBot();
            DevByParser parser = new DevByParser();
            List<Event> currEvents = parser.GetEvents(2);

            foreach (var sub in currEvents)
            {
                bot.SendMessageCustom(sub.ToString(), result.message.chat.Id);
            }

        }
    }
}
