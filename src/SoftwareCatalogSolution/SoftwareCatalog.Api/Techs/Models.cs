using FluentValidation;

namespace SoftwareCatalog.Api.Techs;

public record TechResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
}

public record TechCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
}

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(1).MaximumLength(100);
        RuleFor(v => v.Email)
            .NotEmpty()
            .When(x => string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage($"Either {nameof(TechCreateModel.Email)} or {nameof(TechCreateModel.PhoneNumber)} must be provided.");
        RuleFor(v => v.PhoneNumber)
            .NotEmpty()
            .MinimumLength(10)
            .When(x => string.IsNullOrEmpty(x.Email))
            .WithMessage($"Either {nameof(TechCreateModel.Email)} or {nameof(TechCreateModel.PhoneNumber)} must be provided.");
    }
}