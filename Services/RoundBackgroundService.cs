using CollaborativeStories.Data;
using CollaborativeStories.Models;
using Microsoft.EntityFrameworkCore;

namespace CollaborativeStories.Services;

public class RoundBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public RoundBackgroundService(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope =
                    _scopeFactory.CreateScope();

                var context =
                    scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                var now = DateTime.UtcNow;

                var rounds = await context.Rounds
                    .Where(r =>
                        !r.IsClosed &&
                        r.EndAt <= now)
                    .ToListAsync(stoppingToken);

                foreach (var round in rounds)
                {
                    var contributions =
                        await context.Contributions
                            .Where(c =>
                                c.BranchId ==
                                round.BranchId &&
                                c.Status ==
                                ContributionStatus.Pending)
                            .ToListAsync(
                                stoppingToken);

                    var best = contributions
                        .OrderByDescending(
                            c => c.VotesUp - c.VotesDown)
                        .FirstOrDefault();

                    if (best != null)
                    {
                        best.Status =
                            ContributionStatus.Accepted;

                        best.PromotedAt =
                            DateTime.UtcNow;

                        var author =
                            await context.UserProfiles
                                .FirstOrDefaultAsync(
                                    u => u.Username ==
                                         best.Author,
                                    stoppingToken);

                        if (author != null)
                        {
                            author.Reputation += 10;
                        }

                        foreach (var item in contributions)
                        {
                            if (item.Id != best.Id)
                            {
                                item.Status =
                                    ContributionStatus.Rejected;
                            }
                        }
                    }

                    round.IsClosed = true;
                }

                await context.SaveChangesAsync(
                    stoppingToken);
            }
            catch
            {
               
            }

            await Task.Delay(
                TimeSpan.FromSeconds(10),
                stoppingToken);
        }
    }
}