using FluentValidation;

namespace JobPortal.Application.Features.Educations.Commands.CreateEducation.Validator
{
    public class CreateEducationCommandValidator : AbstractValidator<CreateEducationCommand>
    {
        public CreateEducationCommandValidator()
        {
            RuleFor(x => x.Degree)
                .NotEmpty().WithMessage("Degree is required.")
                .MaximumLength(100).WithMessage("Degree must not exceed 100 characters.");

            RuleFor(x => x.FieldOfStudy)
                .NotEmpty().WithMessage("Field of Study is required.")
                .MaximumLength(100).WithMessage("Field of Study must not exceed 100 characters.");

            RuleFor(x => x.InstitutionName)
                .NotEmpty().WithMessage("Institution Name is required.")
                .MaximumLength(200).WithMessage("Institution Name must not exceed 200 characters.");
        }
    }
}

