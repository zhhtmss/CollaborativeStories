using CollaborativeStories.Features.MergeRequests;
using FluentValidation;

namespace CollaborativeStories.Validators;

public class CreateMergeRequestValidator
    : AbstractValidator<CreateMergeRequestCommand>
{
    public CreateMergeRequestValidator()
    {
        RuleFor(x => x.SourceBranchId)
            .NotEmpty();

        RuleFor(x => x.TargetBranchId)
            .NotEmpty();

        RuleFor(x => x.Author)
            .NotEmpty();
    }
}