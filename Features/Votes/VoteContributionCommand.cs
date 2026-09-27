using MediatR;

namespace CollaborativeStories.Features.Votes
{
    public record VoteContributionCommand(Guid ContributionId,string VoterId,int Value) : IRequest<bool>;
}
