using Telegram;
using TelegramApp.Dependency;


namespace TelegramApp.Commands
{
    public class DefaultCommand : ICommand
    {
        private readonly TelegramBot bot;
        private readonly Result result;
        public DefaultCommand(Result result, TelegramBot bot)
        {
            this.bot = bot;
            this.result = result;
        }
        public void ExecuteCommand()
        {
            string message = $"Dear {result.message.from.first_name}, you entered unknown command!\nPlease Chose command from list:\n" +
                $"/ShowEvents@JonnWickbot -show all events\n" +
                $"/ShowNewEvents@JonnWickbot- show only new events for you";
            bot.SendMessage(message, result.message.chat.Id);
        }
    }
}
