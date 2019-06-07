using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{
    public class TelegramBot
    {

        private string Token = $@"https://api.telegram.org/{ConfigurationManager.AppSettings.Get("BotToken")}";
        private HttpClient client = new HttpClient();
        public string GetMe()
        {
            return client.GetStringAsync($"{Token }/getMe").Result;
        }
        public string GetUpdate(int? offset)
        {
            string a = client.GetStringAsync($"{Token }/getUpdates?offset={offset}&timeout=120").Result;


            return a;
        }
        public string GetUpdate()
        {
            string b = client.GetStringAsync($"{Token }/getUpdates?message&timeout=120").Result;

            return b;
        }
        public string SendMessageMe(string message)
        {
            return client.GetStringAsync($"{Token }/sendMessage?chat_id={ConfigurationManager.AppSettings.Get("ChatMy ID")}&text={message}").Result;
        }
        public string SendMessageGeneral(string message)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={ConfigurationManager.AppSettings.Get("ChatGeneral ID")}&text={message}").Result;
        }


    }
}
