using DevBy;
using System.Collections.Generic;

namespace TelegramApp.Dependency
{
    public interface IStorage
    {
        void SaveNewEventsToStorage( List<Event> CurrEvents);
        List<Event> GetNewEventsFromStorageForUser(int userID);
        string GetLastUpdateTelegramFromStorage();
        void SaveTelegramUpdateToStorage(int update);
        void SaveUserCheckDate(int userId);
    }
}
