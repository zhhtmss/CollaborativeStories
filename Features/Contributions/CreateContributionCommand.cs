using MediatR;

namespace CollaborativeStories.Features.Contributions
{    
    public record CreateContributionCommand(Guid BranchId,string Author,string Text) : IRequest<Guid>;
}
