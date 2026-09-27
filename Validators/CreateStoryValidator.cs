using CollaborativeStories.Features.Stories;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class CreateStoryValidator
    : AbstractValidator<CreateStoryCommand>
{
    public CreateStoryValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);
    }
}