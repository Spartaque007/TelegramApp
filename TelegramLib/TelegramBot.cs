﻿using DevBy;
using Newtonsoft.Json;
using System.Collections.Generic;

using System.Net.Http;

namespace Telegram
{
    public class TelegramBot
    {
        
        public delegate void BotSaveEvent(string UserFileName, List<Event> CurrEvents);
        private readonly string Token;
        private readonly HttpClient client = new HttpClient();

        public TelegramBot(string token)
        {
           Token = $@"https://api.telegram.org/{token}";
        }
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
        public void SendMessageMD(string message, int chatID)
        {
            string requestUri = $"{Token}/sendMessage";

            KeyValuePair<string, string>[] form = new[]
            {
                new KeyValuePair<string, string>("chat_id", chatID.ToString() ),
                new KeyValuePair<string, string>("text", message),
                new KeyValuePair<string, string>("parse_mode", "markdown"),
               new KeyValuePair<string, string>("disable_web_page_preview", "true")
            };
            var httpResponseMessage = client.PostAsync(requestUri, new FormUrlEncodedContent(form)).Result;
        }
        public string SendMessage(string message, int chatID)
        {

            return client.GetStringAsync($"{Token }/sendMessage?chat_id={chatID}&text={message}").Result;
        }



    }
}
