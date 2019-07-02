namespace TelegramApp.Storages.DdModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Update
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LastUpdate { get; set; }
    }
}
