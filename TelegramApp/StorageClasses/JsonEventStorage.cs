using DevBy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithFiles;

namespace TelegramApp
{
    public class JsonEventStorage
    {
        public static List<EventObject> CheckNewEvents(string UserFileName)
        {
            List<EventObject> prevEvents = JsonConvert.DeserializeObject<List<EventObject>>(AppDir.GetDataFromFile(UserFileName).Result)
                ?? new List<EventObject>();

            List<EventObject> currEvents = GetAllEvents(UserFileName);
            return currEvents.Except(prevEvents).ToList<EventObject>();

        }

        public static List<EventObject> GetAllEvents(string UserFileName)
        {
            DevByParser parser = new DevByParser();
            List<EventObject> currEvents = parser.GetEvents();
            SaveEventsToFile(UserFileName, currEvents);
            return currEvents;
        }

        public static void SaveEventsToFile(string UserFileName, List<EventObject> CurrEvents)
        {
            AppDir.SaveTextToFile(UserFileName, JsonConvert.SerializeObject(CurrEvents));
        }
    }
}
