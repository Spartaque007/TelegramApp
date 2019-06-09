
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

            string ChatUpdatesFileName = "ChatUpdates.json";
            Console.OutputEncoding = Encoding.UTF8;
            Updates ChatUpdates = JsonConvert.DeserializeObject<Updates>(AppDir.GetDataFromFile(ChatUpdatesFileName))?? new Updates();
            TelegramBot telegramBot = new TelegramBot();
            string updates = telegramBot.GetUpdate();
            Console.WriteLine(updates);
            TelegramResponse response = JsonConvert.DeserializeObject<TelegramResponse>(updates);
            if (response.result.Count>0)
            {
                foreach (var result in response.result)
                {
                    ChatUpdates.AddOrChangeUpdate(new Update(result.message.chat.Id.ToString(), result.update_id.ToString()));
                }
                AppDir.SaveTextToFile(ChatUpdatesFileName, JsonConvert.SerializeObject(ChatUpdates));
            }
            
            Console.WriteLine("DONE");
            Console.ReadKey();
        }
    }
}
