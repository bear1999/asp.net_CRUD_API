using FluentValidation;
using RestAPICrud.Models;

namespace RestAPICrud.Validator
{
    public class EmployeesInfoValidator : AbstractValidator<EmployeesInfo>
    {
        public EmployeesInfoValidator()
        {
            RuleFor(x => x.IdEmployee);
            RuleFor(x => x.Fullname)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(16);
            RuleFor(x => x.Birthday)
                .NotEmpty();
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
