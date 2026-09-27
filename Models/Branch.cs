namespace CollaborativeStories.Models
{
    public class Branch
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StoryId { get; set; }

        public Guid? ParentBranchId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public Story? Story { get; set; }

        public Branch? ParentBranch { get; set; }

        public List<Contribution> Contributions { get; set; } = new();

        public List<Round> Rounds { get; set; } = new();
    }
}
