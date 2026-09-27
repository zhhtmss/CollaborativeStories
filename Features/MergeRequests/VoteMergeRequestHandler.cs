using CollaborativeStories.Data;
using CollaborativeStories.Models;
using CollaborativeStories.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Features.MergeRequests;

public class VoteMergeRequestHandler
    : IRequestHandler<VoteMergeRequestCommand, bool>
{
    private readonly AppDbContext _context;
    private readonly StoryService _storyService;

    public VoteMergeRequestHandler(
        AppDbContext context,
        StoryService storyService)
    {
        _context = context;
        _storyService = storyService;
    }

    public async Task<bool> Handle(
        VoteMergeRequestCommand request,
        CancellationToken cancellationToken)
    {
        var merge = await _context.MergeRequests
            .FirstOrDefaultAsync(
                m => m.Id == request.MergeRequestId,
                cancellationToken);

        if (merge == null)
            throw new Exception(
                "Merge request не знайдено.");

        var existingVote =
            await _context.MergeVotes
                .FirstOrDefaultAsync(
                    v =>
                        v.MergeRequestId ==
                        request.MergeRequestId &&
                        v.VoterId ==
                        request.VoterId,
                    cancellationToken);

        if (existingVote != null)
            throw new Exception(
                "Ви вже голосували.");

        var vote = new MergeVote
        {
            MergeRequestId =
                request.MergeRequestId,

            VoterId =
                request.VoterId,

            Value =
                request.Value
        };

        _context.MergeVotes.Add(vote);

        if (request.Value == 1)
            merge.VotesUp++;
        else
            merge.VotesDown++;

        await _context.SaveChangesAsync(
            cancellationToken);

        await _storyService
            .TryAcceptMergeAsync(
                request.MergeRequestId);

        return true;
    }
}