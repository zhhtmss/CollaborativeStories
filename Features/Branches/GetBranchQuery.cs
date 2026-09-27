using MediatR;

namespace CollaborativeStories.Features.Branches
{
    public record GetBranchQuery(Guid BranchId) : IRequest<object>;
}
