using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;

namespace TelegramApp.Dependency
{
    public interface ICommand
    {
        void ExecuteCommand(Result result);

    }
}
