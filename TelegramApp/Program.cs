using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram;
using DevBy;
using System.Threading;
using WorkWithFiles;
using System.Configuration;

namespace TelegramApp
{
    class Program
    {
        

        static void Main(string[] args)
        {
           
           
            while (true)
            {

                Console.WriteLine("Work");
                Thread.Sleep(9000);

            }
        }
        static async void FollowTelegram()
        {
            TelegramBot telegramBot = new TelegramBot();
            telegramBot.GetNewEvents += ;
            telegramBot.GetAllEvents += BinaryEventStorage.GetAllEvents;

            TimerCallback checkEvents = new TimerCallback((object ob) =>
            {
                TelegramBot bot = (TelegramBot)ob;
                TelegramResponse resp = new TelegramResponse();
                resp.Ok = true;
                resp.result = new List<Result>();
                Result res = new Result();
                res.message = new Message();
                res.message.chat.Id = int.Parse(ConfigurationManager.AppSettings.Get("ChatGeneral ID"));
                res.message.from.ID = 0;
                res.message.from.Is_bot = true;
                res.message.from.username = "gays";
                res.message.text = @"/SHOWNEWEVENTS@JONNWICKBOT";
                resp.result.Add(res);
                bot.SendAnswer(resp);

            });
            Timer timer = new Timer(checkEvents, telegramBot, 0, 10000);

            while (true)
            {
                //Get All updates from local file
                string ChatUpdatesFileName = ConfigurationManager.AppSettings["hatUpdatesFileName"];
                string value = await AppDir.GetDataFromFile(ChatUpdatesFileName);
                Updates ChatUpdatesOld = JsonConvert.DeserializeObject<Updates>(value) ?? new Updates();

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


    }
}
