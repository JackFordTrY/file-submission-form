using FluentValidation;

namespace TestTask.WebApi.Contracts.SubmitFile.CustomValidationRules;

public class FileCustomValidationRule: AbstractValidator<IFormFile>
{
    public FileCustomValidationRule()
    {
        RuleFor(f => f.ContentType)
            .Must(c => c.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
            .WithMessage("File type can only be .docx!");
    }
}
