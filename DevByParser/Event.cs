using System;

namespace DevBy
{
    [Serializable]
    public class Event
    {

        private DateTime date;
        private string name;
        private string eventURL;

        public DateTime EverntDate
        {
            get
            {
                return date;
            }
            set
            {
                if (value != null) date = value;
                else date = DateTime.MinValue ;
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
        public string EventURL
        {
            get
            {
                return eventURL;
            }
            set
            {
                if (value != null) eventURL = value;
                else eventURL = "No URL";
            }

        }
        public Event()
        {
            EventName = "No name";
            EverntDate = default;
            eventURL = "No URL";
        }
        public Event(string Name, DateTime Date, string URL)
        {
            EventName = Name;
            EverntDate = Date;
            EventURL = URL;
        }

        public override string ToString()
        {
            return $"Date : {EverntDate} Event: {EventName}";
        }
        public override bool Equals(Object obj)
        {
            if (obj is Event && obj != null)
            {
                Event Event_tmp = (Event)obj;

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

