using CollaborativeStories.Data;
using CollaborativeStories.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.Rounds
{
    public class CreateRoundHandler
    : IRequestHandler<CreateRoundCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateRoundHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateRoundCommand request,
            CancellationToken cancellationToken)
        {
            var branch = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == request.BranchId &&
                         b.IsActive,
                    cancellationToken);

            if (branch == null)
                throw new Exception(
                    "Гілку не знайдено.");

            var round = new Round
            {
                BranchId = request.BranchId,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                IsClosed = false
            };

            _context.Rounds.Add(round);

            await _context.SaveChangesAsync(
                cancellationToken);

            return round.Id;
        }
    }
}
