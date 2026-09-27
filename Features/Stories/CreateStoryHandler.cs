using CollaborativeStories.Data;
using CollaborativeStories.Models;
using MediatR;

namespace CollaborativeStories.Features.Stories
{
    public class CreateStoryHandler
    : IRequestHandler<CreateStoryCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateStoryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateStoryCommand request,
            CancellationToken cancellationToken)
        {
            var story = new Story
            {
                Title = request.Title,
                Description = request.Description
            };

            var rootBranch = new Branch
            {
                StoryId = story.Id,
                Title = "Основна гілка"
            };

            story.RootBranchId = rootBranch.Id;

            _context.Stories.Add(story);
            _context.Branches.Add(rootBranch);

            await _context.SaveChangesAsync(cancellationToken);

            return story.Id;
        }
    }
}
