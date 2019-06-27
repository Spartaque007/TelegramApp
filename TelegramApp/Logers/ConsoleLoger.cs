using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramApp.Dependency;

namespace TelegramApp.Logers
{
    class ConsoleLoger : ILoger
    {
        public void WriteLog(string logText)
        {
            Console.WriteLine(logText);
        }
    }
}
