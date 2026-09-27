using CollaborativeStories.Features.Rounds;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class CreateRoundValidator
    : AbstractValidator<CreateRoundCommand>
{
    public CreateRoundValidator()
    {
        RuleFor(x => x.BranchId)
            .NotEmpty();

        RuleFor(x => x.EndAt)
            .GreaterThan(x => x.StartAt);
    }
}