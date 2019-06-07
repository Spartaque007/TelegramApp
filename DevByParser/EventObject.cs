
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBy
{

    public class EventObject
    {

        private string date;
        private string name;

        public string EverntDate
        {
            get
            {
                return date;
            }
            set
            {
                if (value != null) date = value;
                else date = "No date";

            }
        }

        public string EventName
        {
            get
            {
                return name;
            }

            set
            {
                if (value != null) name = value;
                else name = "No name";
            }
        }
        public EventObject()
        {
            EventName = "No name";
            EverntDate = "No date";
        }
        public EventObject(string Name, string Date)
        {
            EventName = Name;
            EverntDate = Date;
        }

        public override string ToString()
        {
            return $"Date : {EverntDate} Event: {EventName}";
        }

        public override bool Equals(Object obj)
        {
            if (obj is EventObject && obj != null)
            {
                EventObject Event_tmp = (EventObject)obj;

                if (Event_tmp.date == this.date && Event_tmp.name == this.name)
                {
                    return true;
                }
            }

            return false;

        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}

