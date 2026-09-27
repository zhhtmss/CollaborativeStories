namespace CollaborativeStories.Models;

public class Story
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid RootBranchId { get; set; }

    public List<Branch> Branches { get; set; } = new();
}