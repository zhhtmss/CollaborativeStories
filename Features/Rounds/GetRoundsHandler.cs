using CollaborativeStories.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Rounds
{
    public class GetRoundsHandler
    : IRequestHandler<GetRoundsQuery, object>
    {
        private readonly AppDbContext _context;

        public GetRoundsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(
            GetRoundsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Rounds
                .AsNoTracking()
                .Where(r => r.BranchId == request.BranchId)
                .OrderByDescending(r => r.StartAt)
                .ToListAsync(cancellationToken);
        }
    }
}
