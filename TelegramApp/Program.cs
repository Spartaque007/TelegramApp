using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram;
using DevBy;
using System.Threading;
using WorkWitFiles;
using System.Threading.Tasks;
using System.Configuration;

namespace TelegramApp
{
    class Program
    {
        static string ChatUpdatesFileName = "ChatUpdates.json";
        static string DefaultMeetingFileName = "Meetings_0.json";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DevByParser devBy = new DevByParser();
            Thread TrelegrammThread = new Thread(FollowTelegram);
            TrelegrammThread.Start();
            TimerCallback Cb = new TimerCallback((object ob) =>
            {
                List<EventObject> newEvents = CheckNewEvents(DefaultMeetingFileName);
                if (newEvents.Count > 0)
                {
                    TelegramBot bot = new TelegramBot();
                    Updates ChatUpdatesOld = JsonConvert.DeserializeObject<Updates>(AppDir.GetDataFromFile(ChatUpdatesFileName).Result) ?? new Updates();
                    TelegramResponse resp = new TelegramResponse();
                    resp.Ok = true;
                    resp.result = new List<Result>();
                    Result res = new Result();
                    res.update_id = int.Parse(ChatUpdatesOld.GetLastUpdate());
                    res.message = new Message();
                    res.message.chat.Id = int.Parse(ConfigurationManager.AppSettings.Get("ChatGeneral ID"));
                    res.message.from.ID = 0;
                    res.message.from.username = "gays";
                    res.message.text = @"/SHOWNEWEVENTS@JONNWICKBOT";
                    resp.result.Add(res);
                    bot.SendAnswer(resp);
                }
            });
            Timer timer = new Timer(Cb, null, 0, 6000);
            while (true)
            {


                Console.WriteLine("Work");
                Thread.Sleep(1500);

            }
        }

        static async void FollowTelegram()
        {
            TelegramBot telegramBot = new TelegramBot();
            telegramBot.GetNewEvents += CheckNewEvents;
            telegramBot.GetAllEvents += GetAllEvents;
            
            while (true)
            {
                //Get All updates from local file
                Updates ChatUpdatesOld = JsonConvert.DeserializeObject<Updates>(await AppDir.GetDataFromFile(ChatUpdatesFileName)) ?? new Updates();
                //Get Last update for all chats
                string updateID = ChatUpdatesOld.GetLastUpdate();
                //Get respons with last updates from telegram
                TelegramResponse ResponsFromTelegram = telegramBot.GetUpdate(updateID);
                //Send answer for each users in each chat
                telegramBot.SendAnswer(ResponsFromTelegram);


                if (ResponsFromTelegram.result.Count > 0)
                {
                    foreach (var result in ResponsFromTelegram.result)
                    {
                        //Save last updateID to List of Updates
                        ChatUpdatesOld.AddOrChangeUpdate(new Update(result.message.chat.Id.ToString(), result.update_id.ToString()));
                        //Show message on console 
                        Console.WriteLine($"Message from {result.message.from.username}\n{result.message.text}");

                    }
                    //Save last updateID to the local file
                    AppDir.SaveTextToFile(ChatUpdatesFileName, JsonConvert.SerializeObject(ChatUpdatesOld));
                }
            }
        }

        static List<EventObject> CheckNewEvents(string UserFileName)
        {

            var currEvents = GetAllEvents(UserFileName);
            List<EventObject> prevEvents = JsonConvert.DeserializeObject<List<EventObject>>(AppDir.GetDataFromFile(UserFileName).Result) ?? new List<EventObject>();
            return currEvents.Except(prevEvents).ToList<EventObject>();

        }
        static List<EventObject> GetAllEvents(string UserFileName)
        {
            DevByParser parser = new DevByParser();
            var currEvents = parser.GetEvents();
            SaveEventsToFile(UserFileName, currEvents);
            return currEvents;
        }
        static void SaveEventsToFile(string UserFileName, List<EventObject> CurrEvents)
        {
            AppDir.SaveTextToFile(UserFileName, JsonConvert.SerializeObject(CurrEvents));
        }
    }
}
