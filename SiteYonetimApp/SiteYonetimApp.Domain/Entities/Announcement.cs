namespace SiteYonetimApp.Domain.Entities
{
    namespace SiteManagement.Entities
    {
      
        public class Announcement
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string Content { get; set; } = null!;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public int CreatedById { get; set; }
            public User CreatedBy { get; set; } = null!;
        }
    }

}
