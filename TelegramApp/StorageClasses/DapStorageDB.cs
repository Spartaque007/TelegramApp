using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DevBy;
using TelegramApp.Dependency;


namespace TelegramApp.StorageClasses
{
    class DapStorageDB : IStorage
    {
        private string eventsTableName = "EventList";
        private string usersTableName = "UsersList";
        private string updatesTableName = "Updates";
        private string connectionString;
        private ILogger logger;
        public DapStorageDB(ILogger logger, string connectionString)
        {
            this.logger = logger;
            this.connectionString = connectionString;
            CheckDatabase();
        }
        public List<Event> GetNewEventsFromStorageForUser(string userId)
        {
            CheckUserInDb(userId);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlSelectQuery = $"Select *,EventLink as EventURL from {eventsTableName}" +
                    $" where EventAddDate>(select LastUpdate from UsersList where UserId='{userId}')";
                List<Event> events = db.Query<Event>(sqlSelectQuery).ToList();
                string sqlUpdateQuery = $@"Update {usersTableName} Set LastUpdate = GETDATE() where UserID='{userId}'";
                db.Execute(sqlUpdateQuery);
                return events;
            }
        }
        public void SaveNewEventsToStorage(List<Event> currEvents)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlSelectQuery = $"Select *,EventLink as EventURL from {eventsTableName}";
                List<Event> events = db.Query<Event>(sqlSelectQuery).ToList();
                List<Event> nonActualEvents = events.Except(currEvents).ToList();

                if (nonActualEvents.Count > 0)
                {
                    foreach (var item in nonActualEvents)
                    {
                        string sqlDeleteQuery = $"Delete from {eventsTableName} where EventName=N'{item.EventName}'";
                        db.Execute(sqlDeleteQuery);
                    }
                }

                List<Event> eventsAfterCleaning = db.Query<Event>(sqlSelectQuery).ToList();
                List<Event> newEvents = currEvents.Except(eventsAfterCleaning).ToList();
                if (newEvents.Count > 0)
                {
                    foreach (var e in newEvents)
                    {
                        string sqlInsertQuery = $@"Insert into {eventsTableName} (EventName, EventDate, EventLink)
                                         values (N'{e.EventName}',N'{e.EventDate}','{e.EventURL}')";
                        db.Execute(sqlInsertQuery);
                    }
                }
            }
        }
        public string GetLastUpdateTelegramFromStorage()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"Select top 1 * from {updatesTableName} ";
                string update = db.Query<string>(sqlQuery).FirstOrDefault();
                if (update == null)
                {
                    string defaultUpdateValue = "0";
                    sqlQuery = $"Insert into {updatesTableName} (LastUpdate) values ('{defaultUpdateValue}')";
                    db.Execute(sqlQuery);
                    return defaultUpdateValue;
                }
                else
                {
                    return update;
                }
            }
        }
        public void SaveTelegramUpdateToStorage(string update)
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"Update {updatesTableName} set LastUpdate = '{update}'";
                db.Execute(sqlQuery);
            }
        }
        public void CheckDatabase()
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    string sqlQuery = $"Select top 1 * from {eventsTableName} ";
                    db.Execute(sqlQuery);
                }
                catch (SqlException ex) when (ex.LineNumber == 1) //No this table
                {
                    string sqlQuery = $@"Create table {eventsTableName}  
                    (EventName nvarchar(100)  not null,
                    EventDate nvarchar(50) not null ,
                    EventLink char(256) not null,
                    EventAddDate datetime not null 
					)";
                    db.Execute(sqlQuery);
                    logger.WriteLog($"Table {eventsTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    logger.WriteLog($"Table {eventsTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");

                }
                ////////////////////////////////////
                try
                {
                    string sqlQuery = $"Select top 1 * from {usersTableName} ";
                    db.Execute(sqlQuery);
                }
                catch (SqlException ex) when (ex.LineNumber == 1) //No this table
                {
                    string sqlQuery = $@"CREATE TABLE {usersTableName}  
                    (UserID int not null,
                    LastUpdate datetime                     
					)";
                    db.Execute(sqlQuery);
                    logger.WriteLog($"Table {usersTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    logger.WriteLog($"Table {usersTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");
                }
                ///////////////////////////////////
                try
                {
                    string sqlQuery = $"Select top 1 * from {updatesTableName} ";
                    db.Execute(sqlQuery);
                }
                catch (SqlException ex) when (ex.LineNumber == 1) //No this table
                {
                    string sqlQuery = $@"CREATE TABLE {updatesTableName}  
                    (LastUpdate int not null)";
                    db.Execute(sqlQuery);
                    logger.WriteLog($"Table {updatesTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    logger.WriteLog($"Table {updatesTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");
                }
            }
        }
        public void CheckUserInDb(string userID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $"Select * from {usersTableName} where UserID={userID}";
                var ID = db.Query(sqlQuery).FirstOrDefault();
                if (ID == null)
                {
                    sqlQuery = $"Insert into {usersTableName} (UserID) values (N'{userID}')";
                    db.Execute(sqlQuery);
                }
            }
        }
        public void SaveUserCheckDate(string userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = $@"Update {usersTableName} Set LastUpdate = GETDATE() where UserID='{userId}'";
                db.Execute(sqlQuery);
            }
        }
    }
}

