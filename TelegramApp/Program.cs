using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.StorageClasses;
using TelegramApp.Logers;
using System.Net;
using System.Configuration;
using TelegramApp.Storages.DdModels;


namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
        //    Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    ILogger logger = new ConsoleLoger();
        //    TelegramBot bot = new TelegramBot();
        //    string connectionString = ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString;
        //    IStorage storage = new DapStorageDB(logger,connectionString);
        //    EventViews viewer = new EventViews();
        //    try
        //    {
        //        TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot, ref viewer, ref logger);
        //        Thread telegramChecker = new Thread(telegramBot.Run);
        //        telegramChecker.Start();
        //    }
        //    catch (AggregateException ex)
        //    {
        //        foreach (var x in ex.InnerExceptions)
        //        {
        //            logger.WriteLog(x.Message);
        //            bot.SendMessageMe(x.Message);
        //        }
        //        Console.ReadKey();
        //    }
         using (TelegramContext context =new TelegramContext())
            {
                 context.Users.Add(new User { UserID = 222, LastUpdate = DateTime.Now });
                 context.SaveChanges();
                var f = context.Users;
                foreach (var item in f)
                {
                    Console.WriteLine(item.UserID);
                }
                Console.WriteLine("Done");
                Console.ReadKey();
            }

        }

    }
}
