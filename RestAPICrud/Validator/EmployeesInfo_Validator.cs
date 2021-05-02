using FluentValidation;
using RestAPICrud.Models;

namespace RestAPICrud.Validator
{
    public class EmployeesInfo_Validator : AbstractValidator<EmployeesInfo>
    {
        public EmployeesInfo_Validator()
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
