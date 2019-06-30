using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.StorageClasses;
using TelegramApp.Logers;
using System.Net;


namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ILoger loger = new ConsoleLoger();
            TelegramBot bot = new TelegramBot();
            IStorage storage = new DapStorageDB(ref loger);
            EventViews viewer = new EventViews();
            try
            {
                TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot, ref viewer, ref loger);
                Thread telegramChecker = new Thread(telegramBot.Run);
                telegramChecker.Start();
            }
            catch (AggregateException ex)
            {
                foreach (var x in ex.InnerExceptions)
                {
                    loger.WriteLog(x.Message);
                    bot.SendMessageMe(x.Message);
                }
                Console.ReadKey();
            }
        }

    }
}
