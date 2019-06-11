﻿using System.Collections.Generic;
using System.Linq;


namespace TelegramApp
{

    public class Updates
    {
        private List<Update> allUpdates = new List<Update>();
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
            if (this.allUpdates.Count == 0 || !is_contain)
            {
                allUpdates.Add(upd);
            }

        }
        public string GetLastUpdate()
        {

            if (int.TryParse((from upd in allUpdates orderby upd.LastUpdateID select upd.LastUpdateID).Max(), out int a))
            {
                return (++a).ToString();
            }
            else return "0";
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
