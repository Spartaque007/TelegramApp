using Telegram;
using TelegramApp.Views;

namespace TelegramApp.Dependency
{
    public interface ICommand
    {
        void ExecuteCommand(Result result, IStorage storage, TelegramBot bot, EventViews views);
    }
}
