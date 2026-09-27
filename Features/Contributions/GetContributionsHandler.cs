using CollaborativeStories.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Contributions
{
    public class GetContributionsHandler
    : IRequestHandler<GetContributionsQuery, object>
    {
        private readonly AppDbContext _context;

        public GetContributionsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(
            GetContributionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Contributions
                .AsNoTracking()
                .Where(c => c.BranchId == request.BranchId)
                .OrderByDescending(
                    c => c.VotesUp - c.VotesDown)
                .Select(c => new
                {
                    c.Id,
                    c.Author,
                    c.Text,
                    c.Status,
                    c.VotesUp,
                    c.VotesDown,
                    Score = c.VotesUp - c.VotesDown,
                    c.CreatedAt,
                    c.PromotedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
