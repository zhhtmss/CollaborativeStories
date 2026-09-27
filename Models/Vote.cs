namespace CollaborativeStories.Models
{
    public class Vote
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ContributionId { get; set; }

        public string VoterId { get; set; } = string.Empty;

        public int Value { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Contribution? Contribution { get; set; }
    }
}
