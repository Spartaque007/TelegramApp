using System;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.Logers;
using System.Net;
using Microsoft.Extensions.Configuration;
using DevBy;

namespace TelegramApp
{
    class Program
    {
        static void Main()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(@"D:\My Projects\TelegramApp\BotApp\conf.json")
                .Build();

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ILogger logger = new ConsoleLoger();
            TelegramBot bot = new TelegramBot(configuration.GetValue<string>("BotToken"));
            IStorage storage = new JsonStorage(configuration.GetValue<string>("DefaultDir"),
                configuration.GetValue<string>("EvetsFileName"),
                configuration.GetValue<string>("UsersFileName"),
                configuration.GetValue<string>("ChatUpdatesFileName")
                );

            EventViews viewer = new EventViews();
            DevByParser parser = new DevByParser(2);

            //try
            //{
                TelegramChecker telegramBot = new TelegramChecker(storage, bot,viewer,logger, parser, configuration.GetValue<string>("ChatMyID"));
                telegramBot.Run();
                
            //}
            //catch (AggregateException ex)
            //{
            //    foreach (var x in ex.InnerExceptions)
            //    {
            //        logger.WriteLog(x.Message);
            //        bot.SendMessage(x.Message, configuration.GetValue<int>("ChatMyID"));
            //    }
            //    Console.ReadKey();
            //}
        }

    }
}
