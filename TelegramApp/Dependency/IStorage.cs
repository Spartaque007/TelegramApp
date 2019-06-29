﻿using DevBy;
using System.Collections.Generic;

namespace TelegramApp.Dependency
{
    public interface IStorage
    {
        void SaveEventsToStorage(string UserID, List<Event> CurrEvents);
        List<Event> GetEventsFromStorageForUser(string userID);
        string GetLastUpdateTelegramFromStorage();
        void SaveUpdateToStorage(string update);
    }
}
