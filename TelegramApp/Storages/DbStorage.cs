using System;
using System.Collections.Generic;
using System.Linq;
using DevBy;
using TelegramApp.Dependency;
using TelegramApp.Storages.DdModels;

namespace TelegramApp.Storages
{
    public class DbStorage : IStorage
    {
        string connectionString;
        public DbStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public int GetLastUpdateTelegramFromStorage()
        {
            using (TelegramContext context = new TelegramContext(connectionString))
            {
                if (context.Updates.Any())
                {
                    return context.Updates.First().LastUpdate;
                }
                else
                {
                    context.Updates.Add(new Update { LastUpdate = 0 });
                    context.SaveChanges();
                    return 0;
                }
            }
        }

        public List<Event> GetNewEventsFromStorageForUser(int userID)
        {
            using (TelegramContext context = new TelegramContext(connectionString))
            {
                User user = context.Users.FirstOrDefault(u => u.UserID == userID) ?? new User { LastUpdate = DateTime.MinValue, UserID = userID };
                List<Event> newEvents = context.EventLists.Where(e => e.EventAddDate > user.LastUpdate).ToList();
                user.LastUpdate = DateTime.Now;
                context.SaveChanges();
                return newEvents;
            }
        }

        public void SaveNewEventsToStorage(List<Event> currEvents)
        {
            using (TelegramContext context = new TelegramContext(connectionString))
            {
                List<Event> eventsFromDb = context.EventLists.ToList();
                List<Event> oldEvents = eventsFromDb.Except(currEvents).ToList();
                context.EventLists.RemoveRange(oldEvents);
                context.SaveChanges();
                var newEvents = currEvents.Except(context.EventLists);
                context.EventLists.AddRange(newEvents);
                context.SaveChanges();
            }
        }

        public void SaveTelegramUpdateToStorage(int update)
        {
            using (TelegramContext context = new TelegramContext(connectionString))
            {
                if (context.Updates.Any())
                {
                    Update upd = context.Updates.First();
                    upd.LastUpdate = update;
                }
                else
                {
                    context.Updates.Add(new Update { LastUpdate = update });
                }
                context.SaveChanges();
            }
        }

        public void SaveUserCheckDate(int userId)
        {
            using (TelegramContext context = new TelegramContext(connectionString))
            {
                try
                {
                    context.Users.Find(userId).LastUpdate = DateTime.Now;
                    context.SaveChanges();
                }
                catch (NullReferenceException)
                {
                    context.Users.Add(new User { UserID = userId, LastUpdate = DateTime.Now });
                    context.SaveChanges();
                }
            }
        }
    }
}
