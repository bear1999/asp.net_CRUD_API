using FluentValidation;

namespace RestAPICrud.Reponsitory
{
    public class obj_EmployeeLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeLoginValidator : AbstractValidator<obj_EmployeeLogin>
    {
        public EmployeeLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
