using DevBy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramApp.Dependency
{
    public interface IStorage
    {
        void SaveEventsToStorage(string UserID, List<Event> CurrEvents);
        List<Event> GetEventsFromStorage(string path);
        string GetLastUpdateTelegramFromStorage();
        void SaveUpdateToStorage(string update);
    }
}
