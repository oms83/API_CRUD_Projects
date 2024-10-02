using Data_Access;
using FluentValidation;

namespace Server_Side
{
    public class EmployeeValidator : AbstractValidator<clsEmployeeDTO>
    {
        private EmployeeValidator(bool withId)
        {
            // Rule for Id: It should be greater than 0.
            RuleFor(employee => employee.Id).GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

            // Rule for FirstName: It should not be empty and have a minimum length of 2.
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.");

            // Rule for LastName: It should not be empty.
            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            // Rule for Age: Should be between 18 and 65.
            RuleFor(employee => employee.Age)
                .InclusiveBetween<clsEmployeeDTO, short>(18, 65)
                .WithMessage("Age must be between 18 and 65.");

            // Rule for Salary: Should be greater than 0.
            RuleFor(employee => employee.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            // Rule for HireDate: Must be in the past.
            RuleFor(employee => employee.HireDate)
                .LessThan(DateTime.Now).WithMessage("Hire date must be in the past.");
        }

        private EmployeeValidator()
        {
            // Rule for FirstName: It should not be empty and have a minimum length of 2.
            RuleFor(employee => employee.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.");

            // Rule for LastName: It should not be empty.
            RuleFor(employee => employee.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            // Rule for Age: Should be between 18 and 65.
            RuleFor(employee => employee.Age)
                .InclusiveBetween<clsEmployeeDTO, short>(18, 65)
                .WithMessage("Age must be between 18 and 65.");

            // Rule for Salary: Should be greater than 0.
            RuleFor(employee => employee.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");

            // Rule for HireDate: Must be in the past.
            RuleFor(employee => employee.HireDate)
                .LessThan(DateTime.Now).WithMessage("Hire date must be in the past.");
        }

        public static EmployeeValidator Validator(bool ValidateWithID)
        {
            if (ValidateWithID)
            {
                return new EmployeeValidator(true);
            }
            else
            {
                return new EmployeeValidator();
            }
        }
    }
}
