using DevBy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TelegramApp.Dependency;
using TelegramApp.Storages.DdModels;
using WorkWithFiles;

namespace TelegramApp
{
    public class JsonStorage : IStorage
    {
        private string updatesFile = ConfigurationManager.AppSettings["ChatUpdatesFileName"];
        private string eventsFile = "Events";
        private string usersFile = "Users";
        public int GetLastUpdateTelegramFromStorage()
        {
            string lastUpd = LocalFile.GetDataFromFile(updatesFile).Result ?? "0";
            return int.Parse(lastUpd);
        }
        public void SaveTelegramUpdateToStorage(int update)
        {
            LocalFile.SaveTextToFile(updatesFile, update.ToString());
        }
        public void SaveNewEventsToStorage(List<Event> CurrEvents)
        {
            string eventsFromStorageText = LocalFile.GetDataFromFile(eventsFile).Result ?? "";
            List<Event> eventsFromStorage = JsonConvert.
                DeserializeObject<List<Event>>(eventsFromStorageText) ?? new List<Event>();
            List<Event> NonActualEvent = eventsFromStorage.Except(CurrEvents).ToList();
            List<Event> ActualEvent = eventsFromStorage.Except(NonActualEvent).ToList();
            List<Event> newEvents = CurrEvents.Except(ActualEvent).ToList();
            eventsFromStorage.AddRange(newEvents);
            string eventsToStorageText = JsonConvert.SerializeObject(eventsFromStorage);
            LocalFile.SaveTextToFile(eventsFile, eventsToStorageText);
        }
        public List<Event> GetNewEventsFromStorageForUser(int userID)
        {
            string eventsText = LocalFile.GetDataFromFile(eventsFile).Result;
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(eventsText);
            DateTime lastCheckDate = GetUserLastCheckDateAndSaveCurrentDate(userID);
            List<Event> newEvents = (from e in events
                                     where e.EventAddDate > lastCheckDate
                                     orderby e.EventAddDate
                                     select e).ToList();
            return newEvents;

        }
        private DateTime GetUserLastCheckDateAndSaveCurrentDate(int userId)
        {
            string allUsersText = LocalFile.GetDataFromFile(usersFile).Result;
            List<User> allUsers = JsonConvert.DeserializeObject<List<User>>(allUsersText) ?? new List<User>();
            int indexOfUser = allUsers.FindIndex(u => u.UserID == userId);
            DateTime lastCheckDate;
            if (indexOfUser < 0)
            {
                allUsers.Add(new User { UserID = userId, LastUpdate = DateTime.Now });
                lastCheckDate = default;
            }
            else
            {
                lastCheckDate = allUsers[indexOfUser].LastUpdate;
                allUsers[indexOfUser].LastUpdate = DateTime.Now;
            }
            allUsersText = JsonConvert.SerializeObject(allUsers);
            LocalFile.SaveTextToFile(usersFile, allUsersText);
            return lastCheckDate;
        }
        public void SaveUserCheckDate(int userId)
        {
            GetUserLastCheckDateAndSaveCurrentDate(userId);
        }
    }
}