
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
            List<EventObject> newEvents = devBy.GetNewEvents(JsonConvert.DeserializeObject<List<EventObject>>(AppDir.GetUsersFile("")));
            TelegramBot bot = new TelegramBot();
            TelegramResponse results = JsonConvert.DeserializeObject<TelegramResponse>(bot.GetUpdate());
            Console.WriteLine("You have new message");

            foreach (var item in results.result)
            {
                Console.WriteLine($"{item.message.text}");

            }
            Console.ReadKey();
        }
    }
}
