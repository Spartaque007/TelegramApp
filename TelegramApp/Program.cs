
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
            List<EventObject> meetings = devBy.GetEvents();

            foreach (var meeting in meetings)
            {
                Console.WriteLine(meeting.ToString());
            }

            string json = JsonConvert.SerializeObject(meetings);

            string prev_json = File.ReadAllText(AppDir.MeetingsFile);

            List<EventObject> meetings_prev = JsonConvert.DeserializeObject<List<EventObject>>(prev_json);

            var meetingDiff = (meetings).Where(p => !meetings_prev.Contains(p));

            Console.WriteLine("\nNew meetings:");

            foreach (var meeting in meetingDiff)
            {
                Console.WriteLine(meeting.ToString());
            }
            File.WriteAllText(AppDir.MeetingsFile, json);

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
