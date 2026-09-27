namespace CollaborativeStories.Models
{
    public enum MergeRequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class MergeRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SourceBranchId { get; set; }

        public Guid TargetBranchId { get; set; }

        public string Author { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public MergeRequestStatus Status { get; set; }
            = MergeRequestStatus.Pending;

        public int VotesUp { get; set; }

        public int VotesDown { get; set; }

        public List<MergeVote> Votes { get; set; } = new();
    }
}
