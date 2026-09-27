using CollaborativeStories.Data;
using CollaborativeStories.Models;
using CollaborativeStories.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Votes
{
    public class VoteContributionHandler
    : IRequestHandler<VoteContributionCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly StoryService _storyService;

        public VoteContributionHandler(
            AppDbContext context,
            StoryService storyService)
        {
            _context = context;
            _storyService = storyService;
        }

        public async Task<bool> Handle(
            VoteContributionCommand request,
            CancellationToken cancellationToken)
        {
            var contribution =
                await _context.Contributions
                    .FirstOrDefaultAsync(
                        c => c.Id == request.ContributionId,
                        cancellationToken);

            if (contribution == null)
                throw new Exception(
                    "Вклад не знайдено.");

            var existingVote =
                await _context.Votes
                    .FirstOrDefaultAsync(
                        v =>
                            v.ContributionId ==
                            request.ContributionId &&
                            v.VoterId ==
                            request.VoterId,
                        cancellationToken);

            if (existingVote != null)
                throw new Exception(
                    "Ви вже голосували за цей вклад.");

            var vote = new Vote
            {
                ContributionId =
                    request.ContributionId,

                VoterId =
                    request.VoterId,

                Value =
                    request.Value
            };

            _context.Votes.Add(vote);

            if (request.Value == 1)
                contribution.VotesUp++;
            else
                contribution.VotesDown++;

            await _context.SaveChangesAsync(
                cancellationToken);

            await _storyService
                .TryPromoteContributionAsync(
                    request.ContributionId);

            return true;
        }
    }
}
