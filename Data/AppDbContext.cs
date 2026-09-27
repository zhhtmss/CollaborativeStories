using CollaborativeStories.Models;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Story> Stories => Set<Story>();

        public DbSet<Branch> Branches => Set<Branch>();

        public DbSet<Contribution> Contributions => Set<Contribution>();

        public DbSet<Vote> Votes => Set<Vote>();

        public DbSet<Round> Rounds => Set<Round>();

        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

        public DbSet<MergeRequest> MergeRequests => Set<MergeRequest>();

        public DbSet<MergeVote> MergeVotes => Set<MergeVote>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.ParentBranch)
                .WithMany()
                .HasForeignKey(b => b.ParentBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.Branches)
                .WithOne(b => b.Story)
                .HasForeignKey(b => b.StoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Contributions)
                .WithOne(c => c.Branch)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Rounds)
                .WithOne(r => r.Branch)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contribution>()
                .HasMany(c => c.Votes)
                .WithOne(v => v.Contribution)
                .HasForeignKey(v => v.ContributionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MergeRequest>()
                .HasMany(m => m.Votes)
                .WithOne(v => v.MergeRequest)
                .HasForeignKey(v => v.MergeRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vote>()
                .HasIndex(v => new
                {
                    v.ContributionId,
                    v.VoterId
                })
                .IsUnique();

            modelBuilder.Entity<MergeVote>()
                .HasIndex(v => new
                {
                    v.MergeRequestId,
                    v.VoterId
                })
                .IsUnique();

            modelBuilder.Entity<Contribution>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<MergeRequest>()
                .Property(m => m.Status)
                .HasConversion<string>();
        }
    }
}
