namespace CollaborativeStories.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = string.Empty;

        public int Reputation { get; set; }
    }
}
