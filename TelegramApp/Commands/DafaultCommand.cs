using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Dependency;

namespace TelegramApp.Commands
{
    public class DafaultCommand : ICommand
    {
        public void ExecuteCommand(Result result)
        {
            Console.WriteLine("DefaultCommand");
        }
    }
}
