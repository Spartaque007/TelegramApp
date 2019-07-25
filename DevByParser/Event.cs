using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevBy
{
    [Serializable]
    [Table("EventList")]
    public class Event
    {

        private string date;
        private string name;
        private string eventURL;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        
        [Column("EventDate")]
        [StringLength(100)]
        public string EventDate
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

        [Column("EventName")]
        [StringLength(250)]
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

        [Column("EventLink")]
        [StringLength(256)]
        public string EventLink
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

        [Column("EventAddDate")]
        public DateTime EventAddDate { get; set; } = DateTime.Now;
        public Event()
        {
            EventName = "No name";
            EventDate = default;
            eventURL = "No URL";
        }
        public Event(string Name, string Date, string URL)
        {
            EventName = Name;
            EventDate = Date;
            EventLink = URL;
        }

        public override string ToString()
        {
            return $"{EventDate}\n{EventName}";
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

