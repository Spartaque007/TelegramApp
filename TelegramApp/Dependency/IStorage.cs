using DevBy;
using System.Collections.Generic;

namespace TelegramApp.Dependency
{
    public interface IStorage
    {
        void SaveNewEventsToStorage( List<Event> CurrEvents);
        List<Event> GetNewEventsFromStorageForUser(string userID);
        string GetLastUpdateTelegramFromStorage();
        void SaveTelegramUpdateToStorage(string update);
        void SaveUserCheckDate(string userId);
    }
}
