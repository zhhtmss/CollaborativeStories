using CollaborativeStories.Features.Votes;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class VoteContributionValidator
    : AbstractValidator<VoteContributionCommand>
{
    public VoteContributionValidator()
    {
        RuleFor(x => x.ContributionId)
            .NotEmpty();

        RuleFor(x => x.VoterId)
            .NotEmpty();

        RuleFor(x => x.Value)
            .Must(x => x == 1 || x == -1)
            .WithMessage(
                "Value має бути 1 або -1.");
    }
}