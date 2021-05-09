using RestAPICrud.Models;
using FluentValidation;

namespace RestAPICrud.SchemeValidator
{
    public class EmployeesValidator : AbstractValidator<Employees>
    {
        public EmployeesValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(x => x.IdRole)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
