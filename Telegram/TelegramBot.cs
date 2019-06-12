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
            string text = result.message.text.ToUpper();
            string senderName = $"{result.message.from.first_name}";
            string userFileName = $@"Meetings_{result.message.from.ID}.json";
            if (text.Contains("@JONNWICKBOT"))
            {
                switch ($@"{result.message.text.ToUpper()}")
                {
                    case @"/HELLO@JONNWICKBOT":
                        SendMessageCustom($@"Hello,{result.message.from.first_name} {result.message.from.last_name} !", result.message.chat.Id.ToString());
                        break;
                    case @"/HOWAREYOU@JONNWICKBOT":
                        SendMessageCustom($@"Те че поговорить не с кем?", result.message.chat.Id.ToString());
                        break;
                    case @"/SHOWEVENTS@JONNWICKBOT":
                        List<EventObject> allEvents = GetAllEvents(userFileName);
                        if (allEvents.Capacity > 0)
                        {
                            foreach (EventObject eo in allEvents)
                            {
                                SendMessageCustom($@"Date:{eo.EverntDate} Event: {eo.EventName}", result.message.chat.Id.ToString());

                            }
                            SendMessageCustom($@"*******That's All*****", result.message.chat.Id.ToString());

                        }
                        break;
                    case @"/SHOWNEWEVENTS@JONNWICKBOT":
                        List<EventObject> newEvents = GetNewEvents(userFileName);
                        if (!result.message.from.Is_bot && newEvents.Capacity > 0)
                        {
                            SendMessageCustom($@"*****New events from DEV.BY*****", result.message.chat.Id.ToString());
                        }

                        if (newEvents.Count > 0)
                        {
                            foreach (EventObject eo in newEvents)
                            {
                                SendMessageCustom($@"Date:{eo.EverntDate} Event: {eo.EventName}", result.message.chat.Id.ToString());

                            }
                            if (!result.message.from.Is_bot && newEvents.Capacity > 0)
                            {
                                SendMessageCustom($@"*******That's All*****", result.message.chat.Id.ToString());
                            }
                        }
                        else
                        {
                            if (!result.message.from.Is_bot)
                            {
                                SendMessageCustom($@"*****No new events for you,{senderName}!*****", result.message.chat.Id.ToString());
                            }

                        }
                        break;

                    default:
                        SendMessageCustom($"A am not understand you, {senderName}!!!\n Chose the command from list!\n List of Commands: \n /ShowEvents- displays active meetings with Dev.by\n /ShowNewEvents-displays new events for you with Dev.by ", result.message.chat.Id.ToString());
                        break;
                }

            }

        }

    }
}
