using Newtonsoft.Json;
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
        public async Task<TelegramResponse> GetUpdate(string updateID)
        {
            string b = await client.GetStringAsync($"{Token}/getUpdates?offset={updateID}?message&timeout=120");
            return  JsonConvert.DeserializeObject<TelegramResponse>(b);
            
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

        public void SendAnswer(TelegramResponse response)
        {
            if (response.result.Count > 0)
            {
                foreach (var resp in response.result)
                {
                    GenerateAnswer(resp);
                }
                
            }
        }
        private void GenerateAnswer(Result result)
        {
            switch ($@"{result.message.text.ToUpper()}")
            {
                case @"/HELLO":
                    SendMessageCustom($@"Hello,{result.message.from.first_name} {result.message.from.last_name} !", result.message.chat.Id.ToString());
                    break;
                case @"/HOW ARE YOU":
                    SendMessageCustom($@"Те че поговорить не с кем?", result.message.chat.Id.ToString());
                    break;

                default:
                    SendMessageCustom($"A am not understand you, Mr.{result.message.from.first_name} {result.message.from.last_name}!!!\n Chose the command from list!\n List of Commands: \n /ShowEvents- displays active meetings with Dev.by\n /ShowNewEvents-displays new events for you with Dev.by ", result.message.chat.Id.ToString());
                    break;
            }



        }

        
    }
}
