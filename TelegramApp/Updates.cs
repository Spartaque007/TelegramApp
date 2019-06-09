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
       private List<Update> allUpdates=new List<Update>();

        public List<Update> AllUpdates
        {
            get
            {
                return allUpdates;
            }

            set
            {
                if (value != null) allUpdates = value;
                else allUpdates = new List<Update>();
            }

        }


        public void AddOrChangeUpdate(Update upd)
        {
            bool is_contain = false;
            if (this.allUpdates.Count > 0)
            {
                foreach (Update u in this.allUpdates)
                {
                    if (u.ChatId == upd.ChatId)
                    {
                        u.LastUpdateID = upd.LastUpdateID;
                        is_contain = true;
                    }
                }
            }
            if (this.allUpdates.Count==0 || !is_contain)
            {
                allUpdates.Add(upd);
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

        public Update(string chatId, string updateId)
        {
            ChatId = chatId;
            LastUpdateID = updateId;
        }

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
