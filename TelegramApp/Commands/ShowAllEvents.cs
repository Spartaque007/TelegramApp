using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramApp.Dependency;

namespace TelegramApp.Commands
{
    class ShowAllEvents : ICommand
    {
        public void ExecuteCommand()
        {
            Console.WriteLine("All Events");
        }
    }
}
