using FluentValidation;

namespace JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting.Validator
{
    public class CreateJobPostingCommandValidator : AbstractValidator<CreateJobPostingCommand>
    {
        public CreateJobPostingCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(x => x.ClosingDate)
                .GreaterThan(DateTime.Now).WithMessage("Closing date must be in the future.");

            RuleFor(x => x.WorkType)
                .IsInEnum().WithMessage("Invalid work type.");

            RuleFor(x => x.WorkLevel)
                .IsInEnum().WithMessage("Invalid work level.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.Responsibilities)
                .NotEmpty().WithMessage("Responsibilities are required.")
                .MaximumLength(1000).WithMessage("Responsibilities must not exceed 1000 characters.");

            RuleFor(x => x.NotificationEmail)
                .NotEmpty().WithMessage("Notification email is required.")
                .EmailAddress().WithMessage("Invalid email address.");
        }
    }
}