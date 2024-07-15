using FluentValidation;
using Para.Data.Domain;

namespace Para.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Length(11).WithMessage("Identity number must be 11 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.CustomerNumber)
                .NotEmpty().WithMessage("Customer number is required.")
                .GreaterThan(0).WithMessage("Customer number must be greater than zero.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.InsertDate)
                .NotEmpty().WithMessage("Insert date is required.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive is required.");

            RuleFor(x => x.InsertUser)
                .NotEmpty().WithMessage("Insert user is required.")
                .MaximumLength(50).WithMessage("Insert user cannot exceed 50 characters.");
        }
    }
}
