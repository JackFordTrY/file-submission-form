using FluentValidation;
using TestTask.WebApi.Contracts.SubmitFile.CustomValidationRules;

namespace TestTask.WebApi.Contracts.SubmitFile
{
    public class SubmitFileRequestValidator : AbstractValidator<SubmitFileRequest>
    {
        public SubmitFileRequestValidator()
        {
            RuleFor(request => request.Email)
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                .WithMessage("Email address is not valid!");
            RuleFor(request => request.File)
                .SetValidator(new FileCustomValidationRule());
        }
    }
}
