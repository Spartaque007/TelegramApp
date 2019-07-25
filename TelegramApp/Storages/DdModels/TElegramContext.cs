namespace TelegramApp.Storages.DdModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using DevBy;

    public partial class TelegramContext : DbContext
    {
        public TelegramContext()
        { }
        public TelegramContext(string connectionString)
            : base(connectionString)
        { }

        public virtual DbSet<Event> EventLists { get; set; }
        public virtual DbSet<Update> Updates { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.EventLink)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
