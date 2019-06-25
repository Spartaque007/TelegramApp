using DevBy;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using TelegramApp.Dependency;
using WorkWithFiles;

namespace TelegramApp
{
    public class JsonEventStorage : IStorage
    {
        static string fileUpdates = ConfigurationManager.AppSettings["ChatUpdatesFileName"];

        public List<EventObject> GetEventsFromStorage(string UserID)
        {
            string userFileName = GetUserFileName(UserID);
            string textFromUserFile = LocalFile.GetDataFromFile(userFileName).Result;
            return JsonConvert.DeserializeObject<List<EventObject>>(textFromUserFile);

        }
        public void SaveEventsToStorage(string UserID, List<EventObject> CurrEvents)
        {
            string userFileName = GetUserFileName(UserID);
            LocalFile.SaveTextToFile(userFileName, JsonConvert.SerializeObject(u));
        }
        public string GetLastUpdateTelegramFromStorage()
        {
            string textFromUserFile = LocalFile.GetDataFromFile(fileUpdates).Result;
            return JsonConvert.DeserializeObject<Update>(textFromUserFile).LastUpdateID;
        }
        public void SaveUpdateToStorage(string update)
        {
            LocalFile.SaveTextToFile(fileUpdates, update);

        }
        private string GetUserFileName(string userID)
        {
            return $"Meetings_{userID}";
        }
               
    }
}