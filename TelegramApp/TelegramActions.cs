using DevBy;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Telegram;
using TelegramApp.Commands;
using TelegramApp.Dependency;

namespace TelegramApp
{
    public class TelegramActions
    {
        public ICommand GetCommandFromMessage(string message)
        {
            
            if (message.ToUpper().Contains("@JONNWICKBOT"))
            {
                switch ($@"{message.ToUpper()}")
                {
                   
                    case @"/SHOWEVENTS@JONNWICKBOT":
                        return  new ShowNewEvents();
                        
                    case @"/SHOWNEWEVENTS@JONNWICKBOT":
                        return new ShowAllEvents();
                        
                }
               
            }
            return new DafaultCommand();
        }

    }
}
