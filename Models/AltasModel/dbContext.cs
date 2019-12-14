namespace Atlas.Models.AltasModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbContext : DbContext
    {
        public dbContext()
            : base("name=dbContext")
        {
        }

        public virtual DbSet<Attendee> Attendees { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<ReqResource> ReqResources { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceType> ResourceTypes { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Meeting>()
                .Property(e => e.StartTime)
                .IsUnicode(false);

            modelBuilder.Entity<Meeting>()
                .Property(e => e.EndTime)
                .IsUnicode(false);

            modelBuilder.Entity<Meeting>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Meeting>()
                .Property(e => e.ProposalName)
                .IsUnicode(false);

            modelBuilder.Entity<Meeting>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ResourceType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Attendees)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
