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
        public List<Event> GetEventsFromStorage(string UserID)
        {
            string userFileName = GetUserFileName(UserID);
            string textFromUserFile = LocalFile.GetDataFromFile(userFileName).Result;
            return JsonConvert.DeserializeObject<List<Event>>(textFromUserFile);

        }
        public void SaveEventsToStorage(string UserID, List<Event> CurrEvents)
        {
            string userFileName = GetUserFileName(UserID);
            LocalFile.SaveTextToFile(userFileName, JsonConvert.SerializeObject(CurrEvents));
        }
        public string GetLastUpdateTelegramFromStorage()
        {
            return LocalFile.GetDataFromFile(fileUpdates).Result;
            
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