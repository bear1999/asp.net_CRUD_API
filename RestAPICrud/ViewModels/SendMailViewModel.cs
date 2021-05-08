using FluentValidation;

namespace RestAPICrud.ViewModels
{
    public class SendMailViewModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string html { get; set; }
    }
    public class SendMailRequestValidator : AbstractValidator<SendMailViewModel>
    {
        public SendMailRequestValidator()
        {
            RuleFor(x => x.from).NotEmpty().EmailAddress();
            RuleFor(x => x.to).NotEmpty().EmailAddress();
            RuleFor(x => x.subject).NotEmpty();
            RuleFor(x => x.html).NotEmpty();
        }
    }
}
