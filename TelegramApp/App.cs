using System;
using System.Linq;
using System.Text;
using Telegram;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp
{
    public class App
    {
        private IStorage storage;
        private TelegramBot telegramBot;
        private EventViews viewer;
        public App(ref IStorage storage, ref TelegramBot bot,ref EventViews viewer)
        {
            this.storage = storage;
            this.telegramBot = bot;
            this.viewer = viewer;
        }

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            TelegramResponse ResponseFromTelegram = telegramBot.GetUpdate(storage.GetLastUpdateTelegramFromStorage());
            if (ResponseFromTelegram.result.Count > 0)
            {
                TelegramActions operation = new TelegramActions();
                
                foreach (var result in ResponseFromTelegram.result)
                {
                    Console.WriteLine($"Message from {result.message.from.first_name}\n" +
                        $"Message Text {result.message.text} \n{DateTime.Now.ToShortTimeString()}");
                    operation.GetCommandFromMessage(result.message.text).ExecuteCommand(result, ref storage,ref telegramBot,ref viewer);
                }
                int MaxUpdate = ResponseFromTelegram.result.Max(X => X.update_id);
                storage.SaveUpdateToStorage((MaxUpdate + 1).ToString());

            }
        }
    }
}
