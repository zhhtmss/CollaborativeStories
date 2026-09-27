using CollaborativeStories.Features.MergeRequests;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class VoteMergeRequestValidator
    : AbstractValidator<VoteMergeRequestCommand>
{
    public VoteMergeRequestValidator()
    {
        RuleFor(x => x.MergeRequestId)
            .NotEmpty();

        RuleFor(x => x.VoterId)
            .NotEmpty();

        RuleFor(x => x.Value)
            .Must(x => x == 1 || x == -1)
            .WithMessage(
                "Value має бути 1 або -1.");
    }
}