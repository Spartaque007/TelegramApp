using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Views;

namespace TelegramApp.Dependency
{
    public interface ICommand
    {
        void ExecuteCommand(Result result, ref IStorage storage, ref TelegramBot bot, ref EventViews views);

    }
}
