using System;
using System.Text;
using Telegram;
using DevBy;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IStorage storage = new JsonEventStorage();
            TelegramBot bot = new TelegramBot();
            EventViews viewer = new EventViews();
            TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot,ref  viewer);
            Thread telegramChecker = new Thread(telegramBot.Run);
            telegramChecker.Start();


            
        }
        
    }
}
