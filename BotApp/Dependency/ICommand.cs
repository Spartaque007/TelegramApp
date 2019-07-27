using Telegram;
using TelegramApp.Views;

namespace TelegramApp.Dependency
{
    public interface ICommand
    {
        void ExecuteCommand();
    }
}
