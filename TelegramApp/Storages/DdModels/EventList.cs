namespace TelegramApp.Storages.DdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EventList")]
    public partial class EventList
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string EventName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string EventDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string EventLink { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime EventAddDate { get; set; }
    }
}
