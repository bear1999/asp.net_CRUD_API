using FluentValidation;

namespace RestAPICrud.Requests
{
    public class SendMailRequest
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string html { get; set; }
    }
    public class SendMailRequestValidator : AbstractValidator<SendMailRequest>
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
