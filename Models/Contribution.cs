namespace CollaborativeStories.Models
{
    public enum ContributionStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class Contribution
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid BranchId { get; set; }

        public string Author { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ContributionStatus Status { get; set; }
            = ContributionStatus.Pending;

        public int VotesUp { get; set; }

        public int VotesDown { get; set; }

        public DateTime? PromotedAt { get; set; }

        public Branch? Branch { get; set; }

        public List<Vote> Votes { get; set; } = new();
    }
}
