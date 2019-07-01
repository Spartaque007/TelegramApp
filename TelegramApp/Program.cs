using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.StorageClasses;
using TelegramApp.Logers;
using System.Net;
using System.Configuration;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ILogger logger = new ConsoleLoger();
            TelegramBot bot = new TelegramBot();
            string connectionString = ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString;
            IStorage storage = new DapStorageDB(logger,connectionString);
            EventViews viewer = new EventViews();
            try
            {
                TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot, ref viewer, ref logger);
                Thread telegramChecker = new Thread(telegramBot.Run);
                telegramChecker.Start();
            }
            catch (AggregateException ex)
            {
                foreach (var x in ex.InnerExceptions)
                {
                    logger.WriteLog(x.Message);
                    bot.SendMessageMe(x.Message);
                }
                Console.ReadKey();
            }
        }

    }
}
