using RestAPICrud.Models;
using FluentValidation;

namespace RestAPICrud.SchemeValidator
{
    public class EmployeeValidator : AbstractValidator<Employees>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(x => x.IdRole)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.ProfileImage).NotEmpty();
        }
    }
}
