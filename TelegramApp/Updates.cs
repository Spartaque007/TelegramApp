using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramApp
{

    public class Updates
    {
        private List<Update> allUpdates ;

        public List<Update> AllUpdates
        {
            get
            {
                return allUpdates;
            }

        }

        public Updates(string allUpdatesFromFile)
        {
            allUpdates = JsonConvert.DeserializeObject<List<Update>>(allUpdatesFromFile) ?? new List<Update>();
        }
        public void AddOrChangeUpdate(Update upd)
        {
            foreach (Update u in this.allUpdates)
            {
                if (u.ChatId == upd.ChatId)
                {
                    u.LastUpdateID = upd.LastUpdateID;
                }
                else
                {
                    allUpdates.Add(upd);
                }
            }
        }
        public string GetLastUpdate(string chatID)
        {
            foreach (Update update in allUpdates)
            {
                if (update.ChatId == chatID)
                {
                    return update.LastUpdateID;
                }
               

            }
            return "";
        }


    }


    public class Update
    {
        public string ChatId { get; set; }
        public string LastUpdateID { get; set; }

        public override string ToString()
        {
            return $"Chat ID: {ChatId} LastUpdateID: {LastUpdateID}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }


    }
}
