using DevBy;
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
        private DevByParser DevByParser;


        public App(ref IStorage storage, ref TelegramBot bot, ref DevByParser parser)
        {
            this.Storage = storage;
            this.TelegramBot = bot;
            this.DevByParser = parser;
            
        }
       public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string ChatUpdatesFileName = ConfigurationManager.AppSettings["ChatUpdatesFileName"];
            TelegramResponse ResponseFromTelegram = TelegramBot.GetUpdate(Storage.GetLastUpdateTelegramFromStorage());
            if (ResponseFromTelegram.result.Count > 0)
            {



            }
        }


    }
}
