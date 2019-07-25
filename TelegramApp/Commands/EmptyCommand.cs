using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    public class EmptyCommand : ICommand
    {
        public void ExecuteCommand(Result result, IStorage storage, TelegramBot bot, EventViews views)
        {
            
        }
    }

}
