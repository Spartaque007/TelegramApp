using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string dBname = "TelegramApp";
        private ILoger loger;
        public DapStorageDB(ref ILoger loger)
        {
            this.loger = loger;
            CheckDatabase();
        }
        public List<Event> GetNewEventsFromStorageForUser(string userID)
        {
            CheckUserInDb(userID);
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = $"Select *,EventLink as EventURL from {eventsTableName} where EventAddDate> " +
                    $"(select LastUpdate from UsersList where UserId='{userID}')";
                List<Event> events = db.Query<Event>(sqlQuery).ToList();
                sqlQuery = $@"Update {usersTableName} Set LastUpdate = GETDATE() where UserID='{userID}'";
                db.Execute(sqlQuery);
                return events;
            }
        }
        public void SaveNewEventsToStorage(List<Event> currEvents)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = $"Select *,EventLink as EventURL from {eventsTableName}";
                List<Event> events = db.Query<Event>(sqlQuery).ToList();
                List<Event> nonActualEvents = events.Except(currEvents).ToList();
                if (nonActualEvents.Count > 0)
                {
                    foreach (var item in nonActualEvents)
                    {
                        sqlQuery = $"Delete from {eventsTableName} where EventName='{item.EventName}'";
                        db.Execute(sqlQuery);
                    }
                }
                List<Event> eventsAfterCleaning = db.Query<Event>(sqlQuery).ToList();
                List<Event> newEvents = currEvents.Except(eventsAfterCleaning).ToList();
                if (newEvents.Count > 0)
                {
                    foreach (var e in newEvents)
                    {
                        sqlQuery = $@"Insert into {eventsTableName} (EventName, EventDate, EventLink)
                                         values (N'{e.EventName}',N'{e.EventDate}','{e.EventURL}')";
                        db.Execute(sqlQuery);
                    }
                }
            }

        }

        public string GetLastUpdateTelegramFromStorage()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = $"Select top 1 * from {updatesTableName} ";
                string update = db.Query<string>(sqlQuery).FirstOrDefault();
                if (update == null)
                {
                    sqlQuery = $"Insert into {updatesTableName} (LastUpdate) values ('0')";
                    db.Execute(sqlQuery);
                    return "0";
                }
                else
                {
                    return update;
                }
            }
        }
        public void SaveTelegramUpdateToStorage(string update)
        {

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = $"Update {updatesTableName} set LastUpdate = '{update}'";
                db.Execute(sqlQuery);
            }
        }
        public void CheckDatabase()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                try
                {
                    db.Open();
                }
                catch (SqlException ex) when (ex.LineNumber == 65536) //Database not created
                {
                    db.ConnectionString = ConfigurationManager.ConnectionStrings["DBconnectionString"].ConnectionString;
                    string sqlQuery = $"CREATE DATABASE {dBname}";
                    db.Execute(sqlQuery);
                    loger.WriteLog($"Database {dBname} not found, but I created her");



                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"DB {dBname} check errror!!! ex.LineNumber == {ex.LineNumber}");

                }
            }
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                try
                {
                    string sqlQuery = $"Select top 1 * from {eventsTableName} ";
                    db.Execute(sqlQuery);
                }
                catch (SqlException ex) when (ex.LineNumber == 1) //No this table
                {
                    string sqlQuery = $@"CREATE TABLE {eventsTableName}  
                    (EventName nvarchar(100)  not null,
                    EventDate nvarchar(50) not null ,
                    EventLink char(256) not null,
                    EventAddDate datetime not null DEFAULT GETDATE()
					)";
                    db.Execute(sqlQuery);
                    loger.WriteLog($"Table {eventsTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"Table {eventsTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");

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
                    loger.WriteLog($"Table {usersTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"Table {usersTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");
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
                    loger.WriteLog($"Table {updatesTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"Table {updatesTableName} check errror!!! ex.LineNumber == {ex.LineNumber}");
                }
            }
        }
        public void CheckUserInDb(string userID)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
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
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                string sqlQuery = $@"Update {usersTableName} Set LastUpdate = GETDATE() where UserID='{userId}'";
                db.Execute(sqlQuery);
            }
        }
    }
}

