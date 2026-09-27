using CollaborativeStories.Data;
using CollaborativeStories.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CollaborativeStories.Features.MergeRequests
{
    public class CreateMergeRequestHandler
    : IRequestHandler<CreateMergeRequestCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateMergeRequestHandler(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateMergeRequestCommand request,
            CancellationToken cancellationToken)
        {
            var source = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == request.SourceBranchId,
                    cancellationToken);

            var target = await _context.Branches
                .FirstOrDefaultAsync(
                    b => b.Id == request.TargetBranchId,
                    cancellationToken);

            if (source == null || target == null)
                throw new Exception(
                    "Гілку не знайдено.");

            if (source.StoryId != target.StoryId)
                throw new Exception(
                    "Можна об'єднувати лише гілки однієї історії.");

            var author = await _context.UserProfiles
                .FirstOrDefaultAsync(
                    u => u.Username == request.Author,
                    cancellationToken);

            if (author == null)
                throw new Exception(
                    "Користувача не знайдено.");

            if (author.Reputation < 20)
                throw new Exception(
                    "Для merge потрібно мінімум 20 репутації.");

            var merge = new MergeRequest
            {
                SourceBranchId =
                    request.SourceBranchId,

                TargetBranchId =
                    request.TargetBranchId,

                Author =
                    request.Author
            };

            _context.MergeRequests.Add(merge);

            await _context.SaveChangesAsync(
                cancellationToken);

            return merge.Id;
        }
    }
}
