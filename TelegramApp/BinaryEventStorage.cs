using DevBy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WorkWithFiles;

namespace TelegramApp
{
    public class BinaryEventStorage
    {
        public static List<EventObject> CheckNewEvents(string UserFileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<EventObject> prevEvents = new List<EventObject>();

            if (AppDir.CheckPathFile(UserFileName))
            {
                using (FileStream fs = new FileStream($@"{ AppDir.Dir }\{UserFileName}", FileMode.OpenOrCreate))
                {
                    prevEvents = (List<EventObject>)formatter.Deserialize(fs);

                }
            }
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
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream($@"{ AppDir.Dir }\{UserFileName}", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, CurrEvents);

            }

        }
    }
}
