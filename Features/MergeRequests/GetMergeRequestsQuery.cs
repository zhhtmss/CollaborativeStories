using MediatR;

namespace CollaborativeStories.Features.MergeRequests
{
    public record GetMergeRequestsQuery
    : IRequest<object>;
}
