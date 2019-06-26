using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    public class DafaultCommand : ICommand
    {
        public void ExecuteCommand(Result result, ref IStorage storage, ref TelegramBot bot, ref EventViews viewer)
        {
            string message = $"Dear {result.message.from.first_name}, you entered unknown command!\nPlease Chose command from list:\n" +
                $"/ShowEvents@JonnWickbot -show all events\n" +
                $"/ShowNewEvents@JonnWickbot- show only new events for you";
            bot.SendMessageCustom(message, result.message.chat.Id);
        }
    }
}
