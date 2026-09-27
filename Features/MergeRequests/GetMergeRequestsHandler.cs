using CollaborativeStories.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.MergeRequests;

public class GetMergeRequestsHandler
    : IRequestHandler<GetMergeRequestsQuery, object>
{
    private readonly AppDbContext _context;

    public GetMergeRequestsHandler(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(
        GetMergeRequestsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.MergeRequests
            .AsNoTracking()
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}