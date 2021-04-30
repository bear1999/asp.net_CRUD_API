using FluentValidation;

namespace RestAPICrud.Reponsitory
{
    public class resEmployeeLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeLoginValidator : AbstractValidator<resEmployeeLogin>
    {
        public EmployeeLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
