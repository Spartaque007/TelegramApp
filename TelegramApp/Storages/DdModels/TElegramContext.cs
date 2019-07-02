namespace TelegramApp.Storages.DdModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TelegramContext : DbContext
    {
        public TelegramContext()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<EventList> EventLists { get; set; }
        public virtual DbSet<Update> Updates { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventList>()
                .Property(e => e.EventLink)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
