using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SiteYonetimApp.Domain.Entities;
using SiteYonetimApp.Domain.Entities.SiteManagement.Entities;

namespace SiteYonetimApp.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet tanımları
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Announcement> Announcements { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Meeting> Meetings { get; set; } = null!;
        public DbSet<MeetingParticipant> MeetingParticipants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // SmartEnum → int olarak DB’ye yaz
            builder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                    role => role.Value,
                    value => UserRole.FromValue(value)
                );

            // Mesaj ilişkileri
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Duyuru ilişkisi
            builder.Entity<Announcement>()
                .HasOne(a => a.CreatedBy)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.CreatedById);

            // Toplantı ilişkisi
            builder.Entity<Meeting>()
                .HasOne(m => m.CreatedBy)
                .WithMany()
                .HasForeignKey(m => m.CreatedById);

            // Katılımcı ilişkisi (N-N)
            builder.Entity<MeetingParticipant>()
                .HasKey(mp => new { mp.MeetingId, mp.UserId });

            builder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.Meeting)
                .WithMany(m => m.Participants)
                .HasForeignKey(mp => mp.MeetingId);

            builder.Entity<MeetingParticipant>()
                .HasOne(mp => mp.User)
                .WithMany(u => u.MeetingParticipants)
                .HasForeignKey(mp => mp.UserId);

            // Domain entity konfigürasyonları
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        }
    }
}
