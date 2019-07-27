using DevBy;
using System;
using System.Collections.Generic;
using Telegram;
using TelegramApp.Commands;
using TelegramApp.Dependency;
using TelegramApp.Views;

namespace TelegramApp
{
    public class TelegramCommandFactory
    {
        private readonly Dictionary<string, Func<ICommand>> commands = new Dictionary<string, Func<ICommand>>();
                            
        public TelegramCommandFactory(Result result, IStorage storage, TelegramBot bot, EventViews views, DevByParser parser)
        {
            commands.Add(@"/SHOWEVENTS@JONNWICKBOT", () => new ShowAllEvents(result, storage, bot, views, parser));
            commands.Add(@"/SHOWNEWEVENTS@JONNWICKBOT", () => new ShowNewEvents(result,storage,bot,views,parser));
            commands.Add(@"Default", () => new DefaultCommand(result, bot));
        }

        public ICommand GetCommandFromMessage(string message)
        {
            if (message != null)
            {
                return commands.ContainsKey(message.ToUpper())
                    ? commands[message.ToUpper()]()
                    : commands["Default"]();
  



               /* if (message.ToUpper().Contains("@JONNWICKBOT"))
                {
                    switch ($@"{message.ToUpper()}")
                    {
                        case @"/SHOWEVENTS@JONNWICKBOT":
                            return new ShowAllEvents();

                        case @"/SHOWNEWEVENTS@JONNWICKBOT":
                            return new ShowNewEvents();
                    }
                }

                return new DafaultCommand();*/
            }
            return new EmptyCommand();
        }
    }
}
