﻿using System;
using System.Text;
using Telegram;
using DevBy;
using TelegramApp.Dependency;
using TelegramApp.Views;
using System.Threading;
using TelegramApp.StorageClasses;
using TelegramApp.Logers;
using System.Net;
using System.Collections.Generic;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                IStorage storage = new JsonStorage();
                TelegramBot bot = new TelegramBot();
                EventViews viewer = new EventViews();
                ILoger loger = new ConsoleLoger();
                TelegramChecker telegramBot = new TelegramChecker(ref storage, ref bot, ref viewer, ref loger);
                Thread telegramChecker = new Thread(telegramBot.Run);
                telegramChecker.Start();
            }
            catch (AggregateException ex)
            {
                foreach (var x in ex.InnerExceptions)
                {
                    Console.WriteLine(x.Message);
                }

                Console.ReadKey();
            }


        }

    }
}
