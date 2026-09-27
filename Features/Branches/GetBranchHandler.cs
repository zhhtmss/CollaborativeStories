using CollaborativeStories.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Branches
{
    public class GetBranchHandler : IRequestHandler<GetBranchQuery, object>
    {
        private readonly AppDbContext _context;

        public GetBranchHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(
            GetBranchQuery request,
            CancellationToken cancellationToken)
        {
            var branch = await _context.Branches
                .AsNoTracking()
                .Include(b => b.Contributions)
                .FirstOrDefaultAsync(
                    b => b.Id == request.BranchId,
                    cancellationToken);

            if (branch == null)
                throw new Exception("Гілку не знайдено.");

            return branch;
        }
    }
}
