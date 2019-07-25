using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    public class DefaultCommand : ICommand
    {
        public void ExecuteCommand(Result result,  IStorage storage,  TelegramBot bot,  EventViews viewer)
        {
            string message = $"Dear {result.message.from.first_name}, you entered unknown command!\nPlease Chose command from list:\n" +
                $"/ShowEvents@JonnWickbot -show all events\n" +
                $"/ShowNewEvents@JonnWickbot- show only new events for you";
            bot.SendMessageCustom(message, result.message.chat.Id);
        }
    }
}
