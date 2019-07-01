using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp.Commands
{
    class EmptyCommand : ICommand
    {
        public void ExecuteCommand(Result result, ref IStorage storage, ref TelegramBot bot, ref EventViews views)
        {
            
        }
    }

}
