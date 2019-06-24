using DevBy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramApp.Dependency;
using WorkWithFiles;

namespace TelegramApp
{
    public class JsonEventStorage :IStorage
            {
        

        public static List<EventObject> GetEventsFromStorage(string UserFileName)
        {
            DevByParser parser = new DevByParser();
            List<EventObject> currEvents = parser.GetEvents();
            SaveEventsToFile(UserFileName, currEvents);
            return currEvents;
        }

        public static void SaveEventsToFile(string UserFileName, List<EventObject> CurrEvents)
        {
            LocalFile.SaveTextToFile(UserFileName, JsonConvert.SerializeObject(CurrEvents));
        }

        
        public string GetLastUpdateTelegramFromStorage()
        {
            
        }

        public void SaveEvents(List<EventObject> events)
        {
           
        }
    }
}
