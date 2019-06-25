﻿using DevBy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram;
using TelegramApp.Dependency;

namespace TelegramApp
{
    public class App
    {
        private IStorage Storage;
        private TelegramBot TelegramBot;



        public App(ref IStorage storage, ref TelegramBot bot)
        {
            this.Storage = storage;
            this.TelegramBot = bot;
        }
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            TelegramResponse ResponseFromTelegram = TelegramBot.GetUpdate(Storage.GetLastUpdateTelegramFromStorage());
            if (ResponseFromTelegram.result.Count > 0)
            {
                TelegramActions operation = new TelegramActions();
                foreach (var result in ResponseFromTelegram.result)
                {
                    Console.WriteLine($"Message from {result.message.from.first_name}\n" +
                        $"Message Text {result.message.text}");
                    operation.GetCommandFromMessage(result.message.text).ExecuteCommand(result);
                }
            }
        }


    }
}
