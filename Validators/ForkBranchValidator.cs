using CollaborativeStories.Features.Branches;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class ForkBranchValidator
    : AbstractValidator<ForkBranchCommand>
{
    public ForkBranchValidator()
    {
        RuleFor(x => x.BranchId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}