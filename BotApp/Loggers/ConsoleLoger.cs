using System;
using System.Text;
using TelegramApp.Dependency;

namespace TelegramApp.Logers
{
    class ConsoleLoger : ILogger
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
