using CollaborativeStories.Data;
using CollaborativeStories.Models;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Services
{
    public class StoryService
    {
        private const int ContributionThreshold = 5;

        private const int MergeThreshold = 3;

        private readonly AppDbContext _context;

        public StoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TryPromoteContributionAsync(
            Guid contributionId)
        {
            var contribution = await _context.Contributions
                .FirstOrDefaultAsync(
                    c => c.Id == contributionId);

            if (contribution == null)
                return false;

            if (contribution.Status !=
                ContributionStatus.Pending)
                return false;

            var score =
                contribution.VotesUp -
                contribution.VotesDown;

            if (score < ContributionThreshold)
                return false;

            contribution.Status =
                ContributionStatus.Accepted;

            contribution.PromotedAt =
                DateTime.UtcNow;

            var user = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    u => u.Username == contribution.Author);

            if (user != null)
            {
                user.Reputation += 10;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TryAcceptMergeAsync(
            Guid mergeRequestId)
        {
            var merge = await _context.MergeRequests
                .FirstOrDefaultAsync(
                    m => m.Id == mergeRequestId);

            if (merge == null)
                return false;

            if (merge.Status !=
                MergeRequestStatus.Pending)
                return false;

            var score =
                merge.VotesUp -
                merge.VotesDown;

            if (score < MergeThreshold)
                return false;

            var source = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == merge.SourceBranchId);

            var target = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == merge.TargetBranchId);

            if (source == null || target == null)
                return false;

            var contributions = await _context.Contributions
                .Where(c =>
                    c.BranchId == source.Id &&
                    c.Status == ContributionStatus.Accepted)
                .ToListAsync();

            foreach (var contribution in contributions)
            {
                var copied = new Contribution
                {
                    BranchId = target.Id,
                    Author = contribution.Author,
                    Text = contribution.Text,
                    Status = ContributionStatus.Accepted,
                    PromotedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Contributions.Add(copied);
            }

            source.IsActive = false;

            merge.Status =
                MergeRequestStatus.Accepted;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
