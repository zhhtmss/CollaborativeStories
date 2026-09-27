using MediatR;

namespace CollaborativeStories.Features.MergeRequests
{
    public record VoteMergeRequestCommand(Guid MergeRequestId,string VoterId,int Value) : IRequest<bool>;
}
