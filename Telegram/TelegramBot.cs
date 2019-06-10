using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Telegram
{
    public class TelegramBot
    {

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
        public Task<string> GetUpdate(string updateID)
        {
            string b = client.GetStringAsync($"{Token }/getUpdates?offset={updateID}?message&timeout=120").Result;

            return b;
        }
        public string SendMessageMe(string message)
        {
            return client.GetStringAsync($"{Token }/sendMessage?chat_id={myChatID}&text={message}").Result;
        }
        public string SendMessageGeneral(string message)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={GeneralChatId}&text={message}").Result;
        }

        public string SendMessageCustom(string message, string chatID)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={chatID}&text={message}").Result;
        }

        public void GenerateAnswer(TelegramResponse response)
        {
            if (response.result.Count > 0)
            {
                foreach (var resp in response.result)
                {
                    SendMessageCustom($"{resp.message.from.username}, we are recieve You message", resp.message.chat.Id.ToString());
                }
                
            }
        }

        
    }
}
