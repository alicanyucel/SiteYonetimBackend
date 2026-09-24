namespace SiteYonetimApp.Domain.Entities;


   
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ScheduledAt { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public ICollection<MeetingParticipant> Participants { get; set; } = new List<MeetingParticipant>();
    }


