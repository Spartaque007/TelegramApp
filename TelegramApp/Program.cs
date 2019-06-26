using System;
using System.Text;
using Telegram;
using DevBy;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IStorage storage = new JsonEventStorage();
            TelegramBot bot = new TelegramBot();
            EventViews viewer = new EventViews();
            App app = new App(ref storage, ref bot,ref  viewer);
           
            while (true)
            {
                app.Run();
               
            }
        }
        //static async void FollowTelegram()
        //{
        //    //TimerCallback checkEvents = new TimerCallback((object ob) =>
        //    //{
        //    //    TelegramBot bot = (TelegramBot)ob;
        //    //    TelegramResponse resp = new TelegramResponse();
        //    //    resp.Ok = true;
        //    //    resp.result = new List<Result>();
        //    //    Result res = new Result();
        //    //    res.message = new Message();
        //    //    res.message.chat.Id = int.Parse(ConfigurationManager.AppSettings.Get("ChatGeneral ID"));
        //    //    res.message.from.ID = 0;
        //    //    res.message.from.Is_bot = true;
        //    //    res.message.from.username = "gays";
        //    //    res.message.text = @"/SHOWNEWEVENTS@JONNWICKBOT";
        //    //    resp.result.Add(res);
        //    //    bot.SendAnswer(resp);

        //    //});
        //    //Timer timer = new Timer(checkEvents, telegramBot, 0, 10000);

        //    while (true)
        //    {

        //    }
        //}


    }
}
