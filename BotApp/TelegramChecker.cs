using DevBy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp
{
    public class TelegramChecker
    {
        private readonly IStorage storage;
        private readonly TelegramBot telegramBot;
        private readonly EventViews viewer;
        private readonly ILogger loger;
        private readonly string defaultChatId;
        private readonly DevByParser parser;
        public TelegramChecker(IStorage storage, TelegramBot bot, EventViews viewer, ILogger loger, DevByParser parser, string defaultChatId)
        {
            this.storage = storage;
            this.telegramBot = bot;
            this.viewer = viewer;
            this.loger = loger;
            this.defaultChatId = defaultChatId;
            this.parser = parser;
        }

        public void Run()
        {

            Timer timer = new Timer(6000);
            timer.AutoReset = true;
            bool timerFlag = false;
            timer.Elapsed += (x, y) => timerFlag = true;
            timer.Start();
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                TelegramResponse responseFromTelegram = telegramBot.GetUpdate(storage.GetLastUpdateTelegramFromStorage());
                if (responseFromTelegram.result.Count > 0)
                {


                    foreach (var result in responseFromTelegram.result)
                    {
                        TelegramCommandFactory operation = new TelegramCommandFactory(result, storage, telegramBot,
                                                    viewer, parser);
                        loger.WriteLog($"Message from {result.message.from.username}\n" +
                            $"Message Text {result.message.text} \n{DateTime.Now.ToShortTimeString()}");
                        operation.GetCommandFromMessage(result.message.text).ExecuteCommand();
                    }
                    int MaxUpdate = responseFromTelegram.result.Max(X => X.Update_id);
                    storage.SaveTelegramUpdateToStorage((MaxUpdate + 1).ToString());
                }
                if (timerFlag)
                {

                    List<Event> currEvents = parser.GetEvents(2);
                    storage.SaveNewEventsToStorage(currEvents);
                    List<Event> newEvents = storage.GetNewEventsFromStorageForUser("0");
                    if (newEvents.Count > 0)
                    {
                        foreach (var @event in newEvents)
                        {
                            telegramBot.SendMessageMD(viewer.ToMdFormat(@event),
                                int.Parse(defaultChatId));
                        }
                    }
                    timerFlag = false;
                }
            }

        }
    }
}
