
using System.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram;
using DevBy;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            AppDir.CheckPathFile();
            DevByParser devBy = new DevByParser();
            var prevEvents = JsonConvert.DeserializeObject<List<EventObject>>(AppDir.GetUsersFile(""));
            List<EventObject> newEvents = devBy.GetNewEvents(prevEvents);
            TelegramBot bot = new TelegramBot();
            TelegramResponse results = JsonConvert.DeserializeObject<TelegramResponse>(bot.GetUpdate());
            bot.GenerateAnswer(results);


            Console.ReadKey();
        }
    }
}
