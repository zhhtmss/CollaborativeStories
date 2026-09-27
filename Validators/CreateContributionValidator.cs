using CollaborativeStories.Features.Contributions;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class CreateContributionValidator
    : AbstractValidator<CreateContributionCommand>
{
    public CreateContributionValidator()
    {
        RuleFor(x => x.BranchId)
            .NotEmpty();

        RuleFor(x => x.Author)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(5000);
    }
}