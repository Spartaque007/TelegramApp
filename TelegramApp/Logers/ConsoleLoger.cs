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
        public ConsoleLoger()
        {
            Console.OutputEncoding = Encoding.UTF8;

        }
        
        public void WriteLog(string logText)
        {
            Console.WriteLine(logText);
        }
    }
}
