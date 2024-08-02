using FluentValidation;

namespace JobPortal.Application.Features.Reviews.Commands.CreateReview.Validator
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.EmployerId)
                .GreaterThan(0).WithMessage("Employer ID must be greater than zero.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(2000).WithMessage("Content must not exceed 2000 characters.");
        }
    }
}