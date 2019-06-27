using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DevBy;
using TelegramApp.Dependency;

namespace TelegramApp.StorageClasses
{
    class DapStorageDB : IStorage
    {
        private string eventListTableName = "EventList";
        private string dBname = "TelegramApp";
        private ILoger loger;
        public DapStorageDB(ref ILoger loger)
        {
            this.loger = loger;
        }
        public List<Event> GetEventsFromStorage(string userID)
        {
            return new List<Event>();
        }

        public string GetLastUpdateTelegramFromStorage()
        {
            return "";
        }

        public void SaveEventsToStorage(string UserID, List<Event> CurrEvents)
        {

        }
        public void SaveUpdateToStorage(string update)
        {

        }
        public void CheckDatabase(string nameDB)
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
                    loger.WriteLog("Database not found, but I created her");



                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"DB check errror!!! ex.LineNumber == {ex.LineNumber}");

                }
            }
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["TelegramApp"].ConnectionString))
            {
                try
                {
                    string sqlQuery = $"Select top 1 * from {eventListTableName} ";
                    db.Execute(sqlQuery);
                }
                catch (SqlException ex) when (ex.LineNumber == 1) //No this table
                {
                    string sqlQuery = $@"CREATE TABLE {eventListTableName}  
                    (EventName nvarchar(100) not null,
                    EventDate DateTime not null ,
                    EventLink nvarchar(256) not null,
                    EventAddDate datetime not null DEFAULT GETDATE()
					)";
                    db.Execute(sqlQuery);
                    loger.WriteLog($"Table {eventListTableName} not found and create now");
                }
                catch (SqlException ex)
                {
                    loger.WriteLog($"Table check errror!!! ex.LineNumber == {ex.LineNumber}");

                }

            }
        }
    }
}

