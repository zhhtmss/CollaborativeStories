namespace CollaborativeStories.Models
{
    public class Round
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid BranchId { get; set; }

        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        public DateTime EndAt { get; set; }

        public bool IsClosed { get; set; }

        public Branch? Branch { get; set; }
    }
}
