using Telegram;
using TelegramApp.Views;

namespace TelegramApp.Dependency
{
    public interface ICommand
    {
        void ExecuteCommand(Result result, ref IStorage storage, ref TelegramBot bot, ref EventViews views);
    }
}
