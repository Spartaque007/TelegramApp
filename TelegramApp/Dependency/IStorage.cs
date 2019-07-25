using DevBy;
using System.Collections.Generic;

namespace TelegramApp.Dependency
{
    public interface IStorage
    {
        void SaveNewEventsToStorage( List<Event> CurrEvents);
        List<Event> GetNewEventsFromStorageForUser(int userID);
        int GetLastUpdateTelegramFromStorage();
        void SaveTelegramUpdateToStorage(int update);
        void SaveUserCheckDate(int userId);
    }
}
