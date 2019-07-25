namespace TelegramApp.Storages.DdModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Update
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpdateId { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LastUpdate { get; set; }
    }
}
