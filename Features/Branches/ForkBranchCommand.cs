using MediatR;

namespace CollaborativeStories.Features.Branches
{
    public record ForkBranchCommand(Guid BranchId, string Title) : IRequest<Guid>;
}
