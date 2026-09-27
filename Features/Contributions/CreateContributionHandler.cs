using CollaborativeStories.Data;
using CollaborativeStories.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Contributions
{
    public class CreateContributionHandler
    : IRequestHandler<CreateContributionCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateContributionHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateContributionCommand request,
            CancellationToken cancellationToken)
        {
            var branchExists = await _context.Branches
                .AnyAsync(
                    b => b.Id == request.BranchId &&
                         b.IsActive,
                    cancellationToken);

            if (!branchExists)
                throw new Exception(
                    "Активну гілку не знайдено.");

            var contribution = new Contribution
            {
                BranchId = request.BranchId,
                Author = request.Author,
                Text = request.Text
            };

            _context.Contributions.Add(contribution);

            var user = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    u => u.Username == request.Author,
                    cancellationToken);

            if (user == null)
            {
                _context.UserProfiles.Add(new UserProfile
                {
                    Username = request.Author,
                    Reputation = 0
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return contribution.Id;
        }
    }
}
