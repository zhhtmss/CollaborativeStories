using MediatR;

namespace CollaborativeStories.Features.MergeRequests
{

    public record CreateMergeRequestCommand(Guid SourceBranchId,Guid TargetBranchId,string Author) : IRequest<Guid>;
}
