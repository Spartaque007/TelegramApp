using DevBy;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;


namespace Telegram
{
    public class TelegramBot
    {
        public delegate List<EventObject> GetNewEvent(string userFileName);
        public event GetNewEvent GetNewEvents;
        public event GetNewEvent GetAllEvents;
        public delegate void BotSaveEvent(string UserFileName, List<EventObject> CurrEvents);
        private static string Token = $@"https://api.telegram.org/{ConfigurationManager.AppSettings.Get("BotToken")}";
        private HttpClient client = new HttpClient();
        private static string myChatID = ConfigurationManager.AppSettings.Get("ChatMy ID");
        private static string GeneralChatId = ConfigurationManager.AppSettings.Get("ChatGeneral ID");
        public string GetMe()
        {
            return client.GetStringAsync($"{Token }/getMe").Result;
        }
        public string GetUpdate(int? offset)
        {
            string a = client.GetStringAsync($"{Token }/getUpdates?offset={offset}&timeout=120").Result;


            return a;
        }
        public TelegramResponse GetUpdate(string updateID)
        {
            string b = client.GetStringAsync($"{Token}/getUpdates?offset={updateID}?message&timeout=120").Result;
            return JsonConvert.DeserializeObject<TelegramResponse>(b);

        }
        public string SendMessageMe(string message)
        {
            return client.GetStringAsync($"{Token }/sendMessage?chat_id={myChatID}&text={message}").Result;
        }
        public string SendMessageGeneral(string message)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={GeneralChatId}&text={message}").Result;
        }
        public string SendMessageCustom(string message, int chatID)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={chatID}&text={message}").Result;
        }
        
        

    }
}
