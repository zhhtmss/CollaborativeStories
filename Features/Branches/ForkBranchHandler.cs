using CollaborativeStories.Data;
using CollaborativeStories.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Branches
{
    public class ForkBranchHandler
    : IRequestHandler<ForkBranchCommand, Guid>
    {
        private readonly AppDbContext _context;

        public ForkBranchHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            ForkBranchCommand request,
            CancellationToken cancellationToken)
        {
            var parent = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == request.BranchId,
                    cancellationToken);

            if (parent == null)
                throw new Exception("Гілку не знайдено.");

            var branch = new Branch
            {
                StoryId = parent.StoryId,
                ParentBranchId = parent.Id,
                Title = request.Title,
                IsActive = true
            };

            _context.Branches.Add(branch);

            await _context.SaveChangesAsync(cancellationToken);

            return branch.Id;
        }
    }
}
