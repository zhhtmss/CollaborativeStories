namespace CollaborativeStories.Models
{
    public class MergeVote
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid MergeRequestId { get; set; }

        public string VoterId { get; set; } = string.Empty;

        public int Value { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public MergeRequest? MergeRequest { get; set; }
    }
}
