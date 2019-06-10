
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
using System.Threading;

namespace TelegramApp
{
    class Program
    {
        static string ChatUpdatesFileName = "ChatUpdates.json";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Task telegramTask = new Task(FollowingTelegram);
            telegramTask.Start();

            while (true)
            {
                if (telegramTask.IsCompleted)
                {
                    telegramTask.
                }
            }
            Console.ReadKey();
        }

        static void FollowingTelegram()
        {
            Updates ChatUpdatesOld = JsonConvert.DeserializeObject<Updates>(AppDir.GetDataFromFile(ChatUpdatesFileName)) ?? new Updates();
            TelegramBot telegramBot = new TelegramBot();
            var ResponsFromTelegram = telegramBot.GetUpdate(ChatUpdatesOld.GetLastUpdate());
            Console.WriteLine(ResponsFromTelegram);
            TelegramResponse MessagesFromTelegram = JsonConvert.DeserializeObject<TelegramResponse>(ResponsFromTelegram);
            if (MessagesFromTelegram.result.Count > 0)
            {
                foreach (var result in MessagesFromTelegram.result)
                {
                    ChatUpdatesOld.AddOrChangeUpdate(new Update(result.message.chat.Id.ToString(), result.update_id.ToString()));
                }
                AppDir.SaveTextToFile(ChatUpdatesFileName, JsonConvert.SerializeObject(ChatUpdatesOld));
            }

        }
    }
}
