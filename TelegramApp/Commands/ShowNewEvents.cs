using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Dependency;

namespace TelegramApp.Commands
{
    class ShowNewEvents : ICommand
    {
        public void ExecuteCommand(Result result)
        {
            Console.WriteLine("Execute command ShowNewEvent");
        }
    }
}
