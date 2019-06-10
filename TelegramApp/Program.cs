
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

using WorkWitFiles;

namespace TelegramApp
{
    class Program
    {
        static string ChatUpdatesFileName = "ChatUpdates.json";
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            DevByParser devBy = new DevByParser();
            Thread TrelegrammThread = new Thread(FollowTelegram);
            TrelegrammThread.Start();
            //TimerCallback checkerDevBy = new TimerCallback(CheckNewEvents);
            //Timer timerOfCheckDevBy = new Timer(checkerDevBy, null, 0, 72);
            while (true)
            {
               



            }


        }

        static async void FollowTelegram()
        {
            TelegramBot telegramBot = new TelegramBot();

            while (true)
            {
                Updates ChatUpdatesOld = JsonConvert.DeserializeObject<Updates>(AppDir.GetDataFromFile(ChatUpdatesFileName)) ?? new Updates();
                string updateID = ChatUpdatesOld.GetLastUpdate();
                TelegramResponse ResponsFromTelegram = await telegramBot.GetUpdate(updateID);
                telegramBot.SendAnswer(ResponsFromTelegram);


                if (ResponsFromTelegram.result.Count > 0)
                {
                    foreach (var result in ResponsFromTelegram.result)
                    {
                        ChatUpdatesOld.AddOrChangeUpdate(new Update(result.message.chat.Id.ToString(), result.update_id.ToString()));
                        Console.WriteLine($"Message from {result.message.from.username}\n{result.message.text}");

                    }
                    AppDir.SaveTextToFile(ChatUpdatesFileName, JsonConvert.SerializeObject(ChatUpdatesOld));
                }
            }
        }

        static void CheckNewEvents(object obj)
        {
            DevByParser parser = new DevByParser();
            List<EventObject> currEvents = parser.GetEvents();
            List<EventObject> prevEvents = JsonConvert.DeserializeObject<List<EventObject>>(AppDir.GetDataFromFile("Meetings.json")) ?? new List<EventObject>();
            List<EventObject> newEvents = currEvents.Except(prevEvents).ToList<EventObject>();
            if (newEvents.Count > 0)
            {

            }

        }
    }
}
