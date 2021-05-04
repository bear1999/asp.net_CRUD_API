using FluentValidation;

namespace RestAPICrud.Reponsitory
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeLoginValidator : AbstractValidator<LoginRequest>
    {
        public EmployeeLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
