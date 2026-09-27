using MediatR;

namespace CollaborativeStories.Features.Rounds
{
    public record GetRoundsQuery(Guid BranchId) : IRequest<object>;
}
