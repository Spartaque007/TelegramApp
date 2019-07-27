using DevBy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TelegramApp.Dependency;
using TelegramApp.StorageClasses.DdModels;


namespace TelegramApp
{
    public class JsonStorage : IStorage
    {
        private readonly string updatesFileName;
        private readonly string defaultDir;
        private readonly string eventsFileName;
        private readonly string usersFileName;
        public JsonStorage(string defaultDirectory, string eventsFileName, string usersFileName, string updatesFileName )
        {
            this.defaultDir = "." + "/" +  defaultDirectory;
            this.eventsFileName =  defaultDir + eventsFileName;
            this.usersFileName = defaultDir + usersFileName;
            this.updatesFileName = defaultDir + updatesFileName;
        }
        public string GetLastUpdateTelegramFromStorage()
        {
            var a = GetDataFromFile(updatesFileName).Result ?? "0";
            return a ;
        }
        public void SaveTelegramUpdateToStorage(string update)
        {
           SaveTextToFile(updatesFileName, update);
        }
        public void SaveNewEventsToStorage(List<Event> CurrEvents)
        {
            string eventsFromStorageText = GetDataFromFile(eventsFileName).Result ?? "";
            List<Event> eventsFromStorage = JsonConvert.
                DeserializeObject<List<Event>>(eventsFromStorageText) ?? new List<Event>();
            List<Event> NonActualEvent = eventsFromStorage.Except(CurrEvents).ToList();
            List<Event> ActualEvent = eventsFromStorage.Except(NonActualEvent).ToList();
            List<Event> newEvents = CurrEvents.Except(ActualEvent).ToList();
            eventsFromStorage.AddRange(newEvents);
            string eventsToStorageText = JsonConvert.SerializeObject(eventsFromStorage);
            SaveTextToFile(eventsFileName, eventsToStorageText);
        }
        public List<Event> GetNewEventsFromStorageForUser(string userID)
        {
            string eventsText = GetDataFromFile(eventsFileName).Result;
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(eventsText);
            DateTime lastCheckDate = GetUserLastCheckDateAndSaveCurrentDate(userID);
            List<Event> newEvents = (from e in events
                                     where DateTime.Parse(e.EventAddDate) > lastCheckDate
                                     orderby DateTime.Parse(e.EventAddDate)
                                     select e).ToList();
            return newEvents;

        }
        private DateTime GetUserLastCheckDateAndSaveCurrentDate(string userId)
        {
            string allUsersText = GetDataFromFile(usersFileName).Result;
            List<User> allUsers = JsonConvert.DeserializeObject<List<User>>(allUsersText) ?? new List<User>();
            int indexOfUser = allUsers.FindIndex(u => u.UserID == userId);
            DateTime lastCheckDate;
            if (indexOfUser < 0)
            {
                allUsers.Add(new User { UserID = userId, LustUpdate = DateTime.Now });
                lastCheckDate = default;
            }
            else
            {
                lastCheckDate = allUsers[indexOfUser].LustUpdate;
                allUsers[indexOfUser].LustUpdate = DateTime.Now;
            }
            allUsersText = JsonConvert.SerializeObject(allUsers);
            SaveTextToFile(usersFileName, allUsersText);
            return lastCheckDate;
        }
        public void SaveUserCheckDate(string userId)
        {
            GetUserLastCheckDateAndSaveCurrentDate(userId);
        }
        public bool CheckUsersFolder()
        {
            if (!Directory.Exists(defaultDir))
            {
                Directory.CreateDirectory(defaultDir);
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool CheckPathFile(string fileName)
        {
            CheckUsersFolder();
            try
            {
                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None)) { }
                return true;
            }
            catch (FileNotFoundException)
            {
                using (FileStream fs = File.Create(fileName)) { }
                return false;
            }
        }
        public async Task<string> GetDataFromFile(string fileName)
        {
            if (CheckPathFile(fileName))
            {
                return await Task.Run(() =>
                {
                    string str = File.ReadAllText(fileName) ?? " ";
                    return str;
                });
            }
            else
            {
                return "";
            }
        }
        public void SaveTextToFile(string fileName, string text)
        {
            CheckPathFile(fileName);
            File.WriteAllText(fileName, text);
        }
    }
}