using MediatR;

namespace CollaborativeStories.Features.Rounds
{
    public record CreateRoundCommand(Guid BranchId,DateTime StartAt,DateTime EndAt) : IRequest<Guid>;
}
