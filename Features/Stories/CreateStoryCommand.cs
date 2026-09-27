using MediatR;

namespace CollaborativeStories.Features.Stories
{
    public record CreateStoryCommand(string Title, string Description) : IRequest<Guid>;
}

