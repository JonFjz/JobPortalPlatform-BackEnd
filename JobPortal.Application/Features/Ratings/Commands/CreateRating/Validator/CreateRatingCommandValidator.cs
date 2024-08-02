using FluentValidation;

namespace JobPortal.Application.Features.Ratings.Commands.CreateRating.Validator
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.EmployerId)
                .GreaterThan(0).WithMessage("Employer ID must be greater than zero.");

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Score must be between 1 and 5.");
        }
    }
}

