using FluentValidation;

namespace RestAPICrud.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeLoginValidator : AbstractValidator<LoginViewModel>
    {
        public EmployeeLoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
