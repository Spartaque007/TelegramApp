﻿using DevBy;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private IStorage storage;
        private TelegramBot telegramBot;
        private EventViews viewer;
        public TelegramChecker(ref IStorage storage, ref TelegramBot bot, ref EventViews viewer)
        {
            this.storage = storage;
            this.telegramBot = bot;
            this.viewer = viewer;
        }

        public void Run()
        {
            Timer timer = new Timer(10000);
            timer.AutoReset = true;
            bool timerFlag = false;
            timer.Elapsed += (x, y) => timerFlag = true;
            timer.Start();
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                TelegramResponse ResponseFromTelegram = telegramBot.GetUpdate(storage.GetLastUpdateTelegramFromStorage());
                if (ResponseFromTelegram.result.Count > 0)
                {
                    TelegramActions operation = new TelegramActions();

                    foreach (var result in ResponseFromTelegram.result)
                    {
                        Console.WriteLine($"Message from {result.message.from.first_name}\n" +
                            $"Message Text {result.message.text} \n{DateTime.Now.ToShortTimeString()}");
                        operation.GetCommandFromMessage(result.message.text).ExecuteCommand(result, ref storage, ref telegramBot, ref viewer);
                    }
                    int MaxUpdate = ResponseFromTelegram.result.Max(X => X.update_id);
                    storage.SaveUpdateToStorage((MaxUpdate + 1).ToString());
                }
                if (timerFlag)
                {
                    DevByParser parser = new DevByParser();
                    List<Event> currEvents = parser.GetEvents(2);
                    List<Event> prevEvents = storage.GetEventsFromStorage("0");
                    List<Event> newEvents = currEvents.Except(prevEvents).ToList<Event>();
                    foreach (var @event in newEvents)
                    {
                        telegramBot.SendMessageMDCustom(viewer.ToMdFormat(@event),
                            int.Parse(ConfigurationManager.AppSettings.Get("ChatGeneral ID")));
                    }
                    storage.SaveEventsToStorage("0", currEvents);
                    Console.WriteLine("TimerFlag");
                    timerFlag = false;
                }

            }
        }
    }
}