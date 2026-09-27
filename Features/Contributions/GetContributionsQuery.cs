using MediatR;

namespace CollaborativeStories.Features.Contributions
{
    public record GetContributionsQuery(Guid BranchId) : IRequest<object>;
}
