using System;
using System.Text;
using Telegram;
using DevBy;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.StorageClasses;
using TelegramApp.Logers;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = Encoding.UTF8;
            //IStorage storage = new JsonStorage();
            //TelegramBot bot = new TelegramBot();
            //EventViews viewer = new EventViews();
            //TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot,ref  viewer);
            //Thread telegramChecker = new Thread(telegramBot.Run);
            //telegramChecker.Start();
            ILoger loger = new ConsoleLoger();
            DapStorageDB storage = new DapStorageDB(ref loger);
            storage.CheckDatabase("TelegramApp");
            Console.Beep();
            Console.ReadKey();
            
        }
        
    }
}
